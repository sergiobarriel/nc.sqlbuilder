namespace NC.SqlBuilder.Models
{
    public class Pagination
    {
        public Pagination() { }

        public Pagination(int first, int size)
        {
            First = first;
            Size = size;
        }

        public int First { get;}
        public int Size { get; }
    }
}
