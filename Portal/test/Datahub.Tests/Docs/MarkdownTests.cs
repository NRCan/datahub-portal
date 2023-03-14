﻿using Datahub.Core.Services.Docs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace Datahub.Tests.Docs
{
    public class MarkdownTests
    {
        [Fact]
        public void GivenMarkdown_RemoveFrontmatter()
        {
            var md = @"---
remarks: Automatically translated with DeepL
source: /Banners/Landing.md
---

_(draft documentation, please review)_

## Qu'est-ce que le DataHub ?

Le DataHub est une plateforme d'entreprise permettant de stocker, de travailler et de collaborer sur des initiatives de données dans tous les secteurs. Il s'agit d'un emplacement central permettant aux utilisateurs de stocker des données, d'effectuer des analyses collaboratives, de manipuler des données à l'aide d'outils d'analyse avancés et de mener des expériences de science des données.

[En savoir plus]()
";
            var expected = @"
_(draft documentation, please review)_

## Qu'est-ce que le DataHub ?

Le DataHub est une plateforme d'entreprise permettant de stocker, de travailler et de collaborer sur des initiatives de données dans tous les secteurs. Il s'agit d'un emplacement central permettant aux utilisateurs de stocker des données, d'effectuer des analyses collaboratives, de manipuler des données à l'aide d'outils d'analyse avancés et de mener des expériences de science des données.

[En savoir plus]()
";
            var cleaned = MarkdownHelper.RemoveFrontMatter(md);
            Assert.DoesNotContain("---", cleaned);
            Assert.Equal(expected, cleaned);

        }

       }
}