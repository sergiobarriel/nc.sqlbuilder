using System;

namespace NC.SqlBuilder.Models
{
    public class Pagination
    {
        public Pagination() { }

        public Pagination(int first, int size)
        {
            if (first < 0) throw new Exception("'First' should be greater than zero.");
            if (size < 1) throw new Exception("'Size' should be greater than zero.");

            First = first;
            Size = size;
        }

        public int First { get; }
        public int Size { get; }
    }
}
