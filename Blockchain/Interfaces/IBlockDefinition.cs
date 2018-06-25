using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain.Interfaces
{
    public interface IBlockDefinition
    {
        int Index { get; set; }
        string Hash { get; set; }
        string PreviousHash { get; set; }
        DateTime TimeStamp { get; set; }
        IBlockData Data { get; set; }
        int Difficulty { get; set; }
        decimal Nonce { get; set; }
    }
}
