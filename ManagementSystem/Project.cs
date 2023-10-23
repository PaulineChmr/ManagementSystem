using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem
{
    class Project
    {
        public int projectId;
        public string name;
        public int managerId;

        public Project(int projectId, string name, int managerId)
        {
            this.projectId = projectId;
            this.name = name;
            this.managerId = managerId;
        }

        public Project(string name, int managerId)
        {
            this.projectId = Counter();
            this.name = name;
            this.managerId = managerId;
            File.WriteAllText("countProject.txt", Convert.ToString(Convert.ToInt32(File.ReadAllText("countProject.txt")) + 1));
            File.AppendAllText("projectDB.txt", this.projectId + "," + this.name + "," + this.managerId + Environment.NewLine);
        }

        public int Counter()
        {
            try
            {
                return (Convert.ToInt32(File.ReadAllText("countProject.txt")));
            }
            catch
            {
                File.WriteAllText("countProject.txt", "1");
                return 1;
            }
        }
    }
}
