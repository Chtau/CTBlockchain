using System;
using Xunit;

namespace xUnitTest
{
    public class BlockchainTest
    {
        [Fact]
        public void CreateBlocks()
        {
            var chain = GetInstance();
            Blockchain.Interfaces.IBlockDefinition lastBlock = null;
            for (int i = 0; i < 30; i++)
            {
                lastBlock = chain.GenerateBlock();
            }

            Assert.True(chain.GetChainLength() == 31 && lastBlock.Difficulty == 2);
        }

        private Blockchain.Blockchain GetInstance()
        {
            return new Blockchain.Blockchain(
                new BasicImplementation.Genesis(), 
                new Blockchain.BlockValidation(new BasicImplementation.HashSHA256()), 
                new BasicImplementation.PeerNetwork()
                );
        }
    }
}
