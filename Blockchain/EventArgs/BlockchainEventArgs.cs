using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain.EventArgs
{
    public class BlockchainEventArgs : System.EventArgs
    {
        public BlockchainEventArgs(Interfaces.IBlockDefinition[] blockchain)
        {
            Blockchain = blockchain;
        }

        public Interfaces.IBlockDefinition[] Blockchain { get; private set; }
    }
}
