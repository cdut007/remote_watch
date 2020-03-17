﻿using SiMay.Core.Enums;
using SiMay.ReflectCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiMay.Core.Packets
{
    public class ScreenKeyPack : EntitySerializerBase
    {
        public MOUSEKEY_ENUM Key { get; set; }
        public int Point1 { get; set; }
        public int Point2 { get; set; }
    }
}
