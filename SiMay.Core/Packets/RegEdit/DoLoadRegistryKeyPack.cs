﻿using SiMay.ReflectCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiMay.Core.Packets.RegEdit
{
    public class DoLoadRegistryKeyPack : EntitySerializerBase
    {
        public string RootKeyName { get; set; }
    }
}
