using BasicImplementation;
using Blockchain;
using Blockchain.EventArgs;
using System;
using Xunit;

namespace ChainTest
{
    public class BlockchainTest
    {
        [Fact]
        public void CreateSomeBlocks()
        {
            var chain = GetBlockchainInstance();
            var block1 = chain.GenerateBlock();
            var block2 = chain.GenerateBlock();
            var block3 = chain.GenerateBlock();
            var block4 = chain.GenerateBlock();
            var block5 = chain.GenerateBlock();

            Assert.True(chain.GetChainLength() == 6 && chain.GetLastHash() == block5.Hash);
        }

        [Fact]
        public void ChainGenerationIntervalDifficulty()
        {
            int blocksCount = 20;

            var settings = Blockchain.Configuration.GetSettings();
            settings.DifficultyAdjustmentInterval = 5;
            Blockchain.Configuration.SetSettings(settings);


            Blockchain.Interfaces.IBlockDefinition lastBlock = null;
            var chain = GetBlockchainInstance();
            for (int i = 0; i < blocksCount; i++)
            {
                var block = chain.GenerateBlock();
                if (i == blocksCount - 1)
                    lastBlock = block;
            }

            Assert.True(chain.GetChainLength() == (blocksCount + 1) && lastBlock.Difficulty == 3);
        }

        [Fact]
        public void ChainTimeAdjustmentDifficulty()
        {
            int blocksCount = 5;

            var settings = Blockchain.Configuration.GetSettings();
            settings.DifficultyAdjustmentInterval = 2;
            settings.BlockGenerationInterval = 1;
            Blockchain.Configuration.SetSettings(settings);


            Blockchain.Interfaces.IBlockDefinition lastBlock = null;
            var chain = GetBlockchainInstance();
            for (int i = 0; i < blocksCount; i++)
            {
                var block = chain.GenerateBlock();
                if (i == 2)
                    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1).Add(TimeSpan.FromSeconds(10)));
                if (i == blocksCount - 1)
                    lastBlock = block;
            }

            Assert.True(chain.GetChainLength() == (blocksCount + 1) && lastBlock.Difficulty == 1);
        }

        private Blockchain.Blockchain GetBlockchainInstance()
        {
            return new Blockchain.Blockchain(new Genesis(), new BlockValidation(new HashSHA256()), new PeerNetwork());
        }
    }
}
