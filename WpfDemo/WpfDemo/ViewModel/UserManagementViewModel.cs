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
    public class UserManagementViewModel : ViewModelBase
    {
        public ObservableCollection<UserViewModel> UserList { get; } = new ObservableCollection<UserViewModel>();
        public UserManagementView _view;


        private UserViewModel _selectedUser;
        public UserViewModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                OnPropertyChanged(nameof(SelectedUserVisibility));
                OnPropertyChanged(nameof(ListUsersViewContextMenuVisibility));
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


        public Visibility SelectedUserVisibility // Kivalasztott felhasznalo lathatosaga
        {
            get
            {
                if (SelectedUser != null)
                {
                    SelectedUser.UserCanceled += OnUserCanceled; // UserCanceled Event("Cancel" gomb megnyomasa) eseten a kivalasztott felhasznalo eltunik

                }
                return SelectedUser == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        private void OnUserCanceled(Object obj)
        {
            SelectedUser = null;
        }

        public Visibility ListUsersViewContextMenuVisibility // (Delete Header Visibility) Csak Admin eseteben jelenik meg jobb klikkre egy torles lehetoseg
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 || SelectedUser.Username == LoginViewModel.LoggedUser.Username ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility UserManagementButtonsVisibility // Uj Felhasznalo letrehozasa gomb lathatossaga(Admin eseteben lathato)
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }


        public RelayCommand CreateUserCommand { get; private set; }
        public RelayCommand ExportToExcelCommand { get; private set; }
        public RelayCommand SearchingCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand ShowUserProfilCommand { get; private set; }

        public UserManagementViewModel(UserManagementView view)
        {
            LoadUsers(); // Felhasznalok betoltese

            _view = view;

            CreateUserCommand = new RelayCommand(CreateUser, CanExecuteShow);
            ExportToExcelCommand = new RelayCommand(ExportToExcel, CanExportToExcel);
            SearchingCommand = new RelayCommand(Search, CanExecuteSearch);
            DeleteCommand = new RelayCommand(DeleteUser, CanDeleteUser);
            ShowUserProfilCommand = new RelayCommand(ShowUserProfil, CanShowUserProfil);
        }


        private bool CanExecuteShow(object arg)
        {
            return true;
        }
        private void CreateUser(object obj)
        {
            SelectedUser = new UserViewModel(new User());
            SelectedUser.UserCreated += OnUserCreated; // UserCreated Event ("Save" gomb megnyomasa) eseten hozzaadodik a listahoz a felhasznalo es ujat tudsz letrehozni megint
        }
        private void OnUserCreated(UserViewModel userViewModel)
        {
            UserList.Add(userViewModel);// hozzaadja a listahoz

            // Uj letrehozasahoz
            SelectedUser = new UserViewModel(new User());
            SelectedUser.UserCreated += OnUserCreated;
        }


        private bool CanExportToExcel(object arg)
        {
            return true;
        }

        private void ExportToExcel(object sender) // http://www.nullskull.com/a/10476796/xaml-datagrid-export-data-to-excel-using-mvvm-design-pattern.aspx + Right Click on the Project name -> Click "Add reference" -> Goto "COM" tab -> Search for "Microsoft Excel Object Library" click "OK" to add the reference.
        {
            DataGrid currentGrid = _view.ListUsersContent.ListUsersDataGrid;
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


        private bool CanExecuteSearch(object arg)
        {
            return true;
        }
        private void Search(object obj) // Lista szurese
        {
            try
            {
                UserList.Clear();

                var users = new UserRepository(new UserLogic()).GetAllUsers().Where(user => user.Username.Contains(_searchValue)
                            || user.FirstName.Contains(_searchValue) || user.LastName.Contains(_searchValue)).ToList();

                users.ForEach(user => UserList.Add(new UserViewModel(user)));
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void LoadUsers()
        {
            try
            {
                UserList.Clear();

                var users = new UserRepository(new UserLogic()).GetAllUsers();
                users.ForEach(user => UserList.Add(new UserViewModel(user)));
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private bool CanDeleteUser(object arg)
        {
            return _selectedUser != null && SelectedUser.Username != LoginViewModel.LoggedUser.Username && LoginViewModel.LoggedUser.Status != 0;
        }
        private void DeleteUser(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Resources.UserDeleteQuestion1 + SelectedUser.Username + Resources.UserDeleteQuestion2, 
                                                Resources.Warning, MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    new UserRepository(new UserLogic()).DeleteUser(SelectedUser.IdUser, SelectedUser.Status);

                    LoadUsers();
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        private bool CanShowUserProfil(object arg)
        {
            return _selectedUser != null;
        }
        private void ShowUserProfil(object obj) // Dupla klikk eseten a UserProfileView-t megnyitja
        {
            UserProfileView Ipage = new UserProfileView(SelectedUser.IdUser);
            (Ipage.DataContext as UserProfileViewModel).CurrentUser = SelectedUser.User;
            Ipage.ShowDialog();
        }
    }
}
