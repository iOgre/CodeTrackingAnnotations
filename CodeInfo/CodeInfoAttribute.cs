using System;

namespace CodeInfo
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CodeInfoAttribute : Attribute
    {
        public string Author { get; set; }
        public string ChangeDate { get; set; }
        public string Reason { get; set; }
    }
}