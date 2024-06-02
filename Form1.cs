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


namespace Telephone_Diary_Management_System
{
    public partial class Form1 : Form
    {
        // Conection string of Database table
        SqlConnection conn=new SqlConnection("Data Source=KHALD\\SQLEXPRESS;Initial Catalog=ClientInfo;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";        //clearing the textBox
            textBox2.Clear();
            textBox3.Text = "";
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;  //when click the NEW button it will clearing the comboBox 
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();   // used it when Sql command is nedded 

            //Where we get the record from textBoxes in the database

            SqlCommand cmd = new SqlCommand(@"INSERT INTO CLIENT (First,Last,Mobile,Email,Catagory)    
				                            VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "')", conn);  // conn, Calling the connection String
            cmd.ExecuteNonQuery();     // calling the command
            conn.Close();
            MessageBox.Show("Successfully Saved..");
            Display();  
        }

        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from CLIENT", conn);  // returning the query,and calling the connection
            DataTable dt = new DataTable();

            sda.Fill(dt);        //Adds or refreshes rows in a specified range in the DataSet to match those in the data source using the DataSet and DataTable names.

            dataGridView1.Rows.Clear();    // before insert it will autometically clear the rows

            foreach (DataRow item in dt.Rows)     // getting the row by row value from dt and store it in the item
            {
                int n = dataGridView1.Rows.Add();         //while executing everytime loop will be increasing and new row will add here
                dataGridView1.Rows[n].Cells[0].Value = item["First"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns[0].HeaderCell.Style.Font = new Font("Tahoma", 12, FontStyle.Bold);  // Header FIle Font
            dataGridView1.Columns[1].HeaderCell.Style.Font = new Font("Tahoma", 12, FontStyle.Bold);
            dataGridView1.Columns[2].HeaderCell.Style.Font = new Font("Tahoma", 12, FontStyle.Bold);
            dataGridView1.Columns[3].HeaderCell.Style.Font = new Font("Tahoma", 12, FontStyle.Bold);
            dataGridView1.Columns[4].HeaderCell.Style.Font = new Font("Tahoma", 12, FontStyle.Bold);


            Display();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)       // select the whole Rows
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            
        }

        private void button3_Click(object sender, EventArgs e)  // for deleting the data 
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM CLIENT WHERE (Mobile = '" + textBox3.Text + "')",conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Deleted Data Successfully ..!");
            Display();


        }

        private void button4_Click(object sender, EventArgs e) // upadte 
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE CLIENT
                                       SET First ='" + textBox1.Text + "', Last ='" + textBox2.Text + "', Mobile ='" + textBox3.Text + "', Email ='" + textBox4.Text + 
                                       "', Catagory ='" + comboBox1.Text + "'      WHERE (Mobile = '" + textBox3.Text + "')", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("The data has been updated successfully..");
            Display();
        }

        private void textBox5_TextChanged(object sender, EventArgs e) // search
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from CLIENT      WHERE (Mobile like '%" + textBox5.Text + "%') or (First like '%" + textBox5.Text + "%') or (Last like '%" + textBox5.Text + "%')", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["First"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
