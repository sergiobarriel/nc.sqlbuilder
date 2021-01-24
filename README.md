# NC.SqlBuilder

## CI/CD status

![build & test](https://github.com/sergiobarriel/sqlbuilder/workflows/build%20&%20test%20package/badge.svg) ![publish](https://github.com/sergiobarriel/sqlbuilder/workflows/publish%20package/badge.svg)

Package available on [NuGet gallery](https://www.nuget.org/packages/NC.SqlBuilder/)

## About

**NC.SqlBuilder** is a tool with which to create SQL queries through a simple fluent API.

## Use

You can lean on ```Table```, ```Condition```, ```Order``` and ```Pagination``` types to build different query segments.

With ```SqlBuilder``` type you can start the fluent builder and populate with previous types.

```csharp
var builder = SqlBuilder.Create()
    .ToTable(new Table("Users", "dbo"))
    .AddFields(new List<string>() { "Id", "Name", "Email", "Age"})
    .AddConditions(new List<Condition>() { new Condition("Age", Operator.GreaterThan, "7") })
    .AddOrder(new Order("Id", Direction.Ascending))
    .AddPagination(new Pagination(0, 10))
    .Build();
```

After ```Build()``` method, you will get an output with SQL query and a parameters dictionary.

```sql
SELECT [Id], [Name], [Email], [Age] 
FROM [dbo].[Users] 
WHERE [Age] > @Age 
ORDER BY [Id] ASC 
OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY
```

Then, you can query your database by using ```SqlConnection``` (for example, through [Dapper](https://www.nuget.org/packages/Dapper/) tool)
```csharp
using (var connection = new SqlConnection(connectionString))
{
    var users = await connection.QueryAsync<User>(builder.Query, builder.Parameters);
}
```

## Contact
You can contact me via Twitter [@sergiobarriel](https://twitter.com/sergiobarriel)
