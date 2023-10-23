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
    public partial class AddTaskForm : Form
    {
        public int accountId;
        public int projectId;

        public AddTaskForm(int accountId, int projectId)
        {
            InitializeComponent();
            this.accountId = accountId;
            this.projectId = projectId;
            Hashtable coworkerTable = readCoworkerDB();
            foreach (DictionaryEntry s in coworkerTable)
            {
                Coworker coworker = (Coworker)s.Value;
                comboBox1.Items.Add(coworker.workerId + " - " + coworker.firstName + " " + coworker.lastName);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

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

        private void AddTaskForm_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();
            int firstSpaceIndex = selectedValue.IndexOf(' ');
            string workerId = selectedValue.Substring(0, firstSpaceIndex);
            Task task = new Task(this.projectId, Convert.ToInt32(workerId), textName.Text);
            MessageBox.Show("Task : " + task.taskName + " added !");
            new DashBoardForm(this.accountId, this.projectId).Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
