/*
 * XenoSave
 * Copyright (C) 2022-2023  damysteryman
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

using System.Collections;
using System.Linq;

namespace dmm.XenoSave.XCDE;

public class EquipCosmetic
{

    public bool Unlocked { get; set; }
    public bool IsNew { get; set; }

    public EquipCosmetic(bool _unlocked, bool _isnew)
    {
        Unlocked = _unlocked;
        IsNew = _isnew;
    }
}

public class CosmeticSet
{
    public const int WEAPON_COUNT = 30;
    public const int ARMOUR_COUNT = 60;
    public const int BITS_COUNT   = 660;

    public EquipCosmetic[] WeaponCosmetics  { get; set; } = new EquipCosmetic[WEAPON_COUNT];
    public EquipCosmetic[] HeadCosmetics    { get; set; } = new EquipCosmetic[ARMOUR_COUNT];
    public EquipCosmetic[] TorsoCosmetics   { get; set; } = new EquipCosmetic[ARMOUR_COUNT];
    public EquipCosmetic[] ArmCosmetics     { get; set; } = new EquipCosmetic[ARMOUR_COUNT];
    public EquipCosmetic[] LegCosmetics     { get; set; } = new EquipCosmetic[ARMOUR_COUNT];
    public EquipCosmetic[] FootCosmetics    { get; set; } = new EquipCosmetic[ARMOUR_COUNT];
}

public class EquipCosmeticsFlagSet : ISaveObject
{
    public const int SIZE = 0x38C;
    public const int CHARACTER_COUNT = 11;
    public CosmeticSet[] CharacterCosmetics { get; set; } = new CosmeticSet[CHARACTER_COUNT];

    public EquipCosmeticsFlagSet(byte[] data)
    {
        BitArray _flags = new BitArray(data);

        for (int i = 0; i < CHARACTER_COUNT; i++)
        {
            CharacterCosmetics[i] = new CosmeticSet();

            for (int j = 0; j < CosmeticSet.WEAPON_COUNT; j++)
            {
                int baseLoc = (i * CosmeticSet.BITS_COUNT) + (j * 2);
                CharacterCosmetics[i].WeaponCosmetics[j] = new EquipCosmetic(_flags[baseLoc], !_flags[baseLoc + 1]);
            }

            for (int j = 0; j < CosmeticSet.ARMOUR_COUNT; j++)
                for (int k = 0; k < 5; k++)
                {
                    int baseLoc = (i * CosmeticSet.BITS_COUNT) + ((CosmeticSet.WEAPON_COUNT + (CosmeticSet.ARMOUR_COUNT * k) + j) * 2);
                    switch(k)
                    {
                        case 0:
                            CharacterCosmetics[i].HeadCosmetics[j]  = new EquipCosmetic(_flags[baseLoc], !_flags[baseLoc + 1]);
                            break;
                        case 1:
                            CharacterCosmetics[i].TorsoCosmetics[j] = new EquipCosmetic(_flags[baseLoc], !_flags[baseLoc + 1]);
                            break;
                        case 2:
                            CharacterCosmetics[i].ArmCosmetics[j]   = new EquipCosmetic(_flags[baseLoc], !_flags[baseLoc + 1]);
                            break;
                        case 3:
                            CharacterCosmetics[i].LegCosmetics[j]   = new EquipCosmetic(_flags[baseLoc], !_flags[baseLoc + 1]);
                            break;
                        case 4:
                            CharacterCosmetics[i].FootCosmetics[j]  = new EquipCosmetic(_flags[baseLoc], !_flags[baseLoc + 1]);
                            break;
                    }
                }
        }  
    }

    public byte[] ToRawData()
    {
        BitArray _flags =  new BitArray(CHARACTER_COUNT * CosmeticSet.BITS_COUNT);
        
        for (int i = 0; i <  CHARACTER_COUNT; i++)
        {
            for (int j = 0; j < CosmeticSet.WEAPON_COUNT; j++)
            {
                int baseLoc         = (i * CosmeticSet.BITS_COUNT) + (j * 2);
                _flags[baseLoc]     = CharacterCosmetics[i].WeaponCosmetics[j].Unlocked;
                _flags[baseLoc + 1] = !CharacterCosmetics[i].WeaponCosmetics[j].IsNew;
            }
                
            for (int j = 0; j < CosmeticSet.ARMOUR_COUNT; j++)
                for (int k = 0; k < 5; k++)
                {
                    int baseLoc = (i * CosmeticSet.BITS_COUNT) + ((CosmeticSet.WEAPON_COUNT + (CosmeticSet.ARMOUR_COUNT * k) + j) * 2);
                    switch(k)
                    {
                        case 0:
                            _flags[baseLoc]     = CharacterCosmetics[i].HeadCosmetics[j].Unlocked;
                            _flags[baseLoc + 1] = !CharacterCosmetics[i].HeadCosmetics[j].IsNew;
                            break;
                        case 1:
                            _flags[baseLoc]     = CharacterCosmetics[i].TorsoCosmetics[j].Unlocked;
                            _flags[baseLoc + 1] = !CharacterCosmetics[i].TorsoCosmetics[j].IsNew;
                            break;
                        case 2:
                            _flags[baseLoc]     = CharacterCosmetics[i].ArmCosmetics[j].Unlocked;
                            _flags[baseLoc + 1] = !CharacterCosmetics[i].ArmCosmetics[j].IsNew;
                            break;
                        case 3:
                            _flags[baseLoc]     = CharacterCosmetics[i].LegCosmetics[j].Unlocked;
                            _flags[baseLoc + 1] = !CharacterCosmetics[i].LegCosmetics[j].IsNew;
                            break;
                        case 4:
                            _flags[baseLoc]     = CharacterCosmetics[i].FootCosmetics[j].Unlocked;
                            _flags[baseLoc + 1] = !CharacterCosmetics[i].FootCosmetics[j].IsNew;
                            break;
                    }
                }
        }

        byte[] result = new byte[SIZE];
        _flags.CopyTo(result, 0);

        return result;
    }
}