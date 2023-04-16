using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xCore.Contexts;
using xCore.Models.Entities;

namespace xCore.Services
{
    public class ArticleService
    {
        private readonly DataContext _dataContext;

        public ArticleService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<bool> UpdateArticleAsync(ArticleEntity article)
        {
            try
            {
                _dataContext.Update(article);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }


        public async Task<bool> DeleteArticleAsync(int id)
        {
            try
            {

                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }



        public async Task<ArticleEntity> GetArticleAsync(int id)
        {
            try
            {
                var articleEntity = await _dataContext.Articles.FirstOrDefaultAsync(x => x.Id == id);


                if (articleEntity != null)
                {
                    return new ArticleEntity
                    {
                        Id = articleEntity.Id,
                        Title = articleEntity.Title,
                        Description = articleEntity.Description,
                        Author = articleEntity.Author,
                        Category = articleEntity.Category,
                        CreatedAt = articleEntity.CreatedAt,
                        UpdatedAt = articleEntity.UpdatedAt
                    };
                }
                return null!;
            }
            catch { return null!; }
        }



        public async Task<IActionResult> GetAllArticleAsync()
        {

            var articles = new List<ArticleEntity>();

            foreach (var article in await _dataContext.Articles.Include(x => x.Category).ToListAsync())
            {
                articles.Add(new ArticleEntity
                {
                    Id = article.Id,
                    Title = article.Title,
                    Description = article.Description,
                    Author = article.Author,
                    Category = article.Category,
                    CreatedAt = article.CreatedAt,
                    UpdatedAt = article.UpdatedAt
                });
            }

            return new OkObjectResult(articles);

        }

    }
}