using NC.SqlBuilder.Models;

namespace NC.SqlBuilder.Abstractions.Fluent
{
    public interface ISqlQueryBuilderWithOrder
    {
        ISqlQueryBuilderWithPagination AddPagination(Pagination pagination);
    }
}