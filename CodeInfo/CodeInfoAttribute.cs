using System;

namespace CodeInfo
{
    [CodeInfo(Author = "Igor", ChangeDate = "2015-04-17", Reason = "Self-documenting documenting attribute")]
    [AttributeUsage(AttributeTargets.Class)]
    public class CodeInfoAttribute : Attribute
    {
        public string Author { get; set; }
        public string ChangeDate { get; set; }
        public string Reason { get; set; }
    }
}