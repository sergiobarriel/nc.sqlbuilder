namespace NC.SqlBuilder.Models
{
    public class Order
    {
        public Order() { }
        public Order(string field, Direction direction)
        {
            Field = field;
            Direction = direction;
        }

        public string Field { get; }
        public Direction Direction { get; }
    }
}
