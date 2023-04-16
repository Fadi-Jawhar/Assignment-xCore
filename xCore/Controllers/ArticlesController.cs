using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xCore.Contexts;
using xCore.Models;
using xCore.Models.Entities;
using xCore.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace xCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleCreateService _articlesCreateService;
        private readonly ArticleService _articleService;
        private readonly DataContext _dataContext;

        public ArticlesController(ArticleCreateService articleCreateService, ArticleService articleService, DataContext dataContext)
        {
            _articlesCreateService = articleCreateService;
            _articleService = articleService;
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article model)
        {
            if (ModelState.IsValid)
            {
                var articleEntity = new ArticleEntity
                {
                    Title = model.Title,
                    Description = model.Description,
                    Author = model.Author,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Category = model.Category
                };
                _articlesCreateService.CreateArticle(articleEntity);
                return new OkResult();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var articleEntity = await _dataContext.Articles.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);


            if (articleEntity != null)
            {
                var articles = new ArticleEntity
                {
                    Id = articleEntity.Id,
                    Title = articleEntity.Title,
                    Description = articleEntity.Description,
                    Author = articleEntity.Author,
                    CreatedAt = articleEntity.CreatedAt,
                    UpdatedAt = articleEntity.UpdatedAt,
                    Category = articleEntity.Category
                };
                return new OkObjectResult(articles);
            }
            return new NotFoundResult();
        }

        [HttpPut]
        public IActionResult Update(int id, Article model)
        {
            if (ModelState.IsValid)
            {
                var articleEntity = new ArticleEntity
                {
                    Id = id,
                    Title = model.Title,
                    Description = model.Description,
                    Author = model.Author,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Category = model.Category
                };
                _articleService.UpdateArticleAsync(articleEntity);
                return new OkResult();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var articleEntity = await _dataContext.Articles.FindAsync(id);
            if (articleEntity == null)
            {
                return NotFound();
            }

            _dataContext.Articles.Remove(articleEntity);
            await _dataContext.SaveChangesAsync();

            return NoContent();


        }
    }
}
