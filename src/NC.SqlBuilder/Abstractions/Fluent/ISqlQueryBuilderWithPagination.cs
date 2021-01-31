using NC.SqlBuilder.Models.Output;

namespace NC.SqlBuilder.Abstractions.Fluent
{
    public interface ISqlQueryBuilderWithPagination
    {
        Sql Build();
    }
}