using Keypass.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.State.Navigators
{
    public enum ViewType 
    {
        Login,
        Home,
        Add,
        Option
    }

    public interface INavigator
    {
        public ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
