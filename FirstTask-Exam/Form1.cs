using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Security;
using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;

//Chou Monyrith-FT-SD-M13

namespace FirstTask_Exam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var items = GetBookItems();
            
            listBox1.Items.AddRange(items.ToArray());
        }
        
        private List<string> GetBookItems()
        {
            var result = new List<string>();
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://gutenberg.pglaf.org");
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
                
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ftpPath = "ftp://gutenberg.pglaf.org/" + listBox1.SelectedItem.ToString();
            string localPath = @"C:\Users\PC\source\repos\FTP\FirstTask-Exam\bin\Debug\" + listBox1.SelectedItem.ToString();
            if (listBox1.SelectedItems ==null)
            {
                MessageBox.Show("Please choose a book");
                return;
            }
            else
            {
                DownloadFile(ftpPath, localPath);
                MessageBox.Show("File downloaded");
            }

            

        }
        private void DownloadFile(string ftpPath, string localPath)
        {
            string result = "";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpPath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
            {
                result = reader.ReadToEnd();
            }

            File.WriteAllText(localPath, result);
        }

       
    }
}
