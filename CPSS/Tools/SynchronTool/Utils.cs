using System;
using System.IO;
using System.Xml.Serialization;

namespace SynchronTool
{
    public class Utils
    {
        public static T GetConfig<T>(string filePath) where T : class, new()
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                using (var streamReader = new StreamReader(filePath))
                {
                    var config = xmlSerializer.Deserialize(streamReader) as T;
                    return config;
                }
            }
            catch
            {
            }
            return new T();
        }
    }
}