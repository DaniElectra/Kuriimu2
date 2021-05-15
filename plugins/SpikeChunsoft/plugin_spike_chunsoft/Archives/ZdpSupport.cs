﻿using Komponent.IO.Attributes;
#pragma warning disable 649

namespace plugin_spike_chunsoft.Archives
{
    class ZdpPartitionHeader
    {
        [FixedLength(8)] 
        public string magic = "datapack";

        public int zero0;
        public int unk1;
    }

    class ZdpHeader
    {
        public int headerSize = 0x18;
        public int fileCount;
        public short entryCount;
        public short nameOffsetCount;
        public int stringCount;
        public int entryOffset;
        public int nameOffsetsOffset;
        public int zero0;
    }

    class ZdpFileEntry
    {
        public int offset;
        public int size;
    }
}
