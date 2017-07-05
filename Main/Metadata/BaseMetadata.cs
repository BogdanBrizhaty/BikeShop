using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Main.Metadata
{
    public class BaseMetadata<T>
    {
        protected Dictionary<string, T> _metadata;
        public BaseMetadata()
        {
            _metadata = new Dictionary<string, T>();
        }
        public void AddMetadata(string key, T data)
        {
            _metadata.Add(key, data);
        }
        public void RemoveMetada(string key)
        {
            _metadata.Remove(key);
        }
        public void AddMetadataToRequest(HttpRequestMessage request)
        {
            foreach (var record in _metadata)
                request.Properties[record.Key] = record.Value;
        }
    }
}