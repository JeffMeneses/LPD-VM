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

        public Instruction(int i, string command, string attribute1, string attribute2)
        {
            this.command = command;
            this.attribute1 = attribute1;
            this.attribute2 = attribute2;
        }

        public void parseInstruction(string assemblyInstruction, int i)
        {
            string newLine;
            newLine = FixSpacing(assemblyInstruction);

            string[] words = newLine.Split();

            this.i = i;
            if (newLine[0].Equals(' '))
            {
                command = words[1];
                if (words.Length > 2) attribute1 = words[2];
                if (words.Length > 3) attribute2 = words[4];
            }
            else
            {
                labels.Add(new Label(words[0], i));
                command = words[1];
                //attribute1 = i;
            }

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

        public int getLabelNumber(String labelName)
        {
            foreach(var label in labels)
            {
                if (label.name == labelName) return label.i;
            }

            return -1;
        }

    }
}
