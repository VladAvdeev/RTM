﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using RTM.WPF.Services;

namespace RTM.WPF.Helpers
{
    class DiConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWindowService>().To<WindowService>();
        }
    }
}
