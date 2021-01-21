using System.Collections.Generic;

namespace SqlBuilder.Models
{
    public class Sql
    {

        public string Query { get; set; }
        public SqlSegment Segment { get; set; }

        public IDictionary<string, object> Parameters { get; set; }
    }
}
