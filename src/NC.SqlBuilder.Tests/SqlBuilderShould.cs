using System.Collections.Generic;
using NC.SqlBuilder.Models;
using Xunit;

namespace NC.SqlBuilder.Tests
{
    public class SqlBuilderShould
    { 
        [Fact]
        public void complete_query()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One", "Two" })
                .AddConditions(new List<Condition>() { new Condition("One", Operator.Equals, "one") })
                .AddOrder(new Order("One", Direction.Ascending))
                .AddPagination(new Pagination(0, 10))
                .Build();

            var query = "SELECT [One], [Two] FROM [dbo].[MyTable] WHERE [One] = @One ORDER BY [One] ASC OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";

            Assert.Equal(builder.Query, query);
        }

    }
}
