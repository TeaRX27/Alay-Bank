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
    public partial class Renew : Form
    {
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
                            CardInsert.encrcardnum = dataReader[0].ToString();
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
        }
        public Renew()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }
        string fullname, expiry;
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
                        if (DateTime.Compare(DateTime.Now, DateTime.ParseExact("30/" + EncryptDecrypt.DecryptString(dataReader[3].ToString(), CreateNewCard
                            .salt), "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture)) >= 0)
                        {
                            iItem = new ListViewItem(EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt));
                            fullname = EncryptDecrypt.DecryptString(dataReader[1].ToString(), CreateNewCard.salt) + " " + EncryptDecrypt.DecryptString(dataReader[2].ToString(), CreateNewCard.salt);
                            iItem.SubItems.Add(fullname);
                            listView1.Items.Add(iItem);
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
    
            if(cardnum!="")
            {
            CreateNewCard.Insert("Update card_list set expiry = '" + EncryptDecrypt.EncryptString(DateTime.Now.AddYears(3).ToString("MM/yy"), CreateNewCard.salt) + "' where Card_No = '" + CardInsert.encrcardnum + "'");
            MessageBox.Show("Card Successfully Renewed!");
            Populate_ListView("select Card_No, FN,LN,Expiry from card_list");
            cardnum = "";
            CardInsert.encrcardnum = "";
               label1.Text = "Expired Card Selected: ";
            }
            else
            {
                MessageBox.Show("No Card Selected", "Renew Failed!");
            }
          
        }
        string cardnum;
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                label1.Text = "Expired Card Selected: ";
                cardnum = item.SubItems[0].Text;
                Select_Card(cardnum);
                label1.Text += cardnum;
            }
            Populate_ListView("select Card_No, FN,LN,Expiry from card_list where Card_No != '" + CardInsert.encrcardnum + "'");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form admin = new AdminMenu();
            admin.Show();
            this.Close();
        }

        private void Renew_Load(object sender, EventArgs e)
        {
            Populate_ListView("select Card_No, FN,LN,Expiry from card_list");
        }
    }
}
