using NC.SqlBuilder.Abstractions.Operations;

namespace NC.SqlBuilder.Models.Operations
{
    public class BetweenOperation : IBetweenOperation
    {
        public BetweenOperation() { }
        public BetweenOperation(string field, Operator @operator, string up, string down)
        {
            Field = field;
            Operator = @operator;
            Up = up;
            Down = down;
        }

        public string Field { get; set; }
        public Operator Operator { get; set; }
        public string Up { get; set; }
        public string Down { get; set; }
    }
}