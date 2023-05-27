// Copyright (c) 2023 Weloveloli. All rights reserved.
// See License in the project root for license information.

namespace AVOne.Plugin.Extractors.Extractors
{
    using System.Collections.Generic;
    using System.Net.Http;
    using AVOne.Configuration;
    using AVOne.Enum;
    using AVOne.Extensions;
    using AVOne.Models.Download;
    using AVOne.Plugin.Extractors.Base;
    using Fizzler.Systems.HtmlAgilityPack;
    using HtmlAgilityPack;
    using Microsoft.Extensions.Logging;

    public class BaiHuSeExtractor : BaseHttpExtractor, IDOMExtractor
    {
        private const string BaiHuSe = "https://paipancon.com/";
        public BaiHuSeExtractor(IConfigurationManager manager, ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory)
            : base(manager, loggerFactory, httpClientFactory, BaiHuSe)
        {
        }

        public static Dictionary<string, string> HeaderForBaiHuSe = new()
        {
            { "referer", BaiHuSe },
            { "origin", BaiHuSe }
        };

        public override string Name => "BaiHuSe";

        public IEnumerable<BaseDownloadableItem> GetItems(string title, HtmlNode node, string url)
        {
            var sources = node.QuerySelectorAll("source.video-source");
            if (sources.IsEmpty())
            {
                yield break;
            }
            foreach (var source in sources)
            {
                var mediaType = source.Attributes["type"]?.Value;

                var mp4Url = source.Attributes["src"]?.Value;
                if (mediaType != "video/mp4" || string.IsNullOrEmpty(mp4Url))
                {
                    continue;
                }
                var sourceTitle = source.Attributes["title"]?.Value;
                var quality = MediaQuality.None;
                if (sourceTitle == "HQ")
                {
                    quality = MediaQuality.High;
                }

                yield return new HttpItem(
                    title.EscapeFileName(),
                    mp4Url,
                    HeaderForBaiHuSe,
                    quality,
                    title);

            }
        }

        public string GetTitle(HtmlNode dom)
        {
            return dom.QuerySelector("h2.video-title")?.InnerText ?? string.Empty;
        }
    }
}
