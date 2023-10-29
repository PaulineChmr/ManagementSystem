using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem
{
    class Account
    {
        public int accountId;
        public int status;
        public string password;
        public string firstName;
        public string lastName;
        public string email;
        public int projectId;

        public Account(int accountId, int status, string password, string firstName, string lastName, string email, int projectId)
        {
            this.password = password;
            this.status = status;
            this.accountId = accountId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.projectId = projectId;
        }
        public Account(string password, int status, string firstName, string lastName, string email)
        {
            this.password = password;
            this.accountId = Counter();
            this.status = status;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.projectId = -1;
            File.WriteAllText("count.txt", Convert.ToString(Convert.ToInt32(File.ReadAllText("count.txt")) + 1));
            File.AppendAllText("accountDB.txt", this.accountId + "," + this.status + "," + this.password + "," + this.firstName + "," + this.lastName + "," +  this.email + "," + this.projectId + Environment.NewLine);
        }

        public Account(string password, int status, string firstName, string lastName, string email, int projectId)
        {
            this.password = password;
            this.accountId = Counter();
            this.status = status;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.projectId = projectId;
            File.WriteAllText("count.txt", Convert.ToString(Convert.ToInt32(File.ReadAllText("count.txt")) + 1));
            File.AppendAllText("accountDB.txt", this.accountId + "," + this.status + "," + this.password + "," + this.firstName + "," + this.lastName + "," + this.email + "," + this.projectId + Environment.NewLine);
        }

        public int Counter()
        {
            try
            {
                return (Convert.ToInt32(File.ReadAllText("count.txt")));
            }
            catch
            {
                File.WriteAllText("count.txt", "10000");
                return 10000;
            }
        }

        public String Name()
        {
            return (this.firstName + this.lastName);
        }

        public void AddProject(int projectId)
        {
            this.projectId = projectId;
            LineChanger(this.accountId + "," + this.status + "," + this.password + "," + this.firstName + "," + this.lastName + "," + this.email + "," + this.projectId, "accountDB.txt", this.accountId - 9999);
        }

        static void LineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }
    }
}
