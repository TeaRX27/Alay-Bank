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
    public partial class Splash_Screen : Form
    {
        bool alaybank, alaycards;
        public static void createdb(string q)
        {
            string query = q;
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, CreateNewCard.conn);
                    cmd.ExecuteNonQuery();
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
        public void checkdb(string ID, string check)
        {
            string query = "show databases like '" + ID + "';";
            MySqlCommand command;
            if (CreateNewCard.OpenConnection())
            {
                try
                {
              
                        command = new MySqlCommand(query, CreateNewCard.conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (check == reader[0].ToString())
                        {
                            if (check == "alaybank")
                            {
                                alaybank = true;
                            }
                            if (check == "alaybank_cards")
                            {
                                alaycards = true;
                            }
                        }

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

        public Splash_Screen()
        {
            InitializeComponent();
            CreateNewCard.Initialize("server = localhost; uid = root; pwd =; sslmode = none; ");
          
        }
        string code;
        private void Splash_Screen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar != (char)Keys.Enter)
            {
                code += e.KeyChar;
            }
            
            if(e.KeyChar == (char)Keys.Enter)
            {
                if(code!="alayadmin")
                {
                    string expiry;
        Form CardInsert = new CardInsert();
                CardInsert.Show();
                this.Hide();
                }
                else 
                {
                    Form adminmenu = new AdminLogin();
                    adminmenu.Show();
                    this.Hide();
                }
            }
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label1.Visible == true)
            {
                label1.Visible = false;
            }
            else
            {
                label1.Visible = true;
            }
        }

        private void Splash_Screen_Load(object sender, EventArgs e)
        {
            
            checkdb("alaybank", "alaybank");
            if(!alaybank)
            {
                createdb("create database alaybank");
                CreateNewCard.Initialize();
                createdb("create table card_list (Card_No varchar(255) , PIN varchar(255) , FN varchar(255) , MI varchar(255) , LN varchar(255) , Balance varchar(255) , Points varchar(255) , Block varchar(255) , primary key(Card_No))");
            }
            checkdb("alaybank_cards", "alaybank_cards");
            if(!alaycards)
            {
                createdb("create database alaybank_cards");
            }
            timer1.Enabled = true;
            code = "";
            CreateNewCard.Initialize();
            CreateNewCard.Insert("Delete from rewardprint");
            CreateNewCard.Insert("Delete from transrec");
        }
    }
}
