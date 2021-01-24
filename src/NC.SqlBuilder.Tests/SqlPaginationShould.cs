using Xunit;

namespace NC.SqlBuilder.Tests
{
    public class SqlPaginationShould
    {
        [Fact]
        public void query_with_pagination()
        {
            var sql = Builder.Create()
                .ToTable(Default.DefaultTable)
                .AddFields(Default.DefaultFields)
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(Default.DefaultPagination)
                .Build();

            var query = "SELECT [One], [Two], [Three] FROM [dbo].[MyTable] OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";
            var pagination = "OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";

            Assert.Equal(sql.Query, query);
            Assert.Equal(sql.Segment.Pagination, pagination);
        }
    }
}