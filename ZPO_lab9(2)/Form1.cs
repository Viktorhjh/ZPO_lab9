using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZPO_lab9_2_
{
    public partial class Form1 : Form
    {
        List<Task<string>> taskList = new List<Task<string>>();

        public Form1()
        {
            InitializeComponent();
        }     

        private void button1_Click(object sender, EventArgs e)
        {
            listLinks.Items.Add(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listLinks.Items.Clear();
        }

        //Download
        private async void Button2_Click(object sender, EventArgs e)
        {
            Parallel.For(0, listLinks.Items.Count, i =>
            {
                taskList.Add(download(listLinks.Items[i].ToString()));
            });                        

            foreach(Task<string> task in taskList)
            {
                MessageBox.Show(task.Result);
                
            }
            
        }

        public async Task<string> download(string link)
        {
            using(HttpClient client = new HttpClient())
            {
                string res = await client.GetStringAsync(link);
                return res;
            }
        }
    }
}
