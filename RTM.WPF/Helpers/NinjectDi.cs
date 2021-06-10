using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace RTM.WPF.Helpers
{
    class NinjectDi
    {
        static public IKernel Instanse { get;} = new StandardKernel(new DiConfigModule());
        private NinjectDi()
        { 
            
        }
    }
}
