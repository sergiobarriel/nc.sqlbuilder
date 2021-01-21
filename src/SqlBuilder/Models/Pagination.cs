namespace SqlBuilder.Models
{
    public class Pagination
    {
        public Pagination() { }

        public Pagination(int first, int size)
        {
            First = first;
            Size = size;
        }

        public int First { get; set; }
        public int Size { get; set; }
    }
}
