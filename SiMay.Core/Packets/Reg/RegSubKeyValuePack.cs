﻿using SiMay.ReflectCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiMay.Core.Packets.Reg
{
    public class RegSubKeyValuePack : EntitySerializerBase
    {
        public string[] SubKeyNames { get; set; }
        public RegValueItem[] Values { get; set; }
    }
}
