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
using LPD_VM.VirtualMachineHandler;
using Microsoft.VisualBasic;

namespace LPD_VM
{
    public partial class Form1 : Form
    {
        VirtualMachine virtualMachine = new VirtualMachine();
        int flag;

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
            int print = 0;

            while (true)
            {
                flag = 0;

                if (virtualMachine.P[virtualMachine.i].command == "RD")
                {
                    int input = Int32.Parse(Interaction.InputBox("Prompt", "Title", ""));

                    if (virtualMachine.debugBP(virtualMachine.P[virtualMachine.i]) == 0)
                    {
                        print = virtualMachine.runCommand(virtualMachine.P[virtualMachine.i], input);
                    }
                    else
                    {
                        flag = 1;
                        break;
                    }

                }
                else
                {
                    if (virtualMachine.debugBP(virtualMachine.P[virtualMachine.i]) == 0)
                    {
                        print = virtualMachine.runCommand(virtualMachine.P[virtualMachine.i]);

                        if (print == -1) break;

                        if (print != 0)
                        {
                            textBox2.Text += " " + print.ToString();
                        }
                    }
                    else
                    {
                        flag = 1;
                        break;
                    }
                }
            }

            if(flag == 1)
            {
                MessageBox.Show("Aperte o botão de DEBUG para começar a depuração!", "AVISO");
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e) //breakPoint
        {

        }
        private void button1_Click(object sender, EventArgs e) //debug
        {
            int print = 0;
            if (flag == 1)
            {
                if (virtualMachine.P[virtualMachine.i].command == "RD")
                {
                    int input = Int32.Parse(Interaction.InputBox("Prompt", "Title", ""));
                    print = virtualMachine.runCommand(virtualMachine.P[virtualMachine.i], input);



                }
                else
                {
                    print = virtualMachine.runCommand(virtualMachine.P[virtualMachine.i]);

                    if (print == -1) return;

                    if (print != 0)
                    {
                        textBox2.Text += " " + print.ToString();
                    }
                }
            }
            
            //
            int n = dataGridView2.Rows.Add();
            dataGridView2.Rows[n].Cells[0].Value = virtualMachine.s;
            dataGridView2.Rows[n].Cells[1].Value = virtualMachine.i;

            // selecionar as linhas conforme a execução
            int linha = dataGridView1.CurrentCell.RowIndex;
            linha += 1;
            dataGridView1.CurrentCell = dataGridView1.Rows[linha].Cells[0];
            dataGridView1.Rows[linha].Selected = true;



        }

        private void button3_Click(object sender, EventArgs e) //ok bp
        {
            int numero = Int32.Parse(textBox3.Text);
            virtualMachine.createBreakPoint(numero);
            MessageBox.Show("BreakPoint criado com sucesso!", "Confirmação");
            textBox3.Text = "";
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) //painel pilha
        {


        }
    }
}
