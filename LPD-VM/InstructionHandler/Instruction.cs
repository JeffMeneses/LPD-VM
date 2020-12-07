using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPD_VM.InstructionHandler
{
    public class Instruction
    {
        //public static List<string> labels = new List<string>();
        public static List<Label> labels = new List<Label>();
        public int i;
        public string command;
        public string attribute1;
        public string attribute2;

        public Instruction()
        {
            
        }

        public void cleanLabels()
        {
            labels.Clear();
        }

        public Instruction(int i, string command, string attribute1, string attribute2)
        {
            this.command = command;
            this.attribute1 = attribute1;
            this.attribute2 = attribute2;
        }

        public void parseInstruction(string assemblyInstruction, int i)
        {
            int index = 0;
            string command = "", param1 = "", param2 = "";

            while((assemblyInstruction.Length > index) && Char.IsLetterOrDigit(assemblyInstruction[index]))
            {
                command += assemblyInstruction[index];
                index++;
            }

            switch (command)
            {
                case "ALLOC":
                case "DALLOC":
                    // 2 PARAM
                    index++;
                    while ((assemblyInstruction.Length > index) && Char.IsDigit(assemblyInstruction[index]))
                    {
                        param1 += assemblyInstruction[index];
                        index++;
                    }
                    index++;
                    while ((assemblyInstruction.Length > index) && Char.IsDigit(assemblyInstruction[index]))
                    {
                        param2 += assemblyInstruction[index];
                        index++;
                    }
                        break;

                case "LDC":
                case "LDV":
                case "STR":
                    // 1 PARAM
                    index++;
                    while ((assemblyInstruction.Length > index) && Char.IsDigit(assemblyInstruction[index]))
                    {
                        param1 += assemblyInstruction[index];
                        index++;
                    }
                    break;

                case "JMP":
                case "JMPF":
                case "CALL":
                    // 1 LABEL
                    index++;
                    while ((assemblyInstruction.Length > index) && Char.IsLetterOrDigit(assemblyInstruction[index]))
                    {
                        param1 += assemblyInstruction[index];
                        index++;
                    }
                    //addLabel(param1, i);
                    break;

                case "ADD":
                case "SUB":
                case "MULT":
                case "DIVI":
                case "INV":
                case "AND":
                case "OR":
                case "NEG":
                case "CME":
                case "CMA":
                case "CEQ":
                case "CDIF":
                case "CMEQ":
                case "CMAQ":
                case "START":
                case "HLT":
                case "RD":
                case "PRN":
                case "RETURN":
                    // 0 PARAM
                    break;
                default:
                    // NULL
                    index++;
                    while ((assemblyInstruction.Length > index) && Char.IsLetterOrDigit(assemblyInstruction[index]))
                    {
                        param1 += assemblyInstruction[index];
                        index++;
                    }
                    addLabel(command, i);
                    break;
            }
            this.command = command;
            attribute1 = param1;
            attribute2 = param2;
        }

        public string FixSpacing(string line)
        {

            string newLine = line.Replace("\t", " ");
            while (newLine.IndexOf("  ") >= 0)
            {
                newLine = newLine.Replace("  ", " ");
            }
            newLine = newLine.Replace(",", " ");
            return newLine;
        }

        public int getLabelNumber(string labelName)
        {
            foreach(var label in labels)
            {
                if (label.name == labelName) return label.i;
            }

            return -1;
        }

        public string getParamNumber(string assemblyInstruction, int index)
        {
            string param = "";

            while ((assemblyInstruction.Length > index) && Char.IsDigit(assemblyInstruction[index]))
            {
                param += assemblyInstruction[index];
                index++;
            }
            return param;
        }

        public void addLabel(string label, int line)
        {
            int flag = 0;

            foreach(var item in labels)
            {
                if (item.name == label) flag = 1;
            }

            if (flag == 0) labels.Add(new Label(label, line));
        }

        //public void updateLabelLine()
    }
}
