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
    public partial class CardInsert : Form
    { string fullname;
        public static string PINCode;
        public static int Balance, Points;
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
                        encrcardnum = dataReader[0].ToString();
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
        public static string encrcardnum;
        string cardblock;
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
                       if (EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt) == cardnum )
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

            query = "select Block from card_list where Card_No ='"+encrcardnum+"'";
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, CreateNewCard.conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        cardblock = EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt);
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
        public CardInsert()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }
        public static string cardnum;
        private void button1_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listView1.SelectedItems)
            {
                 cardnum = item.SubItems[0].Text;
                Select_Card(cardnum);
                if(cardblock == "False")
                {
                    Form Menu = new Menu();
                    Menu.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Your Card Has been Blocked\nPlease Contact Your Bank to Unblock your Card", "Blocked Card Inserted");
                    Form splashscreen = new Splash_Screen();
                    splashscreen.Show();
                    this.Hide();
                }
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Populate_ListView("select Card_No,FN,LN from card_list");
            encrcardnum = null;
            cardblock = "True";
            TopMost = true;
            TopMost = false;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Form adminmenu = new AdminMenu();
            adminmenu.Show();
            Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form admin = new AdminMenu();
            Hide();
            admin.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           if(MessageBox.Show("Do you want to Close the System?", "Do you want to Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageBox.Show("Thank you for Using Alay Bank Atm System");
                Application.ExitThread();
            }
   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form newcard = new CreateNewCard();
            newcard.Show();
            Hide();
        }
    }
}
