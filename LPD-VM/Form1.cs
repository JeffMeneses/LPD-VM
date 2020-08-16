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
using LPD_VM.FileHandler;
using LPD_VM.InstructionHandler;

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
            AssemblyFile assemblyFile = new AssemblyFile();
            Instruction instruction = new Instruction();
            List<Instruction> instructionArray = new List<Instruction>();

            List<string> assemblyProgram;

            assemblyFile.openAssemblyFile();
            assemblyProgram = assemblyFile.readFile(assemblyFile.name);
            instructionArray = instruction.intructionParser(assemblyProgram);

            foreach (var i in instructionArray)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = i.command;
                dataGridView1.Rows[n].Cells[1].Value = i.attribute1;
                dataGridView1.Rows[n].Cells[2].Value = i.attribute2;
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

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
               
        }
    }
}
