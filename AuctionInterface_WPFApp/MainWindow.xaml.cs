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

namespace AuctionInterface_WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AuctionDB_Sets dataSets = new AuctionDB_Sets();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_TryLogin_Click(object sender, RoutedEventArgs e)
        {
            List<Account> accounts = dataSets.Accounts.ToList();
            String email = TextBox_Login.Text;

            try
            {
                Account userAccount = (from account in accounts
                                       where account.Email == email
                                       select account).ToList().First();

                AuctionsWindow auctionsWindow = new AuctionsWindow(userAccount);
                auctionsWindow.ShowDialog();
            }
            catch (Exception)
            {

                MessageBox.Show($"No user with that email found!");
            }


        }
    }
}
