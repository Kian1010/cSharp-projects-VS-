//Done by Kian Naidu, KNPMB070.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace CarsDatabase
{
    public partial class frmSearch : Form
    {
        
        public frmSearch()
        {
            InitializeComponent();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hireDataSet1.Cars' table. You can move, or remove it, as needed.
            this.carsTableAdapter.Fill(this.hireDataSet1.Cars);
            // TODO: This line of code loads data into the 'hireDataSet1.Cars' table. You can move, or remove it, as needed.
            this.carsTableAdapter.Fill(this.hireDataSet1.Cars);

            //declares 2 arrays, so the values for each combobox can be inputted in for loops efficiently. 
            string[] ArrField, ArrOperator;
            ArrField = new string[4] { "Make", "EngineSize", "RentalPerDay", "Available" };
            ArrOperator = new string[5] { "=", "<", ">", "<=", ">=" };

            for (int count = 0; count < 4; count++)
            {

                cboField.Items.Add(ArrField[count]);//adds items from arrfield
            }

            for (int count = 0; count < 5; count++)
            {
                cboOperator.Items.Add(ArrOperator[count]);//adds items from arroperator
            }
            //links the gridview with the bindingsource from frmCars.
            DataGridView.DataSource = carsBindingSource;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //hides this form, then closes it after frmcars is declared and opened.
            this.Hide();
            frmCars cars = new frmCars();
            cars.ShowDialog();
            this.Close();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            //ensures all values are not null,else breaks the application with an error message,stopping the rest of the code from being processed.
            if ((cboField.Text == "") && (cboOperator.Text == "") && (txtValue.Text == ""))
            {
                MessageBox.Show("All criteria must be inputted before getting a result");
                return;
            }

            //if statement checks if the values are Make/EngineSize,then uses a query with single quotes around the criteria needed for strings,
            //else if the values aren't Make/EngineSize then a string is used with no single quotes for integer and other number only type variables.
            if ( (cboField.Text == "Make") || (cboField.Text == "EngineSize") )
            {
                carsBindingSource.Filter = cboField.Text + cboOperator.Text + "'"+txtValue.Text+"'";
            }
            else
            {
                //only allows 1 or 0 if available is selected in combobox
                if ( (cboField.Text == "Available") && (txtValue.Text != "1" || txtValue.Text != "0") )
                {
                    MessageBox.Show("You can only enter 1 or 0 for the search criteria of available");
                    return;
                }

                //checks if textbox only contains floating point value
                double isnumber;
                if (!double.TryParse(txtValue.Text, out isnumber))
                {
                    MessageBox.Show("You can only enter numbers/decimals for this field");
                    return;
                }

                //filters based on criteria using binding source functionality
                carsBindingSource.Filter = cboField.Text + cboOperator.Text + txtValue.Text;
            }
            MessageBox.Show("Data successfully filtered");


        }

    }
}
