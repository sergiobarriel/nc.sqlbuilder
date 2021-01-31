namespace NC.SqlBuilder.Abstractions.Operations
{
    public interface ICoordinatesOperation : IOperation
    {
        double Latitude { get; set; }
        double Longitude { get; set; } 
        int Radio { get; set; }
    }
}