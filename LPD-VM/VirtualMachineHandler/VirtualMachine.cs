using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPD_VM.FileHandler;
using LPD_VM.InstructionHandler;

namespace LPD_VM.VirtualMachineHandler
{
    class VirtualMachine
    {
        public List<Instruction> P = new List<Instruction>();
        //public List<int> M = new List<int>(50);
        public int[] M = new int[50];
        public int i = 0;
        public int s = 0;

        public void openFile ()
        {
            AssemblyFile assemblyFile = new AssemblyFile();
            Instruction instruction = new Instruction();
            List<Instruction> instructionArray = new List<Instruction>();

            List<string> assemblyProgram;

            assemblyFile.openAssemblyFile();
            assemblyProgram = assemblyFile.readFile(assemblyFile.name);
            parseInstructions(assemblyProgram);
        }

        public void parseInstructions(List<string> assemblyProgram)
        {
            List<Instruction> instructionArray = new List<Instruction>();

            foreach (var line in assemblyProgram)
            {
                Instruction instruction = new Instruction();
                instruction.parseInstruction(line);
                P.Add(instruction);
            }
        }

        public void runCommand(Instruction instruction)
        {
            switch(instruction.command)
            {
                case "LDC":
                    s = s + 1;
                    M[s] = Int32.Parse(instruction.attribute1);
                    break;
                case "LDV":
                    s = s + 1;
                    M[s] = M[Int32.Parse(instruction.attribute1)];
                    break;
                case "ADD":
                    M[s - 1] = M[s - 1] + M[s];
                    s = s - 1;
                    break;
                case "SUB":
                    M[s - 1] = M[s - 1] - M[s];
                    s = s - 1;
                    break;
                case "MULT":
                    M[s - 1] = M[s - 1] * M[s];
                    s = s - 1;
                    break;
                case "DIV":
                    M[s - 1] = M[s - 1] / M[s];
                    s = s - 1;
                    break;
                case "INV":
                    M[s] = M[s] * (-1);
                    break;
            }
        }
    }
}
