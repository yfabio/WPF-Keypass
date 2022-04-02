using Keypass.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.ViewModels.WindowServices
{
    public interface IHomeViewService
    {
        void SelectionChanged(Service service);
        void Checked();
        void Unchecked();
        void SearchBoxEmptyEvent();
        void SelectedRadioBox(string content);
        void OnInputSearchHandler();
    }
}
