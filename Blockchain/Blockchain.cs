using System;
using System.Collections.Generic;
using Blockchain.EventArgs;
using Blockchain.Interfaces;

namespace Blockchain
{
    public class Blockchain
    {
        private readonly IGenesis genesis;
        private readonly IBlockValidation blockValidation;
        private readonly IPeerNetwork peerNetwork;
        private List<IBlockDefinition> Chain = new List<IBlockDefinition>();

        public Blockchain(IGenesis genesis, IBlockValidation blockValidation, IPeerNetwork peerNetwork)
        {
            this.blockValidation = blockValidation;
            this.genesis = genesis;
            this.peerNetwork = peerNetwork;
            this.peerNetwork.BlockchainReceived += PeerNetwork_BlockchainReceived;
            this.peerNetwork.LatestBlockReceived += PeerNetwork_LatestBlockReceived;

            Chain.Add(this.genesis.CreateGenesisBlock());
        }

        private void PeerNetwork_LatestBlockReceived(object sender, BlockEventArgs e)
        {
            IBlockDefinition lastReceivedBlock = null;
            IBlockDefinition lastLocalBlock = OnGetLatestBlock();

            if (e.Block == null)
                return;
            else
                lastReceivedBlock = e.Block;

            if (lastReceivedBlock.Index > lastLocalBlock.Index)
            {
                if (lastReceivedBlock.PreviousHash == lastLocalBlock.Hash)
                {
                    OnAddBlockToChain(lastReceivedBlock);
                }
                else
                {
                    // received a Block with higher index and the previous hash dosent match the current hash from local => our local chain is out of date
                    peerNetwork.FullBlockchainRequest(Chain);
                }
            }
            else
            {
                // our local blockchain is longer then the received
                // longer blockchain wins we do nothing
            }
        }

        private void PeerNetwork_BlockchainReceived(object sender, BlockchainEventArgs e)
        {
            if (e.Blockchain == null || e.Blockchain.Length == 0)
                return;
            Chain = new List<IBlockDefinition>(e.Blockchain);
        }

        private IBlockDefinition OnGetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        private int OnGetAdjustedDifficulty()
        {
            IBlockDefinition lastBlock = OnGetLatestBlock();
            IBlockDefinition prevAdjustmentBlock = Chain[Chain.Count - Configuration.CurrentSettings.DifficultyAdjustmentInterval];
            double timeExpected = Configuration.CurrentSettings.BlockGenerationInterval * Configuration.CurrentSettings.DifficultyAdjustmentInterval;
            double timeTaken = (lastBlock.TimeStamp - prevAdjustmentBlock.TimeStamp).TotalMinutes;
            if (timeTaken < timeExpected / 2)
            {
                return prevAdjustmentBlock.Difficulty + 1;
            } else if (timeTaken > timeExpected * 2)
            {
                return prevAdjustmentBlock.Difficulty - 1;
            } else
            {
                return prevAdjustmentBlock.Difficulty;
            }
        }

        private int OnGetDifficulty()
        {
            IBlockDefinition lastBlock = OnGetLatestBlock();
            if (lastBlock.Index % Configuration.CurrentSettings.DifficultyAdjustmentInterval == 0 && lastBlock.Index != 0)
            {
                return OnGetAdjustedDifficulty();
            } else
            {
                return lastBlock.Difficulty;
            }
        }

        private IBlockDefinition OnFindBlock(IBlockDefinition blockDefinition)
        {
            int nonce = 0;
            while (true)
            {
                blockDefinition.Nonce = nonce;
                string hash = this.blockValidation.CalculateHash(blockDefinition);
                if (this.blockValidation.HashMatchesDifficulty(hash, blockDefinition.Difficulty))
                {
                    blockDefinition.Hash = hash;
                    return blockDefinition;
                }
                nonce++;
            }
        }
        

        private bool OnAddBlockToChain(IBlockDefinition newBlock)
        {
            if (blockValidation.ValidateNewBlock(OnGetLatestBlock(), newBlock))
            {
                Chain.Add(newBlock);
                return true;
            }
            return false;
        }

        public IBlockDefinition GenerateBlock(IBlockData blockData = null)
        {
            IBlockDefinition previousBlock = OnGetLatestBlock();
            IBlockDefinition block = new Block
            {
                PreviousHash = previousBlock.Hash,
                Difficulty = OnGetDifficulty(),
                Index = previousBlock.Index + 1,
                TimeStamp = DateTime.UtcNow,
                Data = blockData
            };

            IBlockDefinition newBlock = OnFindBlock(block);
            if (OnAddBlockToChain(newBlock))
            {
                peerNetwork.LatestBlockBroadcast(newBlock);
                return newBlock;
            }
            return null;
        }

        public int GetChainLength()
        {
            return Chain.Count;
        }

        public string GetLastHash()
        {
            return OnGetLatestBlock().Hash;
        }

        public IBlockDefinition[] GetBlockChain()
        {
            IBlockDefinition[] blockDefinitions = new IBlockDefinition[Chain.Count];
            Chain.CopyTo(blockDefinitions);
            return blockDefinitions;
        }

    }
}
