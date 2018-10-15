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
    public partial class Balance_Inquiry : Form
    {
        public static string balance,points;
        public static void getbalpoint()
        {
            string query = "select Balance,Points from card_list where Card_No ='" + CardInsert.encrcardnum + "'";
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, CreateNewCard.conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        balance = EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt);
                        points = EncryptDecrypt.DecryptString(dataReader[1].ToString(), CreateNewCard.salt);
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
        public Balance_Inquiry()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }

        private void Balance_Inquiry_Load(object sender, EventArgs e)
        {
            getbalpoint();
            label2.Text = balance;
            label3.Text = points;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want another Transaction?", "Another Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Form mm = new Menu();
                mm.Show();
                this.Close();
            }
            else
            {
                CreateNewCard.Initialize();
                CreateNewCard.Insert("Insert Into transrec values ('" + CardInsert.cardnum + "','" + balance + "')");
                Form print = new Recieptprint();
                Recieptprint.source = "cash";
                print.ShowDialog();
                MessageBox.Show("Thank You for Using Alay Bank ATM");
                Form splash = new Splash_Screen();
                splash.Show();
                this.Close();
            }

        }
    }
}
