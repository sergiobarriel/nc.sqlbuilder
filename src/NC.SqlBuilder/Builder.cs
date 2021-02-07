using NC.SqlBuilder.Extensions;
using NC.SqlBuilder.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace NC.SqlBuilder
{
    public partial class Builder 
    {
        private IList<Table> Tables { get; set; }
        private IEnumerable<string> Fields { get; set; }
        private bool AllFields { get; set; }
        private IEnumerable<Condition> Conditions { get; set; }
        private Dictionary<string, object> Parameters { get; set; }
        private IList<Order> Orders { get; set; }
        private Pagination Pagination { get; set; }
        private List<string> BlackList { get; set; }
        private NumberFormatInfo Format { get; }

        private Builder()
        {
            Tables = new List<Table>();
            Fields = new List<string>();
            Conditions = new List<Condition>();
            Orders = new List<Order>();
            Parameters = new Dictionary<string, object>();
            AllFields = false;

            BlackList = new List<string>() { "DELETE", "TRUNCATE", "AND", "SELECT", "UPDATE", ";" };

            Format = new NumberFormatInfo { NumberDecimalSeparator = "." };
        }


        #region SELECT segment

        private string BuildSelect()
        {
            if ((Fields == null || !Fields.Any()) && !AllFields) throw new Exception("'Fields' can not be null or empty.");

            var select = AllFields || (Fields == null || !Fields.Any())
                ? "SELECT *"
                : $"SELECT {string.Join(", ", Fields.Select(field => $"[{field}]"))}";

            if (Conditions.IsGeoSpatial())
            {
                var geoSpatialSelect = new List<string>();

                foreach (var condition in Conditions.Where(con => con.Operator == Operator.Near))
                {
                    geoSpatialSelect.Add($"[{condition.Field}].STDistance('POINT({condition.Latitude.ToString(Format)} {condition.Longitude.ToString(Format)})') AS 'Distance'");
                }

                select = $"{select}, {string.Join(", ", geoSpatialSelect)}";
            }

            return select;
        }


        private string BuildSelectForTotal() => $"SELECT COUNT(*)";

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
            if (BlackList.Contains(Conditions.SelectMany(condition => condition.Field))) throw new Exception($"'Field' contains blacklist word.");
            if (BlackList.Contains(Conditions.SelectMany(condition => condition.Value))) throw new Exception($"'Value' contains blacklist word.");

            var where = new List<string>();

            if (Conditions.Any())
            {
                foreach (var condition in Conditions)
                {
                    switch (condition.Operator)
                    {
                        case Operator.Equals:

                            where.Add($"[{condition.Field}] = @{condition.Field}");

                            Parameters.Add(condition.Field, condition.Value);

                            break;
  
                        case Operator.Like:

                            where.Add($"[{condition.Field}] LIKE '%@{condition.Field}%'");

                            Parameters.Add(condition.Field, condition.Value);

                            break;
                        case Operator.LessThan:

                            where.Add($"[{condition.Field}] < @{condition.Field}");

                            Parameters.Add(condition.Field, condition.Value);

                            break;
                        case Operator.LessThanOrEqual:

                            where.Add($"[{condition.Field}] <= @{condition.Field}");

                            Parameters.Add(condition.Field, condition.Value);

                            break;
                        case Operator.GreaterThan:

                            where.Add($"[{condition.Field}] > @{condition.Field}");

                            Parameters.Add(condition.Field, condition.Value);

                            break;
                        case Operator.GreaterThanOrEqual:

                            where.Add($"[{condition.Field}] >= @{condition.Field}");
                            
                            Parameters.Add(condition.Field, condition.Value);

                            break;
                        case Operator.Between:

                            where.Add($"[{condition.Field}] BETWEEN @{@condition.Field}_LEFT AND @{condition.Field}_RIGHT");

                            Parameters.Add($"{condition.Field}_LEFT", condition.Left);
                            Parameters.Add($"{condition.Field}_RIGHT", condition.Right);

                            break;

                        case Operator.Near:

                            where.Add($"[{condition.Field}].STDistance('POINT({condition.Latitude.ToString(Format)} {condition.Longitude.ToString(Format)})') IS NOT NULL");
                            where.Add($"[{condition.Field}].STDistance('POINT({condition.Latitude.ToString(Format)} {condition.Longitude.ToString(Format)})') < {condition.Radio}");

                            break;
                        
                        default:
                            throw new ArgumentOutOfRangeException(nameof(condition.Operator), condition.Operator, null);
                    }
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
            var orderBy = string.Join(", ", Orders.Select(order => $"[{order.Field}] {GetDirectionString(order.Direction)}"));

            return string.IsNullOrEmpty(orderBy)
                ? string.Empty
                : $"ORDER BY {orderBy}";

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
            sql = RemoveBlanks(sql);

            return sql.Trim();
        }

        private string RemoveBlanks(string sql)
        {
            var regex = new Regex("[ ]{2,}", RegexOptions.None);
            return regex.Replace(sql, " ");
        }

    }

  
}
