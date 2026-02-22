using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    public class Mapper<TSource, TDest>
    {
        private readonly IDictionary<TSource, TDest> consts = new Dictionary<TSource, TDest>();

        public Mapper<TSource, TDest> Register(TSource key, TDest value)
        {
            if (consts.ContainsKey(key))
            {
                throw new AppException(string.Format("Mapper already contains key {0}", key));
            }

            consts.Add(key, value);
            return this;
        }

        public Mapper<TSource, TDest> Unregister(TSource key)
        {
            if (!consts.ContainsKey(key))
            {
                throw new AppException(string.Format("Mapper doesn't contain key {0}", key));
            }

            consts.Remove(key);
            return this;
        }

        public virtual TDest Get(TSource key, bool throwIfNotFound = true)
        {
            if (consts.ContainsKey(key))
            {
                return consts[key];
            }

            if (throwIfNotFound)
            {
                throw new AppException(string.Format("Mapper doesn't contain key {0}", key));
            }
            
            return default(TDest);
        }
    }
}
