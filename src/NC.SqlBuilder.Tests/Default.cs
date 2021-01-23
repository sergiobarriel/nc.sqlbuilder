using System.Collections.Generic;
using NC.SqlBuilder.Models;

namespace NC.SqlBuilder.Tests
{
    public static class Default
    {
        public static readonly string DefaultTableName = "MyTable";
        public static readonly string DefaultSchema = "dbo";
        public static readonly Table DefaultTable = new Table(DefaultTableName, DefaultSchema);

        public static readonly IEnumerable<string> DefaultFields = new List<string>() { "One", "Two", "Three" };

        public static readonly Pagination DefaultPagination = new Pagination(0, 10);
    }
}
