﻿using System.Collections.Generic;
using Kanvas;
using Komponent.IO.Attributes;
using Kontract.Kanvas;
using Kontract.Models.Image;

namespace plugin_nintendo.Images
{
    class _3dstHeader
    {
        [FixedLength(7)]
        public string magic;
        public int zero1;
        public int zero2;
        public byte zero3;
        public short width;
        public short height;
        public short format;
    }

    class _3dstSupport
    {
        private static readonly IDictionary<int, IColorEncoding> Formats = new Dictionary<int, IColorEncoding>
        {
            // [0] = ImageFormats.Rgba8888(),
            // [1] = ImageFormats.Rgb888(),
            // [2] = ImageFormats.Rgba5551(),
            // [3] = ImageFormats.Rgb565(),
            // [4] = ImageFormats.Rgba4444(),
            // [5] = ImageFormats.La88(),
            // [6] = ImageFormats.Rg88(),
            // [7] = ImageFormats.L8(),
            // [8] = ImageFormats.A8(),
            // [9] = ImageFormats.La44(),
            // [10] = ImageFormats.L4(),
            // [11] = ImageFormats.A4(),
            // [12] = ImageFormats.Etc1(true),
            // [13] = ImageFormats.Etc1A4(true),
            [0] = ImageFormats.Rgba8888(),
            [4] = ImageFormats.Etc1(true),
        };

        public static EncodingDefinition GetEncodingDefinition()
        {
            var definition = new EncodingDefinition();
            definition.AddColorEncodings(Formats);

            return definition;
        }
    }
}
