using NC.SqlBuilder.Models;

namespace NC.SqlBuilder.Abstractions
{
    public interface ISqlQueryBuilderWithOrder
    {
        ISqlQueryBuilderWithPagination AddPagination(Pagination pagination);
    }
}