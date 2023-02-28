// Copyright (c) 2023 Weloveloli. All rights reserved.
// See License in the project root for license information.

namespace AVOne.Plugin.Template
{
    using AVOne.Common.Plugins;
    using AVOne.Configuration;
    using AVOne.IO;
    using AVOne.Plugins.NM3U8DL.Configuration;

    public class Plugin : BasePlugin<NM3U8DLConfiguration>
    {
        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer) : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public override string Description => "NM3U8DL Plugin for AVOne";

        public override Guid Id => Guid.Parse("F3A4B8C9-7D6E-4E15-A1B2-0C3D4E5F6A7B");

        public override string Name => "NM3U8DL";

        public static Plugin Instance { get; private set; }
    }
}
