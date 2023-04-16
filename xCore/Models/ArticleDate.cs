namespace xCore.Models
{
    public class ArticleDate
    {
        public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
