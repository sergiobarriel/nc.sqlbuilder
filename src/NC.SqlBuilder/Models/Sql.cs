using System;
using System.Collections.Generic;

namespace NC.SqlBuilder.Models
{
    public class Sql
    {
        public Sql(string query, SqlSegment segment, Dictionary<string, object> parameters)
        {
            if(string.IsNullOrEmpty(query)) throw new Exception("'Query' shouldn't be empty.");

            Query = query;
            Segment = segment;
            Parameters = parameters;
        }

        public string Query { get; }
        public SqlSegment Segment { get; }

        public Dictionary<string, object> Parameters { get; }
    }
}
