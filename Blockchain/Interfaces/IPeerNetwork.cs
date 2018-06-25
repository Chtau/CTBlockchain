using Blockchain.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain.Interfaces
{
    public interface IPeerNetwork
    {
        event EventHandler<BlockEventArgs> LatestBlockReceived;
        event EventHandler<BlockchainEventArgs> BlockchainReceived;
        void LatestBlockBroadcast(IBlockDefinition latestblock);
        void FullBlockchainRequest(List<IBlockDefinition> localBlockchain);
    }
}
