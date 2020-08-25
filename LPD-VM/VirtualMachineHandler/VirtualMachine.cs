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
                case "AND":
                    if(M[s-1] == 1 && M[s] == 1){
                        M[s-1] = 1;
                    }
                    else{
                        M[s-1] = 0;
                        s= s - 1;
                    }
                    break;
                case "OR":
                      if(M[s-1] == 1 || M[s] == 1){
                        M[s-1] = 1;
                    }
                    else{
                        M[s-1] = 0;
                        s = s - 1;
                    }
                    break;
                case "NEG":
                    M[s] = 1- M[s];
                    break;
                case "CME":
                    if(M[s-1] < M[s]){
                       M[s-1] = 1; 
                    }
                    else{
                        M[s-1] = 0;
                        s= s - 1;
                    }
                    break;
                case "CMA":
                    if(M[s-1] > M[s]){
                       M[s-1] = 1;
                    }
                    else{
                        M[s-1] = 0;
                        s= s - 1;
                    }
                    break;
                case "CEQ":
                    if(M[s-1] == M[s]){
                       M[s-1] = 1;
                    }
                    else{
                        M[s-1] = 0;
                        s= s - 1;
                    }
                    break;
                case "CDIF":
                    if(M[s-1] != M[s]){
                       M[s-1] = 1;
                    }
                    else{
                        M[s-1] = 0;
                        s= s-1;
                    }
                    break;
                case "CMEQ":
                    if(M[s-1] <= M[s]){
                        M[s-1] = 1;
                    }
                    else{
                        M[s-1] = 0;
                        s = s-1;
                    }
                    break;
                 case "CMAQ":
                    if(M[s-1] >= M[s]){
                        M[s-1] = 1;
                    }
                    else{
                        M[s-1] = 0;
                        s = s-1;
                    }
                    break;
                case "START":
                    s = -1;
                    break;
                case "HLT":
                    // TODO: Forçar parada do programa
                    break;
                case "STR":
                    M[Int32.Parse(instruction.attribute1)] = M[s];
                    s = s-1;
                    break;
                case "JMP":
                    i = Int32.Parse(instruction.attribute1);
                    break;
                case "JMPF":
                    if(M[s] == 0){
                        i = Int32.Parse(instruction.attribute1);
                    }
                    else{
                        i = i + 1;
                        s = s - 1;
                    }
                    break;
                case "NULL":
                    break;
                case "RD":
                    s= s + 1;
                    //  M[s] = "próximo valor de entrada"
                    break;
                case "PRN":
                    //imprimir M[s]
                    s = s - 1;
                    break;
                case "ALLOC":
                    //int m = Int32.Parse(instruction.attribute1);
                    //int n = Int32.Parse(instruction.attribute2);
                    for(int k = 0; k < Int32.Parse(instruction.attribute2); k++){
                        s=s+1;
                         M[s] = M[Int32.Parse(instruction.attribute1) + k];                        
                    }
                    break;
                case "DALLOC":
                    //int m = Int32.Parse(instruction.attribute1);
                    //int n = Int32.Parse(instruction.attribute2);
                    for (int k = Int32.Parse(instruction.attribute2) - 1; k > 0; k--){
                        M[Int32.Parse(instruction.attribute1) + k] = M[s];
                        s = s - 1;
                    }
                    break;
                case "CALL":
                    //int t = instruction.attribute1;
                    s = s + 1;
                    M[s] = i;
                    i = Int32.Parse(instruction.attribute1);
                    break;
                case "RETURN":
                    i = M[s];
                    s = s-1;
                    break;
            }
        }
    }
}
