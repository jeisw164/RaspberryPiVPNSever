using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;
using Renci.SshNet.Sftp;


namespace RaspberryPiSeverGUI
{
    public partial class Form1 : Form
    {
        string IP;
        string username;
        string password;

        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RememberMe)
            {
                this.textBox2.Text = Properties.Settings.Default.Username;
                this.textBox1.Text = Properties.Settings.Default.IP;
                this.textBox3.Text = Properties.Settings.Default.Password;
                this.checkBox1.Checked = Properties.Settings.Default.RememberMe;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(checkBox1.Checked)
            {

                Properties.Settings.Default.Username = this.textBox2.Text;
                Properties.Settings.Default.IP = this.textBox1.Text;
                Properties.Settings.Default.Password = this.textBox3.Text;
                Properties.Settings.Default.RememberMe = this.checkBox1.Checked;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.IP = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.Save();
            }
       }

        public void button1_Click(object sender, EventArgs e)
        {


            Properties.Settings.Default.Username = this.textBox2.Text;
            Properties.Settings.Default.IP = this.textBox1.Text;
            Properties.Settings.Default.Password = this.textBox3.Text;
            Properties.Settings.Default.RememberMe = this.checkBox1.Checked;
            Properties.Settings.Default.Save();


            IP = textBox1.Text;
            username = textBox2.Text;
            password = textBox3.Text;


            if(IP == "" || username == "" || password == "")
            {
                MessageBox.Show("Please make sure to fill in all fields");
                return;
            }

            using (var client = new SshClient(IP, username, password))
            {
                try
                {

                    client.Connect();

                    SshCommand sc = client.CreateCommand("ls");
                    sc.Execute();
                    string answer = sc.Result;

                    //If null the command failed to execute or was not found

                    //MessageBox.Show(answer);

                    //string command = "echo " + "Hello " + textBox2.Text + " > test";


                    //client.RunCommand(command);

                    client.Disconnect();

                    Form2 form2 = new Form2();
                    form2.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Invalid Info Used");
                }

                

// this.Hide();
            }
        }
    }
}
