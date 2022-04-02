using MvvmFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.ViewModels
{

    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

    public class ViewModelBase : WPFFramework
    {        
        public virtual void Dispose() { }
        public Action LoginSucceed;
    }
}
