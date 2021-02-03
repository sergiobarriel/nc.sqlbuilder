using System.Collections.Generic;
using NC.SqlBuilder.Models;
using NC.SqlBuilder.Models.Operations;
using Xunit;

namespace NC.SqlBuilder.Tests
{
    public class GeoSpatialShould
    {

        [Fact]
        public void near_custom_operator()
        {
            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One" })
                .AddConditions(new List<Condition>()
                    {
                        new Condition(new SimpleOperation("One", Operator.Equals, "one")),
                        new Condition(new CoordinatesOperation("Point", Operator.Near, 40.415839121335964, -3.6839833344870967, 50)),}
                )
                .AddOrder(new Order("One", Direction.Ascending))
                .AddPagination(new Pagination(0, 10))
                .Build();

            var query = "SELECT [One], [Point].STDistance('POINT(40.415839121335964 -3.6839833344870967)') AS 'Distance' FROM [dbo].[MyTable] WHERE [One] = @One AND [Point].STDistance('POINT(40.415839121335964 -3.6839833344870967)') IS NOT NULL AND [Point].STDistance('POINT(40.415839121335964 -3.6839833344870967)') < 50 ORDER BY [One] ASC OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";

            Assert.Equal(query, builder.Query);

            Assert.Contains(builder.Parameters, item => item.Key == "One");

            Assert.Contains(builder.Parameters, item => item.Key == "One" && (string)item.Value == "one");

        }
    }
}
