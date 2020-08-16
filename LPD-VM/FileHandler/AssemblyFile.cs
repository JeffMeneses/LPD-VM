using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPD_VM.FileHandler
{
    public class AssemblyFile
    {
        public string name;

        public void openAssemblyFile()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Text File Only|*.txt";
            string fname = "";

            if (fd.ShowDialog().Equals(DialogResult.OK))
            {
                fname = fd.FileName;
                name = fname;
            }
        }

        public List<string> readFile(string fname)
        {
            List<string> lines = File.ReadAllLines(fname).ToList();
            //foreach (var line in lines)
            //{
            //    Console.WriteLine(line);
            //}
            return lines;
        }
    }
}
