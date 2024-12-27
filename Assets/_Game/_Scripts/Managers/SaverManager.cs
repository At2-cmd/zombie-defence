using System;
using UnityEngine;

public static class SaverManager
{
    public static class Keys
    {
        public const string LastLevelIndex = "LevelIndex";
        public const string TotalKilledEnemy = "TotalKilledEnemy";
    }

    public static void Save<T>(string key, T value)
    {
        if (typeof(T) == typeof(int))
        {
            PlayerPrefs.SetInt(key, Convert.ToInt32(value));
        }
        else if (typeof(T) == typeof(float))
        {
            PlayerPrefs.SetFloat(key, Convert.ToSingle(value));
        }
        else if (typeof(T) == typeof(bool))
        {
            PlayerPrefs.SetInt(key, Convert.ToBoolean(value) ? 1 : 0);
        }
        else if (typeof(T) == typeof(string))
        {
            PlayerPrefs.SetString(key, value.ToString());
        }
        else
        {
            Debug.LogError($"Unsupported type: {typeof(T)}");
            return;
        }

        PlayerPrefs.Save();
    }

    public static T Load<T>(string key, T defaultValue = default)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            return defaultValue;
        }

        if (typeof(T) == typeof(int))
        {
            return (T)(object)PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue));
        }
        else if (typeof(T) == typeof(float))
        {
            return (T)(object)PlayerPrefs.GetFloat(key, Convert.ToSingle(defaultValue));
        }
        else if (typeof(T) == typeof(bool))
        {
            return (T)(object)(PlayerPrefs.GetInt(key, Convert.ToBoolean(defaultValue) ? 1 : 0) == 1);
        }
        else if (typeof(T) == typeof(string))
        {
            return (T)(object)PlayerPrefs.GetString(key, defaultValue?.ToString() ?? "");
        }
        else
        {
            Debug.LogError($"Unsupported type: {typeof(T)}");
            return default;
        }
    }

    public static void Delete(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
        }
    }

    public static void ClearAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
