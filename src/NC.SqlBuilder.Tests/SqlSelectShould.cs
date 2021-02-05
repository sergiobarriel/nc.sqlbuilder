using System;
using NC.SqlBuilder.Models;
using Xunit;

namespace NC.SqlBuilder.Tests
{
    public class SqlSelectShould
    {
        [Fact]
        public void query_without_fields_throws_exception()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(null)
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null);

            Assert.Throws<Exception>(() =>  builder.Build() );
        }

        [Fact]
        public void query_with_many_fields_returns_query()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var expectedQuery = "SELECT [One], [Two] FROM [dbo].[MyTable]";
            var expectedOrder = "SELECT [One], [Two]";

            Assert.Equal(expectedOrder, builder.Segment.Select);
            Assert.Equal(expectedQuery, builder.Query);
        }

        [Fact]
        public void query_with_all_fields_returns_query()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddAllFields()
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var expectedQuery = "SELECT * FROM [dbo].[MyTable]";
            var expectedSelect = "SELECT *";

            Assert.Equal(expectedSelect, builder.Segment.Select);
            Assert.Equal(expectedQuery, builder.Query);
        }
    }
}