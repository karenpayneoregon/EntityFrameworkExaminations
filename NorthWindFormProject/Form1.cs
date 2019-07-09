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
using System.Data.Entity;

namespace NorthWindFormProject
{
    
    public partial class Form1 : Form
    {
        //https://github.com/waynebloss/BindingListView
        private BindingListView<Customer> view;
        public Form1()
        {
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            using (var context = new NorthWindAzureContext())
            {
                var customers = context.Customers.Select(cust => 
                    new CustomerSpecial
                    {
                        Id = cust.CustomerIdentifier,
                        CompanyName = cust.CompanyName,
                        ContactName = cust.ContactName,
                        Country = cust.Country.CountryName
                    }).ToList();

                view = new BindingListView<Customer>(customers);

                context.Customers.Load();
                var demo = context.Customers.Local.ToBindingList();

                var testcust = demo.FirstOrDefault(cust => cust.CustomerIdentifier == 3);
                testcust.CompanyName = "SSSSSSSS";
                Console.WriteLine();

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = view;

                

                //var likeResult = context.Customers.Where(c => DbFunctions.Like(c.City, "%on")).ToList();
                //Console.WriteLine();
                //likeResult = context.Customers.Where(c => DbFunctions.Like(c.City, "M%")).ToList();
                //Console.WriteLine();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tester = view;
            Console.WriteLine();

            
        }


    }

    public class CustomerSpecial
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Country { get; set; }
    }

   
}
