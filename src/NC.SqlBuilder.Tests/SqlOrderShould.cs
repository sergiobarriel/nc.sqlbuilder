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

            var query = "SELECT [One], [Two] FROM [dbo].[MyTable] ORDER BY [One] ASC";
            var order = "ORDER BY [One] ASC";

            Assert.Equal(builder.Segment.Order, order);
            Assert.Equal(builder.Query, query);
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

            var query = "SELECT [One], [Two] FROM [dbo].[MyTable] ORDER BY [One] DESC";
            var order = "ORDER BY [One] DESC";

            Assert.Equal(builder.Segment.Order, order);
            Assert.Equal(builder.Query, query);
        }

        //[Fact]
        //public void query_with_unknown_field_to_sort()
        //{
        //    var builder = Builder.Create()
        //        .ToTable(new Table("MyTable"))
        //        .AddFields(new[] { "One", "Two" })
        //        .AddConditions(null)
        //        .AddOrder(new Order("Unknown", Direction.Descending))
        //        .AddPagination(null);


        //    Assert.Throws<Exception>(() => builder.Build());
        //}

    }
}