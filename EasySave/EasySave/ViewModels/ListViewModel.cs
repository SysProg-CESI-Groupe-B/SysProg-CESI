﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.ViewModels
{
    public class ListViewModel
    {
        public IView ListView { get; set; }
        public ITaskModel TaskModel { get; set; }

        public ListViewModel()
        {
        }

        public void ListViewModel()
        {
        }
    }
}
