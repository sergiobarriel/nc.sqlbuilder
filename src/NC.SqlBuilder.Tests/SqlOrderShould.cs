using NC.SqlBuilder.Models;
using Xunit;

namespace NC.SqlBuilder.Tests
{
    public class SqlOrderShould
    {
        [Fact]
        public void query_with_ascending_order()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(null)
                .AddOrder(new Order("One", Direction.Ascending))
                .AddPagination(null)
                .Build();

            var expectedQuery = "SELECT [One], [Two] FROM [dbo].[MyTable] ORDER BY [One] ASC";
            var expectedOrder = "ORDER BY [One] ASC";

            Assert.Equal(expectedOrder, builder.Segment.Order);
            Assert.Equal(expectedQuery, builder.Query);
        }

        [Fact]
        public void query_with_descending_order()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(null)
                .AddOrder(new Order("One", Direction.Descending))
                .AddPagination(null)
                .Build();

            var expectedQuery = "SELECT [One], [Two] FROM [dbo].[MyTable] ORDER BY [One] DESC";
            var expectedOrder = "ORDER BY [One] DESC";

            Assert.Equal(expectedOrder, builder.Segment.Order);
            Assert.Equal(expectedQuery, builder.Query);
        }

    }
}