using System;
using NC.SqlBuilder.Models;
using Xunit;

namespace NC.SqlBuilder.Tests
{
    public class SqlPaginationShould
    {
        [Fact]
        public void query_with_pagination()
        {
            var sql = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(new Pagination(0, 10))
                .Build();

            var query = "SELECT [One], [Two] FROM [dbo].[MyTable] OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";
            var pagination = "OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";

            Assert.Equal(sql.Query, query);
            Assert.Equal(sql.Segment.Pagination, pagination);
        }

        [Fact]
        public void query_with_negative_first_value()
        {
            Assert.Throws<Exception>(() => Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(new Pagination(-1, 10))
                .Build());
        }

        [Fact]
        public void query_with_negative_size_value()
        {
            Assert.Throws<Exception>(() => Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(new Pagination(0, -10))
                .Build());
        }
    }
}