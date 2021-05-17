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

namespace ProductBase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HowICanReceive howICanReceive = new HowICanReceive();
            howICanReceive.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (new SqlCommands().connection("SELECT user.id FROM  user WHERE login = '" + login.Text + "' AND password = '" + password.Password + "' LIMIT 1"))
            {
                if (new SqlCommands().takeRole() == 1)
                {
                    ClientMain clientMain = new ClientMain();
                    clientMain.Show();
                    this.Close();
                }
                else
                {
                    WorkerMain workerMain = new WorkerMain();
                    workerMain.Show();
                    this.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных");
            }
        }
    }
}
