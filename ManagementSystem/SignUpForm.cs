﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementSystem
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void SignInForm_Load(object sender, EventArgs e)
        {

        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            new LogInForm().Show();
            this.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Account account = new Account(textPassword.Text, 0, textFirstName.Text, textLastName.Text, textEmail.Text);
            if (string.IsNullOrEmpty(textFirstName.Text) || string.IsNullOrEmpty(textLastName.Text) || string.IsNullOrEmpty(textEmail.Text) || string.IsNullOrEmpty(textPassword.Text))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }
            MessageBox.Show("Account created, your User ID is : " + account.accountId);


            new LogInForm().Show();
            this.Hide();
        }
    }
}
