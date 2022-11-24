using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class ConfigLoader : MonoBehaviour
{
    public static Config _targetConf;

    public static async Task<Config> GetConfig()
    {
        string path = Application.persistentDataPath + "/config.txt";
        if (!File.Exists(path))
        {
            await File.WriteAllTextAsync(path, JsonConvert.SerializeObject(new Config()));
            return _targetConf;
        }
        else
        {
            string json = await File.ReadAllTextAsync(path);
            _targetConf = JsonConvert.DeserializeObject<Config>(json);
            return _targetConf;
        }
    }
}

[System.Serializable]
public class Config
{
    public string Port;
    public string Ip;

    public Config(string port, string ip)
    {
        Port = port;
        Ip = ip;
    }
    public Config()
    {
        Port = "9090";
        Ip = "185.246.65.199";
    }
}


