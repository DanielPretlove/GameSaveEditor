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
using System.Collections;
using System.Collections.Generic;

namespace dmm.XenoSave.XCDE
{
    /*
    *   XCDESave Structure
    *   Save file for Xenoblade Chronchles: Definitive Edition [WORK IN PROGRESS, PLEASE RESEARCH]
    *
    *   Offset      Type                Name            Description
    *   0x000000    uint8[0x10]         Unk_0x000000        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x000004    ElapseTime          TotalPlayTime       = The Total Time Played on the File
    *   0x000008    RealTime            SaveTimestamp       = The Real-World Time when the File was Saved
    *   0x000010    uint32              Noponstones         = No. of Noponstones
    *   0x000014    int32               FCArtsCoins         = No. of Arts Coins (Future Connected)
    *   0x000018    uint8[0x05D3]       Unk_0x000018        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x0003A2    uint8[0x024C]       Flags               = Collection of bits for various game flags
    *   0x0005EE    uint8[0x0517]       Unk_0x0005EE        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x000B05    uint8[362]          AffinityLinks       = Affinity Values for each Affinity Link in Affinity Chart
    *   0x000C6F    uint8[0x0185]       Unk_0x000C6F        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x000DF4    uint16[5]           AreaAffinity        = Affinity Values for each of the Areas in Affinity Chart
    *   0x000DFE    uint8[0x04]         Unk_0x000DFE        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x000E02    uint16[21]          PartyAffinity       = Affinity Values between Party Characters
    *   0x000E2C    uint8[0x06]         Unk_0x000E2C        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x000E32    uint16[200]         AchievementCounters = Objective Counter values for every Achievement
    *   0x000FC2    uint8[0x2B4E]       Unk_0x000FC2        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x003B10    EquipItem[500]      WeaponBox           = Weapons in inventory (500pcs)
    *   0x0098d0    EquipItem[500]      HeadArmourBox       = Head Equipment in inventory (500pcs)
    *   0x00F690    EquipItem[500]      TorsoArmourBox      = Torso Equipment in inventory (500pcs)
    *   0x015450    EquipItem[500]      ArmArmourBox        = Arm Equipment in inventory (500pcs)
    *   0x01B210    EquipItem[500]      LegArmourBox        = Leg Equipment in inventory (500pcs)
    *   0x020FD0    EquipItem[500]      FootArmourBox       = Foot/Auxiliary Equipment in inventory (500pcs)
    *   0x026D90    CrystalItem[500]    CrystalBox          = Crystals in inventory (500pcs)
    *   0x02C380    CrystalItem[500]    GemBox              = Gems in inventory (500pcs)
    *   0x031970    Item[500]           CollectableBox      = Collectables in inventory (500pcs)
    *   0x034080    Item[500]           MaterialBox         = Materials in inventory (500pcs)
    *   0x036790    Item[500]           KeyItemBox          = Key items in inventory (500pcs)
    *   0x038EA0    Item[500]           ArtsManualBox       = Arts Manuals in inventory (500pcs)
    *   0x03B5B0    uint8[0x0584]       Unk_0x03B5B0        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x03BB34    NonPartyMember[27]  NonPartyMembers     = Equipment setups for Characters not in Party
    *   0x044018    uint8[0x28F0]       Unk_0x044018        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x046908    uint32[13]          ItemBoxSerials      = Serials for all inventory boxes (seems to be 1 extra?)
    *                                                         Note about mapping of Serials to corresponding item boxes:
    *                                                         0: Weapons, 1: Gems, 2: Head Armour, 3: Torso Armour, 4: Arm Armour, 5: Leg Armour, 6: Foot Armour,
    *                                                         7: Crystals, 8: Collectables, 9: Materials, 10: Key Items, 11: Arts Manuals, 12: Unknown / Unused
    *   0x04693C    uint8[0x10276D]     Unk_0x04693C        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x1490A9    uint8[44]           Flags2              = More game flags. Contains Collectopaedia completion flags.
    *   0x1490D5    uint8[0x0B13]       Unk_0x1490D5        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x149BE8    float               CameraZoom          = Zoom Value of in-game Camera
    *   0x149BEC    uint8[0x7F54]       Unk_0x149BEC        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x151B40    uint32              Money               = Amount of Money
    *   0x151B44    uint8[0x04B4]       Unk_0x151B44        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x151FF8    TimeAttackData      TimeAttack          = Time Attack Scores & Times
    *   0x1520E8    uint8[0x0230]       Unk_0x1520E8        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x152318    Party               Party               = Party structure, No. and IDs of characters in party
    *   0x152331    uint8[0x37]         Unk_0x152331        = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x152368    PartyMember[16]     PartyMembers        = Party Member Character Data
    *   0x1536E8    ArtsLevel[188]      ArtsLevels          = Arts Levels + Unlock Levels for each Art (except Ponspector Arts)
    */

    public enum AffinityLink
    {
        None,
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Purple,
        Pink
    }

    public enum AreaAffinity
    {
        Colony_9_Area,
        Colony_6_Area,
        Central_Bionis,
        Upper_Bionis,
        Hidden_Village
    }

    public enum PartyAffinity
    {
        Shulk_Reyn,
        Shulk_Fiora,
        Shulk_Dunban,
        Shulk_Sharla,
        Shulk_Riki,
        Shulk_Melia,
        Reyn_Fiora,
        Reyn_Dunban,
        Reyn_Sharla,
        Reyn_Riki,
        Reyn_Melia,
        Fiora_Dunban,
        Fiora_Sharla,
        Fiora_Riki,
        Fiora_Melia,
        Dunban_Sharla,
        Dunban_Riki,
        Dunban_Melia,
        Sharla_Riki,
        Sharla_Melia,
        Riki_Melia
    }


    public enum Location
    {
        Unk_1 = 1,
        Colony9,
        TephraCave,
        BionisLeg,
        Colony6,
        EtherMine,
        SatorlMarsh,
        MaknaForest,
        FrontierVillage,
        BionisShoulder,
        HighEntiaTomb,
        ErythSea,
        Alcamoth,
        PrisonIslandLocked,
        PrisonIsland,
        ValakMountain,
        SwordValley,
        GalahadFortress,
        FallenArm,
        Unk_20,
        MechonisField,
        Unk_22,
        Agniratha,
        CentralFactory,
        BionisInterior,
        MemorySpace,
        MechonisCore,
        Junks,
        BionisShoulderFC,
        AlcamothFC
    }

    public enum CollectableType
    {
        Veg = 1,
        Fruit,
        Flower,
        Animal,
        Bug,
        Nature,
        Parts,
        Strange
    }

    public class XCDESave : ISaveFile, ISaveObject
    {
        public const int SIZE = 0x153860;

        public byte[]           Unk_0x000000        { get; set; } = new byte[4];
        public ElapseTime       TotalPlayTime       { get; set; }
        public RealTime         SaveTimestamp       { get; set; }
        public uint             Noponstones         { get; set; }
        public int              FCArtsCoins         { get; set; }
        public byte[]           Unk_0x000018        { get; set; }
        public BitArray         Flags               { get; set; }
        public byte[]           Unk_0x0005EE        { get; set; }
        public AffinityLink[]   AffinityLinks       { get; set; } = new AffinityLink[362];
        public byte[]           Unk_0x000C6F        { get; set; }
        public ushort[]         AreaAffinity        { get; set; } = new ushort[5];
        public byte[]           Unk_0x000DFE        { get; set; }
        public ushort[]         PartyAffinity       { get; set; } = new ushort[21];
        public byte[]           Unk_0x000E2C        { get; set; }
        public ushort[]         AchievementCounters { get; set; } = new ushort[200];
        public byte[]           Unk_0x000FC2        { get; set; }
        public EquipItem[]      Weapons             { get; set; } = new EquipItem[500];
        public EquipItem[]      HeadArmour          { get; set; } = new EquipItem[500];
        public EquipItem[]      TorsoArmour         { get; set; } = new EquipItem[500];
        public EquipItem[]      ArmArmour           { get; set; } = new EquipItem[500];
        public EquipItem[]      LegArmour           { get; set; } = new EquipItem[500];
        public EquipItem[]      FootArmour          { get; set; } = new EquipItem[500];
        public CrystalItem[]    Crystals            { get; set; } = new CrystalItem[500];
        public CrystalItem[]    Gems                { get; set; } = new CrystalItem[500];
        public Item[]           Collectables        { get; set; } = new Item[500];
        public Item[]           Materials           { get; set; } = new Item[500];
        public Item[]           KeyItems            { get; set; } = new Item[500];
        public Item[]           ArtsManuals         { get; set; } = new Item[500];
        public byte[]           Unk_0x03B5B0        { get; set; }
        public NonPartyMember[] NonPartyMembers     { get; set; } = new NonPartyMember[27];
        public byte[]           Unk_0x044018        { get; set; }
        public uint[]           ItemBoxSerials      { get; set; } = new uint[13];
        public byte[]           Unk_0x04693C        { get; set; }
        public BitArray         Flags2              { get; set; }
        public byte[]           Unk_0x1490D5        { get; set; }
        public float            CameraZoom          { get; set; }
        public byte[]           Unk_0x149BEC        { get; set; }
        public uint             Money               { get; set; }
        public byte[]           Unk_0x151B44        { get; set; }
        public TimeAttackData   TimeAttack          { get; set; }
        public byte[]           Unk_0x1520E8        { get; set; }
        public Party            Party               { get; set; }
        public byte[]           Unk_0x152331        { get; set; }
        public PartyMember[]    PartyMembers        { get; set; } = new PartyMember[16];
        public ArtsLevel[]      ArtsLevels          { get; set; } = new ArtsLevel[188];

        private static readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
        {
            { "Unk_0x000000",        0x0      },
            { "TotalPlayTime",       0x4      },
            { "SaveTimestamp",       0x8      },
            { "Noponstones",         0x10     },
            { "ArtsCoins",           0x14     },
            { "Unk_0x000018",        0x18     },
            { "Flags",               0x3A2    },
            { "Unk_0x0005EE",        0x5EE    },
            { "AffinityLinks",       0xB05    },
            { "Unk_0x000C6F",        0xC6F    },
            { "AreaAffinity",        0xDF4    },
            { "Unk_0x000DFE",        0xDFE    },
            { "PartyAffinity",       0xE02    },
            { "Unk_0x000E2C",        0xE2C    },
            { "AchievementCounters", 0xE32    },
            { "Unk_0x000FC2",        0xFC2    },
            { "WeaponBox",           0x3B10   },
            { "HeadArmourBox",       0x98D0   },
            { "TorsoArmourBox",      0xF690   },
            { "ArmArmourBox",        0x15450  },
            { "LegArmourBox",        0x1B210  },
            { "FootArmourBox",       0x20FD0  },
            { "CrystalBox",          0x26D90  },
            { "GemBox",              0x2C380  },
            { "CollectableBox",      0x31970  },
            { "MaterialBox",         0x34080  },
            { "KeyItemBox",          0x36790  },
            { "ArtsManualBox",       0x38EA0  },
            { "Unk_0x03B5B0",        0x3B5B0  },
            { "NonPartyMembers",     0x3BB34  },
            { "Unk_0x044018",        0x44018  },
            { "ItemBoxSerials",      0x46908  },
            { "Unk_0x04693C",        0x4693C  },
            { "Flags2",              0x1490A9 },
            { "Unk_0x1490D5",        0x1490D5 },
            { "CameraZoom",          0x149BE8 },
            { "Unk_0x149BEC",        0x149BEC },
            { "Money",               0x151B40 },
            { "Unk_0x151B44",        0x151B44 },
            { "TimeAttack",          0x151FF8 },
            { "Unk_0x1520E8",        0x1520E8 },
            { "Party",               0x152318 },
            { "Unk_0x152331",        0x152331 },
            { "PartyMembers",        0x152368 },
            { "ArtsLevels",          0x1536E8 }
        };

        public XCDESave(byte[] data)
        {
            Unk_0x000000    = data.GetByteSubArray(LOC["Unk_0x000000"], (uint)Unk_0x000000.Length);
            TotalPlayTime   = new ElapseTime(data.GetByteSubArray(LOC["TotalPlayTime"], ElapseTime.SIZE));
            SaveTimestamp   = new RealTime(data.GetByteSubArray(LOC["SaveTimestamp"], RealTime.SIZE));
            Noponstones     = BitConverter.ToUInt32(data.GetByteSubArray(LOC["Noponstones"], (uint)sizeof(uint)), 0);
            Unk_0x000018    = data.GetByteSubArray(LOC["Unk_0x000018"], LOC["Flags"] - LOC["Unk_0x000018"]);
            Flags           = new BitArray(data.GetByteSubArray(LOC["Flags"], LOC["Unk_0x0005EE"] - LOC["Flags"]));
            Unk_0x0005EE    = data.GetByteSubArray(LOC["Unk_0x0005EE"], LOC["AffinityLinks"] - LOC["Unk_0x0005EE"]);
            for (uint i = 0; i < AffinityLinks.Length; i++) AffinityLinks[i]    = (AffinityLink)data[LOC["AffinityLinks"] + (uint)(i * sizeof(byte))];
            Unk_0x000C6F    = data.GetByteSubArray(LOC["Unk_0x000C6F"], LOC["AreaAffinity"] - LOC["Unk_0x000C6F"]);
            for (uint i = 0; i < AreaAffinity.Length; i++)  AreaAffinity[i]     = BitConverter.ToUInt16(data.GetByteSubArray(LOC["AreaAffinity"] + (uint)(i * sizeof(ushort)), (uint)sizeof(ushort)));
            Unk_0x000DFE    = data.GetByteSubArray(LOC["Unk_0x000DFE"], LOC["PartyAffinity"] - LOC["Unk_0x000DFE"]);
            for (uint i = 0; i < PartyAffinity.Length; i++)  PartyAffinity[i]    = BitConverter.ToUInt16(data.GetByteSubArray(LOC["PartyAffinity"] + (uint)(i * sizeof(ushort)), (uint)sizeof(ushort)));
            Unk_0x000E2C    = data.GetByteSubArray(LOC["Unk_0x000E2C"], LOC["AchievementCounters"] - LOC["Unk_0x000E2C"]);
            for (uint i = 0; i < AchievementCounters.Length; i++) AchievementCounters[i] = BitConverter.ToUInt16(data.GetByteSubArray(LOC["AchievementCounters"] + (uint)(i * sizeof(ushort)), (uint)sizeof(ushort)));
            Unk_0x000FC2    = data.GetByteSubArray(LOC["Unk_0x000FC2"], LOC["WeaponBox"] - LOC["Unk_0x000FC2"]);
            for (uint i = 0; i < Weapons.Length; i++)       Weapons[i]      = new EquipItem(data.GetByteSubArray(LOC["WeaponBox"] + (i * EquipItem.SIZE), EquipItem.SIZE));
            for (uint i = 0; i < HeadArmour.Length; i++)    HeadArmour[i]   = new EquipItem(data.GetByteSubArray(LOC["HeadArmourBox"] + (i * EquipItem.SIZE), EquipItem.SIZE));
            for (uint i = 0; i < TorsoArmour.Length; i++)   TorsoArmour[i]  = new EquipItem(data.GetByteSubArray(LOC["TorsoArmourBox"] + (i * EquipItem.SIZE), EquipItem.SIZE));
            for (uint i = 0; i < ArmArmour.Length; i++)     ArmArmour[i]    = new EquipItem(data.GetByteSubArray(LOC["ArmArmourBox"] + (i * EquipItem.SIZE), EquipItem.SIZE));
            for (uint i = 0; i < LegArmour.Length; i++)     LegArmour[i]    = new EquipItem(data.GetByteSubArray(LOC["LegArmourBox"] + (i * EquipItem.SIZE), EquipItem.SIZE));
            for (uint i = 0; i < FootArmour.Length; i++)    FootArmour[i]   = new EquipItem(data.GetByteSubArray(LOC["FootArmourBox"] + (i * EquipItem.SIZE), EquipItem.SIZE));
            for (uint i = 0; i < Crystals.Length; i++)      Crystals[i]     = new CrystalItem(data.GetByteSubArray(LOC["CrystalBox"] + (i * CrystalItem.SIZE), CrystalItem.SIZE));
            for (uint i = 0; i < Gems.Length; i++)          Gems[i]         = new CrystalItem(data.GetByteSubArray(LOC["GemBox"] + (i * CrystalItem.SIZE), CrystalItem.SIZE));
            for (uint i = 0; i < Collectables.Length; i++)  Collectables[i] = new Item(data.GetByteSubArray(LOC["CollectableBox"] + (i * Item.SIZE), Item.SIZE));
            for (uint i = 0; i < Materials.Length; i++)     Materials[i]    = new Item(data.GetByteSubArray(LOC["MaterialBox"] + (i * Item.SIZE), Item.SIZE));
            for (uint i = 0; i < KeyItems.Length; i++)      KeyItems[i]     = new Item(data.GetByteSubArray(LOC["KeyItemBox"] + (i * Item.SIZE), Item.SIZE));
            for (uint i = 0; i < ArtsManuals.Length; i++)   ArtsManuals[i]  = new Item(data.GetByteSubArray(LOC["ArtsManualBox"] + (i * Item.SIZE), Item.SIZE));
            Unk_0x03B5B0    = data.GetByteSubArray(LOC["Unk_0x03B5B0"], LOC["NonPartyMembers"] - LOC["Unk_0x03B5B0"]);
            for (uint i = 0; i < NonPartyMembers.Length; i++) NonPartyMembers[i] = new NonPartyMember(data.GetByteSubArray(LOC["NonPartyMembers"] + (i * NonPartyMember.SIZE), NonPartyMember.SIZE));
            Unk_0x044018    = data.GetByteSubArray(LOC["Unk_0x044018"], LOC["ItemBoxSerials"] - LOC["Unk_0x044018"]);
            for (uint i = 0; i < ItemBoxSerials.Length; i++)  ItemBoxSerials[i] = BitConverter.ToUInt32(data.GetByteSubArray(LOC["ItemBoxSerials"] + (i * sizeof(uint)), sizeof(uint)));
            Unk_0x04693C    = data.GetByteSubArray(LOC["Unk_0x04693C"], LOC["Flags2"] - LOC["Unk_0x04693C"]);
            Flags2          = new BitArray(data.GetByteSubArray(LOC["Flags2"], LOC["Unk_0x1490D5"] - LOC["Flags2"]));
            Unk_0x1490D5    = data.GetByteSubArray(LOC["Unk_0x1490D5"], LOC["CameraZoom"] - LOC["Unk_0x1490D5"]);
            CameraZoom      = BitConverter.ToSingle(data.GetByteSubArray(LOC["CameraZoom"], LOC["Unk_0x149BEC"] - LOC["CameraZoom"]));
            Unk_0x149BEC    = data.GetByteSubArray(LOC["Unk_0x149BEC"], LOC["Money"] - LOC["Unk_0x149BEC"]);
            Money           = BitConverter.ToUInt32(data.GetByteSubArray(LOC["Money"], 0x4), 0);
            Unk_0x151B44    = data.GetByteSubArray(LOC["Unk_0x151B44"], LOC["TimeAttack"] - LOC["Unk_0x151B44"]);
            TimeAttack      = new TimeAttackData(data.GetByteSubArray(LOC["TimeAttack"], TimeAttackData.SIZE));
            Unk_0x1520E8    = data.GetByteSubArray(LOC["Unk_0x1520E8"], LOC["Party"] - LOC["Unk_0x1520E8"]);
            Party           = new Party(data.GetByteSubArray(LOC["Party"], Party.SIZE));
            Unk_0x152331    = data.GetByteSubArray(LOC["Unk_0x152331"], LOC["PartyMembers"] - LOC["Unk_0x152331"]);
            for (uint i = 0; i < PartyMembers.Length; i++)  PartyMembers[i] = new PartyMember(data.GetByteSubArray(LOC["PartyMembers"] + (i * PartyMember.SIZE), PartyMember.SIZE));
            for (uint i = 0; i < ArtsLevels.Length; i++)    ArtsLevels[i]   = new ArtsLevel(data.GetByteSubArray(LOC["ArtsLevels"] + (i * ArtsLevel.SIZE), ArtsLevel.SIZE));
        }

        public byte[] ToRawData()
        {
            List<byte> result = new List<byte>();

            result.AddRange(Unk_0x000000);
            result.AddRange(TotalPlayTime.ToRawData());
            result.AddRange(SaveTimestamp.ToRawData());
            result.AddRange(BitConverter.GetBytes(Noponstones));
            result.AddRange(BitConverter.GetBytes(FCArtsCoins));
            result.AddRange(Unk_0x000018);

            byte[] flags = new byte[LOC["Unk_0x0005EE"] - LOC["Flags"]];
            Flags.CopyTo(flags, 0);
            result.AddRange(flags);

            result.AddRange(Unk_0x0005EE);
            foreach (byte i in AffinityLinks)             result.Add(i);
            result.AddRange(Unk_0x000C6F);
            foreach (ushort i in AreaAffinity)            result.AddRange(BitConverter.GetBytes(i));
            result.AddRange(Unk_0x000DFE);
            foreach (ushort i in PartyAffinity)           result.AddRange(BitConverter.GetBytes(i));
            result.AddRange(Unk_0x000E2C);
            foreach (ushort i in AchievementCounters)     result.AddRange(BitConverter.GetBytes(i));
            result.AddRange(Unk_0x000FC2);
            foreach (EquipItem i in Weapons)              result.AddRange(i.ToRawData());
            foreach (EquipItem i in HeadArmour)           result.AddRange(i.ToRawData());
            foreach (EquipItem i in TorsoArmour)          result.AddRange(i.ToRawData());
            foreach (EquipItem i in ArmArmour)            result.AddRange(i.ToRawData());
            foreach (EquipItem i in LegArmour)            result.AddRange(i.ToRawData());
            foreach (EquipItem i in FootArmour)           result.AddRange(i.ToRawData());
            foreach (CrystalItem i in Crystals)           result.AddRange(i.ToRawData());
            foreach (CrystalItem i in Gems)               result.AddRange(i.ToRawData());
            foreach (Item i in Collectables)              result.AddRange(i.ToRawData());
            foreach (Item i in Materials)                 result.AddRange(i.ToRawData());
            foreach (Item i in KeyItems)                  result.AddRange(i.ToRawData());
            foreach (Item i in ArtsManuals)               result.AddRange(i.ToRawData());
            result.AddRange(Unk_0x03B5B0);
            foreach (NonPartyMember i in NonPartyMembers) result.AddRange(i.ToRawData());
            result.AddRange(Unk_0x044018);
            foreach (uint i in ItemBoxSerials)            result.AddRange(BitConverter.GetBytes(i));
            result.AddRange(Unk_0x04693C);

            byte[] flags2 = new byte[LOC["Unk_0x1490D5"] - LOC["Flags2"]];
            Flags2.CopyTo(flags2, 0);
            result.AddRange(flags2);

            result.AddRange(Unk_0x1490D5);
            result.AddRange(BitConverter.GetBytes(CameraZoom));
            result.AddRange(Unk_0x149BEC);
            result.AddRange(BitConverter.GetBytes(Money));
            result.AddRange(Unk_0x151B44);
            result.AddRange(TimeAttack.ToRawData());
            result.AddRange(Unk_0x1520E8);
            result.AddRange(Party.ToRawData());
            result.AddRange(Unk_0x152331);
            foreach (PartyMember p in PartyMembers)       result.AddRange(p.ToRawData());
            foreach (ArtsLevel a in ArtsLevels)           result.AddRange(a.ToRawData());
            
            return result.ToArray();
        }
    }
}
