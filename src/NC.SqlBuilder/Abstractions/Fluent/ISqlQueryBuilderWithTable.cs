using System.Collections.Generic;

namespace NC.SqlBuilder.Abstractions.Fluent
{
    public interface ISqlQueryBuilderWithTable
    {
        ISqlQueryBuilderWithSelect AddFields(IEnumerable<string> fields);
        ISqlQueryBuilderWithSelect AddAllFields();
    }
}