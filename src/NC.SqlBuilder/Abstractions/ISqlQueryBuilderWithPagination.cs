using NC.SqlBuilder.Models.Output;

namespace NC.SqlBuilder.Abstractions
{
    public interface ISqlQueryBuilderWithPagination
    {
        Sql Build();
    }
}