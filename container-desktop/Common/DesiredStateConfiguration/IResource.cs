﻿using System;
using System.Collections.Generic;

namespace ContainerDesktop.Common.DesiredStateConfiguration
{
    public interface IResource
    {
        string Id { get; }
        string Type => GetTypeName(GetType());
        List<string> DependsOn { get; }
        bool Enabled { get; }
        bool NeedsElevation { get; }
        bool Test(ConfigurationContext context);
        void Set(ConfigurationContext context);

        static string GetTypeName(Type type) => type.Name.EndsWith("Resource") ? type.Name[0..^8] : type.Name;
    }
}
