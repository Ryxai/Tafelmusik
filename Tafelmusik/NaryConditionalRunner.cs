using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tafelmusik
{
    public class NaryConditionalRunner<TKey, TOut> : IApp<TKey, TOut>
    {
        public string Name { get; private set; }
        private Dictionary<TKey, IApp<TKey, TOut>> appBranches;

        private NaryConditionalRunner(string _name)
        {
            Name = _name;
            appBranches = new Dictionary<TKey, IApp<TKey, TOut>>();
        }

        public static NaryConditionalRunner<TKey, TOut> Create(string name)
        {
            var output = new NaryConditionalRunner<TKey, TOut>(name);
            return output;
        }

        public NaryConditionalRunner<TKey, TOut> Add(TKey key, IApp<TKey, TOut> value) 
        {
           appBranches.Add(key, value);
            return this;
        }

        public NaryConditionalRunner<TKey, TOut> Clear()
        {
            appBranches.Clear();
            return this;
        }

        public TOut Run(TKey key)
        {
            return appBranches[key].Run(key);
        }

       
    }
}
