using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ITransaction
    {
        string Id { get; }
        IEnumerable<ITransactionIn> TransactionIns { get; }
        IEnumerable<ITransactionOut> TransactionOuts { get; }
    }
}
