using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem
{
    class Task
    {
        public int taskId;
        public int projectId;
        public int accountId;
        public string taskName;
        public string taskStatus;
        public Task(int projectId, int accountId, string taskName)
        {
            this.taskId = Counter();
            this.projectId = projectId;
            this.accountId = accountId;
            this.taskName = taskName;
            this.taskStatus = "to do";
            File.WriteAllText("countTask.txt", Convert.ToString(Convert.ToInt32(File.ReadAllText("countTask.txt")) + 1));
            File.AppendAllText("Project" + projectId + "/taskDB.txt", this.taskId + "," + this.projectId + "," + this.accountId + "," + this.taskName + "," + this.taskStatus + Environment.NewLine);
        }

        public Task(int taskId, int projectId, int accountId, string taskName, string taskStatus)
        {
            this.taskId = taskId;
            this.projectId = projectId;
            this.accountId = accountId;
            this.taskName = taskName;
            this.taskStatus = taskStatus;
        }

        public int Counter()
        {
            try
            {
                return (Convert.ToInt32(File.ReadAllText("countTask.txt")));
            }
            catch
            {
                File.WriteAllText("countTask.txt", "1");
                return 1;
            }
        }
    }
}
