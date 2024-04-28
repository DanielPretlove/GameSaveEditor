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
using System.Collections.Generic;

namespace dmm.XenoSave.XCDE
{
    /*
    *   PartyMember Structure
    *   Data for an individual Party Member/Player Character
    *
    *   Offset  Type        Name                    Description
    *   0x000   uint32      Level                   = Character's Level
    *   0x004   uint32      EXP                     = Character's amount of EXP
    *   0x008   uint32      AP                      = Character's amount of AP
    *   0x00C   uint32      AffinityCoins           = Character's No. of Affinity Coins
    *   0x010   uint8[4]    Unk_1                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x014   ItemID[6]   Equipment               = Equipped Weapon/Armour Item Types + INDEXES in inventory (NOT Item's actual ID)
    *   0x02C   uint8[4]    Unk_2                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x030   uint32[6]   Cosmetics               = IDs of Set Equipment Types' Cosmetics
    *   0x048   uint16[9]   Arts                    = IDs of Character's set arts, 1st ID is Talent Art, IDs 2~9 are normal set Arts
    *   0x05A   uint16[9]   MonadoArts              = Same as Arts, but 2nd set for when "Activate Monado" is used
    *   0x06C   uint8[0xC]  Unk_3                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x078   uint32      SelectedSkillTreeIndex  = Index of the Character's Currently Selected Skill Tree
    *   0x07C   uint32[5]   SkillTreeSPs            = Skill Points (SP) for each of the Character's Skill Trees
    *   0x090   uint32[5]   SkillTreeUnlockedSkills = The No. of Skills Unlocked in each of the Character's Skill Trees
    *   0x0A4   uint8[0x20] Unk_4                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x0C9   uint8[40]   SkillLinkIDs            = The IDs of each Skill Link Skill on a Character's Skill Link Menu
    *   0x0EC   uint32      ExpertModeLevel         = Character's Level used for Expert Mode feature
    *   0x0F0   uint32      ExpertModeEXP           = Character's EXP used for Expert Mode feature
    *   0x0F4   uint32      ExpertModeReserveEXP    = Character's amount of Reserve EXP saved up for Expert Mode feature
    *   0x0F8   uint8[0x40] Unk_5                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
    */
    public class PartyMember : ISaveObject
    {
        public const uint SIZE = 0x138;

        public uint Level { get; set; }
        public uint EXP { get; set; }
        public uint AP { get; set; }
        public uint AffinityCoins { get; set; }
        public byte[] Unk_1 { get; set; }
        public ItemID[] Equipment { get; set; } = new ItemID[6];
        public ItemID HeadEquip { get => Equipment[0]; }
        public ItemID TorsoEquip { get => Equipment[1]; }
        public ItemID ArmEquip { get => Equipment[2]; }
        public ItemID LegEquip { get => Equipment[3]; }
        public ItemID FootEquip { get => Equipment[4]; }
        public ItemID WeaponEquip { get => Equipment[5]; }
        public byte[] Unk_2 { get; set; }
        public uint[] Cosmetics { get; set; } = new uint[6];
        public uint HeadCosmetic { get => Cosmetics[0]; }
        public uint TorsoCosmetic { get => Cosmetics[1]; }
        public uint ArmCosmetic { get => Cosmetics[2]; }
        public uint LegCosmetic { get => Cosmetics[3]; }
        public uint FootCosmetic { get => Cosmetics[4];}
        public uint WeaponCosmetic { get => Cosmetics[5]; }
        public ushort[] Arts { get; set; } = new ushort[9];
        public ushort[] MonadoArts { get; set; } = new ushort[9];
        public byte[] Unk_3 { get; set; }
        public uint SelectedSkillTreeIndex { get; set; }
        public uint[] SkillTreeSPs { get; set; } = new uint[5];
        public uint[] SkillTreeUnlockedSkills { get; set; } = new uint[5];
        public byte[] Unk_4 { get; set; }
        public byte[] SkillLinkIDs { get; set; } = new byte[40];
        public uint ExpertModeLevel { get; set; }
        public uint ExpertModeEXP { get; set; }
        public uint ExpertModeReserveEXP { get; set; }
        public byte[] Unk_5 { get; set; }

        private static readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
        {
            { "Level", 0x00 },
            { "EXP", 0x04 },
            { "AP", 0x08 },
            { "AffinityCoins", 0x0C },
            { "Unk_1", 0x10 },
            { "Equipment", 0x14},
            { "Unk_2", 0x2C },
            { "Cosmetics", 0x30 },
            { "Arts", 0x48 },
            { "MonadoArts", 0x5A },
            { "Unk_3", 0x6C },
            { "SelectedSkillTreeIndex", 0x78 },
            { "SkillTreeSPs", 0x7C },
            { "SkillTreeUnlockedSkills", 0x90 },
            { "Unk_4", 0xA4 },
            { "SkillLinkIDs", 0xC4 },
            { "ExpertModeLevel", 0xEC },
            { "ExpertModeEXP", 0xF0 },
            { "ExpertModeReserveEXP", 0xF4 },
            { "Unk_5", 0xF8 }
        };

        public PartyMember()
        {
            Unk_1 = new byte[LOC["Equipment"] - LOC["Unk_1"]];
            Equipment = new ItemID[6];
            for (int i = 0; i < Equipment.Length; i++)
                Equipment[i] = new();
            Cosmetics = new uint[6];
            Unk_2 = new byte[LOC["Cosmetics"] - LOC["Unk_2"]];
            Unk_3 = new byte[LOC["SelectedSkillTreeIndex"] - LOC["Unk_3"]];
            Unk_4 = new byte[LOC["SkillLinkIDs"] - LOC["Unk_4"]];
            Unk_5 = new byte[SIZE - LOC["Unk_5"]];
        }
        public PartyMember(byte[] data)
        {
            Level = BitConverter.ToUInt32(data.GetByteSubArray(LOC["Level"], LOC["EXP"] - LOC["Level"]), 0);
            EXP = BitConverter.ToUInt32(data.GetByteSubArray(LOC["EXP"], LOC["AP"] - LOC["EXP"]), 0);
            AP = BitConverter.ToUInt32(data.GetByteSubArray(LOC["AP"], LOC["AffinityCoins"] - LOC["AP"]), 0);
            AffinityCoins = BitConverter.ToUInt32(data.GetByteSubArray(LOC["AffinityCoins"], LOC["Unk_1"] - LOC["AffinityCoins"]), 0);
            Unk_1 = data.GetByteSubArray(LOC["Unk_1"], LOC["Equipment"] - LOC["Unk_1"]);
            Equipment = new ItemID[6];
            for (uint i = 0; i < Equipment.Length; i++) Equipment[i] = new ItemID(data.GetByteSubArray(LOC["Equipment"] + (i * 4), ItemID.SIZE));
            Unk_2 = data.GetByteSubArray(LOC["Unk_2"], LOC["Cosmetics"] - LOC["Unk_2"]);
            Cosmetics = new uint[6];
            for (uint i = 0; i < Cosmetics.Length; i++) Cosmetics[i] = BitConverter.ToUInt32(data.GetByteSubArray(LOC["Cosmetics"] + (i * sizeof(uint)), sizeof(uint)));
            for (uint i = 0; i < Arts.Length; i++) Arts[i] = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Arts"] + (i * sizeof(UInt16)), sizeof(UInt16)), 0);
            for (uint i = 0; i < MonadoArts.Length; i++) MonadoArts[i] = BitConverter.ToUInt16(data.GetByteSubArray(LOC["MonadoArts"] + (i * sizeof(UInt16)), sizeof(UInt16)), 0);
            Unk_3 = data.GetByteSubArray(LOC["Unk_3"], LOC["SelectedSkillTreeIndex"] - LOC["Unk_3"]);
            SelectedSkillTreeIndex = BitConverter.ToUInt32(data.GetByteSubArray(LOC["SelectedSkillTreeIndex"], LOC["SkillTreeSPs"] - LOC["SelectedSkillTreeIndex"]), 0);
            for (uint i = 0; i < SkillTreeSPs.Length; i++) SkillTreeSPs[i] = BitConverter.ToUInt32(data.GetByteSubArray(LOC["SkillTreeSPs"] + (i * sizeof(UInt32)), sizeof(UInt32)), 0);
            for (uint i = 0; i < SkillTreeUnlockedSkills.Length; i++) SkillTreeUnlockedSkills[i] = BitConverter.ToUInt32(data.GetByteSubArray(LOC["SkillTreeUnlockedSkills"] + (i * sizeof(UInt32)), sizeof(UInt32)), 0);
            Unk_4 = data.GetByteSubArray(LOC["Unk_4"], LOC["SkillLinkIDs"] - LOC["Unk_4"]);
            for (uint i = 0; i < SkillLinkIDs.Length; i++) SkillLinkIDs[i] = data[LOC["SkillLinkIDs"] + i];
            ExpertModeLevel = BitConverter.ToUInt32(data.GetByteSubArray(LOC["ExpertModeLevel"], LOC["ExpertModeEXP"] - LOC["ExpertModeLevel"]), 0);
            ExpertModeEXP = BitConverter.ToUInt32(data.GetByteSubArray(LOC["ExpertModeEXP"], LOC["ExpertModeReserveEXP"] - LOC["ExpertModeEXP"]), 0);
            ExpertModeReserveEXP = BitConverter.ToUInt32(data.GetByteSubArray(LOC["ExpertModeReserveEXP"], LOC["Unk_5"] - LOC["ExpertModeReserveEXP"]), 0);
            Unk_5 = data.GetByteSubArray(LOC["Unk_5"], SIZE - LOC["Unk_5"]);
        }

        public byte[] ToRawData()
        {
            List<byte> result = new List<byte>();

            result.AddRange(BitConverter.GetBytes(Level));
            result.AddRange(BitConverter.GetBytes(EXP));
            result.AddRange(BitConverter.GetBytes(AP));
            result.AddRange(BitConverter.GetBytes(AffinityCoins));
            result.AddRange(Unk_1);
            foreach(ItemID e in Equipment) result.AddRange(e.ToRawData());
            result.AddRange(Unk_2);
            foreach (uint c in Cosmetics) result.AddRange(BitConverter.GetBytes(c));
            foreach (UInt16 a in Arts) result.AddRange(BitConverter.GetBytes(a));
            foreach (UInt16 a in MonadoArts) result.AddRange(BitConverter.GetBytes(a));
            result.AddRange(Unk_3);
            result.AddRange(BitConverter.GetBytes(SelectedSkillTreeIndex));
            foreach (UInt32 sp in SkillTreeSPs) result.AddRange(BitConverter.GetBytes(sp));
            foreach (UInt32 su in SkillTreeUnlockedSkills) result.AddRange(BitConverter.GetBytes(su));
            result.AddRange(Unk_4);
            result.AddRange(SkillLinkIDs);
            result.AddRange(BitConverter.GetBytes(ExpertModeLevel));
            result.AddRange(BitConverter.GetBytes(ExpertModeEXP));
            result.AddRange(BitConverter.GetBytes(ExpertModeReserveEXP));
            result.AddRange(Unk_5);

            return result.ToArray();
        }
    }
}