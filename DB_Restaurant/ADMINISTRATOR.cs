using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Бази_данних_4___5
{
    public partial class ADMINISTRATOR : Form
    {
        public ADMINISTRATOR()
        {
            InitializeComponent();
            fillcombo();
            //comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }
        //connectionString = "server=localhost;user=root;database=test;password=legolas22102;";
        MySqlCommand command = new MySqlCommand();
        MySqlConnection connection = new MySqlConnection("server=localhost;user=root;database=restaurant;password=legolas22102;");

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
        void fillcombo()
        {          
            OpenCon();
            string select = "SELECT * FROM restaurant.products;";
            string select1 = "SELECT * FROM dishes;";
            MySqlCommand command = new MySqlCommand(select, connection);
           // MySqlCommand command = new MySqlCommand(select1, connection);

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string sName = reader.GetString("product_name");
                comboBox1.Items.Add(sName);
            }
            CloseCon();
            OpenCon();
            MySqlCommand command1 = new MySqlCommand(select1, connection);
            MySqlDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                string sName = reader1.GetString("dish_name");
                comboBox2.Items.Add(sName);
            }

            CloseCon();
        }

    private void button1_Click(object sender, EventArgs e)
        {
            
            string select = "SELECT*  FROM restaurant.provider;";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(select, connection);
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string select = "SELECT staff.Id_employee,First_name,Last_name, position_in_the_restaurant,salary  FROM restaurant.staff join restaurant.position_in_the_restaurant on staff.Id_employee = position_in_the_restaurant.Id_employee;";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(select, connection);
            adapter.Fill(table);
            dataGridView3.DataSource = table;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string select = "SELECT*  FROM restaurant.dishes;";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(select, connection);
            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string select = "SELECT*  FROM restaurant.reservation;";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(select, connection);
            adapter.Fill(table);
            dataGridView4.DataSource = table;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string select = "SELECT*  FROM restaurant.number_table;";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(select, connection);
            adapter.Fill(table);
            dataGridView5.DataSource = table;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string insertQuery = "insert into provider(provider.company_name, provider.location) values ('" + textBox1.Text + "','" + textBox2.Text + "')";
            string insertQuery1 = "insert into products(products.product_name,products.Id_provider) values('" + textBox3.Text + "',last_insert_id())";
            ExecuteQuery(insertQuery);
            ExecuteQuery(insertQuery1);
            //connToTableАптека();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string selectedState = comboBox1.SelectedItem.ToString();
            //MessageBox.Show(selectedState);

        }

        private void button7_Click(object sender, EventArgs e)
        {
                      
            string insertQuery = "insert into restaurant.dishes(dish_name,price,weight) values('" + textBox4.Text + "','" + textBox6.Text + "','" + Convert.ToInt32(textBox5.Text) + "')";
            ExecuteQuery(insertQuery);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string insertQuery = "insert into restaurant.number_table(number_table.number_of_seats,number_table.VIP_table) values('" + textBox8.Text + "','"+ textBox7.Text+"')";
            ExecuteQuery(insertQuery);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string insertQuery = "insert into staff (First_name,Last_name) values('" + textBox10.Text + "','"+ textBox9.Text + "')";
            string insertQuery1 = "insert into position_in_the_restaurant (Id_employee,position_in_the_restaurant,salary) values(last_insert_id(),'" + textBox11.Text + "','" + textBox12.Text + "')";
            ExecuteQuery(insertQuery);
            ExecuteQuery(insertQuery1);
        }

        private void button10_Click(object sender, EventArgs e)
        {

            string deleteQuery = "Delete  from staff where Id_employee = " + dataGridView3.CurrentCell.Value;
            //string deleteQuery1 = "Delete from position_in_the_restaurant where Id_employee = " + dataGridView3.CurrentRow.Index;
            ExecuteQuery(deleteQuery);


        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            string deleteQuery = "Delete  from provider where Id_provider = " + dataGridView1.CurrentCell.Value;
            //string deleteQuery1 = "Delete from position_in_the_restaurant where Id_employee = " + dataGridView3.CurrentRow.Index;
            ExecuteQuery(deleteQuery);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string deleteQuery = "Delete  from dishes where Id_dish = " + dataGridView2.CurrentCell.Value;
            //string deleteQuery1 = "Delete from position_in_the_restaurant where Id_employee = " + dataGridView3.CurrentRow.Index;
            ExecuteQuery(deleteQuery);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string deleteQuery = "Delete  from number_table where Id_table = " + dataGridView5.CurrentCell.Value;
            //string deleteQuery1 = "Delete from position_in_the_restaurant where Id_employee = " + dataGridView3.CurrentRow.Index;
            ExecuteQuery(deleteQuery);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            START_PAGE startForm = new START_PAGE();
            this.Hide();
            startForm.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string selectedState = comboBox1.SelectedItem.ToString();
            string selectedState1 = comboBox2.SelectedItem.ToString();
            string insertQuery = "insert into restaurant.composition_of_the_dish(Id_dish,Id_products) values((select Id_dish from restaurant.dishes where dish_name like'" + selectedState1 + "'),(select Id_products from restaurant.products where products.product_name like'" + selectedState + "'))";
            ExecuteQuery(insertQuery);
        }
    }

    /*/
    // строка подключения к БД
    string connStr = "server=localhost;user=root;database=test;password=legolas22102;";
    // создаём объект для подключения к БД
    MySqlConnection conn = new MySqlConnection(connStr);
    // устанавливаем соединение с БД
    conn.Open();
            // запрос
            string t = textBox1.Text;
    string sql = "INSERT INTO author(First_name, Last_name, year_of_birth) VALUES(" + t + " ,'Костенко', '1930-03-19' )";
    // объект для выполнения SQL-запроса
    MySqlCommand command = new MySqlCommand(sql, conn);
    // объект для чтения ответа сервера
    ySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                // элементы массива [] - это значения столбцов из запроса SELECT
                listBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString());
            }
reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();
            /*/
}
