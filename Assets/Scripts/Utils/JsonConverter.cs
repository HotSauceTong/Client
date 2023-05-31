using System.IO;
using System.Text;
using UnityEngine;

namespace Utils
{
    public static class JsonConverter
    {
        /// <summary>
        /// convert string to JSON format
        /// </summary>
        public static string ObjectToJson(object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        /// <summary>
        /// convert JSON format to string
        /// </summary>
        public static T JsonToObject<T>(string jsonData)
        {
            return JsonUtility.FromJson<T>(jsonData);
        }
        
        /// <summary>
        /// save JSON files written on string
        /// </summary>
        public static void CreateJsonFile(string createPath, string fileName, string jsonData)
        {
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }

        /// <summary>
        /// load JSON files to generic types
        /// </summary>
        public static T LoadJsonFile<T>(string loadPath, string fileName)
        {
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<T>(jsonData);
        }
    }
}


