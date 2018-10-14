using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_System
{
    public partial class ChangePin : Form
    {
        public ChangePin()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += 6;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += 7;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += 8;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += 9;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            CreateNewCard.Insert("Update card_list set PIN = '" + EncryptDecrypt.EncryptString(textBox1.Text, CreateNewCard.salt) + "' where Card_No = '" + CardInsert.encrcardnum + "'");
            MessageBox.Show("PIN sucessfully changed!");
            if(MessageBox.Show("Do you want another Transaction?","Another Transaction",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                Form mm = new Menu();
                mm.Show();
                this.Close();
            }
            else
            {
               
            MessageBox.Show("Thank You for Using Alay Bank ATM");
            Form splash = new Splash_Screen();
            splash.Show();
            this.Close();
            }
        }

        private void buttonCLEAR_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void buttonCANCEL_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Change PIN cancel!");
            Form splash = new Splash_Screen();
            splash.Show();
            this.Close();
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBox1.Text += 0;
        }
    }
}
