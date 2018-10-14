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
    public partial class ClaimRewards : Form
    {
        public ClaimRewards()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
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
                        iItem.SubItems.Add(EncryptDecrypt.DecryptString(dataReader[1].ToString(),CreateNewCard.salt));
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
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                if(Int32.Parse(Balance_Inquiry.balance) >= Int32.Parse(item.SubItems[1].Text))
                {

                    int newbal = Int32.Parse(Balance_Inquiry.balance) - Int32.Parse(item.SubItems[1].Text);
                    CreateNewCard.Insert("Update card_list set points = '" + EncryptDecrypt.EncryptString(newbal.ToString(), CreateNewCard.salt) + "' where Card_No = '" + CardInsert.encrcardnum + "'");
                    CreateNewCard.Initialize("server=localhost;uid=root;pwd=;database=alaybank_cards;sslmode=none;");
                    CreateNewCard.Insert("Insert into alay" + CardInsert.cardnum + " (`trans_id`, `trans_details`) VALUES (NULL ,'" + EncryptDecrypt.EncryptString("Claimed Reward on " + DateTime.Now, CreateNewCard.salt) + "');");
                    MessageBox.Show("Balance Successfuly Updated\n Thank You for Using Alay Bank ATM System");
                    CreateNewCard.Initialize();
                    CreateNewCard.Insert("insert into rewardprint values('" + item.SubItems[0].Text + "','" + BlockChangepin.build() + "')");
                    Form print = new Recieptprint();
                    Recieptprint.source = "reward";
                    print.ShowDialog();
                    if (MessageBox.Show("Do you want another Transaction?", "Another Transaction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Form mm = new Menu();
                        mm.Show();
                        this.Close();
                    }
                    else
                    {
                        CreateNewCard.Insert("Insert Into transrec values ('" + CardInsert.cardnum + "','" + newbal + " Points')");
                        MessageBox.Show("Thank You for Using Alay Bank ATM");
                        Form splash = new Splash_Screen();
                        splash.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Transaction cannot be processed!\n Insufficient Reward Points\n Thank you for using Alay Bank ATM");
                }
            }
            Form splashscreen = new Splash_Screen();
            splashscreen.Show();
            this.Hide();
        }

        private void ClaimRewards_Load(object sender, EventArgs e)
        {

            Balance_Inquiry.getbalpoint();
            Populate_ListView("select * from rewards");
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
    }
}
