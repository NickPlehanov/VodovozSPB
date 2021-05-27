using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodovozSPB.Helpers;
using VodovozSPB.Models;
using VodovozSPB.Data;
using Microsoft.EntityFrameworkCore;

namespace VodovozSPB {
    class MainWindowViewModel : BaseViewModel {
        private readonly LocaldbMssqllocaldbContext context;
        public MainWindowViewModel() {
            context = new LocaldbMssqllocaldbContext();
            GetEmployees.Execute(null);
            GetDepartments.Execute(null);
            GetOrders.Execute(null);
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
        /// Коллеукция для хранения спика заказов 
        /// </summary>
        private ObservableCollection<Orders> _OrdersList;
        public ObservableCollection<Orders> OrdersList {
            get => _OrdersList;
            set {
                _OrdersList = value;
                OnPropertyChanged(nameof(OrdersList));
            }
        }


        private RelayCommand _GetEmployees;
        public RelayCommand GetEmployees {
            get => _GetEmployees ??= new RelayCommand(async obj => {
                EmployeesList = new ObservableCollection<Employees>(await new LocaldbMssqllocaldbContext().Employees.AsNoTracking().ToListAsync().ConfigureAwait(false));
            });
        }

        private RelayCommand _GetDepartments;
        public RelayCommand GetDepartments {
            get => _GetDepartments ??= new RelayCommand(async obj => {
                DepartmentsList = new ObservableCollection<Departments>(await new LocaldbMssqllocaldbContext().Departments.AsNoTracking().ToListAsync().ConfigureAwait(false));
;            });
        }

        private RelayCommand _GetOrders;
        public RelayCommand GetOrders {
            get => _GetOrders ??= new RelayCommand(async obj => {
                OrdersList = new ObservableCollection<Orders>(await new LocaldbMssqllocaldbContext().Orders.AsNoTracking().ToListAsync().ConfigureAwait(false));
            });
        }
    }
}
