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
    public partial class START_PAGE : Form
    {
        public START_PAGE()
        {
            InitializeComponent();
        }
       
       
        private void button1_Click(object sender, EventArgs e)
        {

            ADMINISTRATOR AdminForm = new ADMINISTRATOR();
            if (textBox1.Text == "1" && textBox2.Text == "1") //"если" в поле1 значение "user" и если в поле2 значение "pass" P.S user - логин ; pass - пароль!!!
            {
                AdminForm.Show();
                this.Hide();

            }
            else //иначе
            {
                MessageBox.Show("Неверный пароль или логин", "Ошибка"); //вылазит ошибка о неверном пароле!
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CLIENT clientForm = new CLIENT();
            clientForm.Show();
            this.Hide();
        }
    }
}
