using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    static public class Log
    {
        static public void Write(string Method, string err) {

            string nomeArquivo = @"c:\Log\log_Method" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            StreamWriter writer = new StreamWriter(nomeArquivo);
            writer.WriteLine(err);
            writer.Close();
        }
    }
}
