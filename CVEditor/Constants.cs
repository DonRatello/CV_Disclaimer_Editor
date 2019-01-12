using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVEditor
{
    public abstract class Constants
    {
        public const string AppName = "CV Disclaimer Editor";

        public abstract class RegistryKeys
        {
            public const string FontName = "FontName";
            public const string FontSize = "FontSize";
            public const string Disclaimer = "Disclaimer";
            public const string PosX = "PosX";
            public const string PosY = "PosY";
            public const string LineHeight = "LineHeight";
        }
    }
}
