using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using TimeSheet.Resource;
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class RecordManagementViewModel : ViewModelBase
    {
        public ObservableCollection<User> UserList { get; } = new ObservableCollection<User>();
        public ObservableCollection<Task> TaskList { get; } = new ObservableCollection<Task>();
        public ObservableCollection<RecordViewModel> RecordList { get; } = new ObservableCollection<RecordViewModel>();

        public RecordManagementView _view;
        private int _lastSelectedRecordID = 0; // utoljara valasztott Record ID-ja
        private int _lastSelectedRecordCount = 0; // utoljara valasztott elem indexe a RecordList-bol


        private RecordViewModel _selectedRecord;
        public RecordViewModel SelectedRecord
        {
            get { return _selectedRecord; }
            set
            {
                _selectedRecord = value;

                if (_selectedRecord != null)
                {
                    if (_lastSelectedRecordID != 0 && _lastSelectedRecordID != _selectedRecord.Record.IdRecord) // ha nem az utoljara valasztott Record id-ja nem egyezik a mostanival
                    {
                        //RecordViewModel LastSelectedRecord = (RecordViewModel)RecordList.Where(recordViewModel => recordViewModel.IdRecord == _lastSelectedRecordID);

                        for (int i = 0; i < RecordList.Count(); i++)
                        {
                            if (RecordList[i].IdRecord == _lastSelectedRecordID) // ha egyezik az utoljara valasztott Record Id-ja a Listaban talalhato i. IdRecord-dal es lementem a megfelelo indexet
                            {
                                _lastSelectedRecordCount = i;
                            }
                        }

                        if (RecordList[_lastSelectedRecordCount].IsSelectedRecordValuesChanged) // visszatoltom az eredeti adatokat ha tortent valtozas
                        {
                            try
                            {
                                Record SelectedRecordValues = new RecordRepository(new RecordLogic()).GetRecordByID(_lastSelectedRecordID);
                                RecordList[_lastSelectedRecordCount].Date = SelectedRecordValues.Date;
                                RecordList[_lastSelectedRecordCount].Comment = SelectedRecordValues.Comment;
                                RecordList[_lastSelectedRecordCount].Duration = SelectedRecordValues.Duration;

                                RecordList[_lastSelectedRecordCount].IsChangedRecordValuesToFalse(); // igy a Save gomb disabled lesz ha visszakattintok
                            }
                            catch (SqlException)
                            {
                                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    _lastSelectedRecordID = _selectedRecord.Record.IdRecord;
                }

                OnPropertyChanged(nameof(SelectedRecord));
                OnPropertyChanged(nameof(SelectedRecordVisibility));
                OnPropertyChanged(nameof(ListRecordsViewContextMenuVisibility));
            }
        }


        private string _searchValue;
        public string SearchValue // keresesi szoveg bindolashoz
        {
            get { return _searchValue; }
            set
            {
                _searchValue = value;
                OnPropertyChanged(nameof(SearchValue));
            }
        }

        private DateTime _dateFrom = DateTime.Today;
        private int _dateFromHelper = 0; // hogy 1x fusson le csak a foreach amivel megkapja a listaban levo legkorabbi datumot belepeskor
        public DateTime DateFrom
        {
            get
            {
                if (_dateFromHelper < 1) // Belepeskor beallitom a DateFrom datumat a legkorabbira
                {
                    foreach (RecordViewModel recordViewModel in RecordList)
                    {
                        if (recordViewModel.Record.Date < _dateFrom)
                        {
                            _dateFrom = recordViewModel.Record.Date;
                        }
                    }
                    _dateFromHelper++;
                }
                return _dateFrom;
            }
            set
            {
                _dateFrom = value;
                OnPropertyChanged(nameof(DateFrom));
            }
        }

        private DateTime _dateTo = DateTime.Parse("0001.01.01");
        private int _dateToHelper = 0; // hogy 1x fusson le csak a foreach amivel megkapja a listaban levo legkesobbi datumot belepeskor
        public DateTime DateTo
        {
            get
            {
                if (_dateToHelper < 1) // Belepeskor beallitom a DateTo datumat a legkesobbire
                {
                    foreach (RecordViewModel recordViewModel in RecordList)
                    {
                        if (recordViewModel.Record.Date > _dateTo)
                        {
                            _dateTo = recordViewModel.Record.Date;
                        }
                    }
                    _dateToHelper++;
                }
                return _dateTo;
            }
            set
            {
                _dateTo = value;
                OnPropertyChanged(nameof(DateTo));
            }
        }

        private int _durationFrom = 720;
        private int _durationFromHelper = 0; // hogy 1x fusson le csak a foreach amivel megkapja a listaban levo legrovidebb Idotartamat belepeskor
        public int DurationFrom
        {
            get
            {
                if (_durationFromHelper < 1) // Belepeskor beallitom a DurationFrom-ot a legrovidebbre
                {
                    foreach (RecordViewModel recordViewModel in RecordList)
                    {
                        if (recordViewModel.Record.Duration < _durationFrom)
                        {
                            _durationFrom = recordViewModel.Record.Duration;
                        }
                    }
                    _durationFromHelper++;
                }
                return _durationFrom;
            }
            set
            {
                _durationFrom = value;
                OnPropertyChanged(nameof(DurationFrom));
            }
        }
        public string DurationFromFormat // Idotartam megfelelo kiirasanak a bindaloshoz
        {
            get
            {
                return TimeSpan.FromMinutes(_durationFrom).ToString("hh':'mm");
            }
            set
            {
                try
                {
                    TimeSpan input = TimeSpan.ParseExact(value, "hh':'mm", null);

                    _durationFrom = (int)input.TotalMinutes;
                    OnPropertyChanged(nameof(DurationFrom));
                }
                catch (FormatException)
                {
                    //Do Nothing
                }
            }
        }

        private int _durationTo = 0;
        private int _durationToHelper = 0; // hogy 1x fusson le csak a foreach amivel megkapja a listaban levo leghosszabb Idotartamat belepeskor
        public int DurationTo
        {
            get
            {
                if (_durationToHelper < 1) // Belepeskor beallitom a DurationTo-t a leghosszabbra
                {
                    foreach (RecordViewModel recordViewModel in RecordList)
                    {
                        if (recordViewModel.Record.Duration > _durationTo)
                        {
                            _durationTo = recordViewModel.Record.Duration;
                        }
                    }
                    _durationToHelper++;
                }
                return _durationTo;
            }
            set
            {
                _durationTo = value;
                OnPropertyChanged(nameof(DurationTo));
            }
        }
        public string DurationToFormat // Idotartam megfelelo kiirasanak a bindaloshoz
        {
            get
            {
                return TimeSpan.FromMinutes(_durationTo).ToString("hh':'mm");
            }
            set
            {
                try
                {
                    TimeSpan input = TimeSpan.ParseExact(value, "hh':'mm", null);

                    _durationTo = (int)input.TotalMinutes;
                    OnPropertyChanged(nameof(DurationTo));
                }
                catch (FormatException)
                {
                    //Do Nothing
                }
            }
        }


        private bool _isMyRecordsCheckBoxChecked = true;
        public bool IsMyRecordsCheckBoxChecked // Sajat rogziteseket mutato checkbox pipajahoz a bindolashoz
        {
            get
            {
                return _isMyRecordsCheckBoxChecked;
            }
            set
            {
                _isMyRecordsCheckBoxChecked = value;
                OnPropertyChanged(nameof(IsMyRecordsCheckBoxChecked));
            }
        }


        public Visibility SelectedRecordVisibility // Kivalasztott rogzites lathatosaga
        {
            get
            {
                if (SelectedRecord != null)
                {
                    SelectedRecord.RecordCanceled += OnRecordCanceled; // RecordCanceled Event("Cancel" gomb megnyomasa) eseten a kivalasztott rogzites eltunik
                }
                return SelectedRecord == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        private void OnRecordCanceled(Object obj)
        {
            if (SelectedRecord != null) // Ha 2x megnyitottam ugyanazt egymas utan akkor valamiert lefutt tobbszor ezert van ez az if
            {
                if (SelectedRecord.IsSelectedRecordValuesChanged) // Cancel eseten visszatoltom az eredti adatokat ha megvaltoztattuk oket
                {
                    try
                    {
                        Record SelectedRecordValues = new RecordRepository(new RecordLogic()).GetRecordByID(SelectedRecord.IdRecord);
                        SelectedRecord.Date = SelectedRecordValues.Date;
                        SelectedRecord.Comment = SelectedRecordValues.Comment;
                        SelectedRecord.Duration = SelectedRecordValues.Duration;
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

                SelectedRecord = null; // null-ozom h a SelectedRecord Visibility-je Collapsed legyen
            }
        }

        public Visibility RecordCheckBoxAndTextVisibility // Sajat rogziteseket mutato checkbox es szoveg lathatosaganak a bindolasahoz (ha admin lep be akkor lathato kulonben meg nem)
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility ListRecordsViewUserVisibility // A rogzitesek listaban a User oszlop lathatosaganak a bindolasahoz (ha admin lep be akkor lathato kulonben meg nem)
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility ListRecordsViewContextMenuVisibility // (Delete Header Visibility) Csak sajat Rogzites eseteben jelenik meg jobb klikkre egy torles lehetoseg
        {
            get
            {
                return SelectedRecord.User.Username != LoginViewModel.LoggedUser.Username ? Visibility.Collapsed : Visibility.Visible;
            }
        }


        public RelayCommand CreateRecordCommand { get; private set; }
        public RelayCommand ExportToExcelCommand { get; private set; }
        public RelayCommand SortingByCheckBoxCommand { get; private set; }
        public RelayCommand SearchingCommand { get; private set; }
        public RelayCommand ResetRecordListCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }


        public RecordManagementViewModel(RecordManagementView view)
        {
            LoadTasks(); // Feladatok betoltese(ha esetleg ujat kapnank)
            ResetRecordList(_searchValue); // Rogzitesek frissitese(Lista frissitese/betoltese)

            _view = view;

            CreateRecordCommand = new RelayCommand(CreateRecord, CanExecuteShow);
            ExportToExcelCommand = new RelayCommand(ExportToExcel, CanExportToExcel);
            SortingByCheckBoxCommand = new RelayCommand(SortingByCheckBox, CanExecuteSort);
            SearchingCommand = new RelayCommand(Search, CanExecuteSearch);
            ResetRecordListCommand = new RelayCommand(ResetRecordList, CanExecuteResetList);
            DeleteCommand = new RelayCommand(DeleteRecord, CanDeleteRecord);
        }


        public void LoadRecords() // Rogzitesek betoltese
        {
            RecordList.Clear();

            try
            {
                var records = new RecordRepository(new RecordLogic()).GetUserRecords(LoginViewModel.LoggedUser.IdUser);
                records.ForEach(record =>
                {
                    var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                    recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                    RecordList.Add(recordViewModel);
                });
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void LoadTasks() // Feladatok betoltese
        {
            TaskList.Clear();

            try
            {
                var tasks = new TaskRepository(new TaskLogic()).GetAllTasks();
                tasks.ForEach(task => TaskList.Add(task));
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private bool CanExecuteShow(object arg)
        {
            return true;
        }
        private void CreateRecord(object obj)
        {
            ResetRecordList(obj);  //KELL?????????????
            LoadTasks();

            SelectedRecord = new RecordViewModel(new Record() { Date = DateTime.Today, Duration = 210 },
                TaskList.Where(task => task.User_idUser == LoginViewModel.LoggedUser.IdUser && task.Status.ToString() != "Done").ToList());
            SelectedRecord.RecordCreated += OnRecordCreated; // RecordCreated Event ("Save" gomb megnyomasa) eseten hozzaadodik a listahoz a rogzites es ujat tudsz letrehozni megint
        }
        private void OnRecordCreated(RecordViewModel recordViewModel)
        {
            RecordList.Add(recordViewModel); // hozzaadja a listahoz

            // Megnezem h az uj Datum kesobbi e mint ami a DateTo-ba van es ha igen akkor kicserelem
            if (recordViewModel.Record.Date > _dateTo)
            {
                _dateTo = recordViewModel.Record.Date;
                OnPropertyChanged(nameof(DateTo));  // kell h megvaltozzon a DatePickerben a datum
            }

            // ha az uj Idotartam rovidebb vagy hosszabb mint amik ki vannak irva akkora megfelelot kicsreli
            if (recordViewModel.Record.Duration > _durationTo)
            {
                _durationTo = recordViewModel.Record.Duration;
                OnPropertyChanged(nameof(DurationToFormat)); // kell h a kiiras megvaltozzon
            }
            if (recordViewModel.Record.Duration < _durationFrom)
            {
                _durationFrom = recordViewModel.Record.Duration;
                OnPropertyChanged(nameof(DurationFromFormat)); // kell h a kiiras megvaltozzon
            }

            // Uj letrehozasahoz
            SelectedRecord = new RecordViewModel(new Record() { Date = DateTime.Today, Duration = 210 },
                TaskList.Where(task => task.User_idUser == LoginViewModel.LoggedUser.IdUser && task.Status.ToString() != "Done").ToList());
            SelectedRecord.RecordCreated += OnRecordCreated;
        }


        private bool CanExportToExcel(object arg)
        {
            return true;
        }

        private void ExportToExcel(object sender) // http://www.nullskull.com/a/10476796/xaml-datagrid-export-data-to-excel-using-mvvm-design-pattern.aspx + Right Click on the Project name -> Click "Add reference" -> Goto "COM" tab -> Search for "Microsoft Excel Object Library" click "OK" to add the reference.
        {
            DataGrid currentGrid = _view.ListRecordsContent.ListRecordsDataGrid;
            if (currentGrid != null)
            {
                StringBuilder sbGridData = new StringBuilder();
                List<string> listColumns = new List<string>();

                List<DataGridColumn> listVisibleDataGridColumns = new List<DataGridColumn>();

                List<string> listHeaders = new List<string>();

                Microsoft.Office.Interop.Excel.Application application = null;

                Microsoft.Office.Interop.Excel.Workbook workbook = null;

                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

                int rowCount = 1;

                int colCount = 1;

                try
                {
                    application = new Microsoft.Office.Interop.Excel.Application();
                    workbook = application.Workbooks.Add(Type.Missing);
                    worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

                    if (currentGrid.HeadersVisibility == DataGridHeadersVisibility.Column || currentGrid.HeadersVisibility == DataGridHeadersVisibility.All)
                    {
                        foreach (DataGridColumn dataGridColumn in currentGrid.Columns.Where(dataGridColumn => dataGridColumn.Visibility == Visibility.Visible))
                        {
                            listVisibleDataGridColumns.Add(dataGridColumn);
                            if (dataGridColumn.Header != null)
                            {
                                listHeaders.Add(dataGridColumn.Header.ToString());
                            }
                            worksheet.Cells[rowCount, colCount] = dataGridColumn.Header;
                            colCount++;
                        }

                        // IEnumerable collection = currentGrid.ItemsSource;

                        foreach (object data in currentGrid.ItemsSource)
                        {
                            listColumns.Clear();

                            colCount = 1;

                            rowCount++;

                            foreach (DataGridColumn dataGridColumn in listVisibleDataGridColumns)
                            {
                                string strValue = string.Empty;
                                Binding objBinding = null;
                                DataGridBoundColumn dataGridBoundColumn = dataGridColumn as DataGridBoundColumn;

                                if (dataGridBoundColumn != null)
                                {
                                    objBinding = dataGridBoundColumn.Binding as Binding;
                                }

                                DataGridTemplateColumn dataGridTemplateColumn = dataGridColumn as DataGridTemplateColumn;

                                if (dataGridTemplateColumn != null)
                                {
                                    // This is a template column...let us see the underlying dependency object

                                    DependencyObject dependencyObject = dataGridTemplateColumn.CellTemplate.LoadContent();

                                    FrameworkElement frameworkElement = dependencyObject as FrameworkElement;
                                    if (frameworkElement != null)
                                    {
                                        FieldInfo fieldInfo = frameworkElement.GetType().GetField("ContentProperty", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                                        if (fieldInfo == null)
                                        {
                                            if (frameworkElement is TextBox || frameworkElement is TextBlock || frameworkElement is ComboBox)
                                            {
                                                fieldInfo = frameworkElement.GetType().GetField("TextProperty");
                                            }
                                            else if (frameworkElement is DatePicker)
                                            {
                                                fieldInfo = frameworkElement.GetType().GetField("SelectedDateProperty");
                                            }
                                        }

                                        if (fieldInfo != null)
                                        {
                                            DependencyProperty dependencyProperty = fieldInfo.GetValue(null) as DependencyProperty;
                                            if (dependencyProperty != null)
                                            {
                                                BindingExpression bindingExpression = frameworkElement.GetBindingExpression(dependencyProperty);
                                                if (bindingExpression != null)
                                                {
                                                    objBinding = bindingExpression.ParentBinding;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (objBinding != null)
                                {

                                    if (!String.IsNullOrEmpty(objBinding.Path.Path))
                                    {

                                        PropertyInfo pi = data.GetType().GetProperty(objBinding.Path.Path);

                                        if (pi != null)
                                        {

                                            object propValue = pi.GetValue(data, null);

                                            if (propValue != null)
                                            {
                                                strValue = Convert.ToString(propValue);
                                            }

                                            else
                                            {
                                                strValue = string.Empty;
                                            }
                                        }
                                    }

                                    if (objBinding.Converter != null)
                                    {
                                        if (!String.IsNullOrEmpty(strValue))
                                        {
                                            strValue = objBinding.Converter.Convert(strValue, typeof(string), objBinding.ConverterParameter, objBinding.ConverterCulture).ToString();
                                        }

                                        else
                                        {
                                            strValue = objBinding.Converter.Convert(data, typeof(string), objBinding.ConverterParameter, objBinding.ConverterCulture).ToString();
                                        }
                                    }
                                }

                                listColumns.Add(strValue);

                                worksheet.Cells[rowCount, colCount] = strValue;

                                colCount++;
                            }
                        }
                    }

                }

                catch (System.Runtime.InteropServices.COMException)
                {
                }

                finally
                {
                    workbook.Close();
                    application.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
                }

            }
        }


        private bool CanExecuteSort(object arg)
        {
            return true;
        }
        private void SortingByCheckBox(object obj) // Lista szurese/frissitese
        {

            try
            {
                if (IsMyRecordsCheckBoxChecked)
                {
                    RecordList.Clear();

                    var records = new RecordRepository(new RecordLogic()).GetUserRecords(LoginViewModel.LoggedUser.IdUser);
                    records.ForEach(record =>
                    {
                        var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                        recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                        RecordList.Add(recordViewModel);
                    });
                }
                else
                {
                    RecordList.Clear();

                    var records = new RecordRepository(new RecordLogic()).GetAllRecords();
                    records.ForEach(record =>
                    {
                        var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                        recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                        RecordList.Add(recordViewModel);
                    });
                }

                _durationFrom = 720;
                _durationTo = 0;
                _dateFrom = DateTime.Today;
                if (RecordList.Count == 0) // ha nincs rekord akkor a mai datumot kapja meg
                {
                    _dateTo = DateTime.Today;
                }
                else
                {
                    _dateTo = DateTime.Parse("0001.01.01");
                }
                foreach (RecordViewModel recordViewModel in RecordList) // frissitett listabol kicserelem ha van uj legrovidebb,leghosszabb Idotartam es legkorabbi vagy legkesobbi Datum
                {
                    if (recordViewModel.Record.Date < _dateFrom)
                    {
                        _dateFrom = recordViewModel.Record.Date;
                    }
                    if (recordViewModel.Record.Date > _dateTo)
                    {
                        _dateTo = recordViewModel.Record.Date;
                    }
                    if (recordViewModel.Record.Duration < _durationFrom)
                    {
                        _durationFrom = recordViewModel.Record.Duration;
                    }
                    if (recordViewModel.Record.Duration > _durationTo)
                    {
                        _durationTo = recordViewModel.Record.Duration;
                    }
                }

                OnPropertyChanged(nameof(DateFrom)); // kell h megvaltozzon a DatePickerben a datum
                OnPropertyChanged(nameof(DateTo));  // kell h megvaltozzon a DatePickerben a datum
                OnPropertyChanged(nameof(DurationFromFormat)); // h megvaltozzon a kiiras
                OnPropertyChanged(nameof(DurationToFormat)); // h megvaltozzon a kiiras
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private bool CanExecuteSearch(object arg)
        {
            return true;
        }
        private void Search(object obj) // Lista szurese/frissitese
        {
            try
            {
                if (String.IsNullOrWhiteSpace(_searchValue))
                {
                    if (IsMyRecordsCheckBoxChecked)
                    {
                        RecordList.Clear();

                        var records = new RecordRepository(new RecordLogic()).GetUserRecords(LoginViewModel.LoggedUser.IdUser).Where(record =>
                                        (record.Date >= _dateFrom && record.Date <= _dateTo) && (record.Duration >= _durationFrom && record.Duration <= _durationTo)).ToList();

                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                            recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                            RecordList.Add(recordViewModel);
                        });
                    }
                    else
                    {
                        RecordList.Clear();

                        var records = new RecordRepository(new RecordLogic()).GetAllRecords().Where(record =>
                                        (record.Date >= _dateFrom && record.Date <= _dateTo) && (record.Duration >= _durationFrom && record.Duration <= _durationTo)).ToList();

                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                            recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                            RecordList.Add(recordViewModel);
                        });
                    }
                }
                else
                {
                    if (IsMyRecordsCheckBoxChecked)
                    {
                        RecordList.Clear();

                        var records = new RecordRepository(new RecordLogic()).GetUserRecords(LoginViewModel.LoggedUser.IdUser).Where(record =>
                                        (record.Date >= _dateFrom && record.Date <= _dateTo) && (record.Duration >= _durationFrom && record.Duration <= _durationTo)
                                        && (record.Comment.Contains(_searchValue) || new TaskRepository(new TaskLogic()).GetTaskByID(record.Task_idTask).Title.Contains(_searchValue))).ToList();

                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                            recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                            RecordList.Add(recordViewModel);
                        });
                    }
                    else
                    {
                        RecordList.Clear();

                        var records = new RecordRepository(new RecordLogic()).GetAllRecords().Where(record =>
                                        (record.Date >= _dateFrom && record.Date <= _dateTo) && (record.Duration >= _durationFrom && record.Duration <= _durationTo)
                                        && (record.Comment.Contains(_searchValue) || new UserRepository(new UserLogic()).GetUserByID(record.User_idUser).Username.Contains(_searchValue)
                                        || new TaskRepository(new TaskLogic()).GetTaskByID(record.Task_idTask).Title.Contains(_searchValue))).ToList();

                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                            recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                            RecordList.Add(recordViewModel);
                        });
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private bool CanExecuteResetList(object arg)
        {
            return true;
        }
        private void ResetRecordList(object obj)
        {
            LoadRecords(); // Rogzitesek betoltese

            // A keresesi szoveget uresse teszem
            _searchValue = "";
            OnPropertyChanged(nameof(SearchValue));

            SortingByCheckBox(obj); // Lista frissitese/szurese
        }


        private bool CanDeleteRecord(object arg)
        {
            return _selectedRecord != null && SelectedRecord.User.Username == LoginViewModel.LoggedUser.Username;
        }

        private void DeleteRecord(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Resources.RecordDeleteQuestion, Resources.Warning, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    new RecordRepository(new RecordLogic()).DeleteRecord(SelectedRecord.IdRecord);

                    ResetRecordList(obj); // Frissiti a listat torles eseten
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
