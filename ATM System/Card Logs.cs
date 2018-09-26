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
    public partial class Card_Logs : Form
    {
        public void Card_Data()
        {
            string query = "SELECT Card_No from card_list";
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, CreateNewCard.conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Card.Items.Add(EncryptDecrypt.DecryptString(reader[0].ToString(), CreateNewCard.salt));
                    }
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
        string transdetails;
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
                        iItem = new ListViewItem(dataReader[0].ToString());
                        transdetails = EncryptDecrypt.DecryptString(dataReader[1].ToString(), CreateNewCard.salt);
                        iItem.SubItems.Add(transdetails);
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
        public Card_Logs()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }

        private void Card_Logs_Load(object sender, EventArgs e)
        {
            Card_Data();
            Card.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Card.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateNewCard.Initialize("server=localhost;uid=root;pwd=;database=alaybank_cards;sslmode=none;");
            Populate_ListView("select * from alay" + Card.SelectedItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form Admin = new AdminMenu();
            Admin.Show();
            this.Close();
        }
    }
}
