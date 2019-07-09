using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseFirstNorthWindLibrary;
using Equin.ApplicationFramework;

namespace NorthWindFormProject
{
    public partial class Form2 : Form
    {
        private NorthWindAzureContext context = new NorthWindAzureContext();
        private BindingListView<Customer> view;
        public Form2()
        {
            InitializeComponent();

            Closing += Form2_Closing;
            Shown += Form2_Shown;
        }

        private void Form2_Shown(object sender, EventArgs e)
        {

            var customerList = context.Customers.ToList();
            view = new BindingListView<Customer>(customerList);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = view;

            dataGridView1.Columns["CompanyNameColumn"].DataPropertyName = "CompanyName";
            dataGridView1.Columns["CustomerIdentifierColumn"].DataPropertyName = "CustomerIdentifier";


        }

        private void Form2_Closing(object sender, CancelEventArgs e)
        {
            context.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var item = view.FirstOrDefault(c => c.CustomerIdentifier == 4);

            var originalEntity = context.Customers.AsNoTracking().FirstOrDefault(me => me.CustomerIdentifier == item.CustomerIdentifier);
            Console.WriteLine();
        }

        private void DisplayTrackedEntities(DbChangeTracker changeTracker)
        {
            Console.WriteLine("");

            var entries = changeTracker.Entries();
            foreach (var entry in entries)
            {
                Console.WriteLine("Entity Name: {0}", entry.Entity.GetType().FullName);
                Console.WriteLine("Status: {0}", entry.State);
                if (entry.State == EntityState.Modified)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine(context.ChangeTracker.Entries().Count());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!context.ChangeTracker.HasChanges()) return;

            var addedEntries = context.ChangeTracker.Entries().Where(data => data.State == EntityState.Added).ToList();

            var modifiedEntries = context.ChangeTracker.Entries().Where(data => data.State == EntityState.Modified).ToList();

            if (modifiedEntries.Count > 0)
            {
                for (int index = 0; index < modifiedEntries.Count; index++)
                {
                    var originalValues = modifiedEntries[index].OriginalValues;
                    
                    ShowValues(originalValues);
                    Console.WriteLine();
                    var currentValues = modifiedEntries[index].CurrentValues;
                    ShowValues(currentValues);
                   
                }
            }
            var deletedEntries = context.ChangeTracker.Entries().Where(data => data.State == EntityState.Deleted).ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Customer customer in view.DataSource)
            {
                var currentValue = context.Entry(customer).Property(x => x.CompanyName).CurrentValue;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Customer customer in view.DataSource)
            {
                var current = context.Entry(customer).Property(x => x.CompanyName).IsModified;
                if (current)
                {
                    Console.WriteLine($"{customer.CompanyName} - {context.Entry(customer).Property(x => x.CompanyName).OriginalValue}");
                }
            }
        }
        public static void ShowValues(DbPropertyValues values)
        {

            foreach (var propertyName in values.PropertyNames)
            {
                Console.WriteLine($"{propertyName} has value {values[propertyName]}");

            }
        }
        private void CheckIfDifferent(DbEntityEntry entry)
        {
            if (entry.State != EntityState.Modified)
                return;

            if (entry.OriginalValues.PropertyNames.Any(propertyName => !entry.OriginalValues[propertyName].Equals(entry.CurrentValues[propertyName])))
                return;

            (context as IObjectContextAdapter).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity).ChangeState(EntityState.Unchanged);
        }

    }

}
