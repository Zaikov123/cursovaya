using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPZaikovAPP.Model;
using Newtonsoft.Json;
using System.IO;
using System.Windows;


namespace DTPZaikovAPP.ViewModel
{
    public class DTPViewModel : ViewModelBase
    {
        public ObservableCollection<Car> Cars { get; set; }
        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Driver> Drivers { get; set; }
        public ObservableCollection<DTP> DTPs { get; set; }
        public ObservableCollection<Accident> Accidents { get; set; }

        public DTPViewModel()
        {
            Cars = LoadDataFromJson<Car>("cars.json");
            Departments = LoadDataFromJson<Department>("departments.json");
            Drivers = LoadDataFromJson<Driver>("drivers.json");
            DTPs = LoadDataFromJson<DTP>("dtps.json");
            Accidents = LoadDataFromJson<Accident>("accidents.json");
        }
        private ObservableCollection<T> LoadDataFromJson<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonConvert.DeserializeObject<ObservableCollection<T>>(json) ?? new ObservableCollection<T>();
                }
                else
                {
                    Console.WriteLine("File doesn't exist");
                    return new ObservableCollection<T>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from {filePath}: {ex.Message}");
                return new ObservableCollection<T>();
            }
        }

        private void SaveDataToFile<T>(string filePath, ObservableCollection<T> collection)
        {
            try
            {
                string json = JsonConvert.SerializeObject(collection, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to {filePath}: {ex.Message}");
            }
        }

        private DateTime _dtpDate;
        public DateTime DtpDate
        {
            get { return _dtpDate; }
            set
            {
                if(_dtpDate!= value)
                {
                    _dtpDate = value;
                    OnPropertyChanged(nameof(DtpDate));
                }
            }
        }
        private string _dtpPlace;
        public string DtpPlace
        {
            get { return _dtpPlace; }
            set
            {
                if(_dtpPlace!= value)
                {
                    _dtpPlace = value;
                    OnPropertyChanged(nameof(DtpPlace));
                }
            }
        }

        private uint _dtpCount;
        public uint DtpCount
        {
            get { return _dtpCount; }
            set
            {
                if(_dtpCount != value)
                {
                    _dtpCount = value;
                    OnPropertyChanged(nameof(DtpCount));
                }
            }
        }

        private uint _actNumber;
        public uint ActNumber
        {
            get { return _actNumber; }
            set
            {
                if (_actNumber != value)
                {
                    _actNumber = value;
                    OnPropertyChanged(nameof(ActNumber));
                }
            }
        }
        private string _dtpReason;
        public string DtpReason
        {
            get { return _dtpReason; }
            set
            {
                if(_dtpReason != value)
                {
                    _dtpReason = value;
                    OnPropertyChanged(nameof(DtpReason));
                }
            }
        }
        private string _dtpType;
        public string DtpType
        {
            get { return _dtpType; }
            set
            {
                if (_dtpType != value)
                {
                    _dtpType = value;
                    OnPropertyChanged(nameof(DtpType));
                }
            }
        }

        public RelayCommand AddCommand => new RelayCommand(execute => AddDtp(DtpDate, DtpPlace, DtpCount, ActNumber, DtpReason, DtpType),
            canExecute => DtpDate != DateTime.MinValue && DtpPlace != null && DtpReason != string.Empty);
        private void AddDtp(DateTime date, string place, uint dtpCount, uint actNumber, string dtpReason, string dtpType)
        {
            var newDtp = new DTP
            {
                DateDTP = date,
                Place = place,
                СasualtyRate = dtpCount,
                ActNumber = actNumber,
                DTPCause = dtpReason,
                TypeOfDTP = dtpType
            };
            DTPs.Add(newDtp);
            SaveDataToFile("dtps.json", DTPs);
        }

        private DTP _dtpToDelete;
        public DTP DtpToDelete
        {
            get { return _dtpToDelete; }
            set
            {
                if(_dtpToDelete != value)
                {
                    _dtpToDelete = value;
                    OnPropertyChanged(nameof(DtpToDelete));

                }
            }
        }
        public RelayCommand DeleteCommand => new RelayCommand(execute => DtpDelete(DtpToDelete), canExecute => DtpToDelete != null);
        private void DtpDelete(DTP delete)
        {
            DTPs.Remove(delete);
            SaveDataToFile("dtps.json", DTPs);
        }



        public RelayCommand ShowListMoreThanOneDTPMembersCommand => new RelayCommand(execute => ShowListMoreThanOneDTPMembers());

        private void ShowListMoreThanOneDTPMembers()
        {

            var multipleDTPMembers = Accidents.Where(m => m.DriversId.Count > 1);

            string message = "Члени, у яких більше одного участія у ДТП:\n";
            foreach (var member in multipleDTPMembers)
            {
                string driverName = string.Join(", ", member.DriversId.Select(d => d.FullName));
                message += $"Ім'я водія: {driverName}, Номер акту ДТП: {member.DTPId.ActNumber}\n";
            }

            MessageBox.Show(message);
        }



        private string _selectedPlaceForShowListAccidentsInPlace;

        public string SelectedPlaceForShowListAccidentsInPlace
        {
            get { return _selectedPlaceForShowListAccidentsInPlace; }
            set
            {
                if(_selectedPlaceForShowListAccidentsInPlace != value)
                {
                    _selectedPlaceForShowListAccidentsInPlace = value;
                    OnPropertyChanged(nameof(SelectedPlaceForShowListAccidentsInPlace));
                }
            }
        }


        public RelayCommand ShowListAccidentsInPlaceCommand => new RelayCommand(execute => ShowListAccidentsInPlace(SelectedPlaceForShowListAccidentsInPlace),
            canExecute => SelectedPlaceForShowListAccidentsInPlace != string.Empty);

        private void ShowListAccidentsInPlace(string place)
        {
            var accidentsInPlace = from accident in Accidents
                                   where accident.DTPId.Id.ToString() == place
                                   select accident;

            string result = "Список водіїв, які беруть участь у ДТП у заданому місці:\n";

            foreach (var accident in accidentsInPlace)
            {
                foreach (var driver in accident.DriversId)
                {
                    result += $"{driver.FullName}\n";
                }
            }

            MessageBox.Show(result);
        }

        private DateTime _selectedDateForShowListAccidentByDate = DateTime.MinValue;
        public DateTime SelectedDateForShowListAccidentByDate
        {
            get { return _selectedDateForShowListAccidentByDate; }
            set
            {
                if(_selectedDateForShowListAccidentByDate != value)
                {
                    _selectedDateForShowListAccidentByDate = value;
                    OnPropertyChanged(nameof(SelectedDateForShowListAccidentByDate));
                }
            }
        }

        public RelayCommand ShowListAccidentsByDateCommand => new RelayCommand(execute => ShowListAccidentsByDate(SelectedDateForShowListAccidentByDate));

        private void ShowListAccidentsByDate(DateTime date)
        {
            var accidentsInPlace = from accident in Accidents
                                   where accident.DTPId.DateDTP.ToShortDateString() == date.ToShortDateString()
                                   select accident;

            string result = "\tСписок водіїв, які беруть участь у ДТП на дану дату:\n";

            foreach (var accident in accidentsInPlace)
            {
                foreach (var driver in accident.DriversId)
                {
                    result += $"{driver.FullName}\n";
                }
            }

            MessageBox.Show(result);
        }


        public RelayCommand AccidentWithMaxСasualtyRateCommand => new RelayCommand(execute => AccidentWithMaxСasualtyRate());

        private void AccidentWithMaxСasualtyRate()
        {
            var maxCasualityRateDTP = DTPs.OrderBy(dtp => dtp.СasualtyRate).Last();

            MessageBox.Show("Номер акту: " + maxCasualityRateDTP.ActNumber.ToString() + " Кількість постраждалих: " + maxCasualityRateDTP.СasualtyRate);
        }


        public RelayCommand AccidentCauseByHitAndRunCarCommand => new RelayCommand(execute => AccidentCauseByHitAndRunCar());

        private void AccidentCauseByHitAndRunCar()
        {
            var accidentCauseByHitAndRunCar = from dtp in DTPs
                                              where dtp.TypeOfDTP == "A hit-and-run with a car"
                                              select dtp;

            string result = "\tСписок водіїв, які беруть участь у ДТП із наїздом на пішоходів:\n";
            foreach (var dtp in accidentCauseByHitAndRunCar)
            {
                result += $"Номер акту: {dtp.ActNumber}\n";
            }
            MessageBox.Show(result);
        }
    }


    

}
