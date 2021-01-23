namespace NC.SqlBuilder.Models
{
    public class SqlSegment
    {
        public SqlSegment(string select, string from, string where, string order, string pagination)
        {
            Select = select;
            From = from;
            Where = where;
            Order = order;
            Pagination = pagination;
        }

        public string Select { get; }
        public string From { get; }
        public string Where { get; }
        public string Order { get; }
        public string Pagination { get; }
    }
}