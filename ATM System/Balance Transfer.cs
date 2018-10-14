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
    public partial class Balance_Transfer : Form
    {
        string balance1,balance2,fullname,encrcardnum;
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
                        balance2 = EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt);
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
                        fullname = EncryptDecrypt.DecryptString(dataReader[1].ToString(), CreateNewCard.salt) + " " + EncryptDecrypt.DecryptString(dataReader[2].ToString(), CreateNewCard.salt);
                        iItem.SubItems.Add(fullname);

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
                        balance1 = EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt);

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
        string cardnum;
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
         
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                label1.Text = "Transfer Balance To :";
                cardnum = item.SubItems[0].Text;
                Select_Card(cardnum);
                label1.Text += cardnum;
                getbal();
            }
            Populate_ListView("select Card_No,FN,LN from card_list where Card_No != '" + CardInsert.encrcardnum + "'");
        }

        private void button2_Click(object sender, EventArgs e)
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

        public Balance_Transfer()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }

        private void Balance_Transfer_Load(object sender, EventArgs e)
        {
            Populate_ListView("select Card_No,FN,LN from card_list where Card_No != '" + CardInsert.encrcardnum+"'");
            getbal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(maskedTextBox1.Text) < Int32.Parse(balance1))
            {
                balance2 = (Int32.Parse(balance2) + Int32.Parse(maskedTextBox1.Text)).ToString();
                balance1 = (Int32.Parse(balance1) - Int32.Parse(maskedTextBox1.Text)).ToString();
                CreateNewCard.Insert("Update card_list set balance = '" + EncryptDecrypt.EncryptString(balance1, CreateNewCard.salt) + "' where Card_No = '" + CardInsert.encrcardnum + "'");
                CreateNewCard.Insert("Update card_list set balance = '" + EncryptDecrypt.EncryptString(balance2, CreateNewCard.salt) + "' where Card_No = '" + encrcardnum + "'");
                CreateNewCard.Initialize("server=localhost;uid=root;pwd=;database=alaybank_cards;sslmode=none;");
                CreateNewCard.Insert("Insert into alay" + CardInsert.cardnum + " (`trans_id`, `trans_details`) VALUES (NULL ,'" + EncryptDecrypt.EncryptString("Transferred Balance to "+cardnum+" on " + DateTime.Now, CreateNewCard.salt) + "');");
                CreateNewCard.Insert("Insert into alay" + cardnum + " (`trans_id`, `trans_details`) VALUES (NULL ,'" + EncryptDecrypt.EncryptString("Recieved Balance Transfer from "+CardInsert.cardnum+" on "+ DateTime.Now, CreateNewCard.salt) + "');");
                if (MessageBox.Show("Do you want another Transaction?", "Another Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            else
            {
                MessageBox.Show("Balance Transfer Failed! \n Insufficient Balance for Transfer");
                Form splash = new Splash_Screen();
                splash.Show();
                this.Close();
            }
        }
    }
}
