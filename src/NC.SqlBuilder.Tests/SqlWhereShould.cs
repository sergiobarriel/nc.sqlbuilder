﻿using System.Collections.Generic;
using NC.SqlBuilder.Models;
using Xunit;

namespace NC.SqlBuilder.Tests
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

        [Fact]
        public void query_with_all_operators()
        {
            var builder = SqlBuilder.Create()
                .ToTable(Default.DefaultTable)
                .AddFields(Default.DefaultFields)
                .AddConditions(new List<Condition>()
                {
                    new Condition("One", Operator.Equals, "value 1"),
                    new Condition("Two", Operator.GreaterThan, "value 2"),
                    new Condition("Three", Operator.GreaterThanOrEqual, "value 3"),
                    new Condition("Four", Operator.LessThan, "value 4"),
                    new Condition("Five", Operator.LessThanOrEqual, "value 5"),
                    new Condition("Six", Operator.Like, "value 6"),
                })
                .AddOrder(null)
                .AddPagination(null)
                .Build();

            var query = "SELECT [One], [Two], [Three] FROM [dbo].[MyTable] WHERE [One] = @One AND [Two] > @Two AND [Three] >= @Three AND [Four] < @Four AND [Five] <= @Five AND [Six] LIKE '%@Six%'";
            var where = "WHERE [One] = @One AND [Two] > @Two AND [Three] >= @Three AND [Four] < @Four AND [Five] <= @Five AND [Six] LIKE '%@Six%'";

            Assert.Equal(builder.Segment.Where, where);
            Assert.Equal(builder.Query, query);

            Assert.Contains(builder.Parameters, item => item.Key == "One");
            Assert.Contains(builder.Parameters, item => item.Key == "Two");
            Assert.Contains(builder.Parameters, item => item.Key == "Three");
            Assert.Contains(builder.Parameters, item => item.Key == "Four");
            Assert.Contains(builder.Parameters, item => item.Key == "Five");
            Assert.Contains(builder.Parameters, item => item.Key == "Six");

            Assert.Contains(builder.Parameters, item => item.Key == "One" && (string)item.Value == "value 1");
            Assert.Contains(builder.Parameters, item => item.Key == "Two" && (string)item.Value == "value 2");
            Assert.Contains(builder.Parameters, item => item.Key == "Three" && (string)item.Value == "value 3");
            Assert.Contains(builder.Parameters, item => item.Key == "Four" && (string)item.Value == "value 4");
            Assert.Contains(builder.Parameters, item => item.Key == "Five" && (string)item.Value == "value 5");
            Assert.Contains(builder.Parameters, item => item.Key == "Six" && (string)item.Value == "value 6");
        }
    }
}