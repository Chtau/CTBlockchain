using Blockchain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain
{
    public class Block : IBlockDefinition
    {
        public int Index { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public DateTime TimeStamp { get; set; }
        public IBlockData Data { get; set; }
        public int Difficulty { get; set; }
        public decimal Nonce { get; set; }

        public Block()
        {
            Index = Configuration.CurrentSettings.NullNumberValue;
            Hash = null;
            PreviousHash = null;
            TimeStamp = Configuration.CurrentSettings.NullDatetimeValue;
            Data = null;
            Difficulty = Configuration.CurrentSettings.NullNumberValue;
            Nonce = Configuration.CurrentSettings.NullNumberValue;
        }
    }
}
