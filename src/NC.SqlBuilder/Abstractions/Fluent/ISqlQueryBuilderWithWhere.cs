using System.Collections.Generic;
using NC.SqlBuilder.Models;

namespace NC.SqlBuilder.Abstractions.Fluent
{
    public interface ISqlQueryBuilderWithWhere
    {
        ISqlQueryBuilderWithOrder AddOrder(Order order);
        ISqlQueryBuilderWithOrder AddOrders(IEnumerable<Order> orders);
    }
}