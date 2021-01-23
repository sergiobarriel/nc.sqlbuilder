using System.Collections.Generic;

namespace NC.SqlBuilder.Models
{
    public class Sql
    {

        public string Query { get; set; }
        public SqlSegment Segment { get; set; }

        public Dictionary<string, object> Parameters { get; set; }
    }
}
