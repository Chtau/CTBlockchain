using Blockchain.Interfaces;
using System;

namespace Blockchain
{
    public class BlockValidation : IBlockValidation
    {
        private readonly IHash hash;

        public BlockValidation(IHash hash)
        {
            this.hash = hash ?? throw new ArgumentNullException("hash", "Hash Interface implementation must be provided");
        }

        public bool ValidateNewBlock(IBlockDefinition oldBlock, IBlockDefinition newBlock)
        {
            if (newBlock.Index == Configuration.CurrentSettings.NullNumberValue || newBlock.Index == oldBlock.Index)
            {
                return false;
            }
            if (newBlock.TimeStamp == Configuration.CurrentSettings.NullDatetimeValue || !OnValidateTimeStampOffset(newBlock.TimeStamp, oldBlock.TimeStamp))
            {
                return false;
            }
            if (newBlock.Difficulty == Configuration.CurrentSettings.NullNumberValue)
            {
                return false;
            }
            if (newBlock.Nonce == Configuration.CurrentSettings.NullNumberValue)
            {
                return false;
            }
            if (CalculateHash(newBlock) != newBlock.Hash)
            {
                return false;
            }
            if (!HashMatchesDifficulty(newBlock.Hash, newBlock.Difficulty))
            {
                return false;
            }
            
            return true;
        }

        private bool OnValidateTimeStampOffset(DateTime timeStamp, DateTime previousTimeStamp)
        {
            if (previousTimeStamp.AddMinutes(-Configuration.CurrentSettings.BlockUTCTimeMinutesOffsetMinus) < timeStamp &&
                timeStamp.AddMinutes(-Configuration.CurrentSettings.BlockUTCTimeMinutesOffsetMinus) < DateTime.UtcNow)
                return true;
            return false;
        }

        public string CalculateHash(IBlockDefinition block)
        {
            return hash.Calculate(block.Index + block.PreviousHash + block.TimeStamp + block.Data?.ToContentString() + block.Difficulty + block.Nonce);
        }

        public bool HashMatchesDifficulty(string hash, int difficulty)
        {
            string difString = "";
            for (int i = 0; i < difficulty; i++)
            {
                difString += "0";
            }
            if (hash.StartsWith(difString))
                return true;
            return false;
        }
    }
}
