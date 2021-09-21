﻿using ContainerDesktop.Common.Services;
using System;

namespace ContainerDesktop.Common.DesiredStateConfiguration
{
    public class WslDistro : ResourceBase
    {
        private readonly IWslService _wslService;

        public string Name { get; set; }
        public string Path { get; set; }
        public string RootfsFileName { get; set; }

        public WslDistro(IWslService wslService)
        {
            _wslService = wslService ?? throw new ArgumentNullException(nameof(wslService));
        }

        public override void Set(ConfigurationContext context)
        {
            if (!Test(context))
            {
                var path = Environment.ExpandEnvironmentVariables(Path);
                if(!context.FileSystem.Directory.Exists(path))
                {
                    context.FileSystem.Directory.CreateDirectory(path);
                }
                var rootfsFileName = Environment.ExpandEnvironmentVariables(RootfsFileName);
                if (!_wslService.Import(Name, path, rootfsFileName))
                {
                    throw new ResourceException($"Could not import distribution '{Name}' from '{rootfsFileName}' to '{path}'.");
                }
            }
        }

        public override bool Test(ConfigurationContext context)
        {
            return _wslService.IsInstalled(Name);
        }
    }
}
