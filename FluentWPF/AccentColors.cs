﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SourceChord.FluentWPF
{
    public class AccentColors
    {
        [DllImport("uxtheme.dll", EntryPoint = "#95", CharSet = CharSet.Unicode)]
        internal static extern uint GetImmersiveColorFromColorSetEx(uint dwImmersiveColorSet, uint dwImmersiveColorType, bool bIgnoreHighContrast, uint dwHighContrastCacheMode);
        [DllImport("uxtheme.dll", EntryPoint = "#96", CharSet = CharSet.Unicode)]
        internal static extern uint GetImmersiveColorTypeFromName(string name);
        [DllImport("uxtheme.dll", EntryPoint = "#98", CharSet = CharSet.Unicode)]
        internal static extern uint GetImmersiveUserColorSetPreference(bool bForceCheckRegistry, bool bSkipCheckOnFail);

        public static Color GetColorByTypeName(string name)
        {
            var colorSet = GetImmersiveUserColorSetPreference(false, false);
            var colorType = GetImmersiveColorTypeFromName(name);
            var rawColor = GetImmersiveColorFromColorSetEx(colorSet, colorType, false, 0);

            var bytes = BitConverter.GetBytes(rawColor);
            return Color.FromArgb(bytes[3], bytes[0], bytes[1], bytes[2]);
        }

        public static Color ImmersiveSystemAccent { get; private set; }
        public static Color ImmersiveSystemAccentDark1 { get; private set; }
        public static Color ImmersiveSystemAccentDark2 { get; private set; }
        public static Color ImmersiveSystemAccentDark3 { get; private set; }
        public static Color ImmersiveSystemAccentLight1 { get; private set; }
        public static Color ImmersiveSystemAccentLight2 { get; private set; }
        public static Color ImmersiveSystemAccentLight3 { get; private set; }


        public static Brush ImmersiveSystemAccentBrush { get; private set; }
        public static Brush ImmersiveSystemAccentDark1Brush { get; private set; }
        public static Brush ImmersiveSystemAccentDark2Brush { get; private set; }
        public static Brush ImmersiveSystemAccentDark3Brush { get; private set; }
        public static Brush ImmersiveSystemAccentLight1Brush { get; private set; }
        public static Brush ImmersiveSystemAccentLight2Brush { get; private set; }
        public static Brush ImmersiveSystemAccentLight3Brush { get; private set; }

        static AccentColors()
        {
            // 各種Color定義
            ImmersiveSystemAccent = GetColorByTypeName("ImmersiveSystemAccent");
            ImmersiveSystemAccentDark1 = GetColorByTypeName("ImmersiveSystemAccentDark1");
            ImmersiveSystemAccentDark2 = GetColorByTypeName("ImmersiveSystemAccentDark2");
            ImmersiveSystemAccentDark3 = GetColorByTypeName("ImmersiveSystemAccentDark3");
            ImmersiveSystemAccentLight1 = GetColorByTypeName("ImmersiveSystemAccentLight1");
            ImmersiveSystemAccentLight2 = GetColorByTypeName("ImmersiveSystemAccentLight2");
            ImmersiveSystemAccentLight3 = GetColorByTypeName("ImmersiveSystemAccentLight3");

            // ブラシ類の定義
            ImmersiveSystemAccentBrush = CreateBrush(ImmersiveSystemAccent);
            ImmersiveSystemAccentDark1Brush = CreateBrush(ImmersiveSystemAccentDark1);
            ImmersiveSystemAccentDark2Brush = CreateBrush(ImmersiveSystemAccentDark2);
            ImmersiveSystemAccentDark3Brush = CreateBrush(ImmersiveSystemAccentDark3);
            ImmersiveSystemAccentLight1Brush = CreateBrush(ImmersiveSystemAccentLight1);
            ImmersiveSystemAccentLight2Brush = CreateBrush(ImmersiveSystemAccentLight2);
            ImmersiveSystemAccentLight3Brush = CreateBrush(ImmersiveSystemAccentLight3);
        }

        internal static Brush CreateBrush(Color color)
        {
            var brush = new SolidColorBrush(color);
            brush.Freeze();
            return brush;
        }
    }
}
