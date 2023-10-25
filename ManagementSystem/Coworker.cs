using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem
{
    class Coworker
    {
        public int workerId;
        public int status;
        public string firstName;
        public string lastName;
        public Color color;

        public Coworker(int workerId, int status, string firstName, string lastName, Color color)
        {
            this.workerId = workerId;
            this.status = status;
            this.firstName = firstName;
            this.lastName = lastName;
            this.color = color;
        }

        public Coworker(int workerId, int status, string firstName, string lastName, Color color, int projectId)
        {
            this.workerId = workerId;
            this.status = status;
            this.firstName = firstName;
            this.lastName = lastName;
            this.color = color;
            File.AppendAllText("Project" + projectId + "/workerDB.txt", this.workerId + "," + this.status + "," + this.firstName + "," + this.lastName + "," + this.color.Name + Environment.NewLine);
        }

        public string GetName()
        {
            return (this.firstName + " " + this.lastName);
        }

    }
}
