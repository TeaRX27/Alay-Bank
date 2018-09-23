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
    public partial class Edit_Rewards : Form
    {
        public void Select_Reward(string rewardname)
        {
            string query = "select rewards from rewards";
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, CreateNewCard.conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt) == rewardname)
                        {
                            RewardEditor.encrrewardname = dataReader[0].ToString();
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

            query = "select * from rewards where rewards ='" + RewardEditor.encrrewardname + "'";
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, CreateNewCard.conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        RewardEditor.rewardname = EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt);
                        RewardEditor.pointsrequired = EncryptDecrypt.DecryptString(dataReader[1].ToString(), CreateNewCard.salt);
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
        string pointrqrd;
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
                        pointrqrd = EncryptDecrypt.DecryptString(dataReader[1].ToString(), CreateNewCard.salt);
                        iItem.SubItems.Add(pointrqrd);
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
        public Edit_Rewards()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            RewardEditor.buttonmode = "Add";
            RewardEditor.rewardname = "";
            RewardEditor.pointsrequired = "";
            RewardEditor.encrrewardname = "";
            Form editor = new RewardEditor();
            editor.ShowDialog();
            Populate_ListView("select * from rewards");
        }

        private void Edit_Events_Load(object sender, EventArgs e)
        {
            Populate_ListView("select * from rewards");
        }

        private void button3_Click(object sender, EventArgs e)
        {     foreach(ListViewItem item in listView1.SelectedItems)
            {
                Select_Reward(item.SubItems[0].Text);
            }
            if(RewardEditor.encrrewardname != "")
            {
            
            RewardEditor.buttonmode = "Edit";
            Form editor = new RewardEditor();
            editor.ShowDialog();
            Populate_ListView("select * from rewards");
            }
            else
            {
                MessageBox.Show("No Item Selected");
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                Select_Reward(item.SubItems[0].Text);
            }
            CreateNewCard.Insert("Delete from rewards where rewards = '" + RewardEditor.encrrewardname+"'");
            Populate_ListView("select * from rewards");
            RewardEditor.encrrewardname = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form admin = new AdminMenu();
            admin.Show();
            this.Close();
        }
    }
}
