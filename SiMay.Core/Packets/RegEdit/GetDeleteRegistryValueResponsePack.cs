﻿using SiMay.ReflectCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiMay.Core.Packets.RegEdit
{
    public class GetDeleteRegistryValueResponsePack : EntitySerializerBase
    {
        public string KeyPath { get; set; }

        public string ValueName { get; set; }

        public bool IsError { get; set; }

        public string ErrorMsg { get; set; }
    }
}
