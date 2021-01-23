using System.Collections.Generic;
using NC.SqlBuilder.Models;

namespace NC.SqlBuilder.Abstractions
{
    public interface ISqlQueryBuilder
    {
        ISqlQueryBuilderWithTable ToTable(Table table);
        ISqlQueryBuilderWithTable ToTables(IEnumerable<Table> tables);
    }

    public interface ISqlQueryBuilderWithTable
    {
        ISqlQueryBuilderWithSelect AddFields(IEnumerable<string> fields);
        ISqlQueryBuilderWithSelect AddAllFields();
    }

    public interface ISqlQueryBuilderWithSelect
    {
        ISqlQueryBuilderWithWhere AddConditions(IEnumerable<Condition> conditions);
    }

    public interface ISqlQueryBuilderWithWhere
    {
        ISqlQueryBuilderWithOrder AddOrder(Order order);
        ISqlQueryBuilderWithOrder AddOrders(IEnumerable<Order> orders);
    }

    public interface ISqlQueryBuilderWithOrder
    {
        ISqlQueryBuilderWithPagination AddPagination(Pagination pagination);
    }

    public interface ISqlQueryBuilderWithPagination
    {
        Sql Build();
    }
}
