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
    public partial class DashBoardForm : Form
    {
        public int accountId;
        public int projectId;
        public DashBoardForm(int accountId, int projectId)
        {
            InitializeComponent();
            Hashtable accountTable = readAccountDB();
            Account account = (Account)accountTable[accountId];
            label3.Text = account.firstName +  " " + account.lastName;
            this.accountId = accountId;
            this.projectId = projectId;
            if(projectId == -1)
            {
                label4.Text = "You have no project assigned";
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                progressBar1.Value = 0;
                Button addProjectButton= new Button();
                addProjectButton.Text = "Add Project";
                addProjectButton.Size = new System.Drawing.Size(150, 51);
                addProjectButton.Location = new Point(440, 250);
                addProjectButton.Font = new Font("Microsoft Sans Serif", 13);
                this.Controls.Add(addProjectButton);
                addProjectButton.Click += (sender, e) =>
                {
                    new NewProjectForm(accountId).Show();
                    this.Hide();
                };
                label5.Text = "";
            }
            else
            {
                Project project = (Project)readProjectDB()[projectId];
                label4.Text = ("Project : " + project.name);
                Button addCowokerButton= new Button();
                addCowokerButton.Text = "Add Coworker";
                addCowokerButton.Size = new System.Drawing.Size(150, 51);
                addCowokerButton.Location = new Point(440, 250);
                addCowokerButton.Font = new Font("Microsoft Sans Serif", 13);
                this.Controls.Add(addCowokerButton);
                addCowokerButton.Click += (sender, e) =>
                {
                    new AddCoworkerForm(accountId, this.projectId).Show();
                    this.Hide();
                };
                Hashtable coworkerTable = readCoworkerDB();
                label5.Text = "";
                foreach (DictionaryEntry s in coworkerTable){
                    Coworker coworker = (Coworker) s.Value;
                    if (coworker.workerId != accountId)
                    {
                        label5.Text += coworker.firstName + " " + coworker.lastName + Environment.NewLine;
                    }
                }
                ArrayList taskList = readTaskDB();
                progressBar1.Minimum = 0;
                progressBar1.Maximum = taskList.Count;
                progressBar1.Value = (from Task task in taskList
                               where task.taskStatus == "done"
                               select task).Count();
                Button addTaskButton = new Button();
                addTaskButton.Text = "Add Task";
                addTaskButton.Size = new System.Drawing.Size(150, 51);
                addTaskButton.Location = new Point(10, 250);
                addTaskButton.Font = new Font("Microsoft Sans Serif", 13);
                this.Controls.Add(addTaskButton);
                addTaskButton.Click += (sender, e) =>
                {
                    new AddTaskForm(accountId, this.projectId).Show();
                    this.Hide();
                };
                foreach (Task task in taskList)
                {
                    listBox1.Items.Add(task.taskName + " " + ((Coworker)coworkerTable[task.accountId]).firstName + " " + ((Coworker)coworkerTable[task.accountId]).lastName + " " + task.taskStatus);
                }
                listBox1.SelectedIndexChanged += new EventHandler(ListBox_Click);
            }
        }

        private void ListBox_Click(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            if (listBox.SelectedIndex != -1)
            {
                new EditTaskForm(this.accountId, listBox.SelectedIndex, this.projectId).Show();
                this.Hide();
            }
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

        private Hashtable readProjectDB()
        {
            Hashtable projectTable = new Hashtable();
            List<string> textFile = new List<string>();
            if (File.Exists("projectDB.txt"))
            {
                textFile = File.ReadAllLines("projectDB.txt").ToList();
                foreach(string line in textFile)
                {
                    string[] items = line.Split(',');
                    Project project = new Project(Convert.ToInt32(items[0]), items[1], Convert.ToInt32(items[2]));
                    projectTable.Add(project.projectId, project);
                }
            }
            return projectTable;
        }

        private Hashtable readCoworkerDB()
        {
            Hashtable coworkerTable = new Hashtable();
            List<string> textFile = new List<string>();
            if (File.Exists("Project" + projectId + "/workerDB.txt"))
            {
                textFile = File.ReadAllLines("Project" + projectId + "/workerDB.txt").ToList();
                foreach (string line in textFile)
                {
                    string[] items = line.Split(',');
                    Coworker coworker = new Coworker(Convert.ToInt32(items[0]), Convert.ToInt32(items[1]), items[2], items[3], Color.FromName(items[4]));
                    coworkerTable.Add(coworker.workerId, coworker);
                }
            }
            return coworkerTable;
        }

        private ArrayList readTaskDB()
        {
            ArrayList taskList = new ArrayList();
            List<string> textFile = new List<string>();
            if (File.Exists("Project" + projectId + "/taskDB.txt"))
            {
                textFile = File.ReadAllLines("Project" + projectId + "/taskDB.txt").ToList();
                foreach (string line in textFile)
                {
                    string[] items = line.Split(',');
                    Task task = new Task(Convert.ToInt32(items[0]), Convert.ToInt32(items[1]), Convert.ToInt32(items[2]), items[3], items[4]);
                    taskList.Add(task);
                }
            }
            return taskList;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void DashBoardForm_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
