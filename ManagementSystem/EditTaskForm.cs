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
    public partial class EditTaskForm : Form
    {
        public int accountId;
        public int projectId;
        public int taskId;
        public EditTaskForm(int accountId, int taskId, int projectId)
        {
            InitializeComponent();
            this.accountId = accountId;
            this.projectId = projectId;
            this.taskId = taskId;
            Hashtable taskTable = readTaskDB();
            label7.Text = ((Task)taskTable[taskId]).taskName;
            label3.Text = ((Task)taskTable[taskId]).accountId.ToString();
            Hashtable coworkerTable = readCoworkerDB();
            label4.Text = ((Coworker)coworkerTable[((Task)taskTable[taskId]).accountId]).GetName();
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

        private Hashtable readTaskDB()
        {
            Hashtable taskTable = new Hashtable();
            List<string> textFile = new List<string>();
            if (File.Exists("Project" + projectId + "/taskDB.txt"))
            {
                textFile = File.ReadAllLines("Project" + projectId + "/taskDB.txt").ToList();
                foreach (string line in textFile)
                {
                    string[] items = line.Split(',');
                    Task task = new Task(Convert.ToInt32(items[0]), Convert.ToInt32(items[1]), Convert.ToInt32(items[2]), items[3], items[4]);
                    taskTable.Add(task.taskId, task);
                }
            }
            return taskTable;
        }

        private void EditTaskForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) {

            }
            else
            {
                string status = comboBox1.SelectedItem.ToString();
                switch (status)
                {
                    case "to do":
                        ChangeStatus("to do");
                        break;
                    case "in progress":
                        ChangeStatus("in progress");
                        break;
                    case "done":
                        ChangeStatus("done");
                        break;
                }
            }
            new CoworkerDashBoardForm(this.accountId, this.projectId).Show();
            this.Hide();
        }

        private void ChangeStatus(string newStatus)
        {
            int lineToModify = Convert.ToInt32(this.taskId.ToString().Remove(0,1)) - 1;
            List<string> lines = new List<string>(File.ReadAllLines("Project" + projectId + "/taskDB.txt"));
            Hashtable taskTable = readTaskDB();
            Task taskToModify = (Task)taskTable[this.taskId];
            lines[lineToModify] = taskToModify.taskId + "," + taskToModify.projectId + "," + taskToModify.accountId + "," + taskToModify.taskName + "," + newStatus;
            File.WriteAllLines("Project" + projectId + "/taskDB.txt", lines);
        }
    }
}
