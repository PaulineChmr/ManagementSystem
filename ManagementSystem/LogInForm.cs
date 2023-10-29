using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementSystem
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private Hashtable ReadAccountDB()
        {
            Hashtable accountTable = new Hashtable();
            List<string> textFile = new List<string>();
            if(File.Exists("accountDB.txt"))
            {
                textFile = File.ReadAllLines("accountDB.txt").ToList();
                foreach (string line in textFile)
                {
                    string[] items = line.Split(',');
                    Account account = new Account(Convert.ToInt32(items[0]), Convert.ToInt32(items[1]), items[2], items[3], items[4], items[5], Convert.ToInt32(items[6]));
                    accountTable.Add(account.accountId, account);
                }
            }
            return accountTable;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hashtable accountTable = ReadAccountDB();
            try
            {
                if (accountTable.Contains(Convert.ToInt32(textId.Text)) && ((Account)accountTable[Convert.ToInt32(textId.Text)]).password == textPassword.Text)
                {
                    Account account = (Account)accountTable[Convert.ToInt32(textId.Text)];
                    int projectId = account.projectId;
                    switch (account.status)
                    {
                        case 0:
                            new ManagerDashBoardForm(Convert.ToInt32(textId.Text), projectId).Show();
                            this.Hide();
                            break;
                        case 1:
                            new CoworkerDashBoardForm(Convert.ToInt32(textId.Text), projectId).Show();
                            this.Hide();
                            break;
                    }



                }
                else
                {
                    MessageBox.Show("The User ID or password entered is incorrect, try again");
                    textId.Clear();
                    textPassword.Clear();
                    textId.Focus();
                }
            }
            catch
            {
                MessageBox.Show("The User ID or password entered is incorrect, try again");
                textId.Clear();
                textPassword.Clear();
                textId.Focus();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            new SignUpForm().Show();
            this.Hide();
        }
    }
}
