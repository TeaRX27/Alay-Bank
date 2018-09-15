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
    public partial class Withdraw : Form
    {
        string balance;
        public void Getbalance()
        {
            string query = "select balance from card_list where Card_No ='" + CardInsert.encrcardnum + "'";
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
        public Withdraw()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Withdraw_Load(object sender, EventArgs e)
        {
            Getbalance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Int32.Parse(maskedTextBox1.Text) > Int32.Parse(balance))
            {
                MessageBox.Show("Insuffient Balance\n Please Try Again");
                Application.Restart();
            }
            else
            {
                string newbalance = (Int32.Parse(balance) - Int32.Parse(maskedTextBox1.Text)).ToString();
                CreateNewCard.Insert("Update card_list set balance = '"+EncryptDecrypt.EncryptString(newbalance,CreateNewCard.salt)+"' where Card_No = '" + CardInsert.encrcardnum+"'");
                MessageBox.Show(CardInsert.cardnum);
                CreateNewCard.Initialize("server=localhost;uid=root;pwd=;database=alaybank_cards;sslmode=none;");
                CreateNewCard.Insert("Insert into alay" + CardInsert.cardnum + " (`trans_id`, `trans_details`) VALUES (NULL ,'" + EncryptDecrypt.EncryptString("Withdrew on " + DateTime.Now, CreateNewCard.salt) + "');");
                MessageBox.Show("Balance Successfuly Updated\n Thank You for Using Alay Bank ATM System");
                Form Insertcard = new CardInsert();
                Insertcard.Show();
                this.Hide();
            }
        }
    }
}
