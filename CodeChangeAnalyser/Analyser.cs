using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CodeInfo;

namespace CodeChangeAnalyser
{
    [CodeInfo(Author = "Igor", ChangeDate = "2015-04-16", Reason = "need to write the analyser")]
    public class Analyser
    {
        public IEnumerable<CodeChangeInfo> AnalyseAssembly()
        {
            var assemblyTypes = Assembly.GetAssembly(typeof (CodeChangeInfo)).DefinedTypes;
            return CodeChangeInfos(assemblyTypes);
        }
        /// <summary>
        /// Extracts Code annotation attributes from assembly in specified file
        /// </summary>
        /// <param name="fileinfo">FileInfo of specified assembly</param>
        /// <returns>list of POCO objects with extracted code annotation info</returns>
        public IEnumerable<CodeChangeInfo> AnalyseAssembly(FileInfo fileinfo)
        {
            var assembly = Assembly.LoadFrom(fileinfo.FullName);
            var assemblyTypes = assembly.DefinedTypes;
            return CodeChangeInfos(assemblyTypes);
        }
        private static IEnumerable<CodeChangeInfo> CodeChangeInfos(IEnumerable<TypeInfo> assemblyTypes)
        {
            var typesWithAttributes =
                assemblyTypes.Select(
                k => new Tuple<TypeInfo, IEnumerable<CodeInfoAttribute>>(k, k.GetCustomAttributes(typeof(CodeInfoAttribute)).OfType<CodeInfoAttribute>()));
            IList<Tuple<TypeInfo, IEnumerable<CodeInfoAttribute>>> withAttributes = typesWithAttributes as IList<Tuple<TypeInfo, IEnumerable<CodeInfoAttribute>>> ?? typesWithAttributes.ToList();
            return (from entry in withAttributes
                let typeInfo = entry.Item1.AssemblyQualifiedName
                from attrData in entry.Item2
                select new CodeChangeInfo
                {
                    ClassName = typeInfo, Author = attrData.Author, Reason = attrData.Reason, ChangeDate = DateTime.Parse(attrData.ChangeDate)
                }).ToList();
        }
    }

    
}