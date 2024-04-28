/*
 * XenoSave
 * Copyright (C) 2018-2023  damysteryman
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmm.XenoSave.XC2
{
    public class XC2Party : ISaveObject
    {
        public const int SIZE = 0x54;
        public const int MEMBERS_COUNT = 10;

        private static readonly Dictionary<string, int> LOC = new Dictionary<string, int>()
            {
                { "Members", 0x0 },
                { "Unk_0x3C", 0x3C },
                { "PartyLeader", 0x40 },
                { "Unk_0x44", 0x44 },
                { "PartyGauge", 0x4C },
                { "Unk_0x4E", 0x4E },
            };

        public PartyMember[] Members { get; set; }
        public Byte[] Unk_0x3C { get; set; }
        public UInt32 Leader { get; set; }
        public Byte[] Unk_0x44 { get; set; }
        public UInt16 PartyGauge { get; set; }
        public Byte[] Unk_0x4E { get; set; }

        public XC2Party(Byte[] data)
        {
            Members = new PartyMember[10];
            for (int i = 0; i < Members.Length; i++)
                Members[i] = new PartyMember(data.GetByteSubArray(LOC["Members"] + (i * PartyMember.SIZE), PartyMember.SIZE));

            Unk_0x3C = data.GetByteSubArray(LOC["Unk_0x3C"], 4);
            Leader = BitConverter.ToUInt32(data.GetByteSubArray(LOC["PartyLeader"], 4), 0);
            Unk_0x44 = data.GetByteSubArray(LOC["Unk_0x44"], 8);
            PartyGauge = BitConverter.ToUInt16(data.GetByteSubArray(LOC["PartyGauge"], 2), 0);
            Unk_0x4E = data.GetByteSubArray(LOC["Unk_0x4E"], 6);
        }

        public Byte[] ToRawData()
        {
            List<Byte> result = new List<Byte>();

            foreach (PartyMember m in Members)
                result.AddRange(m.ToRawData());

            result.AddRange(Unk_0x3C);
            result.AddRange(BitConverter.GetBytes(Leader));
            result.AddRange(Unk_0x44);
            result.AddRange(BitConverter.GetBytes(PartyGauge));
            result.AddRange(Unk_0x4E);

            if (result.Count != SIZE)
            {
                string message = "XC2Party: SIZE ALL WRONG!!!" + Environment.NewLine +
                "Size should be " + SIZE + " bytes..." + Environment.NewLine +
                "...but Size is " + result.Count + " bytes!";

                throw new Exception(message);
            }

            return result.ToArray();
        }
    }
}
