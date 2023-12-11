﻿using System.Text.Json;

namespace CryptoSoft;

public class ConfigEntity 
{
    public LogType LogExtension { get; set; }
    public LangType Language { get; set; }
        
    public string? Key { get; set; }
}

public class ConfigModel
{
    private static string ConfigFileName => "config.json";
    public ConfigEntity? Config { get; private set; }

    public ConfigModel()
    { 
        PullConfigFile();
    }

    private void PullConfigFile()
    {
        var configJson = File.ReadAllText(ConfigFileName);
        Config = JsonSerializer.Deserialize<ConfigEntity>(configJson);
    }
}