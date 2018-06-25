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
            throw new NotImplementedException();
        }

        public void FullBlockchainRequest(List<IBlockDefinition> localBlockchain)
        {
            throw new NotImplementedException();
        }
    }
}
