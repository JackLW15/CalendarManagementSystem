﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_weatherly
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UsertextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordtextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UsertextBox.Text == "john_weatherly15" && PasswordtextBox2.Text == "password")
            {
                this.Hide();
                Form1 ss = new Form1();
                ss.Show();
            }
            else
            {
                MessageBox.Show("Please enter a vaild username or password.");
            }
        }
    }
}
