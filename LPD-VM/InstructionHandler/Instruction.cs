using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPD_VM.InstructionHandler
{
    public class Instruction
    {
        public static List<string> labels = new List<string>();

        public string command;
        public string attribute1;
        public string attribute2;

        public Instruction()
        {
            
        }

        public Instruction(string command, string attribute1, string attribute2)
        {
            this.command = command;
            this.attribute1 = attribute1;
            this.attribute2 = attribute2;
        }

        public List<Instruction> intructionParser(List<string> assemblyProgram)
        {
            List<Instruction> instructionArray = new List<Instruction>();
            string newLine;

            foreach (var line in assemblyProgram)
            {
                Instruction instruction = new Instruction();
                newLine = FixSpacing(line);

                command = null;
                attribute1 = null;
                attribute2 = null;

                string[] words = newLine.Split();
                if (newLine[0].Equals(' '))
                {
                    command = words[1];
                    if (words.Length > 2) attribute1 = words[2];
                    if (words.Length > 3) attribute2 = words[4];
                }
                else
                {
                    labels.Add(words[0]);
                    command = words[1];
                }
                instructionArray.Add(new Instruction(this.command = command, this.attribute1 = attribute1, this.attribute2 = attribute2));
            }
            return instructionArray;
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
    }
}
