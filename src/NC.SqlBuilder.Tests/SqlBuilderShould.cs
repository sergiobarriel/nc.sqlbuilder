﻿using System.Collections.Generic;
using NC.SqlBuilder.Models;
using NC.SqlBuilder.Models.Operations;
using Xunit;

namespace NC.SqlBuilder.Tests
{
    public class SqlBuilderShould
    { 
        [Fact]
        public void full_query_a()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(new List<Condition>()
                {
                    new Condition(new SimpleOperation("One", Operator.Equals, "one")),
                })
                .AddOrder(new Order("One", Direction.Ascending))
                .AddPagination(new Pagination(0, 10))
                .Build();

            var expectedQuery = "SELECT [One], [Two] FROM [dbo].[MyTable] WHERE [One] = @One ORDER BY [One] ASC OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";

            Assert.Equal(expectedQuery, builder.Query);
        }

        [Fact]
        public void full_query_b()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(new List<Condition>()
                {
                    new Condition(new SimpleOperation("One", Operator.Equals, "one")), 
                    new Condition(new SimpleOperation("Two", Operator.LessThan, "10")),
                })
                .AddOrder(new Order("One", Direction.Ascending))
                .AddPagination(new Pagination(0, 10))
                .Build();

            var expectedQuery = "SELECT [One], [Two] FROM [dbo].[MyTable] WHERE [One] = @One AND [Two] < @Two ORDER BY [One] ASC OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";

            Assert.Equal(expectedQuery, builder.Query);

            Assert.Contains(builder.Parameters, item => item.Key == "One");
            Assert.Contains(builder.Parameters, item => item.Key == "Two");

            Assert.Contains(builder.Parameters, item => item.Key == "One" && (string)item.Value == "one");
            Assert.Contains(builder.Parameters, item => item.Key == "Two" && (string)item.Value == "10");

        }


        [Fact]
        public void query_with_many_without_fluent_methods()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] {"One", "Two"})
                .WithoutConditions()
                .WithoutOrder()
                .WithoutPagination()
                .Build();

            var expectedQuery = "SELECT [One], [Two] FROM [dbo].[MyTable]";

            Assert.Equal(expectedQuery, builder.Query);
        }


    }
}
