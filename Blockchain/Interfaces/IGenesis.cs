using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain.Interfaces
{
    public interface IGenesis
    {
        IBlockDefinition CreateGenesisBlock();
    }
}
