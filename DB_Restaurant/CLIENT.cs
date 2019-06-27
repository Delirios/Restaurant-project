using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Бази_данних_4___5
{
    public partial class CLIENT : Form
    {
        public CLIENT()
        {
            InitializeComponent();
            fillcombo();
            //admin.Show();

        }
        START_PAGE startForm = new START_PAGE();
        //ADMINISTRATOR admin = new ADMINISTRATOR();
        MySqlConnection connection = new MySqlConnection("server=localhost;user=root;database=restaurant;password=legolas22102;");
        MySqlCommand command = new MySqlCommand();
        public void OpenCon()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseCon()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public void ExecuteQuery(string q)
        {
            try
            {
                OpenCon();
                command = new MySqlCommand(q, connection);
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Query Executed");
                }
                else
                {
                    MessageBox.Show("Query Not Executed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseCon();
            }
        }


        public void fillcombo()
        {
            OpenCon();
            string select = "SELECT * FROM number_table WHERE Id_table NOT IN(SELECT Id_table FROM reservation)";
           
            

            MySqlCommand command = new MySqlCommand(select, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string table = reader.GetString("number_of_seats");
               
                comboBox1.Items.Add(table);
                comboBox2.Items.Add(table);
                //comboBox3.Items.Add(dish);
            }
            CloseCon();
            OpenCon();
            string select1 = "SELECT * FROM restaurant.dishes";
            MySqlCommand command1 = new MySqlCommand(select1, connection);
            MySqlDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                string dish = reader1.GetString("dish_name");
                comboBox3.Items.Add(dish);
            }
            CloseCon();
            OpenCon();
            string name = "%ціант%";
            string select2 = "SELECT position_in_the_restaurant FROM position_in_the_restaurant where position_in_the_restaurant like '"+ name +"' ";
            MySqlCommand command2 = new MySqlCommand(select2, connection);
            MySqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                string employee = reader2.GetString("position_in_the_restaurant");
                comboBox4.Items.Add(employee);
            }
            CloseCon();
        }
        private void button1_Click(object sender, EventArgs e)      
        {
            string selectedState = comboBox1.SelectedItem.ToString();
            string insertQuery1 = "insert into restaurant.сlient(First_name, Last_name) values('" + textBox1.Text + "','" + textBox2.Text + "')";
            string insertQuery = "insert into restaurant.reservation(Id_table,Id_client, data_reservation,time_reservation) values((select Id_table from number_table where number_table.number_of_seats like'" + Convert.ToInt32(selectedState) + "'),last_insert_id(),'" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm") + "', '" +  Convert.ToInt32(textBox3.Text)+"')";
            ExecuteQuery(insertQuery1);
            ExecuteQuery(insertQuery);
            /*/
            string insertQuery = "insert into nu(First_name,Last_name) values('" + textBox10.Text + "','" + textBox9.Text + "')";
            
            admin.ExecuteQuery(insertQuery);
            /*/
        }
        
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenCon();
            string select = "select price from dishes where  dish_name like '" + comboBox3.Text + "'";
            MySqlCommand command = new MySqlCommand(select, connection);
            MySqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                string price = reader.GetString("price").ToString();
                textBox4.Text = price;
            //comboBox3.Items.Add(dish);
            }
            CloseCon();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            startForm.Show();
        }
    }
}
