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
    /// Логика взаимодействия для MakeOrder.xaml
    /// </summary>
    public partial class MakeOrder : Window
    {
        SqlCommands sqlCommands = new SqlCommands();
        public MakeOrder()
        {
            InitializeComponent();
            sqlCommands.fillComboBox(ref comboBox);
            sqlCommands.fillDataGridBase(ref dataGrid, comboBox);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sqlCommands.fillDataGridBase(ref dataGrid, comboBox);
        }

        private void order_click(object sender, RoutedEventArgs e)
        {
            sqlCommands.makeOrder(Convert.ToInt32(dataGrid.SelectedIndex + 1));
            sqlCommands.fillDataGridBase(ref dataGrid, comboBox);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClientMain clientMain = new ClientMain();
            clientMain.Show();
            this.Close();
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ProductName.Text.Trim().Length > 0)
            {
                sqlCommands.search(ProductName.Text, ref dataGrid);
            }
            else
            {
                sqlCommands.fillDataGridBase(ref dataGrid, comboBox);
            }
        }
    }
}
