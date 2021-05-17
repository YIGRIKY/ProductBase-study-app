using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace ProductBase
{
    class SqlCommands
    {
        static int userId { get; set; }
        static MySqlConnection connectionToDatabase = new MySqlConnection("server=localhost;user=root;database=productbasedb;password=,frkfy123456");
        public bool connection(string command)
        {
            connectionToDatabase.Open();
            MySqlConnection connection = new MySqlConnection("server=localhost;user=root;database=productbasedb;password=,frkfy123456");
            connection.Open();
            MySqlCommand sqlCommand = new MySqlCommand(command, connectionToDatabase);
            sqlCommand.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            try
            {
                userId = Convert.ToInt32(dt.Rows[0][0].ToString());
                return true;
                
            }
            catch (Exception)
            {
                connectionToDatabase.Close();
                return false;
            }
        }
        public int takeRole()
        {
            MySqlCommand sqlCommand = new MySqlCommand($"select usersandroles.roleId from usersandroles where usersandroles.userId = {userId}", connectionToDatabase);
            sqlCommand.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return (int)dt.Rows[0][0];
        }
        public void fillDataGrid(ref DataGrid dataGridReady, ref DataGrid dataGrid)
        {
            MySqlCommand mySqlCommand1 = new MySqlCommand($"Select distinct orders.id, orders.date from orders where orders.idUser = {userId} and orders.date <= now();", connectionToDatabase);
            mySqlCommand1.ExecuteNonQuery();
            MySqlCommand mySqlCommand = new MySqlCommand($"Select orders.id, orders.date from orders where orders.idUser = {userId} and orders.date > now(); ", connectionToDatabase);
            mySqlCommand.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dataAdapter.Fill(dt);
            dataGrid.ItemsSource  = dt.AsDataView();
            dataAdapter = new MySqlDataAdapter(mySqlCommand1);
            dataAdapter.Fill(dt1);
            dataGridReady.ItemsSource = dt1.AsDataView();
        }
        public void fillDataGridWorker(ref DataGrid dataGridReady, ref DataGrid dataGrid)
        {
            MySqlCommand mySqlCommand1 = new MySqlCommand($"Select distinct delivers.id, delivers.date from delivers where delivers.date <= now();", connectionToDatabase);
            mySqlCommand1.ExecuteNonQuery();
            MySqlCommand mySqlCommand = new MySqlCommand($"Select delivers.id, delivers.date from delivers where delivers.date > now(); ", connectionToDatabase);
            mySqlCommand.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dataAdapter.Fill(dt);
            dataGrid.ItemsSource = dt.AsDataView();
            dataAdapter = new MySqlDataAdapter(mySqlCommand1);
            dataAdapter.Fill(dt1);
            dataGridReady.ItemsSource = dt1.AsDataView();
        }
        public void fillComboBox(ref ComboBox comboBox)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("Select producttypes.name from producttypes;", connectionToDatabase);
            mySqlCommand.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            List<ComboBoxItem> comboBoxItemList = new List<ComboBoxItem>();
            foreach (var item in dt.AsEnumerable())
            {
                ComboBoxItem comboItem = new ComboBoxItem();
                comboItem.Content  = item.ItemArray[0];
                comboBoxItemList.Add(comboItem);
            }
            comboBox.ItemsSource = comboBoxItemList;
        }
        public void fillDataGridBase(ref DataGrid dataGrid, ComboBox comboBox)
        {
            MySqlCommand mySqlCommand;
            if (comboBox.SelectedItem != null)
            {
                mySqlCommand = new MySqlCommand($"Select products.name, products.count from products where products.productType = (select producttypes.id from producttypes where producttypes.id = '{comboBox.SelectedIndex + 1}');", connectionToDatabase);
            }
            else
            {
                mySqlCommand = new MySqlCommand($"Select products.name, products.count from products;", connectionToDatabase);
                
            }
            mySqlCommand.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            dataGrid.ItemsSource = dt.AsDataView();
        }

        public void makeOrder(int id)
        {
            MySqlCommand mySqlCommand = new MySqlCommand($"Insert into orders(date, idUser, idProduct) values('2021-{new Random().Next(4, 12)}-{new Random().Next(1, 30)}', {userId}, {id});", connectionToDatabase);
            mySqlCommand.ExecuteNonQuery();
            MySqlCommand mySqlCommand1 = new MySqlCommand($"update products set count = count - 1 where products.id = {id}", connectionToDatabase);
            mySqlCommand1.ExecuteNonQuery();
        }

        public void takeOrder(int id)
        {
            MySqlCommand mySqlCommand = new MySqlCommand($"Select distinct orders.id, orders.date from orders where orders.idUser = {userId} and orders.date <= now();", connectionToDatabase);
            mySqlCommand.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            MySqlCommand mySqlCommand1 = new MySqlCommand($"delete from orders where orders.id = {dt.Rows[id].ItemArray[0]};", connectionToDatabase);
            mySqlCommand1.ExecuteNonQuery();
        }
        public void search(string name, ref DataGrid dataGrid)
        {
            MySqlCommand mySqlCommand = new MySqlCommand($"Select products.name, products.count from products where lower(products.name) like lower('%{name}%')", connectionToDatabase);
            mySqlCommand.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            dataGrid.ItemsSource = dt.AsDataView();
        }
        public void confirmDeliver(int id)
        {
            MySqlCommand mySqlCommand = new MySqlCommand($"Select distinct delivers.id, delivers.count, delivers.idProduct from delivers where delivers.date <= now();", connectionToDatabase);
            mySqlCommand.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            MySqlCommand mySqlCommand1 = new MySqlCommand($"update products set count = count + {dt.Rows[id].ItemArray[1]} where products.id = {dt.Rows[id].ItemArray[2]}", connectionToDatabase);
            mySqlCommand1.ExecuteNonQuery();
            MySqlCommand mySqlCommand2 = new MySqlCommand($"delete from delivers where delivers.id = {dt.Rows[id].ItemArray[0]}", connectionToDatabase);
            mySqlCommand2.ExecuteNonQuery();
        }
        public void exit()
        {
            connectionToDatabase.Close();
        }
    }
}
