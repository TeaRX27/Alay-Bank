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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
            Form PINs = new PIN();
            PINs.ShowDialog();
            if (!PIN.pincancel)
            {
            Form transfer = new Balance_Transfer();
            Close();
            transfer.Show();
            }
            else if (PIN.pincancel)
            {
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            Form PINs = new PIN();
            PINs.ShowDialog();
           
            if(!PIN.pincancel)
            {
            Form withdraw = new Withdraw();
            Close();
            withdraw.Show();
            }
            else if (PIN.pincancel)
            {
                Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form PINs = new PIN();
            PINs.ShowDialog();
            if(!PIN.pincancel)
            {
                Form pay = new Paybills();
                Close();
                pay.Show();
            }
            else if (PIN.pincancel)
            {
                Close();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
           
            Form PINs = new PIN();
            PINs.ShowDialog();
            if (!PIN.pincancel)
            {
                Form rewards = new ClaimRewards();
                Close();
                rewards.Show();
            }
            else if(PIN.pincancel)
            {
                Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            Form PINs = new PIN();
            PINs.ShowDialog();
            if(!PIN.pincancel)
            {
                Form changepin = new ChangePin();
                Close();
                changepin.Show();
            }
            else if (PIN.pincancel)
            {
                Close();
            }
        }

        private void Inquiry_Click(object sender, EventArgs e)
        {
            Form PINins = new PIN();
            PINins.ShowDialog();
            if(!PIN.pincancel)
            {
            Close();
            Form inquiry = new Balance_Inquiry();
            inquiry.Show();
            }
            else if (PIN.pincancel)
            {
                Close();
            }
        }
    }
}
