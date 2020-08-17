﻿using System;
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
using LPD_VM.VirtualMachineHandler;

namespace LPD_VM
{
    public partial class Form1 : Form
    {
        VirtualMachine virtualMachine = new VirtualMachine();

        public Form1()
        {
            InitializeComponent();
        }

        private void arquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void abrirArquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            virtualMachine.openFile();

            foreach (var i in virtualMachine.P)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = n;
                dataGridView1.Rows[n].Cells[1].Value = i.command;
                dataGridView1.Rows[n].Cells[2].Value = i.attribute1;
                dataGridView1.Rows[n].Cells[3].Value = i.attribute2;
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void executarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(var i in virtualMachine.P)
            {
                virtualMachine.runCommand(i);
            }
        }
    }
}
