// Copyright (c) 2023 Weloveloli. All rights reserved.
// See License in the project root for license information.

namespace AVOne.Plugins.Extractors
{
    using AVOne.Common.Plugins;
    using AVOne.Plugins.Extractors.Base;
    using Microsoft.Extensions.DependencyInjection;

    public class PluginServiceRegistrator : IPluginServiceRegistrator
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHttpHelper, DefaultHttpHelper>();
        }
    }
}
