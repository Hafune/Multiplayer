using System;
using System.Collections.Generic;
using Core.Services;

namespace Lib
{
    public class ServicesValues
    {
        [ES3Serializable] public ReleaseVersion version;
        [ES3Serializable] private readonly Dictionary<string, byte[]> _bytes = new();

        public T DeserializeData<T>(object key) where T : new()
        {
            try
            {
                var bytes = GetBytes(key);
                var data = (bytes.Length == 0 ? new() : ES3.Deserialize<T>(bytes)) ?? new();
                return data;
            }
            catch (Exception)
            {
                return new();
            }
        }

        public void SerializeData(object key, object data)
        {
            var bytes = ES3.Serialize(data);
            SetBytes(key, bytes);
        }
        
        private byte[] GetBytes(object obj)
        {
            var key = obj.GetType().FullName!;
            return _bytes.ContainsKey(key) ? _bytes[key] : _bytes[key] = Array.Empty<byte>();
        }

        private void SetBytes(object obj, byte[] value) => _bytes[obj.GetType().FullName!] = value;

        public bool IsEmpty => _bytes.Count == 0;
    }
}