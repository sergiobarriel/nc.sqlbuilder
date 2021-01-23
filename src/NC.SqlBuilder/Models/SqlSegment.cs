namespace NC.SqlBuilder.Models
{
    public class SqlSegment
    {
        public string Select { get; set; }
        public string From { get; set; }
        public string Where { get; set; }
        public string Order { get; set; }
        public string Pagination { get; set; }
    }
}