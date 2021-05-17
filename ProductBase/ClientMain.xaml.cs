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
using System.Windows.Shapes;

namespace ProductBase
{
    /// <summary>
    /// Логика взаимодействия для ClientMain.xaml
    /// </summary>
    public partial class ClientMain : Window
    {
        SqlCommands sqlCommands = new SqlCommands();
        public ClientMain()
        {
            InitializeComponent();
            sqlCommands.fillDataGrid(ref dataGridReady, ref dataGrid);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            sqlCommands.exit();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MakeOrder makeOrder = new MakeOrder();
            makeOrder.Show();
            this.Close();
        }
        private void TakeOrder_Click(object sender, RoutedEventArgs e)
        {
            sqlCommands.takeOrder(Convert.ToInt32(dataGridReady.SelectedIndex));
            sqlCommands.fillDataGrid(ref dataGridReady, ref dataGrid);
        }
    }
}
