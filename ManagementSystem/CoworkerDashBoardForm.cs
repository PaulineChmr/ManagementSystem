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
    public partial class CoworkerDashBoardForm : Form
    {
        public int accountId;
        public int projectId;
        public CoworkerDashBoardForm(int accountId, int projectId)
        {
            InitializeComponent();
            Hashtable accountTable = readAccountDB();
            Account account = (Account)accountTable[accountId];
            label3.Text = account.firstName + " " + account.lastName;
            this.accountId = accountId;
            this.projectId = projectId;
            Project project = (Project)readProjectDB()[projectId];
            label4.Text = (project.name);
            Hashtable coworkerTable = readCoworkerDB();
            label3.ForeColor = ((Coworker)coworkerTable[this.accountId]).color;
            List<Coworker> coworkerList = new List<Coworker>();
            foreach (DictionaryEntry s in coworkerTable)
            {
                Coworker coworker = (Coworker)s.Value;
                if (coworker.workerId != accountId)
                {
                    coworkerList.Add(coworker);
                }
            }
            listBox2.DataSource = coworkerList;
            listBox2.DrawMode = DrawMode.OwnerDrawFixed;
            listBox2.DrawItem += new DrawItemEventHandler(ListBox2_DrawItem);
            List<Task> taskList = readTaskDB();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = taskList.Count;
            progressBar1.Value = (from Task task in taskList
                                  where task.taskStatus == "done"
                                  select task).Count();
            //listBox of tasks
            listBox1.DataSource = taskList.Where(x => x.taskStatus == "to do").ToList();
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);
            listBox1.SelectedIndexChanged += new EventHandler(ListBox1_Click);

            listBox3.DataSource = taskList.Where(x => x.taskStatus == "in progress").ToList();
            listBox3.DrawMode = DrawMode.OwnerDrawFixed;
            listBox3.DrawItem += new DrawItemEventHandler(ListBox3_DrawItem);
            listBox3.SelectedIndexChanged += new EventHandler(ListBox3_Click);

            listBox4.DataSource = taskList.Where(x => x.taskStatus == "done").ToList();
            listBox4.DrawMode = DrawMode.OwnerDrawFixed;
            listBox4.DrawItem += new DrawItemEventHandler(ListBox4_DrawItem);
            listBox4.SelectedIndexChanged += new EventHandler(ListBox4_Click);

        }

        private void ListBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index >= 0)
            {
                Color itemColor = ((Coworker)listBox2.Items[e.Index]).color;
                using (Brush brush = new SolidBrush(itemColor))
                {
                    e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                    e.Graphics.DrawString(((Coworker)listBox2.Items[e.Index]).GetName(), this.Font, brush, e.Bounds, StringFormat.GenericDefault);
                }
            }

            e.DrawFocusRectangle();
        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Hashtable coworkerTable = readCoworkerDB();
            e.DrawBackground();

            if (e.Index >= 0)
            {
                Color itemColor = ((Coworker)coworkerTable[((Task)listBox1.Items[e.Index]).accountId]).color;
                using (Brush brush = new SolidBrush(itemColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                e.Graphics.DrawString(((Task)listBox1.Items[e.Index]).taskName, this.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            }

            e.DrawFocusRectangle();
        }
        
        private void ListBox1_Click(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            List<Task> toDoTaskList = readTaskDB().Where(x => x.taskStatus == "to do").ToList();
            Task selectedTask = ((Task)toDoTaskList[listBox.SelectedIndex]);
            if(listBox.SelectedIndex != -1)
            {
                if (selectedTask.accountId == this.accountId)
                {
                    new EditTaskForm(this.accountId, selectedTask.taskId, this.projectId).Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("You are not assigned to this task !");
                }
            }
        }

        private void ListBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            Hashtable coworkerTable = readCoworkerDB();
            e.DrawBackground();

            if (e.Index >= 0)
            {
                Color itemColor = ((Coworker)coworkerTable[((Task)listBox3.Items[e.Index]).accountId]).color;
                using (Brush brush = new SolidBrush(itemColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                e.Graphics.DrawString(((Task)listBox3.Items[e.Index]).taskName, this.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            }

            e.DrawFocusRectangle();
        }

        private void ListBox3_Click(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            List<Task> toDoTaskList = readTaskDB().Where(x => x.taskStatus == "in progress").ToList();
            Task selectedTask = ((Task)toDoTaskList[listBox.SelectedIndex]);
            if (listBox.SelectedIndex != -1)
            {
                if (selectedTask.accountId == this.accountId)
                {
                    new EditTaskForm(this.accountId, selectedTask.taskId, this.projectId).Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("You are not assigned to this task !");
                }
            }
        }

        private void ListBox4_DrawItem(object sender, DrawItemEventArgs e)
        {
            Hashtable coworkerTable = readCoworkerDB();
            e.DrawBackground();

            if (e.Index >= 0)
            {
                Color itemColor = ((Coworker)coworkerTable[((Task)listBox4.Items[e.Index]).accountId]).color;
                using (Brush brush = new SolidBrush(itemColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                e.Graphics.DrawString(((Task)listBox4.Items[e.Index]).taskName, this.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            }

            e.DrawFocusRectangle();
        }

        private void ListBox4_Click(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            List<Task> toDoTaskList = readTaskDB().Where(x => x.taskStatus == "done").ToList();
            Task selectedTask = ((Task)toDoTaskList[listBox.SelectedIndex]);
            if (listBox.SelectedIndex != -1)
            {
                if (selectedTask.accountId == this.accountId)
                {
                    new EditTaskForm(this.accountId, selectedTask.taskId, this.projectId).Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("You are not assigned to this task !");
                }
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
                foreach (string line in textFile)
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

        private List<Task> readTaskDB()
        {
            List<Task> taskList = new List<Task>();
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

        private void CoworkerDashBoardForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new LogInForm().Show();
            this.Hide();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
