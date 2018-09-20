using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tafelmusik
{
    public interface IApp<T, TOut>
    {
        string Name { get;  }
        TOut Run(T key);
    }
}
