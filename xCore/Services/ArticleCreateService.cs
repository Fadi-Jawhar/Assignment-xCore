using xCore.Models.Entities;
using xCore.Repositories;

namespace xCore.Services
{
    public class ArticleCreateService
    {
        private readonly ArticleRepository _articleRepository;

        public ArticleCreateService(ArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public void CreateArticle(ArticleEntity article)
        {
            ArticleEntity newArticle = article;

            _articleRepository.CreateArticleAsync(newArticle);
        }
    }
}