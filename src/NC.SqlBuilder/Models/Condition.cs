using System;

namespace NC.SqlBuilder.Models
{
    public class Condition
    {
        public Condition() { }
        public Condition(string field, Operator @operator, string value)
        {
            if (@operator == Operator.Between)
                throw new Exception($"'Between' operator require two values.");

            Field = field;
            Operator = @operator;
            Value = value;
        }

        public Condition(string field, Operator @operator, string a, string b)
        {
            if(@operator == Operator.Between && (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))) 
                throw new Exception($"'Between' operator require two values.");
            
            Field = field;
            Operator = @operator;
            Value = $"{a}.{b}";
        }

        public string Field { get; }
        public string Value { get; }
        public Operator Operator { get; }
    }

}
