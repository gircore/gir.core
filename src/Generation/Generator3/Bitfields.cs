﻿using System.Collections.Generic;
using Generator3.Generation.Bitfield;
using Generator3.Publication;

namespace Generator3
{
    public static class Bitfields
    {
        public static void Generate(this IEnumerable<GirModel.Bitfield> bitfields)
        {
            var generator = new Generator(
                template: new Template(),
                publisher: new PublicEnumFilePublisher()
            );

            foreach (var bitfield in bitfields)
                generator.Generate(bitfield);
        }
    }
}
