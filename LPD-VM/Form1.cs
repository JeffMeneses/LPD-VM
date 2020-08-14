using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LPD_VM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void arquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void abrirArquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd1 = new OpenFileDialog();
            fd1.Filter = "Text File Only|*.txt";

            String fname = "";

            if(fd1.ShowDialog().Equals(DialogResult.OK))
            {
                fname = fd1.FileName;
                MessageBox.Show(fname);
            }

            List<string> lines = File.ReadAllLines(fname).ToList();
            foreach (var line in lines)
            {
                //string[] entries = line.Split(',')
                Console.WriteLine(line);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
