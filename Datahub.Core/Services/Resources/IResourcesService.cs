﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datahub.Core.Services.Resources
{
    public interface IResourcesService
    {
        Task<string> LoadPage(string name, List<(string, string)> substitutions = null);
        Task<ResourceLanguageRoot> LoadLanguageRoot(bool isFrench);
        Task<string> LoadResourcePage(ResourceCard card);

        Task Test();
    }
}