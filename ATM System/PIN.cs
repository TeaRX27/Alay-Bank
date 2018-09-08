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
namespace ATM_System
{
    public partial class PIN : Form
    {
        public static string transaction;
        public void CardBlock()
        {
            string query = "UPDATE card_list SET BLOCK = '"+EncryptDecrypt.EncryptString("True",CreateNewCard.salt)+"' where Card_No = '"+CardInsert.encrcardnum+"'";
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, CreateNewCard.conn);
                    cmd.ExecuteNonQuery();


                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    CreateNewCard.CloseConnection();
                }
            }
        }
        public PIN()
        {
            InitializeComponent();
        }
        int tries = 2;
        private void buttonOK_Click(object sender, EventArgs e)
        {
           
            if(textBox1.Text == CardInsert.PINCode)
            {
                
            }
            else
            {
                if(tries == 0 )
                {
                    MessageBox.Show("Your Tries Have Exceeded Allowed Tries\nYour Card Has Been Blocked", "CARD BLOCKED",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    CardBlock();
                    Application.ExitThread();
                }
                else
                {
                    MessageBox.Show("Your PIN is Incorrect!\nYou Have "+tries+" Left", "PIN INCORRECT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tries--;
                }
            }
        }

        private void buttonCLEAR_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void buttonCANCEL_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Transaction Cancelled");
            Application.ExitThread();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += button9.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += button8.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += button7.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += button6.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += button5.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += button4.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += button3.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += button2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += button1.Text;
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBox1.Text += button0.Text;
        }
    }
}
