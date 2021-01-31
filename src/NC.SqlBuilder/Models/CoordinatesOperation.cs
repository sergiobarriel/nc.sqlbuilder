using NC.SqlBuilder.Abstractions.Operations;

namespace NC.SqlBuilder.Models
{
    public class CoordinatesOperation : ICoordinatesOperation
    {
        public CoordinatesOperation() { }
        public CoordinatesOperation(string field, Operator @operator, double latitude, double longitude, int radio)
        {
            Field = field;
            Operator = @operator;
            Latitude = latitude;
            Longitude = longitude;
            Radio = radio;
        }

        public string Field { get; set; }
        public Operator Operator { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Radio { get; set; }
    }
}