namespace NC.SqlBuilder.Abstractions.Operations
{
    public interface IBetweenOperation : IOperation
    {
        string Left { get; set; }
        string Right { get; set; }
    }
}