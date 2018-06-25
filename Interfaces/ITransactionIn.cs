using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ITransactionIn
    {
        string OutId { get; }
        decimal OutIndex { get; }
        string Signature { get; }
    }
}
