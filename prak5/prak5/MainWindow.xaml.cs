using System;
using System.Collections.Generic;
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
using prak5.prak5DataSetTableAdapters;

namespace prak5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        UsersTableAdapter usersTableAdapter = new UsersTableAdapter();

        RolesTableAdapter rolesTableAdapter = new RolesTableAdapter();

        public MainWindow()
        {
            InitializeComponent();
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = usersTableAdapter.GetData().FirstOrDefault(element=> element.login_ == login.Text && element.password_ == password.Password);
            if(user != null)
            {
                var Role = rolesTableAdapter.GetData().First(element => element.ID == user.role_id).role_name;
                if (Role == "admin")


                {
                    AdminWindow role = new AdminWindow();
                    role.Show();
                }
                else if (Role == "Sklad")
                {
                    SkladWindow sk = new SkladWindow();
                    sk.Show();
                }
            }
            
        }
    }
}
