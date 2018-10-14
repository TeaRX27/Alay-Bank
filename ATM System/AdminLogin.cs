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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }
        string username, password,encrusername;
        DateTime lockdate;
        public void Select_account(string cardnum)
        {
            string query = "select * from useraccounts";
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, CreateNewCard.conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt) == cardnum)
                        {
                            encrusername = dataReader[0].ToString();
                            username = EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt);
                        }
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

            query = "select password,lockdate from useraccounts where username ='" + encrusername + "'";
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, CreateNewCard.conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        password = EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt);
                        if(dataReader[1].ToString()!="")
                        {
                            lockdate = DateTime.Parse(EncryptDecrypt.DecryptString(dataReader[1].ToString(), CreateNewCard.salt));
                        }
                        else
                        {
                            lockdate = DateTime.Now;
                        }
                       
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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form splash = new Splash_Screen() ;
            splash.Show();
            this.Close();
        }

        int tries = 2;

        private void button1_Click(object sender, EventArgs e)
        {
            Select_account(textBox1.Text);
            if(username==textBox1.Text&&password==textBox2.Text)
            {
                if(DateTime.Compare(DateTime.Now,lockdate)>=0)
                {
                    CreateNewCard.Insert("update useraccounts set lockdate ='"+EncryptDecrypt.EncryptString(DateTime.Now.ToString("MM/dd/yy"),CreateNewCard.salt)+ "' where username = '" + encrusername +"'");
                    Form admin = new AdminMenu();
                    admin.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Account Is Locked please wait till " + lockdate + " or Contact a System Administrator to Unlock you Account", "Account Locked");
                }
               
            }
            else
            {
                if (tries == 0)
                {
                    MessageBox.Show("Your Tries Have Exceeded Allowed Tries\nYour Account is Temporarily Locked till "+DateTime.Now.AddDays(7).ToString("MM/dd/yy") , "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CreateNewCard.Insert("update useraccounts set lockdate ='" + EncryptDecrypt.EncryptString(DateTime.Now.AddDays(7).ToString("MM/dd/yy"), CreateNewCard.salt) + "' where username = '" + encrusername + "'");
                }
                else
                {
                    MessageBox.Show("Your Password is Incorrect!\nYou Have " + tries + " Left", "Password Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tries--;
                    textBox1.Clear();
                    textBox2.Clear();
                }
            }
        }
    }
}
