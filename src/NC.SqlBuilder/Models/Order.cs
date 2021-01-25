using System;

namespace NC.SqlBuilder.Models
{
    public class Order
    {
        public Order() { }
        public Order(string field, Direction direction)
        {
            if(string.IsNullOrEmpty(field)) throw new Exception("'Field' shouldn't be empty.");

            Field = field;
            Direction = direction;
        }

        public string Field { get; }
        public Direction Direction { get; }
    }
}
