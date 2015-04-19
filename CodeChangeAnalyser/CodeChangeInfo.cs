using System;
using System.Text;
using System.Threading.Tasks;
using CodeInfo;

namespace CodeChangeAnalyser
{
    [CodeInfo(Author = "Igor", ChangeDate = "2015-04-16T16:55", Reason = "need to write the POCO")]
    public class CodeChangeInfo
    {
        public string Author { get; set; }
        public string Reason { get; set; }
        public string ChangeDate { get; set; }
        public override string ToString()
        {
            return string.Format("Author is: {0} Change date is: {1} Reason is: {2}", Author, ChangeDate, Reason);
        }
    }
}
