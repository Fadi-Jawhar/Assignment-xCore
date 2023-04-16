using xCore.Contexts;

namespace xCore.Repositories
{
    
    public class ContentTypeRepository
    {
        private readonly DataContext _dataContext;

        public ContentTypeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
