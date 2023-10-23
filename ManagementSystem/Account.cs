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

        /*static void LineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }

        public void DefinePasswordMenu()
        {
            bool quit = false;
            while (!quit)
            {
                bool valid = false;
                string password1 = "";
                while (!valid)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to DOTNET Hospital Management System {0} {1}", this.firstName, this.lastName);
                    Console.WriteLine("New password: ");
                    password1 = Console.ReadLine();
                    Console.WriteLine("Confirm password: ");
                    string password2 = Console.ReadLine();
                    valid = (password1 == password2);
                    if (!valid)
                    {
                        Console.WriteLine("Passwords not matching");
                        Console.ReadKey();
                    }
                }
                this.password = password1;
                switch (status)
                {
                    case 0:
                        Administrator a = (Administrator)this;
                        Accounts.accountTable[a.accountId] = a;
                        LineChanger(a.accountId + "," + a.status + "," + a.password + "," + a.firstName + "," + a.lastName + "," + a.email, "accountDB.txt", this.accountId - 9999);
                        break;
                    case 1:
                        Doctor d = (Doctor)this;
                        Accounts.accountTable[d.accountId] = d;
                        LineChanger(d.accountId + "," + d.status + "," + d.password + "," + d.firstName + "," + d.lastName + "," + d.email + "," + d.phone + "," + d.streetNumber + "," + d.street + "," + d.city + "," + d.state, "accountDB.txt", this.accountId - 9999);
                        break;
                    case 2:
                        Patient p = (Patient)this;
                        Accounts.accountTable[p.accountId] = p;
                        LineChanger(p.accountId + "," + p.status + "," + p.password + "," + p.firstName + "," + p.lastName + "," + p.email + "," + p.phone + "," + p.streetNumber + "," + p.street + "," + p.city + "," + p.state + "," + p.doctorId, "accountDB.txt", this.accountId - 9999);
                        break;
                }
                Console.WriteLine("Password changed");
                Console.ReadKey();
                quit = true;
                string[] strg = new string[1];
                Program.Main(strg);
            }
        }*/
    }
}
