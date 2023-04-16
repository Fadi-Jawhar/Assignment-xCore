using xCore.Contexts;
using xCore.Models.Entities;

namespace xCore.Services
{
    public class ContentTypeService
    {
        private readonly DataContext _dataContext;

        public ContentTypeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<bool> CreateContentTypeAsync(ContentTypeEntity type)
        {
            try
            {
                _dataContext.Add(type);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
    }
}