using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Jobs;

public class ProductStatusJob(AppDbContext dbContext)
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string CronExpression => "*/1 * * * *";

    public Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var oldProducts = dbContext.Products
        .Where(p => p.IsNew && EF.Functions.DateDiffMinute(p.CreatedAt, DateTime.UtcNow) > 5)
        .ToList();


        oldProducts.ForEach(p => p.IsNew = false);
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
