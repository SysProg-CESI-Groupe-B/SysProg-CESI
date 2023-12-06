﻿using EasySave.Models;
using EasySave.Types;
using EasySave.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.ViewModels
{
    public class ModifyViewModel
    {
        public ModifyView ModifyView { get; set; }
        public HelpView HelpView { get; set; }
        public TaskModel TaskModel { get; set; }

        public ModifyViewModel(string[] args)
        {
            ModifyView = new ModifyView();
            HelpView = new HelpView();

            if (!(args.Length == 3))
            {
                HelpView.DisplayModify();
                HelpView.DisplayMessage();
            }
            else
            {
                string methodName = "Modify" + args[1];

                var method = typeof(ModifyViewModel).GetMethod(methodName);

                var parameters = new object[1];
                parameters[0] = args;

                if (method != null)
                {
                    method.Invoke(this, parameters);
                }
            }
        }

        public void ModifyName(string[] args)
        {
            TaskModel = new TaskModel();
            string result = TaskModel.UpdateTask(false, args[0], null, null, null, args[2]);
            ModifyView.Message = result;
            ModifyView.DisplayMessage();
        }

        public void ModifySource(string[] args)
        {
            TaskModel = new TaskModel();
            string result = TaskModel.UpdateTask(false, args[0], args[2], null, null, null);
            ModifyView.Message = result;
            ModifyView.DisplayMessage();
        }

        public void ModifyDest(string[] args)
        {
            TaskModel = new TaskModel();
            string result = TaskModel.UpdateTask(false, args[0], null, args[2], null, null);
            ModifyView.Message = result;
            ModifyView.DisplayMessage();
        }

        public void ModifyType(string[] args)
        {
            TaskModel = new TaskModel();

            if (Enum.TryParse<BackupType>(args[2], true, out BackupType backupType))
            {
                string result = TaskModel.UpdateTask(false, args[0], null, null, backupType, null);
                ModifyView.Message = result;
            }
            else
            {
                ModifyView.Message = "This is a wrong backup type";
            }

            ModifyView.DisplayMessage();
        }

    }
}
