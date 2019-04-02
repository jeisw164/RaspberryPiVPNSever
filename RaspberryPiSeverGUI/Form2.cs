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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.Username;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.IP;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //update pi

            string username = Properties.Settings.Default.Username;
            string IP = Properties.Settings.Default.IP;
            string password = Properties.Settings.Default.Password;

            MessageBox.Show("Please wait one minute for proccess to finish");
            progressBar1.Value = 0;

            using (var client = new SshClient(IP, username, password))
            {
                try
                {
                    progressBar1.Maximum = 45000;
                    progressBar1.Step = 1000;

                    client.Connect();

                    client.RunCommand("sudo apt-get update");
                    //takes 40 secounds
                    for (int i = 0; i < 40; i++)
                    {
                        progressBar1.PerformStep();
                        System.Threading.Thread.Sleep(1000);
                    }
                    //System.Threading.Thread.Sleep(40000);
                    client.RunCommand("sudo apt-get upgrade");
                    //takes 5 secounds
                    for (int i = 0; i < 5; i++)
                    {
                        progressBar1.PerformStep();
                        System.Threading.Thread.Sleep(1000);
                    }
                    //System.Threading.Thread.Sleep(5000);
                    client.Disconnect();

                    MessageBox.Show("Pi updated succefully");

                    
                }
                catch
                {
                    MessageBox.Show("Something didn't work");
                }


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            //install VPN software

            string username = Properties.Settings.Default.Username;
            string IP = Properties.Settings.Default.IP;
            string password = Properties.Settings.Default.Password;

            MessageBox.Show("Please wait 30 secounds for proccess to finish");
            progressBar2.Value = 0;

            using (var client = new SshClient(IP, username, password))
            {
                try
                {
                    progressBar2.Maximum = 30000;
                    progressBar2.Step = 1000;

                    client.Connect();

                    client.RunCommand("sudo apt-get install openvpn");
                    //takes 40 secounds
                    for (int i = 0; i < 30; i++)
                    {
                        progressBar2.PerformStep();
                        System.Threading.Thread.Sleep(1000);
                    }
                    

                    client.Disconnect();

                    MessageBox.Show("VPN software installed succefully");

                }
                catch
                {
                    MessageBox.Show("Something didn't work");
                }


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {


            //install VPN software
            progressBar3.Value = 0;

            string username = Properties.Settings.Default.Username;
            string IP = Properties.Settings.Default.IP;
            string password = Properties.Settings.Default.Password;
            progressBar3.Maximum = 8000;
            progressBar3.Step = 500;

            MessageBox.Show("Generating keys please wait");
    

            using (var client = new SshClient(IP, username, password))
            {
                try
                {

                    client.Connect();
                    var stream = client.CreateShellStream("xterm", 80, 50, 1024, 1024, 1024);

                    progressBar3.PerformStep();
                    stream.WriteLine("sudo cp -r /usr/share/doc/openvpn/examples/easy-rsa/2.0 /etc/openvpn/easy-rsa");
                    System.Threading.Thread.Sleep(500);
                    progressBar3.PerformStep();
                    stream.WriteLine("cd /etc");
                    System.Threading.Thread.Sleep(500);
                    progressBar3.PerformStep();
                    stream.WriteLine("sudo chmod o+x openvpn/");
                    System.Threading.Thread.Sleep(500);
                    progressBar3.PerformStep();
                    stream.WriteLine("cd openvpn");
                    System.Threading.Thread.Sleep(500);
                    progressBar3.PerformStep();
                    stream.WriteLine("sudo chmod o+x easy-rsa/");
                    System.Threading.Thread.Sleep(500);
                    progressBar3.PerformStep();
                    stream.WriteLine("cd easy-rsa");
                    System.Threading.Thread.Sleep(500);
                    progressBar3.PerformStep();

                    stream.WriteLine("sudo chmod 777 keys");
                    System.Threading.Thread.Sleep(500);
                    progressBar3.PerformStep();

                    stream.WriteLine("source ./vars");
                    System.Threading.Thread.Sleep(500);
                    progressBar3.PerformStep();
                    stream.WriteLine("./clean-all");
                    System.Threading.Thread.Sleep(2000);
                    progressBar3.PerformStep();
                    progressBar3.PerformStep();
                    progressBar3.PerformStep();
                    progressBar3.PerformStep();


                    stream.WriteLine("echo -en '\n\n\n\n\n\n\n\n' | ./build-ca");
                    System.Threading.Thread.Sleep(2000);
                    progressBar3.PerformStep();
                    progressBar3.PerformStep();
                    progressBar3.PerformStep();
                    progressBar3.PerformStep();
                    client.Disconnect();

                    MessageBox.Show("Please check keys folder");

                }
                catch
                {
                    MessageBox.Show("Something didn't work");
                }


            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            string SeverName = textBox2.Text;

            if (SeverName == "")
            {
                MessageBox.Show("Please enter in a server name");
               return;
            }

            //install VPN software
            progressBar4.Value = 0;

            string username = Properties.Settings.Default.Username;
            string IP = Properties.Settings.Default.IP;
            string password = Properties.Settings.Default.Password;
            progressBar4.Maximum = 4000;
            progressBar4.Step = 500;

            MessageBox.Show("Creating server please wait");


            using (var client = new SshClient(IP, username, password))
            {
                try
                {

                    client.Connect();
                    var stream = client.CreateShellStream("xterm", 80, 50, 1024, 1024, 1024);


                    stream.WriteLine("cd /etc");
                    System.Threading.Thread.Sleep(500);
                    progressBar4.PerformStep();
                    stream.WriteLine("cd openvpn");
                    System.Threading.Thread.Sleep(500);
                    progressBar4.PerformStep();

                    stream.WriteLine("cd easy-rsa");
                    System.Threading.Thread.Sleep(500);
                    progressBar4.PerformStep();
                    stream.WriteLine("source ./vars");
                    System.Threading.Thread.Sleep(500);
                    progressBar4.PerformStep();


                    stream.WriteLine("echo -en '\n\n\n\n\n\n\n\n\n\n\ny\ny' | ./build-key-server " + SeverName);
                    System.Threading.Thread.Sleep(2000);
                    progressBar4.PerformStep();
                    progressBar4.PerformStep();
                    progressBar4.PerformStep();
                    progressBar4.PerformStep();
                    client.Disconnect();

                    MessageBox.Show("Sever named: " + SeverName + " created");

                }
                catch
                {
                    MessageBox.Show("Something didn't work");
                }


            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            

            if (textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }

            string userpassword = textBox5.Text;
            //install VPN software
            progressBar5.Value = 0;

            string username = Properties.Settings.Default.Username;
            string IP = Properties.Settings.Default.IP;
            string password = Properties.Settings.Default.Password;
            progressBar5.Maximum = 5000;
            progressBar5.Step = 500;

            MessageBox.Show("Creating user please wait");


            using (var client = new SshClient(IP, username, password))
            {
                try
                {

                    client.Connect();
                    var stream = client.CreateShellStream("xterm", 80, 50, 1024, 1024, 1024);


                    stream.WriteLine("cd /etc");
                    System.Threading.Thread.Sleep(500);
                    progressBar5.PerformStep();
                    stream.WriteLine("cd openvpn");
                    System.Threading.Thread.Sleep(500);
                    progressBar5.PerformStep();

                    stream.WriteLine("cd easy-rsa");
                    System.Threading.Thread.Sleep(500);
                    progressBar5.PerformStep();
                    stream.WriteLine("source ./vars");
                    System.Threading.Thread.Sleep(500);
                    progressBar5.PerformStep();

                    string temp = "echo -en '" + userpassword + "\n" + userpassword + "\n" + "\n\n\n\n\n\n\n\n\n\n\ny\ny'";

                    stream.WriteLine(temp + " | ./build-key-pass " + textBox3.Text);
                    System.Threading.Thread.Sleep(2000);
                    progressBar5.PerformStep();
                    progressBar5.PerformStep();
                    progressBar5.PerformStep();
                    progressBar5.PerformStep();
                    stream.WriteLine("cd keys");
                    System.Threading.Thread.Sleep(500);
                    progressBar5.PerformStep();
                    stream.WriteLine("openssl rsa -in " + textBox3.Text+".key -des3 -out "+ textBox3.Text +".3des.key");
                    System.Threading.Thread.Sleep(500);
                    progressBar5.PerformStep();
                    client.Disconnect();

                    MessageBox.Show("User " + textBox3.Text + " created");

                }
                catch
                {
                    MessageBox.Show("Something didn't work");
                }

              }
            }

        private void button8_Click(object sender, EventArgs e)
        {



            string message = "Are you sure you want to generatea Diffie-Hellman key? This could take a while";
            string caption = "Creating Diffie-Hellman Key confirmation";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {

                return;

            }


            progressBar6.Value = 0;

            string username = Properties.Settings.Default.Username;
            string IP = Properties.Settings.Default.IP;
            string password = Properties.Settings.Default.Password;
            progressBar6.Maximum = 122000;
            progressBar6.Step = 500;

            MessageBox.Show("Creating Diffie-Hellman key");


            using (var client = new SshClient(IP, username, password))
            {
                try
                {

                    client.Connect();
                    var stream = client.CreateShellStream("xterm", 80, 50, 1024, 1024, 1024);

                    string temp;

                    

                    stream.WriteLine("cd /etc");
                    System.Threading.Thread.Sleep(500);
                    progressBar6.PerformStep();
                    stream.WriteLine("cd openvpn");
                    System.Threading.Thread.Sleep(500);
                    progressBar6.PerformStep();

                    stream.WriteLine("cd easy-rsa");
                    System.Threading.Thread.Sleep(500);
                    progressBar6.PerformStep();
                    stream.WriteLine("source ./vars");
                    System.Threading.Thread.Sleep(500);
                    progressBar6.PerformStep();

                    stream.WriteLine("./build-dh");
                    System.Threading.Thread.Sleep(2000);
                    progressBar6.PerformStep();
                    progressBar6.PerformStep();
                    temp = stream.Read();
                    progressBar6.Step = 100;
                    while (temp != "")
                    {
                        System.Threading.Thread.Sleep(500);
                        progressBar6.PerformStep();
                        temp = stream.Read();
                    }


                    
                    client.Disconnect();

                    progressBar6.Value = 122000;

                    MessageBox.Show("Diffie-Hellman key created");

                }
                catch
                {
                    MessageBox.Show("Something didn't work");
                }

            }
        }
    
    }
}
