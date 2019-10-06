using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf10_1.Entities;

namespace Wpf10_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int id = 1;
        private ObservableCollection<User> _users;// = 
                                                  //new ObservableCollection<User>();
        EFContext _context = new EFContext();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            BtnShow_Click(sender, e);
            DbUser dbUser = new DbUser
            {
                Name = "Валера"
            };
            _context.Users.Add(dbUser);
            _context.SaveChanges();
            _users.Add(new User
            {
                Id = dbUser.Id,
                Name = dbUser.Name
            });
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
            {
                var user = (User)lbUsers.SelectedItem;
                var dbUserEdit = _context.Users
                    .SingleOrDefault(u => u.Id == user.Id);
                if (dbUserEdit != null)
                {
                    dbUserEdit.Name = "Петро";
                    _context.SaveChanges();
                    var mUsers = _users.SingleOrDefault(u => u.Id == user.Id);
                    mUsers.Name = dbUserEdit.Name;
                }
            }
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            var model = _context.Users.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name
            }).ToList();
            _users = new ObservableCollection<User>(model);
            lbUsers.ItemsSource = _users;
        }

        private void BtnDete_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
            {
                var user = (User)lbUsers.SelectedItem;
                foreach (var item in _context.Users)
                {
                    if (user.Id == item.Id && user.Name == item.Name)
                    {
                        _context.Users.Remove(item);
                    }
                }
                _context.SaveChanges();
                BtnShow_Click(sender, e);
            }
        }
    }

    public class User : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        public int Id
        {
            get { return this._id; }
            set
            {
                if (this._id != value)
                {
                    this._id = value;
                    this.NotifyPropertyChanged("Id");
                }
            }
        }
        public string Name
        {
            get { return this._name; }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        public string FullName { get { return $"{Id} {Name}"; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                if (propName == "Name" || propName == "Id")
                    this.PropertyChanged(this, new PropertyChangedEventArgs("FullName"));
                else
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
