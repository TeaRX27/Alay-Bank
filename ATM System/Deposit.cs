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
    public partial class Deposit : Form
    {

        public void Populate_ListView(string myquery)
        {
            listView1.Items.Clear();
            ListViewItem iItem;
            string query = myquery;
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, CreateNewCard.conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        iItem = new ListViewItem(EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt));
                        listView1.Items.Add(iItem);
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
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }
        public Deposit()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }
        public string encrcardnum,balance,cardnum;
        public void Select_Card(string cardnum)
        {
            listView1.Items.Clear();
            string query = "select Card_No from card_list";
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
                            encrcardnum = dataReader[0].ToString();
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
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

            query = "select Balance from card_list where Card_No ='" + encrcardnum + "'";
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
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((maskedTextBox1.Text != ""||maskedTextBox1.Text != "0") && cardnum != "")
            {
                string newbalance = (Int32.Parse(balance) + Int32.Parse(maskedTextBox1.Text)).ToString();
                CreateNewCard.Insert("Update card_list set balance = '" + EncryptDecrypt.EncryptString(newbalance, CreateNewCard.salt) + "' where Card_No = '" + encrcardnum + "'");
                CreateNewCard.Initialize("server=localhost;uid=root;pwd=;database=alaybank_cards;sslmode=none;");
                CreateNewCard.Insert("Insert into alay" + cardnum + " (`trans_id`, `trans_details`) VALUES (NULL ,'" + EncryptDecrypt.EncryptString("Deposit on " + DateTime.Now, CreateNewCard.salt) + "');");
                MessageBox.Show("Balance Successfuly Updated");
                maskedTextBox1.Clear();
                cardnum = "";
                label1.Text = "Deposit To: ";
                encrcardnum = "";
            }
            else
            {
                string error = "";
                if(maskedTextBox1.Text == "" || maskedTextBox1.Text == "0")
                {
                    error += "Deposit Amount must be greater than 0\n";
                }
                if(cardnum!="")
                {
                    error += "No Card Selected";
                }
                MessageBox.Show(error, "Deposit Cannot Be Processed");
            }
        }

        private void listView1_DoubleClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                label1.Text = "Deposit To :";
                cardnum = item.SubItems[0].Text;
                Select_Card(cardnum);
                label1.Text += cardnum;
                Populate_ListView("select Card_No from card_list");
                balance = "";
            }
            Populate_ListView("select Card_No from card_list where Card_No != '" + encrcardnum + "'");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form Admin = new AdminMenu();
            Admin.Show();
            this.Close();
        }

        private void Deposit_Load(object sender, EventArgs e)
        {
            Populate_ListView("select Card_No from card_list");
        }
    }
}
