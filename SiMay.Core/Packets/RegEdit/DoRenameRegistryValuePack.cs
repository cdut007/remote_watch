﻿using SiMay.ReflectCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiMay.Core.Packets.RegEdit
{
    public class DoRenameRegistryValuePack : EntitySerializerBase
    {
        public string KeyPath { get; set; }

        public string OldValueName { get; set; }

        public string NewValueName { get; set; }
    }
}
