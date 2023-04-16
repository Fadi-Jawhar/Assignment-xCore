using xCore.Interfaces;
using xCore.Models.Entities;

namespace xCore.Models
{
    public class Article : ArticleDate, IArticle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public override DateTime CreatedAt { get; set; }
        public ContentTypeEntity Category { get; set; }
    }
}
