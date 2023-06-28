// Copyright (c) 2023 Weloveloli. All rights reserved.
// See License in the project root for license information.

namespace AVOne.Plugins.Extractors.Base
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AVOne.Enum;
    using AVOne.Extensions;
    using AVOne.Models.Download;
    using AVOne.Providers.Extractor;
    using Microsoft.Extensions.Logging;

    public abstract class BaseEmbedHttpExtractor : IMediaExtractorProvider
    {
        private readonly string[] _webPagePrefixArray;
        private readonly List<IMediaExtractorProvider> embedExtractor;
        protected readonly ILogger logger;
        private readonly IHttpHelper _httpHelper;

        protected BaseEmbedHttpExtractor(IHttpHelper httpHelper, ILoggerFactory loggerFactory, string webPagePrefix, params IMediaExtractorProvider[] embededExtractorProviders)
        {
            _webPagePrefixArray = webPagePrefix.Split(';').Where(e => !string.IsNullOrEmpty(e)).ToArray();
            this.embedExtractor = embededExtractorProviders.ToList();
            this.logger = loggerFactory.CreateLogger(this.GetType());
            _httpHelper = httpHelper;
        }

        public abstract string Name { get; }

        public int Order => (int)ProviderOrder.Default;

        public async Task<IEnumerable<BaseDownloadableItem>> ExtractAsync(string webPageUrl, CancellationToken token = default)
        {
            var links = Enumerable.Empty<string>();
            var result = new List<BaseDownloadableItem>();
            try
            {
                var html = await this._httpHelper.GetHtmlAsync(webPageUrl, token);
                links = this.GetEmbedPages(webPageUrl, html);
                foreach (var link in links)
                {
                    var extractor = GetEmbededExtractor(link);
                    if (extractor != null)
                    {
                        var items = await extractor.ExtractAsync(link, token);
                        result.AddRange(items);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, message: "failed to fetach downloadable in webpage {webPageUrl}", webPageUrl);
            }
            return result;
        }

        public abstract IEnumerable<string> GetEmbedPages(string url, string html);
        public virtual IMediaExtractorProvider? GetEmbededExtractor(string url)
        {
            return embedExtractor.FirstOrDefault(p => p.Support(url));
        }

        public virtual bool Support(string webPage)
        {
            return !string.IsNullOrEmpty(webPage) && _webPagePrefixArray.Any(webPage.StartsWith);
        }
    }
}
