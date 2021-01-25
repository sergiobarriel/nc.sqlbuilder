using System;

namespace NC.SqlBuilder.Models
{
    public class Table
    {
        public Table() { }

        public Table(string name, string schema = "dbo")
        {
            if(string.IsNullOrEmpty(name)) throw new Exception("'Name' shouldn't be empty.");

            Name = name;
            Schema = schema;
        }

        public string Name { get; }
        public string Schema { get; }
    }
}
