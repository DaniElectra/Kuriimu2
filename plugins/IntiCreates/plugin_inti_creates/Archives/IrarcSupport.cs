﻿using System.Buffers.Binary;
using System.IO;
using Kontract.Interfaces.Progress;
using Kontract.Kompression.Configuration;
using Kontract.Models.Archive;
#pragma warning disable 649

namespace plugin_inti_creates.Archives
{
    class IrarcFileEntry
    {
        public int id;
        public int offset;
        public int size;

        // Bit 4 => IsCompressed
        public int flags;

        public bool IsCompressed => (flags & 0x10) > 0;
    }

    class IrarcArchiveFileInfo : ArchiveFileInfo
    {
        public IrarcFileEntry Entry { get; }

        public IrarcArchiveFileInfo(Stream fileData, string filePath, IrarcFileEntry entry) : base(fileData, filePath)
        {
            Entry = entry;
        }

        public IrarcArchiveFileInfo(Stream fileData, string filePath, IrarcFileEntry entry, IKompressionConfiguration configuration, long decompressedSize) :
            base(fileData, filePath, configuration, decompressedSize)
        {
            Entry = entry;
        }

        public override long SaveFileData(Stream output, bool compress, IProgressContext progress = null)
        {
            var bkPos = output.Position;

            if (UsesCompression)
                output.Position += 0x18;

            var writtenSize = base.SaveFileData(output, compress, progress);

            if (!UsesCompression) 
                return writtenSize;

            writtenSize += 0x18;

            (bkPos, output.Position) = (output.Position, bkPos);

            WriteInt32(output, 0x18);
            WriteInt32(output, 0);
            WriteInt32(output, (int)writtenSize);
            WriteInt32(output, (int)FileSize);
            WriteInt32(output, 0);
            WriteInt32(output, 0);

            output.Position = bkPos;

            return writtenSize;
        }

        private void WriteInt32(Stream input, int value)
        {
            var buffer = new byte[4];
            BinaryPrimitives.WriteInt32LittleEndian(buffer, value);

            input.Write(buffer);
        }
    }
}
