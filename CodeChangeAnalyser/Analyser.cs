using System;
using System.Collections.Generic;
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
            IEnumerable<Attribute> attributes = assemblyTypes.SelectMany(t => t.GetCustomAttributes(typeof (CodeInfoAttribute)));
            foreach (var attribute in attributes)
            {
                CodeInfoAttribute attribute1 = attribute as CodeInfoAttribute;
                if (attribute1 != null)
                    yield return new CodeChangeInfo
                    {
                        Author = attribute1.Author, ChangeDate = attribute1.ChangeDate, Reason = attribute1.Reason
                    };
            }
        }

        
    }
}