using Keypass.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.ViewModels.Factories
{
    public interface IKeyPassViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
