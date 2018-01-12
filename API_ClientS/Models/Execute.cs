using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace API_ClientS.Models
{
    public class Execute<T>
    {
        public T Entity { get; set; }

        [IgnoreDataMember]
        private Dictionary<string, object> Properties { get; set; }

        public bool HasErro { get; set; }

        public List<string> Messages { get; set; }

        public Execute()
        {
            Messages = new List<string>();
        }

        public virtual void AddMessage(string message)
        {
            Messages.Add(message);
        }

        public virtual TValue GetProperty<TValue>(string key)
        {
            if (Properties.TryGetValue(key, out object result))
            {
                return (TValue)result;
            }

            return default(TValue);
        }

        public virtual bool SetProperty<TValue>(string key, TValue value)
        {
            try
            {
                Properties[key] = value;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
