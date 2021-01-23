using NC.SqlBuilder.Models;

namespace NC.SqlBuilder.Abstractions
{
    public interface ISqlQueryBuilderWithPagination
    {
        Sql Build();
    }
}