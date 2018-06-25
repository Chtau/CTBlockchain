using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Block
{
    public class Block : IBlockDefinition
    {
        public int Index { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public DateTime TimeStamp { get; set; }
        public IEnumerable<ITransaction> Data { get; set; }
        public int Difficulty { get; set; }
        public decimal Nonce { get; set; }
    }
}
