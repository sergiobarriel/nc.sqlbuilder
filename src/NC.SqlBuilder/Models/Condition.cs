using NC.SqlBuilder.Abstractions.Operations;

namespace NC.SqlBuilder.Models
{
    public class Condition : IOperation
    {
        public Condition () { }

        public Condition(string field, Operator @operator, string value)
        {
            Field = field;
            Operator = @operator;
            Value = value;
        }
        public Condition(string field, Operator @operator, string left, string right)
        {
            Field = field;
            Operator = @operator;
            Right = right;
            Left = left;
        }
        public Condition(string field, Operator @operator, double latitude, double longitude, int radio)
        {
            Field = field;
            Operator = @operator;
            Latitude = latitude;
            Longitude = longitude;
            Radio = radio;
        }

        public Condition(ICoordinatesOperation @operation)
        {
            Field = operation.Field;
            Operator = operation.Operator;
            Latitude = operation.Latitude;
            Longitude = operation.Longitude;
            Radio = operation.Radio;
        }
        public Condition(IBetweenOperation @operation)
        {
            Field = operation.Field;
            Operator = operation.Operator;
            Right = operation.Right;
            Left = operation.Left;
        }
        public Condition(ISimpleOperation @operation)
        {
            Field = operation.Field;
            Operator = operation.Operator;
            Value = operation.Value;
        }

        public string Field { get; set; }
        public Operator Operator { get; set; }

        public string Value { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Radio { get; set; }

        public string Left { get; set; }
        public string Right { get; set; }
    }

}
