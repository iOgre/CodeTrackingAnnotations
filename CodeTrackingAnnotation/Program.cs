using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CodeChangeAnalyser;
using CodeInfo;

namespace CodeTrackingAnnotation
{
    [CodeInfo(Author = "igor", ChangeDate="2015-04-17", Reason="test task")]
    class Program
    {
        static void Main(string[] args) 
        {
            var analyser = new Analyser();
            foreach (var codeInfo in analyser.AnalyseAssembly())
            {
                Console.WriteLine(codeInfo.ToString());
            }
            Console.ReadLine();
        }
    }
}
