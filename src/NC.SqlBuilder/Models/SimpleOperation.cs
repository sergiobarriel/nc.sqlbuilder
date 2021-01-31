using NC.SqlBuilder.Abstractions.Operations;

namespace NC.SqlBuilder.Models
{
    public class SimpleOperation : ISimpleOperation
    {
        public SimpleOperation() { }
        public SimpleOperation(string field, Operator @operator, string value)
        {
            Field = field;
            Operator = @operator;
            Value = value;
        }

        public string Field { get; set; }
        public Operator Operator { get; set; }
        public string Value { get; set; }
    }
}