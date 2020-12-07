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
        VirtualMachine virtualMachine;
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
            int n = 0;
            virtualMachine = new VirtualMachine();
            virtualMachine.openFile();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            textBox2.Text = "";

            foreach (var i in virtualMachine.P)
            {
                n = dataGridView1.Rows.Add();
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
            //ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Green, ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Green, ButtonBorderStyle.Dashed);
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Green, ButtonBorderStyle.Dashed);
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
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Green, ButtonBorderStyle.Dashed);
        }

        private void executarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int print = 0;

            if (virtualMachine == null)
            {
                MessageBox.Show("Não há nenhum arquivo aberto!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //virtualMachine = new VirtualMachine();
                //virtualMachine.parseInstructions(virtualMachine.assemblyProgram);
                virtualMachine.resetVirtualMachine();
                textBox2.Text = "";
            }

            while (true)
            {
                flag = 0;

                if (virtualMachine.P[virtualMachine.i].command == "RD")
                {
                    int input = Int32.Parse(Interaction.InputBox("Instrução: RD Valor", "Entrada de Dados", ""));

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

                        if (print == -99902) break;

                        if (print != -99901)
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
            int linha = dataGridView1.CurrentCell.RowIndex;
            int n = dataGridView2.Rows.Add();
            

            if (flag == 1)
            {
                if (virtualMachine.P[virtualMachine.i].command == "RD")
                {
                    int input = Int32.Parse(Interaction.InputBox("Intrução: RD Valor", "Entrada de Dados", ""));
                    print = virtualMachine.runCommand(virtualMachine.P[virtualMachine.i], input);



                }
                else
                {
                    print = virtualMachine.runCommand(virtualMachine.P[virtualMachine.i]);

                    if (print == -99902) return;

                    if (print != -99901)
                    {
                        textBox2.Text += " " + print.ToString();
                    }
                }
            }

            if (virtualMachine.P[virtualMachine.i].command != "HLT")
            {
                //
                dataGridView2.Rows[n].Cells[0].Value = virtualMachine.s;
                dataGridView2.Rows[n].Cells[1].Value = virtualMachine.i;
                dataGridView2.Rows[n].Cells[2].Value = virtualMachine.M[virtualMachine.s];

                // selecionar as linhas conforme a execução              
                linha = virtualMachine.i;
                dataGridView1.CurrentCell = dataGridView1.Rows[linha].Cells[0];
                dataGridView1.Rows[linha].Selected = true;
            }
            else
            {
                linha = virtualMachine.i -1;
                dataGridView1.Rows[linha].Selected = false;
                if (virtualMachine.debugBP(virtualMachine.P[virtualMachine.i]) == 2)
                {
                    MessageBox.Show("Depuração terminada, BreakPoint retirado com sucesso!", "AVISO");
                }
            }


        }

        private void button3_Click(object sender, EventArgs e) //ok bp
        {

            int linha = dataGridView1.CurrentCell.RowIndex;
            int numero = Int32.Parse(textBox3.Text);

            virtualMachine.createBreakPoint(numero);
            MessageBox.Show("BreakPoint criado com sucesso!", "Confirmação");
            textBox3.Text = "";
           
            linha = numero;
            dataGridView1.CurrentCell = dataGridView1.Rows[linha].Cells[0];
            dataGridView1.Rows[linha].Selected = true;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) //painel pilha
        {


        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Green, ButtonBorderStyle.Dashed);
        }
    }
}
