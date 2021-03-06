using Datahub.Metadata.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Datahub.Metadata.Utils
{
    public static class CatalogObjectExtensions
    {
        public static async Task<List<CatalogObject>> QueryCatalog(this MetadataDbContext ctx, string query)
        {
            var results = await ctx.CatalogObjects
                .FromSqlRaw(query)
                .Include(e => e.ObjectMetadata)
                .ThenInclude(s => s.FieldValues)
                .ToListAsync();

            return results;
        }
    }
}
