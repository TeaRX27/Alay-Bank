﻿using System;
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
    public partial class Balance_Inquiry : Form
    {
        public static string balance,points;
        public static void getbalpoint()
        {
            string query = "select Balance,Points from card_list where Card_No ='" + CardInsert.encrcardnum + "'";
            if (CreateNewCard.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, CreateNewCard.conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        balance = EncryptDecrypt.DecryptString(dataReader[0].ToString(), CreateNewCard.salt);
                        points = EncryptDecrypt.DecryptString(dataReader[1].ToString(), CreateNewCard.salt);
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
        public Balance_Inquiry()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }

        private void Balance_Inquiry_Load(object sender, EventArgs e)
        {
            getbalpoint();
            label2.Text = balance;
            label4.Text = points;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form splashscreen = new Splash_Screen();
            splashscreen.Show();
            this.Close();
        }
    }
}
