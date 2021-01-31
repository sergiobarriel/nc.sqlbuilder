using System.Collections.Generic;
using NC.SqlBuilder.Models;
using Xunit;

namespace NC.SqlBuilder.Tests
{
    public class SqlWhereShould
    {
        [Fact]
        public void query_with_conditions()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(new List<Condition>()
                {
                    new Condition(new SimpleOperation("One", Operator.Equals, "value 1")),
                    new Condition(new SimpleOperation("Two", Operator.Equals, "value 2")),
                })
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var query = "SELECT [One], [Two] FROM [dbo].[MyTable] WHERE [One] = @One AND [Two] = @Two";
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
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(null)
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var query = "SELECT [One], [Two] FROM [dbo].[MyTable]";
            var where = "";

            Assert.Equal(builder.Segment.Where, where);
            Assert.Equal(builder.Query, query);
        }

        [Fact]
        public void query_with_all_operators()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] {"One", "Two" })
                .AddConditions(new List<Condition>()
                {
                    new Condition(new SimpleOperation("One", Operator.Equals, "value 1")),
                    new Condition(new SimpleOperation("Two", Operator.GreaterThan, "value 2")),
                    new Condition(new SimpleOperation("Three", Operator.GreaterThanOrEqual, "value 3")),
                    new Condition(new SimpleOperation("Four", Operator.LessThan, "value 4")),
                    new Condition(new SimpleOperation("Five", Operator.LessThanOrEqual, "value 5")),
                    new Condition(new SimpleOperation("Six", Operator.Like, "value 6")),
                    new Condition(new BetweenOperation("Seven", Operator.Between, "value 7a", "value 7b")),
                })
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var query = "SELECT [One], [Two] FROM [dbo].[MyTable] WHERE [One] = @One AND [Two] > @Two AND [Three] >= @Three AND [Four] < @Four AND [Five] <= @Five AND [Six] LIKE '%@Six%' AND [Seven] BETWEEN @Seven_DOWN AND @Seven_UP";
            var where = "WHERE [One] = @One AND [Two] > @Two AND [Three] >= @Three AND [Four] < @Four AND [Five] <= @Five AND [Six] LIKE '%@Six%' AND [Seven] BETWEEN @Seven_DOWN AND @Seven_UP";

            Assert.Equal(builder.Segment.Where, where);
            Assert.Equal(builder.Query, query);

            Assert.Contains(builder.Parameters, item => item.Key == "One");
            Assert.Contains(builder.Parameters, item => item.Key == "Two");
            Assert.Contains(builder.Parameters, item => item.Key == "Three");
            Assert.Contains(builder.Parameters, item => item.Key == "Four");
            Assert.Contains(builder.Parameters, item => item.Key == "Five");
            Assert.Contains(builder.Parameters, item => item.Key == "Six");
            Assert.Contains(builder.Parameters, item => item.Key == "Seven_DOWN");
            Assert.Contains(builder.Parameters, item => item.Key == "Seven_UP");

            Assert.Contains(builder.Parameters, item => item.Key == "One" && (string)item.Value == "value 1");
            Assert.Contains(builder.Parameters, item => item.Key == "Two" && (string)item.Value == "value 2");
            Assert.Contains(builder.Parameters, item => item.Key == "Three" && (string)item.Value == "value 3");
            Assert.Contains(builder.Parameters, item => item.Key == "Four" && (string)item.Value == "value 4");
            Assert.Contains(builder.Parameters, item => item.Key == "Five" && (string)item.Value == "value 5");
            Assert.Contains(builder.Parameters, item => item.Key == "Six" && (string)item.Value == "value 6");
            Assert.Contains(builder.Parameters, item => item.Key == "Seven_DOWN" && (string)item.Value == "value 7b");
            Assert.Contains(builder.Parameters, item => item.Key == "Seven_UP" && (string)item.Value == "value 7a");
        }
    }
}