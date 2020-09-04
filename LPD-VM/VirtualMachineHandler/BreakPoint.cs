using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPD_VM.VirtualMachineHandler
{
    public class BreakPoint
    {
        public int number_instruction;

        public BreakPoint(int number_instruction)
        {
            this.number_instruction = number_instruction;
        }
    }
}
