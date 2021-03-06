﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tranquility_Login
{
    public static class Constants
    {
        /// <summary>
        /// 程序基本信息
        /// </summary>
        public static String version = $"1.0.0.0";

        public static String self_name = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        public static String self_path = AppDomain.CurrentDomain.BaseDirectory;
        public static String self = self_path + "\\" + self_name + ".exe";

        public static String minecraft_path = self_path + "\\" + ".minecraft\\";
    }
}
