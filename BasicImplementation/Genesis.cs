using Blockchain;
using Blockchain.Interfaces;
using System;

namespace BasicImplementation
{
    public class Genesis : IGenesis
    {
        public IBlockDefinition CreateGenesisBlock()
        {
            return new Block
            {
                Index = 0,
                Hash = "58774f9f6e6e29d5421fc7cee8b6d702ecef861be473b729e86556b8d4e57ac1",
                PreviousHash = "",
                TimeStamp = DateTime.UtcNow,
                Data = null,
                Difficulty = 0,
                Nonce = 0
            };
        }
    }
}
