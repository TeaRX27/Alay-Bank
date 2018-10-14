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
    public partial class CreateNewCard : Form
    {

        public static MySqlConnection conn;
        public static string myConnectionString;
        public static string query;
        public static string salt = "Alaykana";
        string cardnumdb = "";
        public static void Initialize(string connstr)
        {
            myConnectionString = connstr;
            conn = new MySqlConnection(myConnectionString);
        }

        public static void Initialize()
        {
            myConnectionString = "server=localhost;uid=root;pwd=;database=alaybank;";
            conn = new MySqlConnection(myConnectionString);
        }
        public static bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static void Insert(string myquery)
        {
            string query = myquery;
            if (OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        public static bool CloseConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void call(string cardnum)
        { 
            string query = "Select Card_NO from card_list where Card_NO = '" + EncryptDecrypt.EncryptString(cardnum, salt) + "'";
            if (OpenConnection())
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cardnumdb = EncryptDecrypt.DecryptString(reader.GetString("Card_NO"), salt);
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }
        private string build()
        {
            int length = 11;
            Random random = new Random();
            string numbers = "0123456789";
            StringBuilder cardno = new StringBuilder(length);
            for(int i = 0; i<=length;i++)
            {
                cardno.Append(numbers[random.Next(numbers.Length)]);
            }
            return cardno.ToString();
        }
        public CreateNewCard()
        {
            InitializeComponent();
            Initialize();
        }

        private void CreateNewCard_Load(object sender, EventArgs e)
        {
            string cardno = "";
            while (cardno == cardnumdb)
            {
                cardno = build();
                call(cardno);
            }
            PIN.Text = BlockChangepin.build();
            CardNo.Text = cardno;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PIN.MaskCompleted && FirstName.Text != "" && LastName.Text != ""&& PIN.Text != "911911")
            {
                Insert("Insert into card_list values ('" + EncryptDecrypt.EncryptString(CardNo.Text, salt) + "','" + EncryptDecrypt.EncryptString(PIN.Text, salt) + "','" +
                EncryptDecrypt.EncryptString(FirstName.Text, salt) + "', '" + EncryptDecrypt.EncryptString(MI.Text, salt) + "', '" +
                EncryptDecrypt.EncryptString(LastName.Text, salt) + "','" + EncryptDecrypt.EncryptString("500", salt)+"','" + EncryptDecrypt.EncryptString("0", salt)+"','"+
                EncryptDecrypt.EncryptString("False", salt)+"','"+ EncryptDecrypt.EncryptString(DateTime.Now.AddYears(3).ToString("MM/yy"), salt)+"')");
                Initialize("server=localhost;uid=root;pwd=;database=alaybank_cards;sslmode=none;");
                Insert("create table alay" + CardNo.Text + " (trans_id int(255) auto_increment,trans_details varchar(255) not null, primary key(trans_id));");
                MessageBox.Show("You Have Created A New Card Sucessfully!", "Card Creation Successful!");
                Form cardinsert = new AdminMenu();
                cardinsert.Show();
                Close();
            }

            else
            {
                string errormessage = "";
;                if (!PIN.MaskCompleted) 
                {
                    errormessage += "Pin Must not be less than 6\n";
                }
                 if(FirstName.Text =="")
                {
                    errormessage += "First Name must not be Empty\n";
                }
                 if(PIN.Text == "911911")
                {
                    errormessage += "Entered PIN is a Reserved Code\n";
                }
                 if(LastName.Text == "")
                {
                    errormessage += "Last Name must not be Empty";
                }
                MessageBox.Show(errormessage, "Card Creation Failed!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form cardinsert = new  AdminMenu();
            cardinsert.Show();
            Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void PIN_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
