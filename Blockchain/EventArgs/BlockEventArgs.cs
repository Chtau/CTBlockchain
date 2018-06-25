using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain.EventArgs
{
    public class BlockEventArgs : System.EventArgs
    {
        public BlockEventArgs(Interfaces.IBlockDefinition block)
        {
            Block = block;
        }

        public Interfaces.IBlockDefinition Block { get; private set; }
    }
}
