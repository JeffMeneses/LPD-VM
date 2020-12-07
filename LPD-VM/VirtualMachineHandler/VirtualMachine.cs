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
        public List<BreakPoint> breakPointArray = new List<BreakPoint>();
        public List<Instruction> P = new List<Instruction>();
        public AssemblyFile assemblyFile = new AssemblyFile();
        public List<string> assemblyProgram;

        public int[] M = new int[1000];
        public int i = 0;
        public int s = 0;

        public void openFile()
        {         
            assemblyFile.openAssemblyFile();
            assemblyProgram = assemblyFile.readFile(assemblyFile.name);
            parseInstructions(assemblyProgram);
        }

        public void resetVirtualMachine()
        {
            i = 0;
            s = 0;
            P[0].cleanLabels();
        }

        public void parseInstructions(List<string> assemblyProgram)
        {
            List<Instruction> instructionArray = new List<Instruction>();
            int i = 0;
            foreach (var line in assemblyProgram)
            {
                Instruction instruction = new Instruction();
                instruction.i = i;
                instruction.parseInstruction(line, i);
                P.Add(instruction);
               //debugBP(instruction);
                i++;

            }
            setLabelNumbers();
        }

        public void setLabelNumbers()
        {
            foreach (var instruction in P)
            {
                if (instruction.command == "JMP" || instruction.command == "JMPF" || instruction.command == "CALL")
                {
                    instruction.attribute1 = instruction.getLabelNumber(instruction.attribute1).ToString();
                }
            }
        }

        public int runCommand(Instruction instruction, int input = 0)
        {
            int print = 0, RD_PRN_flag = 0;
            switch (instruction.command)
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
                case "DIVI":
                    M[s - 1] = M[s - 1] / M[s];
                    s = s - 1;
                    break;
                case "INV":
                    M[s] = M[s] * (-1);
                    break;
                case "AND":
                    if (M[s - 1] == 1 && M[s] == 1)
                    {
                        M[s - 1] = 1;
                    }
                    else
                    {
                        M[s - 1] = 0;
                    }
                    s = s - 1;
                    break;
                case "OR":
                    if (M[s - 1] == 1 || M[s] == 1)
                    {
                        M[s - 1] = 1;
                    }
                    else
                    {
                        M[s - 1] = 0;
                    }
                    s = s - 1;
                    break;
                case "NEG":
                    M[s] = 1 - M[s];
                    break;
                case "CME":
                    if (M[s - 1] < M[s])
                    {
                        M[s - 1] = 1;
                    }
                    else
                    {
                        M[s - 1] = 0;
                    }
                    s = s - 1;
                    break;
                case "CMA":
                    if (M[s - 1] > M[s])
                    {
                        M[s - 1] = 1;
                    }
                    else
                    {
                        M[s - 1] = 0;
                    }
                    s = s - 1;
                    break;
                case "CEQ":
                    if (M[s - 1] == M[s])
                    {
                        M[s - 1] = 1;
                    }
                    else
                    {
                        M[s - 1] = 0;
                    }
                    s = s - 1;
                    break;
                case "CDIF":
                    if (M[s - 1] != M[s])
                    {
                        M[s - 1] = 1;
                    }
                    else
                    {
                        M[s - 1] = 0;
                    }
                    s = s - 1;
                    break;
                case "CMEQ":
                    if (M[s - 1] <= M[s])
                    {
                        M[s - 1] = 1;
                    }
                    else
                    {
                        M[s - 1] = 0;
                    }
                    s = s - 1;
                    break;
                case "CMAQ":
                    if (M[s - 1] >= M[s])
                    {
                        M[s - 1] = 1;
                    }
                    else
                    {
                        M[s - 1] = 0;
                    }
                    s = s - 1;
                    break;
                case "START":
                    s = -1;
                    break;
                case "HLT":
                    // TODO: Forçar parada do programa
                    return -99902;
                   // break;
                case "STR":
                    M[Int32.Parse(instruction.attribute1)] = M[s];
                    s = s - 1;
                    break;
                case "JMP":
                    i = Int32.Parse(instruction.attribute1);
                    break;
                case "JMPF":
                    if (M[s] == 0)
                    {
                        i = Int32.Parse(instruction.attribute1);
                    }
                    else
                    {
                        i = i + 1;
                    }
                    s = s - 1;
                    break;
                case "NULL":
                    break;
                case "RD":
                    s = s + 1;
                    M[s] = input;
                    //  M[s] = "próximo valor de entrada"
                    break;
                case "PRN":
                    RD_PRN_flag = 1;
                    print = M[s];
                    s = s - 1;
                    break;
                case "ALLOC":
                    for (int k = 0; k <= Int32.Parse(instruction.attribute2) - 1; k++)
                    {
                        s = s + 1;
                        M[s] = M[Int32.Parse(instruction.attribute1) + k];
                    }
                    break;
                case "DALLOC":
                    for (int k = Int32.Parse(instruction.attribute2) - 1; k >= 0; k--)
                    {
                        M[Int32.Parse(instruction.attribute1) + k] = M[s];
                        s = s - 1;
                    }
                    break;
                case "CALL":
                    s = s + 1;
                    M[s] = i + 1;
                    i = Int32.Parse(instruction.attribute1);
                    break;
                case "RETURN":
                    i = M[s];
                    s = s - 1;
                    break;
            }

            if (instruction.command != "JMPF" && instruction.command != "CALL" && instruction.command != "JMP" && instruction.command != "RETURN") i++;
            if (RD_PRN_flag == 0) return -99901;
            else return print;
        }

        public void createBreakPoint(int number)
        {
            breakPointArray.Add(new BreakPoint(number));
        }

        public int debugBP(Instruction instruction)
        {

            if (breakPointArray == null)
            {
                return 0;
            }
            else
            {
                foreach (var bp in breakPointArray)
                {
                    if (instruction.i >= bp.number_instruction)
                    {
                        if (instruction.command == "HLT")
                        {
                            breakPointArray.Remove(bp);
                            return 2;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    
                }                
            }

            return 0;
        }

    }
}
