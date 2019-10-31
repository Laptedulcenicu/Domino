using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UMLV.MVC
{
    public class DataStorage : MonoBehaviour
    {
        private Dictionary<string, Object> storage = new Dictionary<string, Object>();

        public void SaveData(string key, Object data)
        {
            storage[key] = data;
        }

        public T GetData<T>(string key) where T : Object
        {
            if (!storage.ContainsKey(key))
            {
                Debug.LogError("No data under key: " + key);
                return null;
            }

            var data = storage[key];
            return data as T;
        }
    }
}
