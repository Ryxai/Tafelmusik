using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tafelmusik
{
    public class Lambda<TKey, TOut> : IApp<TKey, TOut>
    {
        private readonly Func<TOut> func;
        public string Name { get; private set; }

        private Lambda(Func<TOut> func)
        {
            this.func = func;
            Name = string.Empty;
        }

        public TOut Run(TKey key)
        {
            return func();
        }


        public static Lambda<TKey, TOut> Create(Func<TOut> function)
        {
            var output = new Lambda<TKey, TOut>(function);
            return output;
        }
    }
}
