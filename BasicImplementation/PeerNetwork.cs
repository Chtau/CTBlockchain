using System;
using System.Collections.Generic;
using System.Text;
using Blockchain.EventArgs;
using Blockchain.Interfaces;

namespace BasicImplementation
{
    public class PeerNetwork : IPeerNetwork
    {
        public event EventHandler<BlockchainEventArgs> BlockchainReceived;
        public event EventHandler<BlockEventArgs> LatestBlockReceived;

        public void LatestBlockBroadcast(IBlockDefinition latestblock)
        {
            System.Diagnostics.Debug.Print("LatestBlockBroadcast");
        }

        public void FullBlockchainRequest(List<IBlockDefinition> localBlockchain)
        {
            System.Diagnostics.Debug.Print("FullBlockchainRequest");
        }
    }
}
