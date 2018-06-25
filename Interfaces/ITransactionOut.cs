using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ITransactionOut
    {
        string Address { get; }
        decimal Amount { get; }
    }
}
