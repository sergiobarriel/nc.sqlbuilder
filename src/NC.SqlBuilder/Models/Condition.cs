namespace NC.SqlBuilder.Models
{
    public class Condition
    {
        public Condition() { }
        public Condition(string field, Operator @operator, string value)
        {
            Field = field;
            Operator = @operator;
            Value = value;
        }

        public string Field { get; set; }
        public string Value { get; set; }
        public Operator Operator { get; set; }
    }
}
