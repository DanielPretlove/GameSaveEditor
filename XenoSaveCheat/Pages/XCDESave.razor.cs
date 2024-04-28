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
using Microsoft.AspNetCore.Components;

namespace XenoSaveCheat.Pages;

public partial class XCDESave
{
    [Inject]
    public dmm.XenoSaveCheatCore.Core Core { get; set; } = null!;
    [Inject]
    public Services.DataLookupService DLS  { get; set; } = null!;

    private string _file = string.Empty;

    [Parameter]
    public string File { get => _file; set => _file = value.Replace('-', '.'); }

    protected override void OnInitialized()
    {
        dmm.XenoSave.XCDE.XCDESave Save = (dmm.XenoSave.XCDE.XCDESave)(Core.SAVEDATA[File]);
        DLS.XCDERegenItemIdxLookup(Save.Weapons,     ItemType.Weapon);
        DLS.XCDERegenItemIdxLookup(Save.HeadArmour,  ItemType.HeadArmour);
        DLS.XCDERegenItemIdxLookup(Save.TorsoArmour, ItemType.TorsoArmour);
        DLS.XCDERegenItemIdxLookup(Save.ArmArmour,   ItemType.ArmArmour);
        DLS.XCDERegenItemIdxLookup(Save.LegArmour,   ItemType.LegArmour);
        DLS.XCDERegenItemIdxLookup(Save.FootArmour,  ItemType.FootArmour);
        DLS.XCDERegenItemIdxLookup(Save.Gems,        ItemType.Gem);
    }
}