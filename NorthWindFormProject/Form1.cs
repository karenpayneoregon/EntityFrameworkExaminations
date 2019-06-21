using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseFirstNorthWindLibrary;
using Equin.ApplicationFramework;

namespace NorthWindFormProject
{
    
    public partial class Form1 : Form
    {
        //https://github.com/waynebloss/BindingListView
        private BindingListView<CustomerGridEdition> view;
        public Form1()
        {
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            using (var context = new NorthWindAzureContext())
            {
                var customers = context.Customers.Select(cust => new CustomerGridEdition {Id = cust.CustomerIdentifier, CompanyName = cust.CompanyName, ContactName = cust.ContactName,Country = cust.Country.CountryName}) .ToList();
                view = new BindingListView<CustomerGridEdition>(customers);
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = view;

            }
        }
    }

 
}
