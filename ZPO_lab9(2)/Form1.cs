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
        private void Button2_Click(object sender, EventArgs e)
        {        
            List<Task<string>> tasks = new List<Task<string>>();
            Parallel.For(0, listLinks.Items.Count, i => tasks.Add(download(listLinks.Items[i].ToString())));
        }

        List<string> completedLink = new List<string>();

        public async Task<string> download(string link)
        {
            using(HttpClient client = new HttpClient())
            {
                string result = await client.GetStringAsync(link);
                MessageBox.Show($"{link} is done", "Progress");
                System.Diagnostics.Debug.WriteLine(link);
                completedLink.Add(result);             
                return result;                
            }
        }        

        private void listLinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(completedLink[listLinks.SelectedIndex]);
        }
    }
}
