﻿using EasySave.Models;
using EasySave.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveGraphic.ViewModels
{
    internal class ConfigViewModel
    {
        public ConfigModel ConfigModel { get; set; }

        public string[] LangTypeComboItem { get; set; }
        public string[] LogTypeComboItem { get; set; }

        public ConfigViewModel()
        {
            ConfigModel = new ConfigModel();
            GetComboItem();
        }

        public void GetComboItem()
        {
            LangTypeComboItem = Enum.GetNames(typeof(LangType));
            LogTypeComboItem = Enum.GetNames(typeof(LogType));
        }

        public void UpdateConfigFile(LogType? logExtension, LangType? lang)
        {
            ConfigModel.UpdateConfigFile(logExtension, lang);
        }
    }
}