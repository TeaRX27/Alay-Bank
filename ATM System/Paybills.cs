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
    public partial class Paybills : Form
    {
        public Paybills()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Paybills_Load(object sender, EventArgs e)
        {
            Balance_Inquiry.getbalpoint();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(Balance_Inquiry.balance) >= Int32.Parse(Amount.Text))
            {
                int rbalance;
                rbalance = Int32.Parse(Balance_Inquiry.balance) - Int32.Parse(Amount.Text);
                if(Int32.Parse(Amount.Text) >= 1000)
                {
                    int newpoints = Int32.Parse(Balance_Inquiry.points) + 1;
                    CreateNewCard.Insert("Update card_list set ponts = '" + EncryptDecrypt.EncryptString(newpoints.ToString(), CreateNewCard.salt) + "' where Card_No = '" + CardInsert.encrcardnum + "'");
                }
                CreateNewCard.Insert("Update card_list set balance = '" + EncryptDecrypt.EncryptString(rbalance.ToString(), CreateNewCard.salt) + "' where Card_No = '" + CardInsert.encrcardnum + "'");
                CreateNewCard.Initialize("server=localhost;uid=root;pwd=;database=alaybank_cards;sslmode=none;");
                CreateNewCard.Insert("Insert into alay" + CardInsert.cardnum + " (`trans_id`, `trans_details`) VALUES (NULL ,'" + EncryptDecrypt.EncryptString("Paid bill on " + DateTime.Now, CreateNewCard.salt) + "');");
                MessageBox.Show("Balance Successfuly Updated\n Thank You for Using Alay Bank ATM System");

            }
            else
            {
                MessageBox.Show("Transaction cannot be processed!\n Insufficient Balance\n Thank you for using Alay Bank ATM");
            }
            Form splash = new Splash_Screen();
            splash.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (DialogResult.Yes == MessageBox.Show("Do you Want to Cancel Transacation?", "Cancel Transaction", MessageBoxButtons.YesNo))
            {
                MessageBox.Show("Transaction Canceled");
                Form Splash = new Splash_Screen();
                Splash.Show();
                this.Close();
            }
            else
            {

            }
        }
    }
}
