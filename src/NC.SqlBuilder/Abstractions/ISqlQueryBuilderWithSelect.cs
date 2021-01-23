using System.Collections.Generic;
using NC.SqlBuilder.Models;

namespace NC.SqlBuilder.Abstractions
{
    public interface ISqlQueryBuilderWithSelect
    {
        ISqlQueryBuilderWithWhere AddConditions(IEnumerable<Condition> conditions);
    }
}