using NC.SqlBuilder.Models;
using NC.SqlBuilder.Models.Operations;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace NC.SqlBuilder.Tests
{
    public class GeoSpatialShould
    {
        private NumberFormatInfo Format { get; set; }

        public GeoSpatialShould()
        {
            Format = new NumberFormatInfo { NumberDecimalSeparator = "." };
        }


        [Fact]
        public void near_custom_operator()
        {
            var latitude = 40.415839121335964;
            var longitude = -3.6839833344870967;
            var radio = 50;

            var builder = Builder.Create()
                .ToTable(new Table("MyTable"))
                .AddFields(new[] { "One" })
                .AddConditions(new List<Condition>() 
                {
                    new Condition(new SimpleOperation("One", Operator.Equals, "one")),
                    new Condition(new CoordinatesOperation("Point", Operator.Near, latitude, longitude, radio))
                })
                .AddOrder(new Order("One", Direction.Ascending))
                .AddPagination(new Pagination(0, 10))
                .Build();

            var expectedQuery = $"SELECT [One], [Point].STDistance('POINT({latitude.ToString(Format)} {longitude.ToString(Format)})') AS 'Distance' FROM [dbo].[MyTable] WHERE [One] = @One AND [Point].STDistance('POINT({latitude.ToString(Format)} {longitude.ToString(Format)})') IS NOT NULL AND [Point].STDistance('POINT({latitude.ToString(Format)} {longitude.ToString(Format)})') < 50 ORDER BY [One] ASC OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";

            Assert.Equal(expectedQuery, builder.Query);

            Assert.Contains(builder.Parameters, item => item.Key == "One");

            Assert.Contains(builder.Parameters, item => item.Key == "One" && (string)item.Value == "one");

        }
    }
}
