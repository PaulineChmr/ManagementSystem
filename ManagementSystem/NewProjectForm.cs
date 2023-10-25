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
    public partial class NewProjectForm : Form
    {
        private int userId;
        public NewProjectForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Project project = new Project(textName.Text, userId);
            Hashtable accountTable = readAccountDB();
            Account user = (Account)accountTable[userId];
            user.AddProject(project.projectId);
            Directory.CreateDirectory("Project" + project.projectId);
            File.AppendAllText("Project" + project.projectId + "/workerDB.txt", user.accountId + "," + user.status + "," + user.firstName + "," + user.lastName + "," + Color.Red.Name + Environment.NewLine);
            MessageBox.Show("Your project : " + textName.Text + " is now created !");
            new ManagerDashBoardForm(userId, project.projectId).Show();
            this.Hide();
        }

        private Hashtable readAccountDB()
        {
            Hashtable accountTable = new Hashtable();
            List<string> textFile = new List<string>();
            if (File.Exists("accountDB.txt"))
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

        private void NewProjectForm_Load(object sender, EventArgs e)
        {

        }
    }
}
