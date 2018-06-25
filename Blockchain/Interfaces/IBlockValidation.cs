namespace Blockchain.Interfaces
{
    public interface IBlockValidation
    {
        bool ValidateNewBlock(IBlockDefinition oldBlock, IBlockDefinition newBlock);
        string CalculateHash(IBlockDefinition block);
        bool HashMatchesDifficulty(string hash, int difficulty);
    }
}
