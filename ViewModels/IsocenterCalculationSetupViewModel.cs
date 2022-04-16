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
    enum PatientLengthLimits
    {
        ShortPatientLimit = 1500, // TODO: settings, PatientLengthLimits, should be read from settings, although more loose limits
        MediumPatientLimit = 1900,
        LongPatientLimit = 2050
    }
    internal class IsocenterCalculationSetupViewModel : ViewModelBase
    {
        
        public static int MinJunction = 7; // TODO: settings MinJunction, OptimalJunction, MaxJunction, MinDeltaHFS, MaxDeltaHFS, readback to setting and convert to mm
        public static int OptimalJunction = 10;
        public static int MaxJunction = 15;
        public static int MinDeltaHFS = 17;
        public static int MaxDeltaHFS = 20;
        public static int MaxDeltaFFS = 22;
        public static int CoverageMarginCranial = 3;
        public static int CoverageMarginCaudal = 3;


        // need to get possible index from patient length, i.e. reduced list. Two way binding to update the selected index in PreReqData
        internal IsocenterCalculationSetupViewModel()
        {

            double patientLength = 1900;
            _possibleIndexForMaskBasePlate = new List<CouchInfo.CouchLongCoord>();
            GetPossibleIndexForMaskBasePlate(patientLength);
            MinimumJunctionOptions = GetIntegerList(MinJunction, MaxJunction);
            MaximumJunctionOptions = GetIntegerList(MinJunction, MaxJunction);
            MinDeltaHFSOptions = GetIntegerList(MinDeltaHFS, MaxDeltaHFS);
            MaxDeltaHFSOptions = GetIntegerList(MinDeltaHFS, MaxDeltaHFS);
            SelectedMinJunction = MinJunction;
            SelectedMaxJunction = OptimalJunction;
            SelectedMaxDeltaHFS = MaxDeltaHFS;
            SelectedMinDeltaHFS = MinDeltaHFS;
            MaxDeltaFFSOptions = GetIntegerList(MinDeltaHFS, MaxDeltaFFS);
            SelectedMaxDeltaFFS = MaxDeltaFFS;
            CoverageMarginHFSOptions = GetIntegerList(fromInteger: -5, toInteger: 10);
            CoverageMarginFFSOptions = GetIntegerList(fromInteger: -5, toInteger: 10);
            SelectedCoverageMarginCranial = CoverageMarginCranial;
            SelectedCoverageMarginCaudal = CoverageMarginCaudal;
            CalculateIsocentersCommand = new RelayCommand(execute => { ButtonExec(); }, canExecute => { return true; });
        }

        public ICommand CalculateIsocentersCommand { get; set; }

        private List<CouchInfo.CouchLongCoord> _possibleIndexForMaskBasePlate;

        public List<CouchInfo.CouchLongCoord> PossibleIndexForMaskBasePlate
        {
            get { return _possibleIndexForMaskBasePlate; }
            set
            {
                _possibleIndexForMaskBasePlate = value;
                OnPropertyChanged();
            }
        }


        private CouchInfo.CouchLongCoord _selectedIndexForMaskBasePlate = CouchInfo.CouchLongCoord.F6;

        public CouchInfo.CouchLongCoord SelectedIndexForMaskBasePlate
        {
            get { return _selectedIndexForMaskBasePlate; }
            set
            {
                _selectedIndexForMaskBasePlate = value;
                OnPropertyChanged(nameof(SelectedIndexForMaskBasePlate));
                OnPropertyChanged(nameof(PossibleNrOfIsocentersHFS));
                OnPropertyChanged(nameof(SelectedNrOfIsocentersHFS));
            }
        }


        //------------   Nunber of isocenters  -------------------


        public List<int> PossibleNrOfIsocentersHFS
        {
            get
            {
                return GetPossibleNrOfIsocentersHFS(SelectedIndexForMaskBasePlate);
                //OnPropertyChanged acts from SelectedIndex(?)
            }
        }


        private int _selectedNrOfIsocentersHFS;
        public int SelectedNrOfIsocentersHFS
        {
            get { return _selectedNrOfIsocentersHFS; }
            set
            {
                _selectedNrOfIsocentersHFS = value;
                OnPropertyChanged();
            }
        }

        //-------------------------- DELTA HFS ---------------------------

        public List<int> MinDeltaHFSOptions { get; set; }

        private int _selectedMinDeltaHFS;

        public int SelectedMinDeltaHFS
        {
            get { return _selectedMinDeltaHFS; }
            set 
            { 
                _selectedMinDeltaHFS = value;
                OnPropertyChanged();
                MaxDeltaHFSOptions = GetIntegerList(SelectedMinDeltaHFS, MaxDeltaHFS);
                if (SelectedMinDeltaHFS > SelectedMaxDeltaHFS)
                {
                    SelectedMaxDeltaHFS = SelectedMinDeltaHFS;
                }
                OnPropertyChanged(nameof(MaxDeltaHFSOptions));
            }
        }

        

        public List<int> MaxDeltaHFSOptions { get; set; }

        private int _selectedMaxDeltaHFS;

        public int SelectedMaxDeltaHFS
        {
            get { return _selectedMaxDeltaHFS; }
            set 
            { 
                _selectedMaxDeltaHFS = value;
                OnPropertyChanged();
            }
        }
        //-------------------------- CRANIAL COVERAGE MARGIN ------------------------------

        public List<int> CoverageMarginHFSOptions { get; set; }
       
        private int _selectedCoverageMarginCranial;

        public int SelectedCoverageMarginCranial
        {
            get { return _selectedCoverageMarginCranial; }
            set 
            { 
                _selectedCoverageMarginCranial = value;
                OnPropertyChanged();
            }
        }


        //-------------------------- JUNCTION ------------------------------

        public List<int> MinimumJunctionOptions { get; set; }


        private int _selectedMinJunction = 7;
        public int SelectedMinJunction
        {
            get { return _selectedMinJunction; }
            set
            {
                _selectedMinJunction = value;
                OnPropertyChanged(nameof(SelectedMinJunction));
                OnPropertyChanged(nameof(MinimumJunctionOptions));
                if (SelectedMaxJunction < SelectedMinJunction)
                {
                    SelectedMaxJunction = SelectedMinJunction;
                }
                MaximumJunctionOptions = GetIntegerList(SelectedMinJunction, MaxJunction);
                OnPropertyChanged(nameof(MaximumJunctionOptions));
            }
        }

        public List<int> MaximumJunctionOptions { get; set; }

        private int _selectedMaxJunction = 10;
        public int SelectedMaxJunction
        {
            get { return _selectedMaxJunction; }
            set
            {
                _selectedMaxJunction = value;
                OnPropertyChanged(nameof(SelectedMaxJunction));
                OnPropertyChanged(nameof(MinimumJunctionOptions));
                OnPropertyChanged(nameof(MaximumJunctionOptions));
            }
        }

        //-------------------------- CAUDAL COVERAGE MARGIN ------------------------------

        public List<int> CoverageMarginFFSOptions { get; set; }
        private int _selectedCoverageMarginCaudal;

        public int SelectedCoverageMarginCaudal
        {
            get { return _selectedCoverageMarginCaudal; }
            set
            {
                _selectedCoverageMarginCaudal = value;
                OnPropertyChanged();
            }
        }

        //-------------------------- DELTA FFS ------------------------------


        public List<int> MaxDeltaFFSOptions { get; set; }

        private int _selectedMaxDeltaFFS;

        public int SelectedMaxDeltaFFS
        {
            get { return _selectedMaxDeltaFFS; }
            set
            {
                _selectedMaxDeltaFFS = value;
                OnPropertyChanged();
            }
        }



        public void ButtonExec()
        {
            MessageBox.Show("Executing command!");
        }




        /// <summary>
        /// Gets the possible number of iso and sets a default number of iso depending on the fixation index.
        /// Per default 5 isocenters is always available to choose but the default selected changes depending
        /// on fixation index.
        /// </summary>
        /// <param name="selectedIndex"></param>
        /// <returns></returns>
        private List<int> GetPossibleNrOfIsocentersHFS(CouchInfo.CouchLongCoord selectedIndex)
        {
            List<int> isos = new List<int>();

            switch (selectedIndex)
            {
                case CouchInfo.CouchLongCoord.F5:
                    isos.Add(4);
                    isos.Add(5);
                    _selectedNrOfIsocentersHFS = 4;
                    break;
                case CouchInfo.CouchLongCoord.F6:
                    isos.Add(5);
                    _selectedNrOfIsocentersHFS = 5;
                    break;
                case CouchInfo.CouchLongCoord.F7:
                    isos.Add(5);
                    isos.Add(6);
                    _selectedNrOfIsocentersHFS = 6;
                    break;
            }
            return isos;
        }


        /// <summary>
        /// Gets the possible index and sets a default selected index for the fixation depending on the patient length.
        /// Per default the index F6 is always available to choose but the default selected changes depending
        /// on patient length.
        /// </summary>
        /// <param name="patientLength"></param>
        private void GetPossibleIndexForMaskBasePlate(double patientLength)
        {

            PossibleIndexForMaskBasePlate.Add(CouchInfo.CouchLongCoord.F6);

            if (patientLength < (int)PatientLengthLimits.ShortPatientLimit)
            {
                PossibleIndexForMaskBasePlate.Add(CouchInfo.CouchLongCoord.F5);
                SelectedIndexForMaskBasePlate = CouchInfo.CouchLongCoord.F5;
            }
            else if (patientLength < (int)PatientLengthLimits.MediumPatientLimit)
            {
                SelectedIndexForMaskBasePlate = CouchInfo.CouchLongCoord.F6;
            }
            else if (patientLength < (int)PatientLengthLimits.LongPatientLimit)
            {
                PossibleIndexForMaskBasePlate.Add(CouchInfo.CouchLongCoord.F7);
                SelectedIndexForMaskBasePlate = CouchInfo.CouchLongCoord.F6;
            }
            else
            {
                PossibleIndexForMaskBasePlate.Add(CouchInfo.CouchLongCoord.F7);
                SelectedIndexForMaskBasePlate = CouchInfo.CouchLongCoord.F7;
            }
        }

        private List<int> GetIntegerList(int fromInteger, int toInteger, int integerStep = 1)
        {
            List<int> integerList = new List<int>();
            for (int i = fromInteger; i <= toInteger; i++)
            {
                integerList.Add(i);
            }
            return integerList;
        }

    }
}
