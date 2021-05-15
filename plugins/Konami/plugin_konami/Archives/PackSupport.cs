﻿using System;
using System.Collections.Generic;
using System.Text;
using Komponent.IO.Attributes;
#pragma warning disable 649

namespace plugin_konami.Archives
{
    class PackHeader
    {
        [FixedLength(4)]
        public string magic;
        public short unk1;
        public short fileCount;
        public int stringOffsetsOffset;
        public int stringOffset;
        public int decompressedDataEnd;     // All decompressed files are written first, then all compressed files;
                                            // This offset points to where the decompressed file blob end
        public int decompSize;
        public int compSize;
        public int zero1;
    }

    class PackEntry
    {
        [FixedLength(4)]
        public string magic;
        public int zero1;
        public int decompSize;
        public int decompOffset;
        public int unk1;
        public int flags;
        public int compSize;
        public int compOffset;

        public bool IsCompressed => (flags & 0x1) == 1;
    }
}
