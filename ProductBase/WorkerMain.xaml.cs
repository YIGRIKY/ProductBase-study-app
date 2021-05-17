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
    /// Логика взаимодействия для WorkerMain.xaml
    /// </summary>
    public partial class WorkerMain : Window
    {
        SqlCommands sqlCommands = new SqlCommands();
        public WorkerMain()
        {
            InitializeComponent();
            sqlCommands.fillDataGridWorker(ref dataGridReady, ref dataGrid);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            sqlCommands.exit();
            mainWindow.Show();
            this.Close();
        }
        private void ConfirmDeliver_Click(object sender, RoutedEventArgs e)
        {
            sqlCommands.confirmDeliver(Convert.ToInt32(dataGridReady.SelectedIndex));
            sqlCommands.fillDataGridWorker(ref dataGridReady, ref dataGrid);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InfoAboutProducts infoAboutProducts = new InfoAboutProducts();
            infoAboutProducts.Show();
            this.Close();
        }
    }
}
