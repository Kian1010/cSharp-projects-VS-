//Done by Kian Naidu, KNPMB070
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
    public partial class frmCars : Form
    {
         
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + 
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Hire.mdf" + @";Integrated Security=True");
        //Extracts filepath of the solution on the current device then checks for the database, 
        //so solution works on any computer its sent to.


        public frmCars()
        {
            InitializeComponent();
        }

        private void frmCars_Load(object sender, EventArgs e)
        {
          
            // TODO: This line of code loads data into the 'hireDataSet1.Cars' table. You can move, or remove it, as needed.
            this.carsTableAdapter.Fill(this.hireDataSet1.Cars);
            txtCount.Enabled = false;
            //gets lowermost and uppermost values of the rows from the database
            txtCount.Text = (carsBindingSource.Position + 1) + " of " + (carsBindingSource.Count);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //clears the textboxes and checkbox for writing new data
            txtDateRegistered.Clear();
            txtEngineSize.Clear();
            txtMake.Clear();
            txtRegNo.Clear();
            txtRentalPerDay.Clear();
            chkbxavaiable.Checked = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //hides,then closes this form after initilizing and opening the frmsearch form
            this.Hide();
            frmSearch search = new frmSearch();
            search.ShowDialog();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //exits the application, after confirmation with a nice goodbye message 
           if  (MessageBox.Show("Are you sure you want to exit","Confirm exit",MessageBoxButtons.YesNo,MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                MessageBox.Show("Thanks for viewing Bowman Car Hire, have a good day!");
                Application.Exit();
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //opens connection to the database to allow for sql queries, this one being a delete query
            con.Open();
            string query = "DELETE FROM Cars WHERE VehicleRegNo= '"+txtRegNo.Text+"' ";
            SqlDataAdapter sql = new SqlDataAdapter(query, con);
            sql.SelectCommand.ExecuteNonQuery();
            con.Close();
            bindingSource1.ResetBindings(true);
            this.hireDataSet1.Reset();
            this.carsTableAdapter.Fill(this.hireDataSet1.Cars);
            txtCount.Text = (carsBindingSource.Position + 1) + " of " + (carsBindingSource.Count);//refreshes position, if the dataset actually refreshes
            MessageBox.Show("Data successfully deleted");

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int available = 0; //assumes value is false/checkbox is unticked

            if (chkbxavaiable.Checked==true)
            {
                available = 1;//corrects value if checkbox is ticked
            }

            //opens connection to the database to allow for sql queries, this one being an insert query
            con.Open();
            string query = "INSERT INTO Cars(VehicleRegNo,Make,EngineSize,DateRegistered,RentalPerDay,Available) VALUES " +
            "('"+txtRegNo.Text+"','"+txtMake.Text+"','"+txtEngineSize.Text+"','"+txtDateRegistered.Text+"',"
            +double.Parse(txtRentalPerDay.Text)+", "+available+")";
            SqlDataAdapter sql = new SqlDataAdapter(query, con);
            sql.SelectCommand.ExecuteNonQuery();
            con.Close();
            //   bindingSource1.ResetBindings(true);//trying to refresh the data after being inserted,no method seemed to work,ignore this block of comments
            //    this.hireDataSet1.Reset();
            //  this.hireDataSet1.Cars.Clear();
            //this.carsTableAdapter.Fill(this.hireDataSet1.Cars);
            ///  bindingSource1.DataSource = null;
            ///    bindingSource1.DataSource = carsBindingSource;
            //      carsBindingSource.DataSource = null;
            ///    carsBindingSource.DataSource = hireDataSet1;
            txtCount.Text = (carsBindingSource.Position + 1) + " of " + (carsBindingSource.Count);
            MessageBox.Show("Data successfully added");


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // carsTableAdapter.Update(hireDataSet1.Cars); a method of trying to refresh dataset, doesn't seem to work either
            // hireDataSet1.Merge(hireDataSet1.Cars); 
            int available = 0; //assumes checkbox is not ticked
            if (chkbxavaiable.Checked == true)
            {
                available = 1;//if checkbox is ticked the value is corrected
            }

            if (txtRegNo.Text == "")
            {
                MessageBox.Show("Primary field data is not added");//ensures that at minimum the primary key has to be entered.
                txtRegNo.Focus();//sets focus on the reg no field so user has ease of typing the primary key field
                return;//breaks the application so the rest of the code does not run, but does not exit the application
            }


            //opens connection to the database to allow for sql queries, this one being an update query
            con.Open();
            string query = "UPDATE Cars SET Make= '"+txtMake.Text+
                "',EngineSize= '"+txtEngineSize.Text+"',DateRegistered= '"+txtDateRegistered.Text+
                "',RentalPerDay= "+double.Parse(txtRentalPerDay.Text)+",Available= "+available+
                " WHERE VehicleRegNo= '"+txtRegNo.Text+"' ";
            SqlDataAdapter sql = new SqlDataAdapter(query, con);
            sql.SelectCommand.ExecuteNonQuery();
            con.Close();
            this.hireDataSet1.Reset();
            this.carsTableAdapter.Fill(this.hireDataSet1.Cars);
            txtCount.Text = (carsBindingSource.Position + 1) + " of " + (carsBindingSource.Count);
            MessageBox.Show("Database successfully updated");
            

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //moves the pointer one forward and updates the value in the count textbox
            carsBindingSource.MoveNext();
            txtCount.Text = (carsBindingSource.Position + 1) + " of " + (carsBindingSource.Count);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            //moves the pointer one backward and updates the value in the count textbox
            carsBindingSource.MovePrevious();
            txtCount.Text = (carsBindingSource.Position + 1) + " of " + (carsBindingSource.Count);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            //moves the pointer to the first row and updates the value in the count textbox
            carsBindingSource.MoveFirst();
            txtCount.Text = (carsBindingSource.Position + 1) + " of " + (carsBindingSource.Count);
        }
   
        private void btnLast_Click(object sender, EventArgs e)
        {
            //moves the pointer to the last row and updates the value in the count textbox
            carsBindingSource.MoveLast();
            txtCount.Text = (carsBindingSource.Position + 1) + " of " + (carsBindingSource.Count);
          
        }
    }
}
