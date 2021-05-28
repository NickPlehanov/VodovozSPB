using System;
using System.Collections.ObjectModel;
using System.Linq;
using VodovozSPB.Helpers;
using VodovozSPB.Models;
using VodovozSPB.Data;
using Microsoft.EntityFrameworkCore;
using Notifications.Wpf;

namespace VodovozSPB {
    class MainWindowViewModel : BaseViewModel {
        NotificationManager notificationManager;
        public MainWindowViewModel() {
            GetEmployees.Execute(null);
            GetDepartments.Execute(null);
            GetOrders.Execute(null);

            notificationManager = new NotificationManager();
            EmployeeBirthday = DateTime.Now;
            OrderDate = DateTime.Now;
            GendersEnum = Enum.GetValues(typeof(Genders));
            IsNew = true;
        }
        /// <summary>
        /// Коллекция для хранения списка сотрудников
        /// </summary>
        private ObservableCollection<Employees> _EmployeesList;
        public ObservableCollection<Employees> EmployeesList {
            get => _EmployeesList;
            set {
                _EmployeesList = value;
                OnPropertyChanged(nameof(EmployeesList));
            }
        }
        /// <summary>
        /// Коллекция для хранения перечисления полов
        /// </summary>
        private Array _GendersEnum;
        public Array GendersEnum {
            get => _GendersEnum;
            set {
                _GendersEnum = value;
                OnPropertyChanged(nameof(GendersEnum));
            }
        }
        /// <summary>
        /// Свойство хранящее название отдела при добавлении или редактировании отдела
        /// </summary>
        private string _DepartmentName;
        public string DepartmentName {
            get => _DepartmentName;
            set {
                _DepartmentName = value;
                OnPropertyChanged(nameof(DepartmentName));
            }
        }
        /// <summary>
        /// Свойство хранящее выбранный пол в окне добавления/редактирования сотрудников
        /// </summary>
        private string _SelectedGender;
        public string SelectedGender {
            get => _SelectedGender;
            set {
                _SelectedGender = value;
                OnPropertyChanged(nameof(SelectedGender));
            }
        }
        /// <summary>
        /// Управляет видимостью плавающей области для добавления/редактирования отделов
        /// </summary>
        private bool _IsOpenFlyoutDepartment;
        public bool IsOpenFlyoutDepartment {
            get => _IsOpenFlyoutDepartment;
            set {
                _IsOpenFlyoutDepartment = value;
                OnPropertyChanged(nameof(IsOpenFlyoutDepartment));
            }
        }
        /// <summary>
        /// Управляет видимостью плавающей области для добавления/редактирования сотрудников
        /// </summary>
        private bool _IsOpenFlyoutEmployee;
        public bool IsOpenFlyoutEmployee {
            get => _IsOpenFlyoutEmployee;
            set {
                _IsOpenFlyoutEmployee = value;
                OnPropertyChanged(nameof(IsOpenFlyoutEmployee));
            }
        }
        /// <summary>
        /// Управляет видимостью плавающей области для добавления/редактирования заказов
        /// </summary>
        private bool _IsOpenFlyoutOrder;
        public bool IsOpenFlyoutOrder {
            get => _IsOpenFlyoutOrder;
            set {
                _IsOpenFlyoutOrder = value;
                OnPropertyChanged(nameof(IsOpenFlyoutOrder));
            }
        }
        /// <summary>
        /// Наименование партнера
        /// </summary>
        private string _PartnerName;
        public string PartnerName {
            get => _PartnerName;
            set {
                _PartnerName = value;
                OnPropertyChanged(nameof(PartnerName));
            }
        }
        /// <summary>
        /// Дата заказа
        /// </summary>
        private DateTime _OrderDate;
        public DateTime OrderDate {
            get => _OrderDate;
            set {
                _OrderDate = value;
                OnPropertyChanged(nameof(OrderDate));
            }
        }
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        private string _EmployeeLastName;
        public string EmployeeLastName {
            get => _EmployeeLastName;
            set {
                _EmployeeLastName = value;
                OnPropertyChanged(nameof(EmployeeLastName));
            }
        }
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        private string _EmployeeFirstName;
        public string EmployeeFirstName {
            get => _EmployeeFirstName;
            set {
                _EmployeeFirstName = value;
                OnPropertyChanged(nameof(EmployeeFirstName));
            }
        }
        /// <summary>
        /// Отчество сотрудника
        /// </summary>
        private string _EmployeeMiddleName;
        public string EmployeeMiddleName {
            get => _EmployeeMiddleName;
            set {
                _EmployeeMiddleName = value;
                OnPropertyChanged(nameof(EmployeeMiddleName));
            }
        }
        /// <summary>
        /// День рождения сотрудника
        /// </summary>
        private DateTime _EmployeeBirthday;
        public DateTime EmployeeBirthday {
            get => _EmployeeBirthday;
            set {
                _EmployeeBirthday = value;
                OnPropertyChanged(nameof(EmployeeBirthday));
            }
        }
        /// <summary>
        /// Выбранный отдел при добавлении сотрудника
        /// </summary>
        private Departments _SelectedDepartment;
        public Departments SelectedDepartment {
            get => _SelectedDepartment;
            set {
                _SelectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
            }
        }
        /// <summary>
        /// Выбранный отдел в DataGrid
        /// </summary>
        private Departments _SelectedDepartmentByList;
        public Departments SelectedDepartmentByList {
            get => _SelectedDepartmentByList;
            set {
                _SelectedDepartmentByList = value;
                OnPropertyChanged(nameof(SelectedDepartmentByList));
            }
        }
        /// <summary>
        /// Выбранный сотрудник при заполнении заказа или выборе начальника отдела
        /// </summary>
        private Employees _SelectedEmployee;
        public Employees SelectedEmployee {
            get => _SelectedEmployee;
            set {
                _SelectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }
        /// <summary>
        /// Выбранный сотрудник из общего списка сотрудников
        /// </summary>
        private Employees _SelectedEmployeeByList;
        public Employees SelectedEmployeeByList {
            get => _SelectedEmployeeByList;
            set {
                _SelectedEmployeeByList = value;
                OnPropertyChanged(nameof(SelectedEmployeeByList));
            }
        }
        /// <summary>
        /// Выбранный заказ из общего списка заказов
        /// </summary>
        private Orders _SelectedOrderByList;
        public Orders SelectedOrderByList {
            get => _SelectedOrderByList;
            set {
                _SelectedOrderByList = value;
                OnPropertyChanged(nameof(SelectedOrderByList));

            }
        }
        /// <summary>
        /// Флаг определяеющий тип операции Добавление или Редактирование
        /// </summary>
        private bool _IsNew;
        public bool IsNew {
            get => _IsNew;
            set {
                _IsNew = value;
                OnPropertyChanged(nameof(IsNew));
            }
        }

        /// <summary>
        /// Коллекция для хранения списка отделов
        /// </summary>
        private ObservableCollection<Departments> _DepartmentsList;
        public ObservableCollection<Departments> DepartmentsList {
            get => _DepartmentsList;
            set {
                _DepartmentsList = value;
                OnPropertyChanged(nameof(DepartmentsList));
            }
        }
        /// <summary>
        /// Коллеукция для хранения списка заказов 
        /// </summary>
        private ObservableCollection<Orders> _OrdersList;
        public ObservableCollection<Orders> OrdersList {
            get => _OrdersList;
            set {
                _OrdersList = value;
                OnPropertyChanged(nameof(OrdersList));
            }
        }
        /// <summary>
        /// Отображает в окне програмы область для добавления/редактирования сотрудника
        /// obj - принимает параметры Add или Edit из View.
        /// Add - добавление нового экземпляра, предварительно все поля для ввода очищаем
        /// Edit - редактирование выбранного
        /// </summary>
        private RelayCommand _ShowFlyoutEmployee;
        public RelayCommand ShowFlyoutEmployee {
            get => _ShowFlyoutEmployee ??= new RelayCommand(async obj => {
                IsOpenFlyoutEmployee = true;
                if ((obj as string).Equals("Edit")) {
                    IsNew = false;
                    if (SelectedEmployeeByList != null) {
                        EmployeeLastName = SelectedEmployeeByList.LastName;
                        EmployeeFirstName = SelectedEmployeeByList.FirstName;
                        EmployeeMiddleName = SelectedEmployeeByList.MiddleName;
                        EmployeeBirthday = SelectedEmployeeByList.BirthDay;
                        SelectedGender = Enum.GetName(typeof(Genders), (Genders)SelectedEmployeeByList.Gender);
                        SelectedDepartment = DepartmentsList.FirstOrDefault(x => x.Id == SelectedEmployeeByList.Department.Id);
                    }
                }
                else {
                    IsNew = true;
                    SelectedEmployee = null;
                    SelectedDepartment = null;
                    EmployeeLastName = null;
                    EmployeeFirstName = null;
                    EmployeeMiddleName = null;
                    SelectedDepartment = null;
                    SelectedGender = null;
                }
            });
        }
        /// <summary>
        /// Отображает в окне програмы область для добавления/редактирования отдела
        /// obj - принимает параметры Add или Edit из View.
        /// Add - добавление нового экземпляра, предварительно все поля для ввода очищаем
        /// Edit - редактирование выбранного
        /// </summary>
        private RelayCommand _ShowFlyoutDepartment;
        public RelayCommand ShowFlyoutDepartment {
            get => _ShowFlyoutDepartment ??= new RelayCommand(async obj => {
                IsOpenFlyoutDepartment = true;
                if ((obj as string).Equals("Edit")) {
                    IsNew = false;
                    if (SelectedDepartmentByList != null) {
                        DepartmentName = SelectedDepartmentByList.Name;
                        SelectedGender = Enum.GetName(typeof(Genders), (Genders)SelectedEmployeeByList.Gender);
                        SelectedEmployee = EmployeesList.FirstOrDefault(x => x.Id == SelectedDepartmentByList.BossId);
                    }
                }
                else {
                    IsNew = true;
                    DepartmentName = null;
                    SelectedEmployee = null;
                }
            });
        }
        /// <summary>
        /// Отображает в окне програмы область для добавления/редактирования заказа
        /// obj - принимает параметры Add или Edit из View.
        /// Add - добавление нового экземпляра, предварительно все поля для ввода очищаем
        /// Edit - редактирование выбранного
        /// </summary>
        private RelayCommand _ShowFlyoutOrder;
        public RelayCommand ShowFlyoutOrder {
            get => _ShowFlyoutOrder ??= new RelayCommand(async obj => {
                IsOpenFlyoutOrder = true;
                if ((obj as string).Equals("Edit")) {
                    IsNew = false;
                    if (SelectedOrderByList != null) {
                        PartnerName = SelectedOrderByList.Partner;
                        OrderDate = SelectedOrderByList.DateOrder;
                        SelectedEmployee = EmployeesList.FirstOrDefault(x => x.Id == SelectedOrderByList.OwnerId);
                    }
                }
                else {
                    IsNew = true;
                    PartnerName = null;
                    SelectedEmployee = null;
                }
            });
        }
        /// <summary>
        /// Команда взаимодействия с базой данных(сотрудники), определяющая через флаг, тип операции
        /// </summary>
        private RelayCommand _AddNewEmployeeCommand;
        public RelayCommand AddNewEmployeeCommand {
            get => _AddNewEmployeeCommand ??= new RelayCommand(async obj => {
                var context = new VodovozContext();
                if (IsNew) {
                    Employees emp = new() {
                        LastName = EmployeeLastName,
                        FirstName = EmployeeFirstName,
                        MiddleName = EmployeeMiddleName,
                        BirthDay = EmployeeBirthday.Date,
                        Gender = (int)Enum.Parse(typeof(Genders), SelectedGender),
                        DepartmentId = SelectedDepartment.Id
                    };
                    context.Entry<Employees>(emp).State = EntityState.Added;
                }
                else {
                    Employees employee = new VodovozContext().Employees.Find(SelectedEmployeeByList.Id);
                    if (employee != null) {
                        employee.LastName = EmployeeLastName;
                        employee.FirstName = EmployeeFirstName;
                        employee.MiddleName = EmployeeMiddleName;
                        employee.BirthDay = EmployeeBirthday.Date;
                        employee.Gender = (int)Enum.Parse(typeof(Genders), SelectedGender);
                        employee.DepartmentId = SelectedDepartment.Id;
                        context.Entry<Employees>(employee).State = EntityState.Modified;
                    }
                }

                int result = await context.SaveChangesAsync();
                if (result.Equals(1)) {
                    notificationManager.Show(new NotificationContent {
                        Title = "Информация",
                        Message = "Данные сохранены",
                        Type = NotificationType.Success
                    });
                    GetEmployees.Execute(null);
                    IsOpenFlyoutEmployee = false;
                    SelectedEmployee = null;
                    SelectedDepartment = null;
                    EmployeeLastName = null;
                    EmployeeFirstName = null;
                    EmployeeMiddleName = null;
                    SelectedDepartment = null;
                    SelectedGender = null;
                }
                else
                    notificationManager.Show(new NotificationContent {
                        Title = "Информация",
                        Message = "При сохранении произошло ошибка",
                        Type = NotificationType.Error
                    });                
            }, obj => !string.IsNullOrEmpty(EmployeeLastName) && !string.IsNullOrEmpty(EmployeeFirstName) && SelectedGender != null && SelectedDepartment != null);
        }
        /// <summary>
        /// Команда взаимодействия с базой данных (отделы), определяющая через флаг, тип операции
        /// </summary>
        private RelayCommand _AddEditNewDepartmentCommand;
        public RelayCommand AddEditNewDepartmentCommand {
            get => _AddEditNewDepartmentCommand ??= new RelayCommand(async obj => {
                var context = new VodovozContext();
                if (IsNew) {
                    Departments d = new() {
                        Name = DepartmentName,
                        BossId = SelectedEmployee.Id
                    };
                    context.Entry<Departments>(d).State = EntityState.Added;
                }
                else {
                    Departments d = new VodovozContext().Departments.Find(SelectedDepartmentByList.Id);
                    if (d != null) {
                        d.Name = DepartmentName;
                        d.BossId = SelectedEmployee.Id;
                        context.Entry<Departments>(d).State = EntityState.Modified;
                    }
                }


                var result = await context.SaveChangesAsync();
                if (result.Equals(1)) {
                    notificationManager.Show(new NotificationContent {
                        Title = "Информация",
                        Message = "Данные сохранены",
                        Type = NotificationType.Success
                    });
                    GetDepartments.Execute(null);
                    IsOpenFlyoutDepartment = false;
                    DepartmentName = null;
                    SelectedEmployee = null;
                }
                else
                    notificationManager.Show(new NotificationContent {
                        Title = "Информация",
                        Message = "При сохранении произошло ошибка",
                        Type = NotificationType.Error
                    });
            },obj=> !string.IsNullOrEmpty(DepartmentName) && SelectedEmployee!=null);
        }
        /// <summary>
        /// Команда взаимодействия с базой данных (заказы), определяющая через флаг, тип операции
        /// </summary>
        private RelayCommand _AddEditOrderCommand;
        public RelayCommand AddEditOrderCommand {
            get => _AddEditOrderCommand ??= new RelayCommand(async obj => {
                var context = new VodovozContext();
                if (IsNew) {
                //if (SelectedOrderByList == null) {
                    Orders o = new() {
                        Partner = PartnerName,
                        DateOrder = OrderDate,
                        OwnerId = SelectedEmployee.Id
                    };
                    context.Entry<Orders>(o).State = EntityState.Added;
                }
                else {
                    Orders o = new VodovozContext().Orders.Find(SelectedOrderByList.Id);
                    if (o != null) {
                        o.Partner = PartnerName;
                        o.DateOrder = OrderDate;
                        o.OwnerId = SelectedEmployee.Id;
                        context.Entry<Orders>(o).State = EntityState.Modified;
                    }
                }
                var result = await context.SaveChangesAsync();
                if (result.Equals(1)) {
                    notificationManager.Show(new NotificationContent {
                        Title = "Информация",
                        Message = "Данные сохранены",
                        Type = NotificationType.Success
                    });
                    IsOpenFlyoutOrder = false;
                    PartnerName = null;
                    SelectedEmployee = null;
                    GetOrders.Execute(null);
                }
                else
                    notificationManager.Show(new NotificationContent {
                        Title = "Информация",
                        Message = "При сохранении произошло ошибка",
                        Type = NotificationType.Error
                    });
                
            },obj=> !string.IsNullOrEmpty(PartnerName) && SelectedEmployee!=null);
        }
        /// <summary>
        /// Получаем список всех сотрудников
        /// </summary>
        private RelayCommand _GetEmployees;
        public RelayCommand GetEmployees {
            get => _GetEmployees ??= new RelayCommand(async obj => {
                EmployeesList = new ObservableCollection<Employees>(await new VodovozContext().Employees.Include(x => x.Department).AsNoTracking().ToListAsync().ConfigureAwait(false));
            });
        }
        /// <summary>
        /// Получаем список всех отедлов
        /// </summary>
        private RelayCommand _GetDepartments;
        public RelayCommand GetDepartments {
            get => _GetDepartments ??= new RelayCommand(async obj => {
                DepartmentsList = new ObservableCollection<Departments>(await new VodovozContext().Departments.Include(x => x.Boss).AsNoTracking().ToListAsync().ConfigureAwait(false));
                ;
            });
        }
        /// <summary>
        /// Получаем список всех заказов
        /// </summary>
        private RelayCommand _GetOrders;
        public RelayCommand GetOrders {
            get => _GetOrders ??= new RelayCommand(async obj => {
                OrdersList = new ObservableCollection<Orders>(await new VodovozContext().Orders.Include(x => x.Owner).AsNoTracking().ToListAsync().ConfigureAwait(false));
            });
        }
    }
}
