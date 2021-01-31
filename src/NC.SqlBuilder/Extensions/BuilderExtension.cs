using NC.SqlBuilder.Models;
using System.Collections.Generic;
using System.Linq;

namespace NC.SqlBuilder.Extensions
{
    public static class BuilderExtension
    {
        public static bool IsGeoSpatial(this IEnumerable<Condition> conditions) => conditions.Any(condition => condition.Operator == Operator.Near);
    }
}
