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
    public partial class AdminMenu : Form
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form deposit = new Deposit();
            deposit.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form editreward = new Edit_Rewards();
            editreward.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form Splash = new Splash_Screen();
            Splash.Show();
            this.Close();
        }
        private void newcard_Click(object sender, EventArgs e)
        {
            Form newcard = new CreateNewCard();
            newcard.Show();
            Hide();
        }

        private void AdminMenu_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form cardlog = new Card_Logs();
            cardlog.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form block = new Block_Card();
            block.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form changeblockpin = new BlockChangepin();
            changeblockpin.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form renew = new Renew();
            renew.Show();
            this.Close();
        }
    }
}
