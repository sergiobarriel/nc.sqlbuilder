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

        public string Field { get; }
        public string Value { get; }
        public Operator Operator { get; }
    }
}
