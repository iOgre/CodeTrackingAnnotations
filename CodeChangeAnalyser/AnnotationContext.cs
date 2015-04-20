using System.Data.Entity;
using CodeInfo;

namespace CodeChangeAnalyser
{
    /// <summary>
    /// Just a simple EF Code First context class to work with data
    /// </summary>
    [CodeInfo(Author="Igor", ChangeDate = "2015-04-20", Reason = "Need to store in db")]
    public class AnnotationContext : DbContext
    {
        public AnnotationContext()
            : base()
        {

        }

        public DbSet<CodeChangeInfo> CodeChanges { get; set; }

    }
}