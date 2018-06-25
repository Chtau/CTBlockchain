using Interfaces;
using System;

namespace Block
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
            if (newBlock.Index == BlockBase.NULL_NUMBER_VALUE || newBlock.Index == oldBlock.Index)
            {
                return false;
            }
            if (newBlock.TimeStamp == DateTime.MinValue || newBlock.TimeStamp == oldBlock.TimeStamp)
            {
                return false;
            }
            if (newBlock.Difficulty == BlockBase.NULL_NUMBER_VALUE)
            {
                return false;
            }
            if (newBlock.Nonce == BlockBase.NULL_NUMBER_VALUE)
            {
                return false;
            }
            //check for valid hash
            
            return true;
        }

        public string CalculateHash(IBlockDefinition block)
        {
            return hash.Calculate(block.Index + block.PreviousHash + block.TimeStamp + block.Data + block.Difficulty + block.Nonce);
        }
    }
}
