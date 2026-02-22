using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Utils
{
    public class PropertyStrBuilder
    {
        private readonly string[] properties;

        private int idx;

        public PropertyStrBuilder(int size = 10)
        {
            properties = new string[size];
        }

        public PropertyStrBuilder Add<T>(string propertyName, T propertyValue, Func<T, bool> propertyPredicate = null, Func<T, string> propertyFormatter = null)
        {
            var predicate = propertyPredicate ?? (value => !Equals(value, default(T)));
            if (predicate(propertyValue))
            {
                var formatter = propertyFormatter ?? (value => value.ToString());
                properties[idx++] = string.Format("{0} = {1}", propertyName, formatter(propertyValue));
            }

            return this;
        }

        public PropertyStrBuilder AddQuoted<T>(string propertyName, T propertyValue, Func<T, bool> propertyPredicate = null, Func<T, string> propertyFormatter = null)
        {
            var formatter = propertyFormatter ?? (value => value.ToString());
            return Add(propertyName, propertyValue, propertyPredicate, value => "'" + formatter(value) + "'");
        }

        public string Build()
        {
            return "(" + properties.Where(token => !string.IsNullOrWhiteSpace(token)).Join() + ")";
        }
    }
}