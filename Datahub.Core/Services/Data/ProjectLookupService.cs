using Datahub.Core.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datahub.Core.Services.Data
{
    public class ProjectLookupService
    {
        private readonly IDbContextFactory<DatahubProjectDBContext> dbContextFactory;

        public ProjectLookupService(IDbContextFactory<DatahubProjectDBContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
    }
}
