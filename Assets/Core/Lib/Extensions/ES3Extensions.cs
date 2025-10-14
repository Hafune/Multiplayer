using System;

namespace Lib
{
    public static class ES3Functions
    {
        private static ES3Settings settings = new()
        {
            compressionType = ES3.CompressionType.Gzip   
        };

        public static string SerializeToBase64(object data)
        {
            var serializeData = ES3.Serialize(data, settings);
            // Debug.Log("UnCompressed Size: " + (int)(Encoding.UTF8.GetBytes(Convert.ToBase64String(serializeData)).Length / 1024.0) + "KB");

            return Convert.ToBase64String(serializeData);
        }

        public static T DeserializeFromBase64<T>(string data)
        {
            var decodeByte = Convert.FromBase64String(data);
            
            return ES3.Deserialize<T>(decodeByte, settings) ?? default;
        }
    }
}