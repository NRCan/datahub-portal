﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Datahub.Core.Services.ExcelParser;

public class ExcelHtmlContentParser : IExcelContentParser
{
    public String ValidMimeType { get; } = "text/html";

    public async Task<IList<String[]>> GetRows(String input)
    {
        var context = BrowsingContext.New(AngleSharp.Configuration.Default);
        var document = await context.OpenAsync(reg => reg.Content(input));

        var element = document.QuerySelector<IHtmlTableElement>("table");
        var result = element.Rows.Select(x => x.Cells.Select(y => y.TextContent).ToArray()).ToList();
        return result;
    }
}