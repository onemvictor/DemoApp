using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Interfaces;
using System.Collections.ObjectModel;
using DemoApp.Infrastructure;

namespace UserControls.ViewModel
{
    public class MainMenuViewModel:AbstractNotifier
    {
        private ObservableCollection<IAdapterModule> _adapterModules;

        public MainMenuViewModel()
        {
            AdapterModules = new ObservableCollection<IAdapterModule>();
        }

        public ObservableCollection<IAdapterModule> AdapterModules
        {
            get
            {
                return _adapterModules;
            }
            set
            {
                SetValue(ref _adapterModules, value);
            }
        }
    }
}
