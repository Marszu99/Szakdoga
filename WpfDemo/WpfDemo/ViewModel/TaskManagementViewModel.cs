using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
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
    public class TaskManagementViewModel : ViewModelBase
    {
        public ObservableCollection<User> UserList { get; } = new ObservableCollection<User>();
        public ObservableCollection<TaskViewModel> TaskList { get; } = new ObservableCollection<TaskViewModel>();

        public TaskManagementView _view;
        private int _lastSelectedTaskID = 0; // utoljara valasztott Task ID-ja
        private int _lastSelectedTaskCount = 0; // utoljara valasztott elem indexe a TaskList-bol
        private bool _isLanguageEnglish = true;


        private TaskViewModel _selectedTask;
        public TaskViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;

                if (_selectedTask != null)
                {
                    SelectedTask.TaskUpdated += OnTaskUpdated; // ha tortent modositas a kivalasztott Feladatnal akkor megnezem ezzel h a modositott ertek(ek) kozul az uj Hatarido kicsereli-e a keresesi Hataridot

                    if (_lastSelectedTaskID != 0 && _lastSelectedTaskID != _selectedTask.Task.IdTask) // ha nem az utoljara valasztott Task id-ja nem egyezik a mostanival
                    {
                        for (int i = 0; i < TaskList.Count(); i++)
                        {
                            if (TaskList[i].IdTask == _lastSelectedTaskID) // ha egyezik az utoljara valasztott Task Id-ja a Listaban talalhato i. IdTask-dal es lementem a megfelelo indexet
                            {
                                _lastSelectedTaskCount = i;
                            }
                        }

                        if (TaskList[_lastSelectedTaskCount].IsSelectedTaskValuesChanged) // visszatoltom az eredeti adatokat ha tortent valtozas
                        {
                            try
                            {
                                Task SelectedTaskValues = new TaskRepository(new TaskLogic()).GetTaskByID(_lastSelectedTaskID);
                                TaskList[_lastSelectedTaskCount].Title = SelectedTaskValues.Title;
                                TaskList[_lastSelectedTaskCount].Description = SelectedTaskValues.Description;
                                TaskList[_lastSelectedTaskCount].Deadline = SelectedTaskValues.Deadline;
                                TaskList[_lastSelectedTaskCount].Status = SelectedTaskValues.Status;

                                TaskList[_lastSelectedTaskCount].IsChangedTaskValuesToFalse(); // igy a Save gomb disabled lesz ha visszakattintok
                            }
                            catch (SqlException)
                            {
                                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    _lastSelectedTaskID = _selectedTask.Task.IdTask;
                }

                OnPropertyChanged(nameof(SelectedTask));
                OnPropertyChanged(nameof(SelectedTaskVisibility));
            }
        }
        private void OnTaskUpdated(TaskViewModel taskViewModel)
        {
            ResetDeadlineSearchingValues(); // kicserelem ha a modositott Task Hatarideje a legkorabbi vagy legkesobbi Hatarido
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

        private DateTime _deadlineFrom = DateTime.Today.AddYears(100);
        private DateTime _deadlineFromLowest;
        public DateTime DeadlineFrom
        {
            get
            {
                return _deadlineFrom;
            }
            set
            {
                _deadlineFrom = value;
                OnPropertyChanged(nameof(DeadlineFrom));
            }
        }

        private DateTime _deadlineTo = DateTime.Parse("0001.01.01");
        private DateTime _deadlineToHighest;
        public DateTime DeadlineTo
        {
            get
            {
                return _deadlineTo;
            }
            set
            {
                _deadlineTo = value;
                OnPropertyChanged(nameof(DeadlineTo));
            }
        }


        private bool _isMyTasksCheckBoxChecked = true;
        public bool IsMyTasksCheckBoxChecked // Sajat feladatokat mutato checkbox pipajahoz a bindolashoz
        {
            get
            {
                return _isMyTasksCheckBoxChecked;
            }
            set
            {
                _isMyTasksCheckBoxChecked = value;
                OnPropertyChanged(nameof(IsMyTasksCheckBoxChecked));
            }
        }


        private bool _isActiveTasksCheckBoxChecked = true;
        public bool IsActiveTasksCheckBoxChecked // aktiv feladatokat mutato checkbox pipajahoz a bindolashoz
        {
            get
            {
                return _isActiveTasksCheckBoxChecked;
            }
            set
            {
                _isActiveTasksCheckBoxChecked = value;
                OnPropertyChanged(nameof(IsActiveTasksCheckBoxChecked));
            }
        }


        private bool _isNotificationsCheckBoxChecked = true;
        public bool IsNotificationsCheckBoxChecked // ertesiteseket mutato checkbox pipajahoz a bindolashoz
        {
            get
            {
                return _isNotificationsCheckBoxChecked;
            }
            set
            {
                _isNotificationsCheckBoxChecked = value;
                OnPropertyChanged(nameof(IsNotificationsCheckBoxChecked));
            }
        }


        public Visibility SelectedTaskVisibility // Kivalasztott feladat lathatosaga
        {
            get
            {
                if (SelectedTask != null)
                {
                    SelectedTask.TaskCanceled += OnTaskCanceled; // TaskCanceled Event("Cancel" gomb megnyomasa) eseten a kivalasztott feladat eltunik
                }
                return SelectedTask == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        private void OnTaskCanceled(Object obj)
        {
            if (SelectedTask != null) // Ha 2x megnyitottam ugyanazt egymas utan akkor valamiert lefutt tobbszor ezert van ez az if
            {
                if (SelectedTask.IsSelectedTaskValuesChanged) // Cancel eseten visszatoltom az eredti adatokat ha megvaltoztattuk oket
                {
                    try
                    {
                        Task SelectedTaskValues = new TaskRepository(new TaskLogic()).GetTaskByID(SelectedTask.IdTask);
                        SelectedTask.Title = SelectedTaskValues.Title;
                        SelectedTask.Description = SelectedTaskValues.Description;
                        SelectedTask.Deadline = SelectedTaskValues.Deadline;
                        SelectedTask.Status = SelectedTaskValues.Status;
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

                SelectedTask = null; // null-ozom h a SelectedTask Visibility-je Collapsed legyen
            }
        }

        public Visibility NewTaskButtonVisibility // uj feladat letrehozasa gomb lathatosaganak bindaloasahoz (ha admin lep be akkor lathato kulonben meg nem)
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility TaskCheckBoxAndTextVisibility // Sajat feladatokat mutato checkbox es szoveg lathatosaganak a bindolasahoz (ha admin lep be akkor lathato kulonben meg nem)
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility ListTasksViewUserVisibility // A feladatok listaban a User oszlop lathatosaganak a bindolasahoz (ha admin lep be akkor lathato kulonben meg nem)
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility ListTasksViewDeleteHeaderVisibility // Csak admin eseteben jelenik meg jobb klikkre egy torles lehetoseg
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }


        public RelayCommand CreateTaskCommand { get; private set; }
        public RelayCommand ExportToExcelCommand { get; private set; }
        public RelayCommand SortingByCheckBoxCommand { get; private set; }
        public RelayCommand SearchingCommand { get; private set; }
        public RelayCommand ResetTaskListCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand SpentTimeWithCommand { get; private set; }
        public RelayCommand HasReadCommand { get; private set; }
        public RelayCommand NotificationsSwitchOnOffCommand { get; private set; }



        public TaskManagementViewModel(TaskManagementView view)
        {
            LoadUsers(); // Felhasznalok betoltese(ha esetleg ujat hozna letre)
            ResetTaskList(_searchValue); // Feladatok frissitese(Lista frissitese/betoltese)

            _view = view;

            CreateTaskCommand = new RelayCommand(CreateTask, CanExecuteShow);
            ExportToExcelCommand = new RelayCommand(ExportToExcel, CanExportToExcel);
            SortingByCheckBoxCommand = new RelayCommand(SortingByCheckBox, CanExecuteSort);
            SearchingCommand = new RelayCommand(Search, CanExecuteSearch);
            ResetTaskListCommand = new RelayCommand(ResetTaskList, CanExecuteReset);
            DeleteCommand = new RelayCommand(DeleteTask, CanExecuteDeleteTask);
            SpentTimeWithCommand = new RelayCommand(CalculateDurationForTask, CanExecuteCalculateDurationForTask);
            HasReadCommand = new RelayCommand(HasRead, CanExecuteReadTaskNotifications);
            NotificationsSwitchOnOffCommand = new RelayCommand(NotificationSwitchOnOff, CanExecuteSwitch);
        }


        public void LoadTasks() // Feladatok betoltese
        {
            TaskList.Clear();

            try
            {
                var tasks = new TaskRepository(new TaskLogic()).GetAllTasks();

                tasks.ForEach(task =>
                {
                    var taskViewModel = new TaskViewModel(task, UserList.ToList());
                    taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                    TaskList.Add(taskViewModel);
                });
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void LoadUsers() // Felhasznalok betoltese
        {
            UserList.Clear();

            try
            {
                var users = new UserRepository(new UserLogic()).GetAllUsers();
                users.ForEach(user => UserList.Add(user));
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
        private void CreateTask(object obj)
        {
            LoadUsers();

            SelectedTask = new TaskViewModel(new Task() { Deadline = DateTime.Today.AddDays(1) }, UserList.ToList());
            SelectedTask.TaskCreated += OnTaskCreated; // TaskCreated Event ("Save" gomb megnyomasa) eseten hozzaadodik a listahoz a feladat es ujat tudsz letrehozni megint
        }
        private void OnTaskCreated(TaskViewModel taskViewModel)
        {
            /* Kiszedtem mert igy ez a 2 hiba megoldodik: Letrehozas utan nem lehet rakattintani vagy torolni es Create utan nem frissitem a listat es torlom az ujonnan letrehozottat majd a create gombra kattintok
            TaskList.Add(taskViewModel); // hozzaadja a listahoz*/

            // Iddeiglenesen iderakom mert nem tudom mashogy megoldani
            if (!String.IsNullOrWhiteSpace(_searchValue) || _deadlineFromLowest != _deadlineFrom || _deadlineToHighest != _deadlineTo) // ha esetleg elotte keresett vmire igy az a kereses/szures megmarad
            {
                Search(taskViewModel);
            }
            else
            {
                ResetTaskList(taskViewModel); // frissitem a listat es a frissitett listabol kicserelem ha az uj Task Hatarideje a legkorabbi vagy legkesobbi Hatarido
            }

            // Uj letrehozasahoz
            SelectedTask = new TaskViewModel(new Task() { Deadline = DateTime.Today.AddDays(1) }, UserList.ToList());
            SelectedTask.TaskCreated += OnTaskCreated;
        }


        private bool CanExportToExcel(object arg)
        {
            return true;
        }

        private void ExportToExcel(object sender) // http://www.nullskull.com/a/10476796/xaml-datagrid-export-data-to-excel-using-mvvm-design-pattern.aspx + Right Click on the Project name -> Click "Add reference" -> Goto "COM" tab -> Search for "Microsoft Excel Object Library" click "OK" to add the reference.
        {
            DataGrid currentGrid = _view.ListTasksContent.ListTasksDataGrid;
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
                int SelectedTaskID = 0;
                bool IsSelectedTaskNull = true; // Ha volt elotte kivalasztva Feladat akkor ez segit annak a visszatoltesehez
                if (SelectedTask != null) // ha van valasztott Feladat akkor belep
                {
                    IsSelectedTaskNull = false;
                    SelectedTaskID = SelectedTask.Task.IdTask;
                }

                // betoltom a helyes listat a CheckBoxnak megfeleloen
                if (IsMyTasksCheckBoxChecked && !IsActiveTasksCheckBoxChecked)
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(LoginViewModel.LoggedUser.IdUser);

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task, UserList.ToList());
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else if (IsMyTasksCheckBoxChecked && IsActiveTasksCheckBoxChecked)
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasksFromUser(LoginViewModel.LoggedUser.IdUser);

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task, UserList.ToList());
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else if (!IsMyTasksCheckBoxChecked && IsActiveTasksCheckBoxChecked)
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasks();

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task, UserList.ToList());
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetAllTasks();

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task, UserList.ToList());
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }

                SortTaskListByDeadline(); // Rendezzuk a listat csokkeno sorrendben a Hataridok szerint
                ResetDeadlineSearchingValues(); // Beallitom a listaban levo legkorabbi es legkesobbi Hataridot a kereseshez

                if (!IsSelectedTaskNull) // ha volt valasztott Feladat akkor megkeresem az ID-jat a mostani listaban es ha benne van amostani listaban akkor azt Feladatot visszatoltom
                {
                    for (int i = 0; i < TaskList.Count(); i++)
                    {
                        if (TaskList[i].IdTask == SelectedTaskID)
                        {
                            SelectedTask = TaskList[i];
                        }
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SortTaskListByDeadline() // Rendezzuk a listat csokkeno sorrendben a Hataridok szerint
        {
            var sortedTaskListByDeadline = TaskList.OrderByDescending(task => DateTime.Parse(task.Deadline.ToString()));

            sortedTaskListByDeadline.ToList().ForEach(task =>
            {
                TaskList.Remove(task);
                TaskList.Add(task);
            });
        }

        public void ResetDeadlineSearchingValues() // Beallitom a listaban levo legkorabbi es legkesobbi Hataridot a kereseshez
        {
            _deadlineFrom = DateTime.Today.AddYears(1);
            if (TaskList.Count == 0) // ha nincs task akkor a mai datumot kapja meg
            {
                _deadlineTo = DateTime.Today;
            }
            else
            {
                _deadlineTo = DateTime.Parse("0001.01.01");
            }
            foreach (TaskViewModel taskViewModel in TaskList)
            {
                if (taskViewModel.Task.Deadline < _deadlineFrom)
                {
                    _deadlineFrom = taskViewModel.Task.Deadline;
                    _deadlineFromLowest = _deadlineFrom;
                }
                if (taskViewModel.Task.Deadline > _deadlineTo)
                {
                    _deadlineTo = taskViewModel.Task.Deadline;
                    _deadlineToHighest = _deadlineTo;
                }
            }

            OnPropertyChanged(nameof(DeadlineFrom)); // kell h megvaltozzon a DatePickerben a datum
            OnPropertyChanged(nameof(DeadlineTo));  // kell h megvaltozzon a DatePickerben a datum
        }


        private bool CanExecuteSearch(object arg)
        {
            // Ezzel vizsgalom azt hogyha nyelvet valtottunk akkor a lista frissuljon es igy megvaltozik a listaban levo Statuszok es Ertesitesek nyelve a megfelelore
            int SelectedTaskCount = 0;
            bool IsSelectedTaskNull = true; // Ha volt elotte kivalasztva Feladat akkor ez segit annak a visszatoltesehez

            if (_isLanguageEnglish && _view.ShowingMyTasksTextBlock.Text.ToString() == "Saját") // Ha a nyelv Angol de a kiiras "Sajat" akkor belepek
            {
                _isLanguageEnglish = false; // megvaltoztatom a bool-t h a nyelv Magyar

                if (!String.IsNullOrWhiteSpace(_searchValue) || _deadlineFromLowest != _deadlineFrom || _deadlineToHighest != _deadlineTo) // ha esetleg elotte keresett vmire igy az a kereses/szures megmarad
                {
                    if (SelectedTask != null) // ha van valasztott Feladat akkor belep
                    {
                        IsSelectedTaskNull = false;

                        for (int i = 0; i < TaskList.Count(); i++) // Megkeresem a valasztott Feladat indexet a listaban
                        {
                            if (TaskList[i].IdTask == SelectedTask.Task.IdTask) // ha egyezik az utoljara valasztott Task Id-ja a Listaban talalhato i. IdTask-dal es lementem a megfelelo indexet
                            {
                                SelectedTaskCount = i;
                            }
                        }
                    }

                    Search(arg); // Keresest hajtok vegre
                }
                else
                {
                    if (SelectedTask != null) // ha van valasztott Feladat akkor belep
                    {
                        IsSelectedTaskNull = false;

                        for (int i = 0; i < TaskList.Count(); i++) // Megkeresem a valasztott Feladat indexet a listaban
                        {
                            if (TaskList[i].IdTask == SelectedTask.Task.IdTask) // ha egyezik az utoljara valasztott Task Id-ja a Listaban talalhato i. IdTask-dal es lementem a megfelelo indexet
                            {
                                SelectedTaskCount = i;
                            }
                        }
                    }

                    ResetTaskList(arg); // Frissitem a listat
                }

                if (!IsSelectedTaskNull) // ha elotte volt kivalasztott Feladat akkor Visszamentem a kivalasztott Feladatot mert frissiteskor az eltunik(nullozodik)
                {
                    SelectedTask = TaskList[SelectedTaskCount];
                }
            }

            if (!_isLanguageEnglish && _view.ShowingMyTasksTextBlock.Text.ToString() == "My") // Ha a nyelv Magyar de a kiiras "My" akkor belepek
            {
                _isLanguageEnglish = true; // megvaltoztatom a bool-t h a nyelv Angol

                if (!String.IsNullOrWhiteSpace(_searchValue) || _deadlineFromLowest != _deadlineFrom || _deadlineToHighest != _deadlineTo) // ha esetleg elotte keresett vmire igy az a kereses/szures megmarad
                {
                    if (SelectedTask != null) // ha van valasztott Feladat akkor belep
                    {
                        IsSelectedTaskNull = false;

                        for (int i = 0; i < TaskList.Count(); i++) // Megkeresem a valasztott Feladat indexet a listaban
                        {
                            if (TaskList[i].IdTask == SelectedTask.Task.IdTask) // ha egyezik az utoljara valasztott Task Id-ja a Listaban talalhato i. IdTask-dal es lementem a megfelelo indexet
                            {
                                SelectedTaskCount = i;
                            }
                        }
                    }

                    Search(arg); // Keresest hajtok vegre
                }
                else
                {
                    if (SelectedTask != null) // ha van valasztott Feladat akkor belep
                    {
                        IsSelectedTaskNull = false;

                        for (int i = 0; i < TaskList.Count(); i++) // Megkeresem a valasztott Feladat indexet a listaban
                        {
                            if (TaskList[i].IdTask == SelectedTask.Task.IdTask) // ha egyezik az utoljara valasztott Task Id-ja a Listaban talalhato i. IdTask-dal es lementem a megfelelo indexet
                            {
                                SelectedTaskCount = i;
                            }
                        }
                    }

                    ResetTaskList(arg); // Frissitem a listat
                }

                if (!IsSelectedTaskNull) // ha elotte volt kivalasztott Feladat akkor Visszamentem a kivalasztott Feladatot mert frissiteskor az eltunik(nullozodik)
                {
                    SelectedTask = TaskList[SelectedTaskCount];
                }
            }

            return true;
        }
        private void Search(object obj) // Lista szurese/frissitese
        {
            try
            {
                if (String.IsNullOrWhiteSpace(_searchValue))
                {
                    if (IsMyTasksCheckBoxChecked && !IsActiveTasksCheckBoxChecked)
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(LoginViewModel.LoggedUser.IdUser).Where(task =>
                                    task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    else if (IsMyTasksCheckBoxChecked && IsActiveTasksCheckBoxChecked)
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasksFromUser(LoginViewModel.LoggedUser.IdUser).Where(task =>
                                    task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    else if (!IsMyTasksCheckBoxChecked && IsActiveTasksCheckBoxChecked)
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasks().Where(task =>
                                    task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    else
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetAllTasks().Where(task =>
                                    task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                }
                else
                {
                    if (IsMyTasksCheckBoxChecked && !IsActiveTasksCheckBoxChecked)
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(LoginViewModel.LoggedUser.IdUser).Where(task =>
                                    (task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo) 
                                    && (task.Title.Contains(_searchValue) || task.Description.Contains(_searchValue) 
                                    || ResourceHandler.GetResourceString(task.Status.ToString()).Contains(_searchValue))).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    else if (IsMyTasksCheckBoxChecked && IsActiveTasksCheckBoxChecked)
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasksFromUser(LoginViewModel.LoggedUser.IdUser).Where(task =>
                                    (task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo)
                                    && (task.Title.Contains(_searchValue) || task.Description.Contains(_searchValue) 
                                    || ResourceHandler.GetResourceString(task.Status.ToString()).Contains(_searchValue))).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    else if (!IsMyTasksCheckBoxChecked && IsActiveTasksCheckBoxChecked)
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasks().Where(task =>
                                    (task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo)
                                    && (task.Title.Contains(_searchValue) || task.Description.Contains(_searchValue) 
                                    || ResourceHandler.GetResourceString(task.Status.ToString()).Contains(_searchValue)
                                    || new UserRepository(new UserLogic()).GetUserByID(task.User_idUser).Username.Contains(_searchValue))).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    else
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetAllTasks().Where(task =>
                                    (task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo)
                                    && (task.Title.Contains(_searchValue) || task.Description.Contains(_searchValue) 
                                    || ResourceHandler.GetResourceString(task.Status.ToString()).Contains(_searchValue)
                                    || new UserRepository(new UserLogic()).GetUserByID(task.User_idUser).Username.Contains(_searchValue))).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                }

                SortTaskListByDeadline(); // Rendezzuk a listat csokkeno sorrendben a Hataridok szerint
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private bool CanExecuteReset(object arg)
        {
            return true;
        }
        private void ResetTaskList(object obj)
        {
            LoadTasks(); // Feladatok betoltese

            // A keresesi szoveget uresse teszem
            _searchValue = "";
            OnPropertyChanged(nameof(SearchValue));

            SortingByCheckBox(obj); // Lista frissitese/szurese
        }


        private bool CanExecuteDeleteTask(object arg)
        {
            return _selectedTask != null && LoginViewModel.LoggedUser.Status != 0;
        }

        private void DeleteTask(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Resources.TaskDeleteQuestion1 + SelectedTask.Title + Resources.TaskDeleteQuestion2,
                                                Resources.Warning, MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    new TaskRepository(new TaskLogic()).DeleteTask(SelectedTask.IdTask);

                    if(this.SelectedTask.User.Username != LoginViewModel.LoggedUser.Username) // ha a User nem admin(Miutan erre csak az Admin kepes)
                    {
                        SendNotificationEmail(SelectedTask.Title); // kuld emailt h toroltek a feladatat
                    }

                    if (!String.IsNullOrWhiteSpace(_searchValue) || _deadlineFromLowest != _deadlineFrom || _deadlineToHighest != _deadlineTo) // ha esetleg elotte keresett vmire igy az a kereses/szures megmarad
                    {
                        Search(obj);
                    }
                    else
                    {
                        ResetTaskList(obj); // Frissitem a listat
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        private bool CanExecuteCalculateDurationForTask(object arg)
        {
            return _selectedTask != null;
        }
        private void CalculateDurationForTask(object obj) // Kiszamitja a feladattal foglalkozott/eltoltott idot
        {
            int spentTime = 0;

            foreach(Record record in new RecordRepository(new RecordLogic()).GetTaskRecords(SelectedTask.IdTask)) // a kivalasztott feladat rogziteseinek az idotartamat osszeadjuk
            {
                spentTime += record.Duration;
            }

            MessageBox.Show(Resources.SpentTimeMessage1 + SelectedTask.Title + Resources.SpentTimeMessage2 + TimeSpan.FromMinutes(spentTime).ToString("hh':'mm"), Resources.Information);
        }


        private bool CanExecuteReadTaskNotifications(object arg)
        {
            if (TaskViewModel.IsNotificationsOn) // ha lathatoak az ertesitesek
            {
                if (LoginViewModel.LoggedUser.Status == 1) // megkapja az Adminhoz tartozo ertesiteseket a megfelelo feladathoz
                {
                    return _selectedTask != null && new NotificationRepository(new NotificationLogic()).GetTaskNotificationsForAdmin(SelectedTask.IdTask).Count > 0;
                }
                else // megkapja a sima Userhez tartozo ertesiteseket a megfelelo feladathoz
                {
                    return _selectedTask != null && new NotificationRepository(new NotificationLogic()).GetTaskNotificationsForEmployee(SelectedTask.IdTask).Count > 0;
                }
            }
            else
            {
                return false;
            }
        }

        private void HasRead(object obj) // elolvassa az ertesitest
        {
            try
            {
                new NotificationRepository(new NotificationLogic()).HasReadNotification(SelectedTask.IdTask, LoginViewModel.LoggedUser.Status);

                int SelectedTaskCount = 0;
                for (int i = 0; i < TaskList.Count(); i++) // Megkeresem a valasztott Feladat indexet a listaban
                {
                    if (TaskList[i].IdTask == SelectedTask.Task.IdTask) // ha egyezik az utoljara valasztott Task Id-ja a Listaban talalhato i. IdTask-dal es lementem a megfelelo indexet
                    {
                        SelectedTaskCount = i;
                    }
                }

                ResetTaskList(obj); // frissiti a listat h eltunjon az ertesites

                SelectedTask = TaskList[SelectedTaskCount];
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CanExecuteSwitch(object arg)
        {
            return true;
        }
        private void NotificationSwitchOnOff(object obj)
        {
            int SelectedTaskCount = 0;
            bool IsSelectedTaskNull = true; // Ha volt elotte kivalasztva Feladat akkor ez segit annak a visszatoltesehez

            if (SelectedTask != null) // ha van valasztott Feladat akkor belep
            {
                IsSelectedTaskNull = false;

                for (int i = 0; i < TaskList.Count(); i++) // Megkeresem a valasztott Feladat indexet a listaban
                {
                    if (TaskList[i].IdTask == SelectedTask.Task.IdTask) // ha egyezik az utoljara valasztott Task Id-ja a Listaban talalhato i. IdTask-dal es lementem a megfelelo indexet
                    {
                        SelectedTaskCount = i;
                    }
                }
            }

            if (IsNotificationsCheckBoxChecked) // ha ki van pipalva a CheckBox
            {
                TaskViewModel.IsNotificationsOn = true; // ertesitesek be vannak kapcsolva

                if (!String.IsNullOrWhiteSpace(_searchValue) || _deadlineFromLowest != _deadlineFrom || _deadlineToHighest != _deadlineTo) // ha esetleg elotte keresett vmire igy az a kereses/szures megmarad
                {
                    Search(obj);
                }
                else
                {
                    ResetTaskList(obj); // Frissitem a listat
                }
            }
            else
            {
                TaskViewModel.IsNotificationsOn = false; // ertesitesek ki vannak kapcsolva

                if (!String.IsNullOrWhiteSpace(_searchValue) || _deadlineFromLowest != _deadlineFrom || _deadlineToHighest != _deadlineTo) // ha esetleg elotte keresett vmire igy az a kereses/szures megmarad
                {
                    Search(obj);
                }
                else
                {
                    ResetTaskList(obj); // Frissitem a listat
                }
            }

            if (!IsSelectedTaskNull) // ha elotte volt kivalasztott Feladat akkor Visszamentem a kivalasztott Feladatot mert frissiteskor az eltunik(nullozodik)
            {
                SelectedTask = TaskList[SelectedTaskCount];
            }
        }


        private void SendNotificationEmail(string TaskTitle)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            //client.Timeout = 10;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("wpfszakdoga@gmail.com", "Marszu99");
            string EmailSubject = "Task Notification";
            string EmailMessage = TaskTitle + " has been deleted!";
            MailMessage mm = new MailMessage("wpfszakdoga@gmail.com", SelectedTask.User.Email, EmailSubject, EmailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }
    }
}
