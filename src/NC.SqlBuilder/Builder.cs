using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NC.SqlBuilder.Abstractions;
using NC.SqlBuilder.Models;

namespace NC.SqlBuilder
{
    public class Builder : 
        ISqlQueryBuilder, 
        ISqlQueryBuilderWithTable, 
        ISqlQueryBuilderWithSelect, 
        ISqlQueryBuilderWithWhere, 
        ISqlQueryBuilderWithOrder, 
        ISqlQueryBuilderWithPagination
    {
        private IList<Table> Tables { get; set; }
        private IEnumerable<string> Fields { get; set; }
        private bool AllFields { get; set; }
        private IEnumerable<Condition> Conditions { get; set; }
        private Dictionary<string, object> Parameters { get; set; }
        private IList<Order> Orders { get; set; }
        private Pagination Pagination { get; set; }
        private List<string> BlackList { get; set; }

        #region Fluent flow

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
        public ISqlQueryBuilderWithPagination AddPagination(Pagination pagination)
        {
            if (pagination != null)
            {
                Pagination = pagination;
            }

            return this;
        }

        public Sql Build()
        {
            var select = Clean($"{BuildSelect()}");
            var from = Clean($" {BuildFrom()}");
            var where = Clean($"{BuildWhere()}");
            var order = Clean($" {BuildOrder()}");
            var pagination = Clean($"{BuildPagination()}");

            var query = Clean($"{select} {from} {where} {order} {pagination}");
            var segment = new SqlSegment(select, from, where, order, pagination);
            
            return new Sql(query, segment, Parameters);
        }

        #endregion

        private Builder()
        {
            Tables = new List<Table>();
            Fields = new List<string>();
            Conditions = new List<Condition>();
            Orders = new List<Order>();
            Parameters = new Dictionary<string, object>();
            AllFields = false;

            BlackList = new List<string>() { "DELETE", "TRUNCATE", "AND", "SELECT", "UPDATE", ";" };
        }


        #region SELECT segment

        private string BuildSelect()
        {
            if ((Fields == null || !Fields.Any()) && !AllFields) throw new Exception("Fields can not be null or empty.");

            return AllFields || (Fields == null || !Fields.Any())
                ? "SELECT *"
                : $"SELECT {string.Join(", ", Fields.Select(field => $"[{field}]"))}";
        }

        #endregion

        #region FROM segment

        private string BuildFrom()
        {
            if (Tables == null) throw new Exception("Tables can not be null.");
            if (!Tables.Any()) throw new Exception("Tables can not be empty.");

            if (Tables.Any() && Tables.Count() == 1)
            {
                return $"FROM [{Tables.First().Schema}].[{Tables.First().Name}]";
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region WHERE segment

        private string BuildWhere()
        {
            if (BlackList.Contains(Conditions.SelectMany(condition => condition.Field))) throw new Exception($"Field contains blacklist word");
            if (BlackList.Contains(Conditions.SelectMany(condition => condition.Value))) throw new Exception($"Value contains blacklist word");

            var where = new List<string>();

            if (Conditions.Any())
            {
                foreach (var condition in Conditions)
                {
                    switch (condition.Operator)
                    {
                        case Operator.Equals:

                            where.Add($"[{condition.Field}] = @{condition.Field}");

                            break;
  
                        case Operator.Like:

                            where.Add($"[{condition.Field}] LIKE '%@{condition.Field}%'");

                            break;
                        case Operator.LessThan:

                            where.Add($"[{condition.Field}] < @{condition.Field}");

                            break;
                        case Operator.LessThanOrEqual:

                            where.Add($"[{condition.Field}] <= @{condition.Field}");

                            break;
                        case Operator.GreaterThan:

                            where.Add($"[{condition.Field}] > @{condition.Field}");

                            break;
                        case Operator.GreaterThanOrEqual:

                            where.Add($"[{condition.Field}] >= @{condition.Field}");

                            break;
                        case Operator.Between:

                            //var f = condition.Value.Split(".");

                            //where.Add($"[{condition.Field}] BETWEEN @{@condition.Field}_A AND @{condition.Field}_B");

                            throw new NotImplementedException();

                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(condition.Operator), condition.Operator, null);
                    }

                    Parameters.Add(condition.Field, condition.Value);
                }
            }

            return !where.Any()
                ? string.Empty
                : $"WHERE {string.Join(" AND ", @where)}";
        }

        #endregion

        #region ORDER segment

        private string BuildOrder()
        {
            ValidateIfOrderingFieldsExistsOnFields();

            var orderBy = string.Join(", ", Orders.Select(order => $"[{order.Field}] {GetDirectionString(order.Direction)}"));

            return string.IsNullOrEmpty(orderBy)
                ? string.Empty
                : $"ORDER BY {orderBy}";

        }

        private void ValidateIfOrderingFieldsExistsOnFields()
        {
            foreach (var order in Orders)
            {
                if (!Fields.Contains(order.Field)) throw new Exception($"Field {order.Field} not included on select.");
            }
        }
        #endregion

        #region PAGINATION segment

        private string BuildPagination()
        {
            return Pagination == null
                ? string.Empty
                : $"OFFSET {Pagination.First} ROWS FETCH NEXT {Pagination.Size} ROWS ONLY";
        }

        private string GetDirectionString(Direction direction)
        {
            switch (direction)
            {
                case Direction.Ascending:
                    return "ASC";
                case Direction.Descending:
                    return "DESC";
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        #endregion

        private string Clean(string sql)
        {
            static string RemoveBlanks(string sql)
            {
                var regex = new Regex("[ ]{2,}", RegexOptions.None);
                return regex.Replace(sql, " ");
            }

            static string Trim(string sql) => sql.Trim();

            sql = RemoveBlanks(sql);
            sql = Trim(sql);

            return sql;
        }


    }
}
