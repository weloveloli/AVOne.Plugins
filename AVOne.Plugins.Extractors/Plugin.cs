// Copyright (c) 2023 Weloveloli. All rights reserved.
// See License in the project root for license information.

namespace AVOne.Plugin.Template
{
    using System;
    using AVOne.Common.Plugins;
    using AVOne.Configuration;
    using AVOne.IO;

    public class Plugin : BasePlugin<PluginConfiguration>
    {
        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer) : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public override string Name => "ExtraExtractors";

        public override string Description => "Extractors Collection";

        public override Guid Id => Guid.Parse("1C244026-DB79-9B2F-BCD2-ED635EDD2022");

        public static Plugin Instance { get; private set; }
    }
}
