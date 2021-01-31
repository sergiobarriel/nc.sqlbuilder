namespace NC.SqlBuilder.Abstractions.Operations
{
    public interface IBetweenOperation : IOperation
    {
        string Down { get; set; }
        string Up { get; set; }
    }
}