﻿using EasySave.Types;
using System.Text.Json;

namespace EasySave.Models
{
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
            if (!File.Exists(ConfigFileName))
            {
                CreateConfigFile();
            }

            PullConfigFile();
        }

        private void CreateConfigFile()
        {
            var defaultConfig = new ConfigEntity
            {
                LogExtension = LogType.Json,
                Language = LangType.En,
                Key = "0123456789ABCDEF"
            };

            var defaultConfigJson = JsonSerializer.Serialize(defaultConfig);
            File.WriteAllText(ConfigFileName, defaultConfigJson);
        }

        private void PullConfigFile()
        {
            var configJson = File.ReadAllText(ConfigFileName);
            Config = JsonSerializer.Deserialize<ConfigEntity>(configJson);
        }

        public void UpdateConfigFile(LogType? logExtension, LangType? lang)
        {
            if (logExtension != null)
            {
                Config!.LogExtension = (LogType)logExtension;
            }

            if (lang != null)
            {
                Config!.Language = (LangType)lang;
            }

            var updatedConfigJson = JsonSerializer.Serialize(Config);
            File.WriteAllText(ConfigFileName, updatedConfigJson);
        }
    }
}
