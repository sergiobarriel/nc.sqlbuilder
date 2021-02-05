using NC.SqlBuilder.Abstractions.Fluent;
using NC.SqlBuilder.Models;
using NC.SqlBuilder.Models.Output;
using System.Collections.Generic;
using System.Linq;

namespace NC.SqlBuilder
{
    public partial class Builder :
        ISqlQueryBuilder,
        ISqlQueryBuilderWithTable,
        ISqlQueryBuilderWithSelect,
        ISqlQueryBuilderWithWhere,
        ISqlQueryBuilderWithOrder,
        ISqlQueryBuilderWithPagination

    {
        public static ISqlQueryBuilder Create() => new Builder();

        public ISqlQueryBuilderWithTable ToTable(string table) => ToTable(new Table(table));

        public ISqlQueryBuilderWithTable ToTable(Table table)
        {
            if (table != null)
            {
                Tables.Add(table);
            }

            return this;
        }
        public ISqlQueryBuilderWithTable ToTables(IEnumerable<Table> tables)
        {
            if (tables != null && tables.Any())
            {
                Tables = tables as IList<Table>;
            }

            return this;
        }

        public ISqlQueryBuilderWithSelect AddFields(IEnumerable<string> fields)
        {
            if (fields != null)
            {
                Fields = fields;
            }
            return this;
        }
        public ISqlQueryBuilderWithSelect AddAllFields()
        {
            AllFields = true;
            return this;
        }

        public ISqlQueryBuilderWithWhere AddConditions(IEnumerable<Condition> conditions)
        {
            if (conditions != null)
            {
                Conditions = conditions;
            }

            return this;
        }

        public ISqlQueryBuilderWithWhere WithoutConditions() => this;
        

        public ISqlQueryBuilderWithOrder AddOrder(Order order)
        {
            if (order != null)
            {
                Orders.Add(order);
            }

            return this;
        }
        public ISqlQueryBuilderWithOrder AddOrders(IEnumerable<Order> orders)
        {
            if (orders != null && orders.Any())
            {
                Orders = orders as IList<Order>;
            }

            return this;
        }

        public ISqlQueryBuilderWithOrder WithoutOrder() => this;

        public ISqlQueryBuilderWithPagination AddPagination(Pagination pagination)
        {
            if (pagination != null)
            {
                Pagination = pagination;
            }

            return this;
        }

        public ISqlQueryBuilderWithPagination WithoutPagination() => this;

        public Sql Build()
        {
            var select = Clean($"{BuildSelect()}");
            var selectForTotal = Clean($"{BuildSelectForTotal()}");

            var from = Clean($" {BuildFrom()}");
            var where = Clean($"{BuildWhere()}");
            var order = Clean($" {BuildOrder()}");
            var pagination = Clean($"{BuildPagination()}");

            var query = Clean($"{select} {from} {where} {order} {pagination}");
            var queryForTotal = Clean($"{selectForTotal} {from} {where}");

            var segment = new SqlSegment(select, from, where, order, pagination);

            return new Sql(query, queryForTotal, segment, Parameters);
        }


    }
}
