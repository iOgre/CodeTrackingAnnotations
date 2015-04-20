using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
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
            if (args.Any())
            {
                AnalyseAssembly(args[0]);
            }
            else
            {
                NoParametersPassed();
            }
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        private static void NoParametersPassed()
        {
            Console.WriteLine("Type corresponding number to load assembly for analyse");
            Console.WriteLine(Environment.NewLine);
            int index = 0;
            Dictionary<int, string> files = Directory.EnumerateFileSystemEntries(".", "*.*", SearchOption.AllDirectories)
                .Where(t => t.EndsWith(".dll") || t.EndsWith(".exe"))
                .ToDictionary(k => index++, l => l);

            foreach (var item in files)
            {
                Console.WriteLine("{0}. {1}", item.Key, item.Value);
            }
            Console.WriteLine(Environment.NewLine);
            string number = Console.ReadLine();
            int fileIndex;
            if (int.TryParse(number, out fileIndex))
            {
                string fileName;
                if (files.TryGetValue(fileIndex, out fileName))
                {
                    try
                    {
                        AnalyseAssembly(fileName);
                    }
                    catch (CodeTrackingException exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                    
                }
                else
                {
                    Console.WriteLine("No file with specified index");
                }
            }
        }

        private static void AnalyseAssembly(string name)
        {
            using (var context = new AnnotationContext())
            {
                var file = new FileInfo(name);
                var analyser = new Analyser();
                var extractedData = analyser.AnalyseAssembly(file);
                if (extractedData == null || !extractedData.Any())
                {
                    throw new CodeTrackingException("No code tracking attributes assigned to selected ");
                }
                foreach (var codeInfo in analyser.AnalyseAssembly(file))
                {
                    context.CodeChanges.Add(codeInfo);
                    Console.WriteLine(codeInfo.ToString());
                }
                context.SaveChanges();
            }
        }
    }
}
