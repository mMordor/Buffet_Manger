using DataLayer;
using DataLayer.Interfaces;
using DataLayer.Models;
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

namespace Bofe_Management.App
{
    /// <summary>
    /// Interaction logic for BalanceEditPage.xaml
    /// </summary>
    public partial class BalanceEditPage : Window
    {
        Order order;

        public BalanceEditPage(Order temp)
        {
            InitializeComponent();
            order = temp;

        }

        private void Paybtn_Click(object sender, RoutedEventArgs e)
        {
            if (Decimal.TryParse(Balancetxt.Text, out decimal b))
            {
                order.Balance -= b;
                using (UnitOfWork db = new UnitOfWork())
                {
                    Customer nc = db.cAccesData.customers.First(x => x.Id == order.CustomerId);
                    nc.Account -= b;
                    db.oAccesData.Updte(order);
                    db.cAccesData.Updte(nc);
                    db.Save();
                }
            }
            else
            {
                MessageBox.Show("لطفا عدد وارد کنید", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            this.Close();
        }

        private void Balancebtn_Click(object sender, RoutedEventArgs e)
        {
            Balancetxt.Text = order.Balance.ToString();
        }
    }
}
