﻿using SiMay.ReflectCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiMay.Core.Packets.SysManager
{
    public class CreateProcessAsUserPack : EntitySerializerBase
    {
        public int SessionId { get; set; }
    }

    public class SessionsPack : EntitySerializerBase
    {
        public SessionItem[] Sessions { get; set; }
    }

    public class SessionItem : EntitySerializerBase
    {
        public string UserName { get; set; }
        public int SessionId { get; set; }
        public int SessionState { get; set; }
        public string WindowStationName { get; set; }
        public bool HasUserProcess { get; set; }
    }
}
