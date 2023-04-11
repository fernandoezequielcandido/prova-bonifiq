namespace ProvaPub.Models
{
    public abstract class BaseList
    {
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
    }
}
