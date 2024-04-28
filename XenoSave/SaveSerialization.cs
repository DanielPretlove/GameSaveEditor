/*
 * XenoSave
 * Copyright (C) 2020-2023  damysteryman
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.IO.Compression;
using dmm.XenoSave.XCDE;
using dmm.XenoSave.XC2;
using dmm.XenoSave.XC3;

namespace dmm.XenoSave
{
    public static class SaveSerialization
    {
        private static Byte[] CMP_HEADER = new Byte[] { 0x7E, 0xC0, 0xE2, 0xC9, 0x45, 0x98, 0xCE, 0x03, 0xA7, 0x98, 0xF1, 0x33, 0x6A, 0xE7, 0x25, 0x80 };
        public static ISaveFile Deserialize(Byte[] data)
        {
            if (data.GetByteSubArray(0, CMP_HEADER.Length).SequenceEqual(CMP_HEADER))
                data = Decompress(data);

            switch (data.Length)
            {

                case XCDESave.SIZE:
                    Console.WriteLine("XCDESave detected.");
                    return new XCDESave(data);

                case XCDESettings.SIZE:
                    Console.WriteLine("XCDESettings detected.");
                    return new XCDESettings(data);

                case XC2Save.SIZE:
                    Console.WriteLine("XC2Save (old) detected.");
                    return new XC2Save(data);

                case XC2Save150.SIZE:
                    Console.WriteLine("XC2Save150 detected.");
                    return new XC2Save150(data);

                case XC2SaveIra.SIZE:
                    Console.WriteLine("XC2SaveIra detected.");
                    return new XC2SaveIra(data);
                
                case XC3Save100.SIZE:
                    Console.WriteLine("XC3Save100 detected.");
                    return new XC3Save100(data);

                case XC3Save120.SIZE:
                    Console.WriteLine("XC3Save120 detected.");
                    return new XC3Save120(data);
                
                case XCSaveThumbnail.TMB_SIZE:
                    Console.WriteLine("XCDE/XC3 Save Thumbnail detected.");
                    return new XCSaveThumbnail(data);

                default:
                    string message = "Given save data has incorrect filesize!" + Environment.NewLine +
                        Environment.NewLine +
                        "Expected Size:" + Environment.NewLine +
                        String.Format("0x{0:X} for XCDE Save data.", XCDESave.SIZE) + Environment.NewLine +
                        String.Format("0x{0:X} for XCDE Settings data.", XCDESettings.SIZE) + Environment.NewLine +
                        String.Format("0x{0:X} for XC2 old save data.", XC2Save.SIZE) + Environment.NewLine +
                        String.Format("0x{0:X} for XC2 ver 1.5.0 save data.", XC2Save150.SIZE) + Environment.NewLine +
                        String.Format("0x{0:X} for XC2 Torna Golden Country DLC save data.", XC2SaveIra.SIZE) + Environment.NewLine +
                        String.Format("0x{0:X} for XC3 ver 1.0.0 Save data.", XC3Save100.SIZE) + Environment.NewLine +
                        String.Format("0x{0:X} for XC3 ver 1.2.0 Save data.", XC3Save120.SIZE) + Environment.NewLine +
                        String.Format("0x{0:X} for XC Save Thumbnail data.", XCSaveThumbnail.TMB_SIZE) + Environment.NewLine +
                        Environment.NewLine +
                        "Actual Size: 0x" + data.Length.ToString("X");
                    throw new Exception(message);
            }
        }
        public static byte[] Serialize(ISaveFile save, bool compress) => compress ? Compress(save.ToRawData()) : save.ToRawData();

        public static byte[] Serialize(ISaveFile save) => Serialize(save, save.GetType() == typeof(XC2SaveIra));

        private static Byte[] Decompress(Byte[] data)
        {
            Byte[] result;

            using (MemoryStream cmpStr = new MemoryStream(data, CMP_HEADER.Length, data.Length - CMP_HEADER.Length))
            {
                using (ZLibStream zStr = new ZLibStream(cmpStr, CompressionMode.Decompress, true))
                {
                    using (MemoryStream decStr = new MemoryStream())
                    {
                        int bytesReadCount = 0;
                        byte[] buf = new byte[1024];

                        while ((bytesReadCount = zStr.Read(buf, 0, buf.Length)) > 0)
                            decStr.Write(buf, 0, bytesReadCount);

                        result = decStr.ToArray();
                    }
                }
            }

            return result;
        }
        private static Byte[] Compress(Byte[] data)
        {
            List<Byte> result = new List<Byte>();
            result.AddRange(CMP_HEADER);

            using (MemoryStream decStr = new MemoryStream(data, 0, data.Length))
            {
                using (MemoryStream cmpStr = new MemoryStream())
                {
                    using (ZLibStream zStr = new ZLibStream(cmpStr, CompressionLevel.Optimal, true))
                    {
                        int bytesReadCount = 0;
                        byte[] buf = new byte[1024];

                        while ((bytesReadCount = decStr.Read(buf, 0, buf.Length)) > 0)
                            zStr.Write(buf, 0, bytesReadCount);
                    }
                    result.AddRange(cmpStr.ToArray());
                }
            }

            return result.ToArray();
        }
    }
}
