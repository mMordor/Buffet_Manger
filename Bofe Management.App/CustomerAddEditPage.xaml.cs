using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bofe_Management.App
{
    /// <summary>
    /// Interaction logic for CustomerAddEditPage.xaml
    /// </summary>
    public partial class CustomerAddEditPage : Window
    {
        bool isEdit = false;
        int index = 0;
        public CustomerAddEditPage()
        {
            InitializeComponent();
        }

        public CustomerAddEditPage(Customer temp)
        {
            InitializeComponent();
            isEdit = true;
            index = temp.Id;
            Nametxt.Text = temp.Name;
            LNametxt.Text = temp.LastName;
            Phonetxt.Text = temp.PhoneNumber.ToString();
            Accounttxt.Text = temp.Account.ToString();
        }


        private void Closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Setbtn_Click(object sender, RoutedEventArgs e)
        {
            bool isvalid = Validation();
            if (isvalid)
            {
                if (isEdit)
                {
                    ulong.TryParse(Phonetxt.Text, out ulong p);
                    Decimal.TryParse(Accounttxt.Text, out Decimal a);
                    Customer temp = new Customer(index, Nametxt.Text, LNametxt.Text, p, a);

                    using (UnitOfWork db = new UnitOfWork())
                    {
                        db.cAccesData.Updte(temp);
                        db.Save();
                    }
                }
                else
                {
                    ulong.TryParse(Phonetxt.Text, out ulong p);
                    Decimal.TryParse(Accounttxt.Text, out Decimal a);

                    using (UnitOfWork db = new UnitOfWork())
                    {
                        Customer temp = new Customer(db.cAccesData.NextID(), Nametxt.Text, LNametxt.Text, p, a);
                        db.cAccesData.Add(temp);
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
                MessageBox.Show("لطفا نام مشتری را وارد کنید", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (!UInt64.TryParse(Phonetxt.Text, out ulong phone))
            {
                isvalid = false;
                MessageBox.Show("لطفا در قسمت موبایل فقط عدد وارد کنید", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (!Decimal.TryParse(Accounttxt.Text, out decimal account))
            {
                isvalid = false;
                MessageBox.Show("لطفا در قسمت حساب فقط عدد وارد کنید", "هشدار", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

            return isvalid;

        }
    }
}
