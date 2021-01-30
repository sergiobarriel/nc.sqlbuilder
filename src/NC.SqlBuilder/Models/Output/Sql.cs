using System;
using System.Collections.Generic;

namespace NC.SqlBuilder.Models.Output
{
    public class Sql
    {
        public Sql(string query, string queryForTotal, SqlSegment segment, Dictionary<string, object> parameters)
        {
            if(string.IsNullOrEmpty(query)) throw new Exception("'Query' shouldn't be empty.");

            Query = query;
            QueryForTotal = queryForTotal;
            Segment = segment;
            Parameters = parameters;
        }

        public string Query { get; }
        public string QueryForTotal { get; }
        public SqlSegment Segment { get; }

        public Dictionary<string, object> Parameters { get; }
    }
}
