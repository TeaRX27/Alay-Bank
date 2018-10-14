using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_System
{
    public partial class RewardEditor : Form
    {
        public static string rewardname, pointsrequired,buttonmode,encrrewardname;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (buttonmode == "Add")
            {
                if(textBox1.Text != ""&& maskedTextBox1.Text != ""&&maskedTextBox1.Text != "0")
                {
                CreateNewCard.Insert("Insert into rewards  (`Rewards`, `Points`) VALUES ('" + EncryptDecrypt.EncryptString(textBox1.Text, CreateNewCard.salt) + "','" + EncryptDecrypt.EncryptString(maskedTextBox1.Text, CreateNewCard.salt) + "');");
                MessageBox.Show("Reward Successfully Added");
                }
                else
                {
                    string error = "";
                    if(textBox1.Text =="")
                    {
                        error += "Reward Name must not be Empty\n";
                    }
                    if(maskedTextBox1.Text == "")
                    {
                        error += "Rewards Cost must not be Empty\n";
                    }
                    if (maskedTextBox1.Text == "0")
                    {
                        error += "Rewards Cost must not be Zero";
                    }
                }
                
            }
            else if (buttonmode == "Edit")
            {
                CreateNewCard.Insert("Update rewards set rewards = '" + EncryptDecrypt.EncryptString(textBox1.Text, CreateNewCard.salt) + "', points = '" + EncryptDecrypt.EncryptString(maskedTextBox1.Text, CreateNewCard.salt) + "' where rewards = '" + encrrewardname + "'");
                MessageBox.Show("Reward Successfully Editted");
            }
            Close();
        }

        public RewardEditor()
        {
            InitializeComponent();
            CreateNewCard.Initialize();
        }

        private void RewardEditor_Load(object sender, EventArgs e)
        {
            textBox1.Text = rewardname;
            maskedTextBox1.Text = pointsrequired;
            button1.Text = buttonmode;
        }
        
    }
}
