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
    public class ContentTypesController : ControllerBase
    {
        private readonly ContentTypeService _contentTypeService;
        private readonly DataContext _dataContext;

        public ContentTypesController(ContentTypeService contentTypeService, DataContext dataContext)
        {
            _contentTypeService = contentTypeService;
            _dataContext = dataContext;
        }


        [HttpPost]
        public async Task<IActionResult> Create(ContentType model)
        {
            if (ModelState.IsValid)
            {
                var contentTypeEntity = new ContentTypeEntity
                {
                    Category = model.Category
                };
                _contentTypeService.CreateContentTypeAsync(contentTypeEntity);
                return new OkResult();
            }
            return BadRequest();
        }

        [HttpGet("{Category}")]
        public async Task<IActionResult> GetAllByType(int Category)
        {

            var articles = new List<ArticleEntity>();

            foreach (var article in await _dataContext.Articles.Include(x => x.Category).ToListAsync())
            {
                if (article.Category.Id == Category)
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
                   
            }

            return new OkObjectResult(articles);
        }
    }
}
