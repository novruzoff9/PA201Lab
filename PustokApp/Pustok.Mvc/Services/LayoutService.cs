using Pustok.Mvc.Data;
using Pustok.Mvc.Models;

namespace Pustok.Mvc.Services;

public class LayoutService(AppDbContext db)
{
    public Dictionary<string,string> GetSettings()
    {
        return db.Settings.ToDictionary(s => s.Key, s => s.Value);
    }
}
