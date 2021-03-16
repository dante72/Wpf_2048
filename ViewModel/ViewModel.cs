using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_2048.ViewModel
{
    public class ViewModel : BaseViewModel
    {
        public ObservableCollection<string> MyCollection { get; }
        public ViewModel(Model model)
        {
            MyCollection = model.observableCollection;
        }


    }
}
