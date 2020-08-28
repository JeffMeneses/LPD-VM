using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPD_VM.InstructionHandler
{
    public class Label
    {
        public String name;
        public int i;

        public Label (String name, int i)
        {
            this.name = name;
            this.i = i;
        }
    }
}
