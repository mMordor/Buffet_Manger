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
using System.Windows.Shapes;

namespace Bofe_Management.App
{
    /// <summary>
    /// Interaction logic for OrderAddEditPage.xaml
    /// </summary>
    public partial class OrderAddEditPage : Window
    {

        Order newOrder;
        ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
        ObservableCollection<Product> products = new ObservableCollection<Product>();
        ObservableCollection<Product> sellectedProducts = new ObservableCollection<Product>();
        bool isedit = false;
        int index = 0;
        Customer Currentcustomer = new Customer();
        Product Currentproduct = new Product();
        public OrderAddEditPage()
        {
            InitializeComponent();
            using (UnitOfWork db = new UnitOfWork())
            {
                customers = db.cAccesData.customers;
                products = db.pAccesData.products;
            }
            OrderCustomerTbl.ItemsSource = customers;
            OrderProductTbl.ItemsSource = products;
            OrderTbl.ItemsSource = sellectedProducts;
        }
        public OrderAddEditPage(Order temp)
        {
            InitializeComponent();
            using (UnitOfWork db = new UnitOfWork())
            {
                customers = db.cAccesData.customers;
                products = db.pAccesData.products;
            }
            OrderCustomerTbl.ItemsSource = customers;
            OrderProductTbl.ItemsSource = products;
            sellectedProducts = temp.ProductsList;
            OrderTbl.ItemsSource = sellectedProducts;
            isedit = true;
            index = temp.Id;
            Currentcustomer = customers.First(x => x.Id == temp.CustomerId);
        }


        private void OrderCustomerTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderCustomerTbl.SelectedIndex >= 0)
            {
                Currentcustomer = OrderCustomerTbl.SelectedItem as Customer;

            }
        }
        private void OrderProductTbl_Selected(object sender, RoutedEventArgs e)
        {

            if (OrderCustomerTbl.SelectedIndex < 0)
            {
                MessageBox.Show("لطفا از جدول مشتری ها یک گزینه را انتخاب کنبد", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (OrderProductTbl.SelectedIndex >= 0)
            {
                Product temp = OrderProductTbl.SelectedItem as Product;
                sellectedProducts.Add(temp);
                using (UnitOfWork db = new UnitOfWork())
                {
                    Product result = db.pAccesData.products.First(x => x.Id == temp.Id);
                    result.AvalebleCount -= 1;
                    db.pAccesData.Updte(result);
                    db.Save();
                }
            }

        }
        private void OrderTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderTbl.SelectedIndex >= 0)
            {
                Currentproduct = OrderTbl.SelectedItem as Product;

            }
        }



        private void OrderRemoveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (Currentproduct != null)
            {
                sellectedProducts.Remove(Currentproduct);
                using (UnitOfWork db = new UnitOfWork())
                {
                    Product result = db.pAccesData.products.First(x => x.Id == Currentproduct.Id);
                    result.AvalebleCount += 1;
                    db.pAccesData.Updte(result);
                    db.Save();
                }
                Currentproduct = null;
            }


        }

        private void OrderAddBTN_Click(object sender, RoutedEventArgs e)
        {
            if (Currentproduct != null)
            {
                sellectedProducts.Add(Currentproduct);
                using (UnitOfWork db = new UnitOfWork())
                {
                    Product result = db.pAccesData.products.First(x => x.Id == Currentproduct.Id);
                    result.AvalebleCount -= 1;
                    db.pAccesData.Updte(result);
                    db.Save();
                }
                Currentproduct = null;
            }

        }




        private void CanselBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void setBTN_Click(object sender, RoutedEventArgs e)
        {
            if (Currentcustomer.Id <= 0)
            {
                MessageBox.Show("لطفا از جدول مشتری ها یک گزینه را انتخاب کنبد", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (isedit)
            {
                newOrder = new Order(index, sellectedProducts, Currentcustomer.Id);
                using (UnitOfWork db = new UnitOfWork())
                {
                    Order temp = db.oAccesData.Orders.First(p => p.Id == newOrder.Id);
                    newOrder.Balance = temp.Balance;
                    temp.Balance += newOrder.TotalPrice - temp.TotalPrice;
                    Customer nc = db.cAccesData.customers.First(x => x.Id == newOrder.CustomerId);
                    nc.Account += temp.Balance - newOrder.Balance;
                    newOrder.Balance = temp.Balance;
                    db.oAccesData.Updte(newOrder);
                    db.cAccesData.Updte(nc);
                    db.Save();

                }
            }
            else
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    newOrder = new Order(db.oAccesData.NextID(), sellectedProducts, Currentcustomer.Id);
                    Customer nc = db.cAccesData.customers.First(x => x.Id == newOrder.CustomerId);
                    nc.Account += newOrder.Balance;
                    db.oAccesData.Add(newOrder);
                    db.cAccesData.Updte(nc);
                    db.Save();
                }

            }

            this.Close();
        }
    }
}
