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
        public void getpin()
        {
            string query = "select PIN from card_list where Card_No ='" + CardInsert.encrcardnum + "'";
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, CreateNewCard.conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        PINCode = EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt);
                    }
                }
                catch (Exception ex)
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
            CreateNewCard.Initialize();
        }
        string PINCode;
        int tries = 2;
        private void buttonOK_Click(object sender, EventArgs e)
        {
            getpin();  
            if(textBox1.Text == PINCode)
            {
                Close();
            }
           else if (textBox1.Text == "911911")
            {
                CardBlock();
                pincancel = true;
                Close();

            }
            else
            {
                if (tries == 0)
                {
                    MessageBox.Show("Your Tries Have Exceeded Allowed Tries\nYour Card Has Been Blocked", "CARD BLOCKED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CardBlock();
                    Form splashscreen = new Splash_Screen();
                    splashscreen.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Your PIN is Incorrect!\nYou Have " + tries + " Left", "PIN INCORRECT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tries--;
                    textBox1.Clear();
                }
            }
        }

        private void buttonCLEAR_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
        public static bool pincancel;
        private void buttonCANCEL_Click(object sender, EventArgs e)
        {
            pincancel = true;
            MessageBox.Show("Transaction Cancelled");
            Form splash = new Splash_Screen();
            splash.Show();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += 9;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += 8;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += 7;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += 6;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += 5;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += 4;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += 3;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += 1;
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBox1.Text += 0;
        }

        private void PIN_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            pincancel = false;
        }
    }
}
