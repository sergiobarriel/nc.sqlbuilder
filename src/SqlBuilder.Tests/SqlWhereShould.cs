using System.Collections.Generic;
using SqlBuilder.Models;
using Xunit;

namespace SqlBuilder.Tests
{
    public class SqlWhereShould
    {
        [Fact]
        public void query_with_conditions()
        {
            var builder = SqlBuilder.Create()
                .ToTable(Default.DefaultTable)
                .AddFields(Default.DefaultFields)
                .AddConditions(new List<Condition>()
                {
                    new Condition("One", Operator.Equals, "value 1"),
                    new Condition("Two", Operator.Equals, "value 2"),
                })
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var query = "SELECT [One], [Two], [Three] FROM [dbo].[MyTable] WHERE [One] = @One AND [Two] = @Two";
            var where = "WHERE [One] = @One AND [Two] = @Two";

            Assert.Equal(builder.Segment.Where, where);
            Assert.Equal(builder.Query, query);

            Assert.Contains(builder.Parameters, item => item.Key == "One");
            Assert.Contains(builder.Parameters, item => item.Key == "Two");

            Assert.Contains(builder.Parameters, item => item.Key == "One" && (string)item.Value == "value 1");
            Assert.Contains(builder.Parameters, item => item.Key == "Two" && (string)item.Value == "value 2");
        }

        [Fact]
        public void query_without_conditions()
        {
            var builder = SqlBuilder.Create()
                .ToTable(Default.DefaultTable)
                .AddFields(Default.DefaultFields)
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var query = "SELECT [One], [Two], [Three] FROM [dbo].[MyTable]";
            var where = "";

            Assert.Equal(builder.Segment.Where, where);
            Assert.Equal(builder.Query, query);
        }


    }
}