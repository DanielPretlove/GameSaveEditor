/*
 * XenoSaveCheat
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

using dmm.XenoSave.XCDE;

namespace XenoSaveCheat.Dto.XCDE;

public class InventoryDto
{
    public List<EquipItem> Weapons { get; set; }
    public List<EquipItem> HeadArmour { get; set; }
    public List<EquipItem> TorsoArmour { get; set; }
    public List<EquipItem> ArmArmour { get; set; }
    public List<EquipItem> LegArmour { get; set; }
    public List<EquipItem> FootArmour { get; set; }
    public List<CrystalItem> Crystals { get; set; }
    public List<CrystalItem> Gems { get; set; }
    public List<Item> Collectables { get; set; }
    public List<Item> Materials { get; set; }
    public List<Item> KeyItems { get; set; }
    public List<Item> ArtsManuals { get; set; }

    public InventoryDto()
    {
        Weapons = new();
        HeadArmour = new();
        TorsoArmour = new();
        ArmArmour = new();
        LegArmour = new();
        FootArmour = new();
        Crystals = new();
        Gems = new();
        Collectables = new();
        Materials = new();
        KeyItems = new();
        ArtsManuals = new();
    }
}