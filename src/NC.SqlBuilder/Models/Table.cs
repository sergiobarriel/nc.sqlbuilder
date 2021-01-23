namespace NC.SqlBuilder.Models
{
    public class Table
    {
        public Table() { }

        public Table(string name, string schema)
        {
            Name = name;
            Schema = schema;
        }

        public string Name { get; }
        public string Schema { get; }
    }
}
