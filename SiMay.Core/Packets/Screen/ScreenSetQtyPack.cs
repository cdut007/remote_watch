﻿using SiMay.ReflectCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiMay.Core.Packets.Screen
{
    public class ScreenSetQtyPack : EntitySerializerBase
    {
        public long Quality { get; set; }
    }
}
