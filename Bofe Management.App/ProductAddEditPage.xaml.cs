using DataLayer;
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
    /// Interaction logic for ProductAddEditPage.xaml
    /// </summary>
    public partial class ProductAddEditPage : Window
    {
        bool isEdit = false;
        int index = 0;
        public ProductAddEditPage()
        {
            InitializeComponent();
        }
        public ProductAddEditPage(Product temp)
        {
            InitializeComponent();
            isEdit = true;
            index = temp.Id;
            Nametxt.Text = temp.Name;
            Counttxt.Text = temp.AvalebleCount.ToString();
            Buytxt.Text = temp.BuyPrice.ToString();
            Selltxt.Text = temp.SellPrice.ToString();
        }

        private void Setbtn_Click(object sender, RoutedEventArgs e)
        {
            bool isvalid = Validation();
            if (isvalid)
            {
                if (isEdit)
                {
                    int.TryParse(Counttxt.Text, out int c);
                    Decimal.TryParse(Buytxt.Text, out Decimal b);
                    Decimal.TryParse(Selltxt.Text, out Decimal s);
                    Product temp = new Product(index, Nametxt.Text, c, b, s);
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        db.pAccesData.Updte(temp);
                        db.Save();
                    }
                }
                else
                {
                    int.TryParse(Counttxt.Text, out int c);
                    Decimal.TryParse(Buytxt.Text, out Decimal b);
                    Decimal.TryParse(Selltxt.Text, out Decimal s);
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        Product temp = new Product(db.pAccesData.NextID(), Nametxt.Text, c, b, s);
                        db.pAccesData.Add(temp);
                        db.Save();
                    }

                }
                this.Close();
            }

        }

        private bool Validation()
        {
            bool isvalid = true;
            if (Nametxt.Text == "")
            {
                isvalid = false;
                MessageBox.Show("لطفا نام محصول را وارد کنید", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (!Int32.TryParse(Counttxt.Text, out int count))
            {
                isvalid = false;
                MessageBox.Show("لطفا در قسمت تعداد فقط عدد وارد کنید", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (!Decimal.TryParse(Buytxt.Text, out decimal buy))
            {
                isvalid = false;
                MessageBox.Show("لطفا در قسمت قیمت خرید فقط عدد وارد کنید", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (!Decimal.TryParse(Selltxt.Text, out decimal sell))
            {
                isvalid = false;
                MessageBox.Show("لطفا در قسمت قیمت فروش فقط عدد وارد کنید", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

            return isvalid;

        }
        private void Closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
