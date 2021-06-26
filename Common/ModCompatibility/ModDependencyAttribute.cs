﻿using System;

namespace CataclysmMod.Common.ModCompatibility
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ModDependencyAttribute : Attribute
    {
        public string Mod { get; }

        public ModDependencyAttribute(string mod)
        {
            Mod = mod;
        }
    }
}