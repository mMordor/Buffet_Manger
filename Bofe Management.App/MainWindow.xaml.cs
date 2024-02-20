using DataLayer;
using DataLayer.Interfaces;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Bofe_Management.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        ObservableCollection<Customer> customers;
        ObservableCollection<Product> products;
        ObservableCollection<Order> orders;

        Order currentOrder = new Order();
        Product currentProduct = new Product();
        Customer currentCustomer = new Customer();

        public MainWindow()
        {


            InitializeComponent();
            BindData();



        }

        void BindData()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                customers = db.cAccesData.customers;
                products = db.pAccesData.products;
                orders = db.oAccesData.Orders;
                List<NewOrderModel> orderResult = (from o in orders
                                   join c in customers
                                   on o.CustomerId equals c.Id
                                   select new NewOrderModel()
                                   {
                                       Id= o.Id,
                                       CustomerId= o.CustomerId,
                                       Name= c.Name,
                                       Lastname= c.LastName,
                                       TotalPrice = o.TotalPrice,
                                       Balance = o.Balance
                                   }).ToList();

                Ordertb.ItemsSource = orderResult;
                Producttb.ItemsSource = products;
                Customerttb.ItemsSource = customers;
            }

        }

        private void refreshbtn_Click(object sender, RoutedEventArgs e)
        {
            BindData();
        }
        private void Homebtn_Click(object sender, RoutedEventArgs e)
        {
            HomePage.Visibility = Visibility.Visible;
            orderPage.Visibility = Visibility.Collapsed;
            productPage.Visibility = Visibility.Collapsed;
            customerPage.Visibility = Visibility.Collapsed;
        }
        private void Orderbtn_Click(object sender, RoutedEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            orderPage.Visibility = Visibility.Visible;
            productPage.Visibility = Visibility.Collapsed;
            customerPage.Visibility = Visibility.Collapsed;
        }
        private void Productbtn_Click(object sender, RoutedEventArgs e)
        {
            HomePage.Visibility = Visibility.Visible;
            orderPage.Visibility = Visibility.Collapsed;
            productPage.Visibility = Visibility.Visible;
            customerPage.Visibility = Visibility.Collapsed;
        }
        private void Customerbtn_Click(object sender, RoutedEventArgs e)
        {
            HomePage.Visibility = Visibility.Collapsed;
            orderPage.Visibility = Visibility.Collapsed;
            productPage.Visibility = Visibility.Collapsed;
            customerPage.Visibility = Visibility.Visible;
        }

        private void Ordertb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Ordertb.SelectedIndex >= 0)
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    var temp = Ordertb.SelectedItem as NewOrderModel;
                    currentOrder = db.oAccesData.Orders.First(x => x.Id == temp.Id);
                }
            }
        }
        private void Customerttb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Customerttb.SelectedIndex >= 0)
            {
                currentCustomer = Customerttb.SelectedItem as Customer;
            }
        }
        private void Producttb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Producttb.SelectedIndex >= 0)
            {
                currentProduct = Producttb.SelectedItem as Product;
            }
        }

        private void CAddbtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerAddEditPage cform = new CustomerAddEditPage();
            cform.ShowDialog();
            BindData();
        }
        private void CEditbtn_Click(object sender, RoutedEventArgs e)
        {
            if (Customerttb.SelectedIndex < 0)
            {
                MessageBox.Show("لطفا از جدول یک گزینه را انتخاب کنبد", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                CustomerAddEditPage cform = new CustomerAddEditPage(currentCustomer);
                cform.ShowDialog();
                BindData();
            }

        }
        private void CDelbtn_Click(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                db.cAccesData.Remove(currentCustomer.Id);
                db.Save();
            }
            BindData();
        }


        private void PAddbtn_Click(object sender, RoutedEventArgs e)
        {
            ProductAddEditPage cform = new ProductAddEditPage();
            cform.ShowDialog();
            BindData();
        }
        private void PEditbtn_Click(object sender, RoutedEventArgs e)
        {
            if (Producttb.SelectedIndex < 0)
            {
                MessageBox.Show("لطفا از جدول یک گزینه را انتخاب کنبد", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                ProductAddEditPage cform = new ProductAddEditPage(currentProduct);
                cform.ShowDialog();
                BindData();
            }
        }
        private void PDelbtn_Click(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                db.pAccesData.Remove(currentProduct.Id);
                db.Save();
            }
            BindData();
        }



        private void OAddbtn_Click(object sender, RoutedEventArgs e)
        {
            OrderAddEditPage oform = new OrderAddEditPage();
            oform.ShowDialog();
            BindData();
        }
        private void ODelbtn_Click(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {


                Order temp = db.oAccesData.Orders.First(x => x.Id == currentOrder.Id);
                Customer nc = db.cAccesData.customers.First(y => y.Id == currentOrder.CustomerId);
                nc.Account -= temp.Balance;
                db.oAccesData.Remove(currentOrder.Id);
                db.cAccesData.Updte(nc);
                db.Save();
            }
            BindData();
        }
        private void OEditbtn_Click(object sender, RoutedEventArgs e)
        {
            OrderAddEditPage oform = new OrderAddEditPage(currentOrder);
            oform.ShowDialog();
            BindData();
        }

        private void OBalancebtn_Click(object sender, RoutedEventArgs e)
        {
            if (Ordertb.SelectedIndex < 0)
            {
                MessageBox.Show("لطفا از جدول یک گزینه را انتخاب کنبد", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                BalanceEditPage bform = new BalanceEditPage(currentOrder);
                bform.ShowDialog();
                BindData();
            }
        }


    }
}
