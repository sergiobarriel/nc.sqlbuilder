using NC.SqlBuilder.Abstractions.Operations;

namespace NC.SqlBuilder.Models.Operations
{
    public class BetweenOperation : IBetweenOperation
    {
        public BetweenOperation() { }
        public BetweenOperation(string field, Operator @operator, string left, string right)
        {
            Field = field;
            Operator = @operator;
            Left = left;
            Right = right;
        }

        public string Field { get; set; }
        public Operator Operator { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
    }
}