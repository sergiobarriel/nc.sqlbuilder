﻿using System.Collections.Generic;
using NC.SqlBuilder.Models;

namespace NC.SqlBuilder.Abstractions
{
    public interface ISqlQueryBuilder
    {
        ISqlQueryBuilderWithTable ToTable(Table table);
        ISqlQueryBuilderWithTable ToTable(string table);
        ISqlQueryBuilderWithTable ToTables(IEnumerable<Table> tables);
    }
}
