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
         string balance;
        public void getbal()
        {
            string query = "select Balance from card_list where Card_No ='" + CardInsert.encrcardnum + "'";
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, CreateNewCard.conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        balance = EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt);
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
            getbal();
            label2.Text = balance;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form Insertcard = new CardInsert();
            Insertcard.Show();
            this.Hide();
        }
    }
}
