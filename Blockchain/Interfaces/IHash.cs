using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain.Interfaces
{
    public interface IHash
    {
        string Calculate(string value);
    }
}
