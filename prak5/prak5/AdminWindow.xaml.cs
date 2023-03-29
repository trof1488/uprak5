using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using prak5.prak5DataSetTableAdapters;

namespace prak5
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        RolesTableAdapter rolesTableAdapter = new RolesTableAdapter();

        UsersTableAdapter usersTableAdapter = new UsersTableAdapter();

        customerTableAdapter CustomerTableAdapter = new customerTableAdapter();

        order_TableAdapter or = new order_TableAdapter();

        UsersTableAdapter users = new UsersTableAdapter();

        paymentTableAdapter pay = new paymentTableAdapter();

        int users_ = 0, order_id_ = 0;
        public AdminWindow()
        {
            InitializeComponent();

            RolesDataGrid.ItemsSource = rolesTableAdapter.GetData();

            UsersDataGrid.ItemsSource = usersTableAdapter.GetData();

            CustomerDataGrid.ItemsSource = CustomerTableAdapter.GetData();

            OrderDataGrid.ItemsSource = or.GetData();

            paymentDataGrid1.ItemsSource = pay.GetData();

            combx.ItemsSource = rolesTableAdapter.GetData();
            combx.DisplayMemberPath = "ID";

            cmbox1.ItemsSource = users.GetData();
            cmbox1.DisplayMemberPath = "users_name";

            cmbox.ItemsSource = or.GetData();
            cmbox.DisplayMemberPath = "order_id";

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Roletbx.Text))
            {
                MessageBox.Show("Ошибка: пустая строка");

            }

            else if (rolesTableAdapter.GetData().FirstOrDefault(element => element.role_name.ToLower() == Roletbx.Text.ToLower()) != null)
            {
                MessageBox.Show("Ошибка: такое значение уже существует");
            }

            else if (Roletbx.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0")
            {
                MessageBox.Show("Ошибка: Строка не может содержать цифры");
            }
            else
            {
                try
                {
                    rolesTableAdapter.InsertQuery(Roletbx.Text);
                }
                catch
                {
                    MessageBox.Show("Ошибка: не удалось добавить запись");
                }
                finally
                {
                    RolesDataGrid.ItemsSource = rolesTableAdapter.GetData();
                    Roletbx.Text = null;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (RolesDataGrid.SelectedItem != null)
            {
                try
                {
                    rolesTableAdapter.DeleteQuery((int)(RolesDataGrid.SelectedItem as DataRowView).Row[0]);
                }
                catch
                {
                    MessageBox.Show("Ошибка: не удалось удалить запись");
                }
                finally
                {
                    RolesDataGrid.SelectedItem = null;
                    RolesDataGrid.ItemsSource = rolesTableAdapter.GetData();
                }

            }
            else
            {
                MessageBox.Show("Ошибка: элемент для удаления не выбран");
            }

        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Roletbx.Text))
            {
                MessageBox.Show("Ошибка: пустая строка");
            }
            else if (rolesTableAdapter.GetData().FirstOrDefault(element => element.role_name.ToLower() == Roletbx.Text.ToLower()) != null)
            {
                MessageBox.Show("Ошибка: такое значение уже существует");
            }
            else if (Roletbx.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0")
            {
                MessageBox.Show("Ошибка: Строка не может содержать цифры");
            }
            else
            {
                try
                {
                    rolesTableAdapter.UpdateQuery(Roletbx.Text, (int)(RolesDataGrid.SelectedItem as DataRowView).Row[0]);
                }
                catch
                {
                    MessageBox.Show("Ошибка: не удалось изменить запись");
                }
                finally
                {
                    RolesDataGrid.ItemsSource = rolesTableAdapter.GetData();
                }

            }
        }

        private void RolesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Roletbx.Text = (RolesDataGrid.SelectedItem as DataRowView).Row[1].ToString();
            }
            catch
            {
                Roletbx.Text = null;
            }
        }


        private void add1_Click_1(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(usertbx.Text) || string.IsNullOrWhiteSpace(usertbx1.Text) || string.IsNullOrWhiteSpace(usertbx2.Text) || string.IsNullOrWhiteSpace(usertbx3.Text) || string.IsNullOrWhiteSpace(usertbx4.Text) || string.IsNullOrWhiteSpace(combx.Text))
            {
                MessageBox.Show("Ошибка: пустая строка");
            }
            else if (usertbx.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0" || usertbx1.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0" || usertbx2.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0")
            {
                MessageBox.Show("Ошибка: Строка не может содержать цифры");
            }
            else
            {
                try
                {
                    usersTableAdapter.InsertQuery(usertbx.Text, usertbx1.Text, usertbx2.Text, Convert.ToInt32((combx.SelectedItem as DataRowView).Row[0]), usertbx3.Text, usertbx4.Text);
                }
                catch
                {
                    MessageBox.Show("Ошибка: не удалось добавить значение");
                }
                finally
                {
                    UsersDataGrid.ItemsSource = usersTableAdapter.GetData();
                }
            }
        }

        private void delete1_Click(object sender, RoutedEventArgs e)
        {

            if (UsersDataGrid.SelectedItem != null)
            {
                try
                {
                    object id = (UsersDataGrid.SelectedItem as DataRowView).Row[0];

                    usersTableAdapter.DeleteQuery((int)id);
                }
                catch
                {
                    MessageBox.Show("Ошибка: не удалось удалить значения");
                }
                finally
                {
                    UsersDataGrid.ItemsSource = usersTableAdapter.GetData();
                }
            }
        }

        private void Change1_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(usertbx.Text) || string.IsNullOrWhiteSpace(usertbx1.Text) || string.IsNullOrWhiteSpace(usertbx2.Text) || string.IsNullOrWhiteSpace(usertbx3.Text) || string.IsNullOrWhiteSpace(usertbx4.Text) || string.IsNullOrWhiteSpace(combx.Text))
            {
                MessageBox.Show("Ошибка: пустая строка");
            }
            else if (usertbx.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0" || usertbx1.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0" || usertbx2.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0")
            {
                MessageBox.Show("Ошибка: Строка не может содержать цифры");
            }
            
            else
            {
                try
                {
                    object id = (UsersDataGrid.SelectedItem as DataRowView).Row[0];

                    usersTableAdapter.UpdateQuery(usertbx.Text, usertbx1.Text, usertbx2.Text, Convert.ToInt32((combx.SelectedItem as DataRowView).Row[0]), usertbx3.Text, usertbx4.Text, (int)id);
                }
                catch
                {
                    MessageBox.Show("Ошибка: не удалось обновить значение");
                }
                finally
                {
                    UsersDataGrid.ItemsSource = usersTableAdapter.GetData();
                }
            }
        }

        private void Add2_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(custbx4.Text))
            {
                MessageBox.Show("Ошибка: пустая строка");
            }
            else if (custbx4.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0")
            {
                MessageBox.Show("Ошибка: Строка не может содержать цифры");
            }
            else
            {
                try
                {
                    CustomerTableAdapter.InsertQuery(custbx.Text, custbx1.Text, custbx2.Text, custbx3.Text, Convert.ToInt32(custbx4.Text), custbx5.Text, users_);
                    CustomerDataGrid.ItemsSource = or.GetData();
                }
                catch (System.FormatException e_)
                {
                    MessageBox.Show("Ошибка: не удалось изменить запись");
                }
                finally
                {
                    CustomerDataGrid.ItemsSource = CustomerTableAdapter.GetData();
                    custbx4.Text = null;
                }


            }
        }

        private void Delete2_Click(object sender, RoutedEventArgs e)
        {
            object id = (CustomerDataGrid.SelectedItem as DataRowView).Row[0];
            if (CustomerDataGrid.SelectedItem != null)
            {
                try
                {
                    usersTableAdapter.DeleteQuery(Convert.ToInt32(id));
                    CustomerDataGrid.ItemsSource = usersTableAdapter.GetData();
                }
                catch
                {
                    CustomerTableAdapter.DeleteQuery((int)id);
                    CustomerDataGrid.ItemsSource = CustomerTableAdapter.GetData();
                }
                finally
                {

                    UsersDataGrid.ItemsSource = usersTableAdapter.GetData();

                }
            }
            else
            {
                MessageBox.Show("Ошибка: элемент для удаления не выбран");
            }

        }

        private void Change2_Click(object sender, RoutedEventArgs e)
        {
            object id = (CustomerDataGrid.SelectedItem as DataRowView).Row[0];
            if (string.IsNullOrWhiteSpace(custbx6.Text))
            {
                MessageBox.Show("Ошибка: пустая строка");
            }

            else if (custbx6.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0")
            {
                MessageBox.Show("Ошибка: Строка не может содержать цифры");
            }
            else
            {

                try
                {

                    CustomerTableAdapter.UpdateQuery(custbx.Text, custbx1.Text, custbx2.Text, custbx3.Text, Convert.ToInt32(custbx4.Text), custbx5.Text, Convert.ToInt32((cmbox.SelectedItem as DataRowView).Row[0]), (int)id);

                    CustomerDataGrid.ItemsSource = CustomerTableAdapter.GetData();
                }
                catch
                {
                    MessageBox.Show("Ошибка: не удалось удалить запись");
                }
                finally
                {

                    OrderDataGrid.ItemsSource = or.GetData();
                }
            }
        }

        private void Add4_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(custbx6.Text))
            {
                MessageBox.Show("Ошибка: пустая строка");

            }
            else if (custbx6.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0")
            {
                MessageBox.Show("Ошибка: Строка не может содержать цифры");
            }
            else
            {
                try
                {
                    or.InsertQuery(custbx6.Text, users_);
                    OrderDataGrid.ItemsSource = or.GetData();
                }
                catch (System.FormatException e_)
                {

                    MessageBox.Show(e_.Message);

                }
                finally
                {
                    OrderDataGrid.ItemsSource = or.GetData();
                    custbx6.Text = null;
                }
            }
        }

        private void cmbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object cell = (cmbox1.SelectedItem as DataRowView).Row[0];
            users_ = Convert.ToInt32(cell);

        }

        private void Delete5_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDataGrid.SelectedItem != null)
            {
                try
                {
                    object id = (OrderDataGrid.SelectedItem as DataRowView).Row[0];
                    or.DeleteQuery(Convert.ToInt32(id));
                    OrderDataGrid.ItemsSource = or.GetData();
                }
                catch
                {
                    MessageBox.Show("Ошибка: не удалось удалить запись");
                }
                finally
                {
                    OrderDataGrid.SelectedItem = null;
                    OrderDataGrid.ItemsSource = or.GetData();
                }
            }
        }

        private void Change6_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(custbx6.Text))
            {
                MessageBox.Show("Ошибка: пустая строка");
            }

            else if (custbx6.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0")
            {
                MessageBox.Show("Ошибка: Строка не может содержать цифры");
            }
            else
            {
                try
                {
                    object id = (OrderDataGrid.SelectedItem as DataRowView).Row[0];
                    or.UpdateQuery(custbx6.Text, users_, Convert.ToInt32(id));
                    OrderDataGrid.ItemsSource = or.GetData();
                }
                catch
                {
                    MessageBox.Show("Ошибка: не удалось изменить запись");
                }
                finally
                {
                    OrderDataGrid.ItemsSource = or.GetData();
                }
            }
        }

        private void OrderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                custbx6.Text = (OrderDataGrid.SelectedItem as DataRowView).Row[1].ToString();

            }
            catch
            {
                custbx6.Text = null;

            }

        }

        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                usertbx.Text = (UsersDataGrid.SelectedItem as DataRowView).Row[1].ToString();
                usertbx1.Text = (UsersDataGrid.SelectedItem as DataRowView).Row[2].ToString();
                usertbx2.Text = (UsersDataGrid.SelectedItem as DataRowView).Row[3].ToString();
                usertbx3.Text = (UsersDataGrid.SelectedItem as DataRowView).Row[4].ToString();
                usertbx4.Text = (UsersDataGrid.SelectedItem as DataRowView).Row[5].ToString();

            }
            catch
            {
                usertbx.Text = null;
                usertbx1.Text = null;
                usertbx2.Text = null;
                usertbx3.Text = null;
                usertbx4.Text = null;
            }

        }

        private void cmbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object cell = (cmbox.SelectedItem as DataRowView).Row[0];
            users_ = Convert.ToInt32(cell);

        }

        private void cmbox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object cell = (order_id.SelectedItem as DataRowView).Row[0];
            order_id_ = Convert.ToInt32(cell);
        }

        private void Delete6_Click(object sender, RoutedEventArgs e)
        {
            object id = (paymentDataGrid1.SelectedItem as DataRowView).Row[0];

            if (paymentDataGrid1.SelectedItem != null)
            {
                try
                {
                    pay.DeleteQuery(Convert.ToInt32(id));
                    paymentDataGrid1.ItemsSource = pay.GetData();
                }
                catch
                {
                    MessageBox.Show("Ошибка: не удалось удалить запись");
                }
                finally
                {
                    paymentDataGrid1.SelectedItem = null;
                    paymentDataGrid1.ItemsSource = pay.GetData();
                }

            }
            else
            {
                MessageBox.Show("Ошибка: элемент для удаления не выбран");
            }


        }

        private void Change7_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Roletbx.Text))
            {
                MessageBox.Show("Ошибка: пустая строка");
            }

            else if (Roletbx.Text.FirstOrDefault(element => char.IsDigit(element)).ToString() != "\0")
            {
                MessageBox.Show("Ошибка: Строка не может содержать цифры");
            }
            else
            {

                try
                {
                    object id = (paymentDataGrid1.SelectedItem as DataRowView).Row[0];
                    pay.UpdateQuery(order_id_, Convert.ToInt32(Roletbx.Text)) ;
                }
                catch
                {
                    MessageBox.Show("Ошибка: не удалось удалить запись");
                }
                finally
                {

                    paymentDataGrid1.ItemsSource = pay.GetData();
                }
            }

        }

        private void CustomerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Add5_Click(object sender, RoutedEventArgs e)
        {
            int custId = 0;
            if (int.TryParse(custbx7.Text, out custId))
            {
                pay.InsertQuery(order_id_, custId);
                paymentDataGrid1.ItemsSource = or.GetData();
            }
            else
            {
                // Handle invalid input
            }

        }
    }
}


