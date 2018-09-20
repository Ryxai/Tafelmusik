using System;
using System.Collections.Generic;

namespace Tafelmusik
{
    public class LinearRunner<TKey, T, TOut> : IApp<TKey, TOut>
    {
        private List<IApp<TKey, T>> apps;
        private Func<T, bool> continueEvaluation;
        private Func<T, TOut, TOut> reduce;
        private Func<TOut> baseGenerator;
        public string Name { get; private set; }

        private LinearRunner(string _name, Func<T, bool> _continueEvalFunc, Func<T, TOut, TOut> _reduce, Func<TOut> _baseGenerator)
        {
            apps = new List<IApp<TKey, T>>();
            Name = _name;
            continueEvaluation = _continueEvalFunc;
            reduce = _reduce;
            baseGenerator = _baseGenerator;
        }
        
        public LinearRunner<TKey, T, TOut> Add(IApp<TKey, T> app)
        {
            apps.Add(app);
            return this;
        }

        public LinearRunner<TKey, T, TOut> Clear()
        {
            apps.Clear();
            return this;
        }

        public static LinearRunner<TKey, T, TOut> Create(string name, Func<T, bool> continueEvalFunc, Func<T, TOut, TOut> reduce, Func<TOut> baseGenerator)
        {
            var output = new LinearRunner<TKey, T, TOut>(name, continueEvalFunc, reduce, baseGenerator);
            return output;
        }

        public TOut Run(TKey key)
        {
            var res = default(T);
            var acc = baseGenerator();
            var firstapp = true;
            foreach (var app in apps)
            {
                if (!firstapp && !continueEvaluation(res))
                    return acc;
                firstapp = false;
                res = app.Run(key);
                acc = reduce(res, acc);
            }
            return acc;
        }
    }
}