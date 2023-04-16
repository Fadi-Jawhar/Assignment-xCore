using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using xCore.Interfaces;

namespace xCore.Models.Entities
{
    public class ArticleEntity : ArticleDate, IArticle
    {        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public ContentTypeEntity Category { get; set; }

    }
}
