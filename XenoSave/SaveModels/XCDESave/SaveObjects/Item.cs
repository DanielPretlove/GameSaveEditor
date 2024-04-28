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
    public enum ItemType
        {
            None = 0,
            UniqueGem,
            Weapon = 2,
            Gem,
            HeadArmour = 4,
            TorsoArmour,
            ArmArmour,
            LegArmour,
            FootArmour,
            Crystal,
            Collectable,
            Material,
            KeyItem,
            ArtsManual
        }

    /*
    *   ItemID Structure
    *   A combination of an an ID for the Item's type and an ID for the Item itself
    *
    *   Offset  Type    Name        Description
    *   0x0    uint16  ID       = ID of Item (Ex. 1 = Monado, 4 = Monado II, 642 = Colony Cap, see https://xenoblade.github.io/xb1de/bdat/bdat_common/ITM_itemlist.html)
    *   0x2    uint16  TypeID   = ID of Item's Type (Ex. 2 = Weapon, 5 = Torso Armour, 10 = Collectable)
    */
    public class ItemID : ISaveObject
    {
        public const uint SIZE = 4;
        public ushort ID { get; set; }
        public ItemType TypeID { get; set;}

        public ItemID()
        {
            TypeID = ItemType.None;
            ID = 0;
        }
        
        public ItemID(byte[] data)
        {
            ID = BitConverter.ToUInt16(data.GetByteSubArray(0x0, 2), 0);
            TypeID = (ItemType)BitConverter.ToUInt16(data.GetByteSubArray(0x2, 2), 0);
        }

        public byte[] ToRawData()
        {
            List<byte> result = new List<byte>();
            
            result.AddRange(BitConverter.GetBytes(ID));
            result.AddRange(BitConverter.GetBytes((ushort)TypeID));

            return result.ToArray();
        }

        public override string ToString()
        {
            return $"{(ushort)TypeID:D2}-{ID:D4}";
        }

        public bool IsEmpty(bool _strict = false)
        {
            return ID == 0 && (_strict ? TypeID == 0 : true);
        }
    }

    /*
    *   Item Structure
    *   A generic Item in player's inventory, used by Collectables, Materials, Key items, and Arts Manuals
    *   and then used a base for EquipItem and CrystalItem
    *
    *   Offset  Type    Name        Description
    *   0x00    uint16  Index       = The Index of this item in its respective "bag/box/tab" in the inventory
    *   0x02    uint16  Type        = The ID for the Item's Type (Ex. 2 = Weapon, 5 = Torso Armour, 10 = Collectable)
    *   0x04    ItemID  FullID      = The ItemID of the Item, see ItemID structure for more info
    *   0x08    uint16  Quantity    = The Quantity of this Item
    *   0x0A    uint16  Unk_1       = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x0C    uint32  SerialNo    = The unique Serial No. of the Item in its respective "bag/box/tab" in the inventory
    *   0x10    uint8   Exists      = Flag determining whether or not this item "exists" in player's inventory (game "deletes" Items by setting this to false)
    *   0x11    uint8   Favourite   = Flag determining whether or not this Item is marked as "Favourite" in inventory
    *   0x12    uint16  Unk_2       = ???   [UNKNOWN, PLEASE INVESTIGATE]
    */
    public class Item : ISaveObject
    {
        public const uint SIZE = 0x14;

        public ushort Index { get; set; }
        public ItemType Type { get; set; }
        public ItemID FullID { get; set; }
        public ushort Quantity { get; set; }
        public ushort Unk_1 { get; set; }
        public uint SerialNo { get; set; }
        public bool Exists { get; set; }
        public bool Favourite { get; set; }
        public ushort Unk_2 { get; set; }

        private static readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
        {
            { "Index", 0x00 },
            { "Type", 0x02 },
            { "FullID", 0x04 },
            { "Quantity", 0x08 },
            { "Unk_1", 0x0A },
            { "SerialNo", 0x0C },
            { "Exists", 0x10 },
            { "Favourite", 0x11 },
            { "Unk_2", 0x12 },
        };

        public Item()
        {
            Index = 0;
            Type = ItemType.None;
            FullID = new ItemID();
            Quantity = 0;
            Unk_1 = 0;
            SerialNo = 0;
            Exists = false;
            Favourite = false;
            Unk_2 = 0;
        }

        public Item(byte[] data)
        {
            Index = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Index"], 2), 0);
            Type = (ItemType)BitConverter.ToUInt16(data.GetByteSubArray(LOC["Type"], 2), 0);
            FullID = new ItemID(data.GetByteSubArray(LOC["FullID"], 4));
            Quantity = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Quantity"], 2), 0);
            Unk_1 = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Unk_1"], 2), 0);
            SerialNo = BitConverter.ToUInt32(data.GetByteSubArray(LOC["SerialNo"], 4), 0);
            Exists = data[LOC["Exists"]] == 1;
            Favourite = data[LOC["Favourite"]] == 1;
            Unk_2 = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Unk_2"], 2), 0);
        }

        public byte[] ToRawData()
        {
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes(Index));
            result.AddRange(BitConverter.GetBytes((ushort)Type));
            result.AddRange(FullID.ToRawData());
            result.AddRange(BitConverter.GetBytes(Quantity));
            result.AddRange(BitConverter.GetBytes(Unk_1));
            result.AddRange(BitConverter.GetBytes(SerialNo));
            result.Add(Exists ? (byte)0x01 : (byte)0x00);
            result.Add(Favourite ? (byte)0x01 : (byte)0x00);
            result.AddRange(BitConverter.GetBytes(Unk_2));

            return result.ToArray();
        }

        public override string ToString()
        {
            return $"{Index},{(Exists ? 1 : 0)},{Type},{FullID},{Quantity},{SerialNo}";
        }

        public bool IsEmpty()
        {
            return 
                FullID.IsEmpty() &&
                Quantity    == 0 &&
                Unk_1       == 0 &&
                SerialNo    == 0 &&
                !Exists          &&
                !Favourite       &&
                Unk_2       == 0;
        }
    }

    /*
    *   EquipItem Structure
    *   An Equippable Item (Equipment) in player's inventory, used by Weapons and Armour
    *
    *   Offset  Type    Name        Description
    *   0x00    Item                = (Item base, see Item Structure)
    *   0x14    uint8   Weight      = Weight of Equipment
    *   0x15    uint8   GemSlots    = No. of Gem Slots/Socket in Equipment (Max 3)
    *   0x16    uint16  Unk_3       = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x18    ItemID  Gem1        = Gem placed in Gem Slot 1
    *   0x1C    ItemID  UniqueGem1  = Unique Gem in Gem Slot 1
    *   0x20    ItemID  Gem2        = Gem placed in Gem Slot 2
    *   0x24    ItemID  UniqueGem2  = Unique Gem in Gem Slot 2
    *   0x28    ItemID  Gem3        = Gem placed in Gem Slot 3
    *   0x2C    ItemID  UniqueGem3  = Unique Gem in Gem Slot 3
    */
    public class EquipItem : Item, ISaveObject
    {
        public new const uint SIZE = 0x30;
        
        public byte Weight         { get; set; }
        public byte GemSlots       { get; set; }
        public ushort Unk_3        { get; set; }
        public ItemID[] Gems       { get; set; }
        public ItemID[] UniqueGems { get; set; }

        private static readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
        {
            { "Index", 0x00 },
            { "Type", 0x02 },
            { "FullId", 0x04 },
            { "Quantity", 0x08 },
            { "Unk_1", 0x0A },
            { "SerialNo", 0x0C },
            { "Exists", 0x10 },
            { "Favourite", 0x11 },
            { "Unk_2", 0x12 },
            { "Weight" , 0x14 },
            { "GemSlots", 0x15 },
            { "Unk_3", 0x16 },
            { "Gem1", 0x18 },
            { "Gem1Alt", 0x1C },
            { "Gem2", 0x20 },
            { "Gem2Alt", 0x24 },
            { "Gem3", 0x28 },
            { "Gem3Alt", 0x2C },
        };

        public EquipItem() : base()
        {
            Gems       = new ItemID[3];
            UniqueGems = new ItemID[3];
        }

        public EquipItem(byte[] data) : base(data)
        {
            Weight        = data[LOC["Weight"]];
            GemSlots      = data[LOC["GemSlots"]];
            Unk_3         = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Unk_3"], 2), 0);
            Gems          = new ItemID[3];
            UniqueGems    = new ItemID[3];
            Gems[0]       = new ItemID(data.GetByteSubArray(LOC["Gem1"], 4));
            UniqueGems[0] = new ItemID(data.GetByteSubArray(LOC["Gem1Alt"], 4));
            Gems[1]       = new ItemID(data.GetByteSubArray(LOC["Gem2"], 4));
            UniqueGems[1] = new ItemID(data.GetByteSubArray(LOC["Gem2Alt"], 4));
            Gems[2]       = new ItemID(data.GetByteSubArray(LOC["Gem3"], 4));
            UniqueGems[2] = new ItemID(data.GetByteSubArray(LOC["Gem3Alt"], 4));
        }

        public new byte[] ToRawData()
        {
            List<byte> result = new List<byte>();

            result.AddRange(base.ToRawData());
            result.Add(Weight);
            result.Add(GemSlots);
            result.AddRange(BitConverter.GetBytes(Unk_3));
            result.AddRange(Gems[0].ToRawData());
            result.AddRange(UniqueGems[0].ToRawData());
            result.AddRange(Gems[1].ToRawData());
            result.AddRange(UniqueGems[1].ToRawData());
            result.AddRange(Gems[2].ToRawData());
            result.AddRange(UniqueGems[2].ToRawData());

            return result.ToArray();
        }

        public new bool IsEmpty()
        {
            return
                base.IsEmpty()          &&
                Weight   == 0           &&
                GemSlots == 0           &&
                Gems[0].IsEmpty()       &&
                UniqueGems[0].IsEmpty() &&
                Gems[1].IsEmpty()       &&
                UniqueGems[1].IsEmpty() &&
                Gems[2].IsEmpty()       &&
                UniqueGems[2].IsEmpty();
        }
    }

    /*
    *   CrystalItem Structure
    *   A Crystal or Gem in player's inventory
    *
    *   Offset  Type    Name            Description
    *   0x00    Item                    = (Item base, see Item Structure)
    *   0x14    uint16  CrystalNameID   = ID of Crystal's Name (Ex. "White Crystal", "Bunnit Crystal", "Volff Crystal")
    *   0x16    uint8   Rank            = Crystal/Gem Rank, 1~5 for Crystals, 1~6 for Gems
    *   0x17    uint8   Element         = Element of Crystal/Gem, 0~3:White (unused?), 4:Fire, 5:Water, 6:Electric, 7:Ice, 8:Wind, 9:Earth, 10:White/Mixed? (monster drop)
    *   0x18    uint8   Unk_3           = ???   [UNKNOWN, PLEASE INVESTIGATE]
    *   0x19    uint8   IsCylinder      = Flag determning whether or not the Crystal is actually a Cylinder
    *   0x1A    uint16  BuffCount       = No. of Buffs the Crystal contains (Max 4)
    *   0x1C    uint16  Buff1ID         = ID of the Crystal/Gem's 1st Buff
    *   0x1E    uint16  Buff1Value      = Amount Value of the Crystal/Gem's 1st Buff (whether it is absolute or percent amount depends on the Buff)
    *   0x20    uint16  Buff2ID         = ID of the Crystal's 2nd Buff [Ignored by Gems]
    *   0x22    uint16  Buff2Value      = Amount Value of the Crystal's 2nd Buff [Ignored by Gems]
    *   0x24    uint16  Buff3ID         = ID of the Crystal's 3rd Buff [Ignored by Gems]
    *   0x26    uint16  Buff3Value      = Amount Value of the Crystal's 3rd Buff [Ignored by Gems]
    *   0x28    uint16  Buff4ID         = ID of the Crystal's 4th Buff [Ignored by Gems]
    *   0x2A    uint16  Buff4Value      = Amount Value of the Crystal's 4th Buff [Ignored by Gems]
    */
    public class CrystalItem : Item, ISaveObject
    {
        public new const int SIZE = 0x2C;

        public byte             Element         { get; set; }
        public byte             Rank            { get; set; }
        public ushort           CrystalNameID   { get; set; }
        public byte             Unk_3           { get; set; }
        public bool             IsCylinder      { get; set; }
        public ushort           BuffCount       { get; set; }
        public CrystalBuff[]    Buffs           { get; set; }

        public CrystalItem() : base()
        {
            Buffs = new CrystalBuff[4];
        }
        public CrystalItem(byte[] data) : base(data)
        {
            CrystalNameID   = BitConverter.ToUInt16(data.GetByteSubArray(0x14, 0x2), 0);
            Rank            = data[0x16];
            Element         = data[0x17];
            Unk_3           = data[0x18];
            IsCylinder      = data[0x19] == 1;
            BuffCount       = BitConverter.ToUInt16(data.GetByteSubArray(0x1A, 0x2), 0);
            Buffs           = new CrystalBuff[4];
            for (int i = 0; i < Buffs.Length; i++)  Buffs[i] = new CrystalBuff(data.GetByteSubArray(0x1C + (i * CrystalBuff.SIZE), CrystalBuff.SIZE));
        }

        public new byte[] ToRawData()
        {
            List<byte> result = new List<byte>();

            result.AddRange(base.ToRawData());
            result.AddRange(BitConverter.GetBytes(CrystalNameID));
            result.Add(Rank);
            result.Add(Element);
            result.Add(Unk_3);
            result.Add(IsCylinder ? (byte) 0x01 : (byte) 0x00);
            result.AddRange(BitConverter.GetBytes(BuffCount));
            foreach (CrystalBuff b in Buffs)    result.AddRange(b.ToRawData());

            return result.ToArray();
        }

        public new bool IsEmpty()
        {
            return
                base.IsEmpty()     &&
                FullID.TypeID == 0 &&
                CrystalNameID == 0 &&
                Rank          == 0 &&
                Element       == 0 &&
                Unk_3         == 0 &&
                !IsCylinder        &&
                BuffCount     == 0 &&
                Buffs[0].IsEmpty() &&
                Buffs[1].IsEmpty() &&
                Buffs[2].IsEmpty() &&
                Buffs[3].IsEmpty();
        }
    }
}