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
                        if (DateTime.Compare(DateTime.Now, DateTime.ParseExact("30/" + EncryptDecrypt.DecryptString(dataReader[4].ToString(), CreateNewCard
                            .salt), "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture)) >= 0)
                        {
                            iItem = new ListViewItem(EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt));
                            fullname = EncryptDecrypt.DecryptString(dataReader[1].ToString(), CreateNewCard.salt) + " " + EncryptDecrypt.DecryptString(dataReader[2].ToString(), CreateNewCard.salt);
                            expiry = EncryptDecrypt.DecryptString(dataReader[4].ToString(), CreateNewCard.salt);
                            iItem.SubItems.Add(fullname);
                            iItem.SubItems.Add(expiry);
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

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void Renew_Load(object sender, EventArgs e)
        {
            Populate_ListView("select Card_No, FN,LN,Expiry from card_list");
        }
    }
}
