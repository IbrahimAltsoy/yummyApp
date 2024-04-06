using yummyApp.Application.Abstract.DbContext;

namespace yummyApp.Infrastructure.Common
{
    public interface ISeeder
    {
        Task Seed(IYummyAppDbContext context);
    }
}
