using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMTest.ViewModels.Base;
using MVVMTest.ViewModels.Commands;
using MVVMTest.Constants;
using System.Windows.Input;
using System.Windows;


namespace MVVMTest.ViewModels
{
    internal class IsocenterCalculationMainViewModel : ViewModelBase
    {

        internal IsocenterCalculationSetupViewModel  IsoSetup { get; set; }

        internal IsocenterCalculationMainViewModel()
        {
           
            //MessageBox.Show($"New IsocenterCalculationSetupViewModel instance created. {IsoSetup.SelectedMaxDeltaHFS}");
            CalculateIsocentersCommand = new RelayCommand(execute => { CalculateIso(); }, canExecute => { return true; });
            MaxDeltaHFS = 3;
        }

        public ICommand CalculateIsocentersCommand { get; set; }

        private int _maxDeltaHFS;

        public int MaxDeltaHFS
        {
            get { return _maxDeltaHFS; }
            set 
            { 
                _maxDeltaHFS = value;
                OnPropertyChanged();
            }
        }


        public void CalculateIso()
        {
            MessageBox.Show($"Testing to get data from calcSetup: {IsoSetup.SelectedMaxDeltaHFS}");
        }



    }
}
