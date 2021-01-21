namespace SqlBuilder.Models
{
    public class Table
    {
        public Table() { }

        public Table(string name, string schema)
        {
            Name = name;
            Schema = schema;
        }

        public string Name { get; set; }
        public string Schema { get; set; }
    }
}
