using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Saves.Utils
{
    public static class SaveLoadUtils
    {
        public static void Save<T>(T data, string filename)
        {
            var binaryFormatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "/" + filename + ".class";
            var fileStream = new FileStream(path, FileMode.Create);
            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
        }

        public static T Load<T>(out bool success, string filename)
        {
            var path = Application.persistentDataPath + "/" + filename + ".class";
            FileStream fileStream;
            try
            {
                fileStream = new FileStream(path, FileMode.Open);
            }
            catch (FileNotFoundException e)
            {
                Debug.Log("file doesn't exist");
                success = false;
                return default;
            }

            if (File.Exists(path) && fileStream.Length > 0)
            {
                var binaryFormatter = new BinaryFormatter();
                var data = (T) binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                success = true;
                return data;
            }

            Debug.Log("file doesn't exist");
            success = false;
            return default;
        }
    }
}