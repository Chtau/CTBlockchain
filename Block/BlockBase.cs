using Interfaces;
using System;
using System.Collections.Generic;

namespace Block
{
    public abstract class BlockBase
    {
        public const int NULL_NUMBER_VALUE = -1;

        private readonly IBlockValidation blockValidation;
        public readonly IBlockDefinition blockDefinition;

        public bool IsValid { get; private set; }

        public int Index => blockDefinition != null ? blockDefinition.Index : NULL_NUMBER_VALUE;

        public string Hash => blockDefinition?.Hash;

        public string PreviousHash => blockDefinition?.PreviousHash;

        public DateTime TimeStamp => blockDefinition != null ? blockDefinition.TimeStamp : DateTime.MinValue;

        IEnumerable<ITransaction> Data => blockDefinition?.Data;

        public int Difficulty => blockDefinition != null ? blockDefinition.Difficulty : NULL_NUMBER_VALUE;

        public decimal Nonce => blockDefinition != null ? blockDefinition.Nonce : NULL_NUMBER_VALUE;

        public BlockBase(IBlockDefinition block, IBlockValidation blockValidation)
        {
            IsValid = false;
            this.blockValidation = blockValidation ?? throw new ArgumentNullException("blockValidation", "BlockValidation Interface implementation must be provided");
            this.blockDefinition = block;
            Validate();
        }

        public void SetHash()
        {
            this.blockDefinition.Hash = blockValidation.CalculateHash(blockDefinition);
        }

        public virtual void Validate()
        {
            if (Index == NULL_NUMBER_VALUE)
            {
                IsValid = false;
                return;
            }
            if (TimeStamp == DateTime.MinValue)
            {
                IsValid = false;
                return;
            }
            if (Difficulty == NULL_NUMBER_VALUE)
            {
                IsValid = false;
                return;
            }
            if (Nonce == NULL_NUMBER_VALUE)
            {
                IsValid = false;
                return;
            }
            if (this.blockDefinition.Hash == blockValidation.CalculateHash(blockDefinition))
            {
                // VALID
                IsValid = true;
            }
            else
            {
                IsValid = false;
            }
        }

        
    }
}
