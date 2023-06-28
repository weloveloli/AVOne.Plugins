// Copyright (c) 2023 Weloveloli. All rights reserved.
// See License in the project root for license information.

namespace AVOne.Plugins.Extractors.Extractors
{
    using System;
    using System.Collections.Generic;
    using AVOne.Plugins.Extractors.Base;
    using AVOne.Plugins.Extractors.Embeded;
    using Microsoft.Extensions.Logging;

    public class TKtubeExtractor : BaseEmbedHttpExtractor
    {
        public TKtubeExtractor(IHttpHelper httphelper, ILoggerFactory loggerFactory)
            : base(httphelper, loggerFactory, "https://tktube.com", new TKtubeEmbededExtractor(httphelper, loggerFactory))
        {
        }

        public override string Name => "TKtube";

        public override IEnumerable<string> GetEmbedPages(string url, string html)
        {
            var videoId = GetVideoId(url);
            var embedUrl = $"https://tktube.com/embed/{videoId}";
            return new List<string> { embedUrl };
        }

        // add a function that extract 121939 from https://tktube.com/videos/121939/1854/
        public string GetVideoId(string url)
        {
            var uri = new Uri(url);
            var path = uri.AbsolutePath;
            var parts = path.Split('/');
            return parts[2];
        }
    }
}
