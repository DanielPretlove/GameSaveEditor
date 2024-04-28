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
using XenoSaveCheat.Services.DataLookup;

namespace XenoSaveCheat.Pages.XCDE;

public partial class ItemTable
{
    private uint _lastSerial;
    [Inject]
    public Services.DataLookupService DLS {get; set; } = null!;

    [Parameter]
    public ItemType BoxItemType { get; set; }
    [Parameter]
    public Item[]? ItemBox { get; set; }
    [Parameter]
    public EquipItem[]? EquipItemBox { get; set; }
    [Parameter]
    public CrystalItem[]? CrystalItemBox { get; set; }
    [Parameter]
    public uint LastSerial
    {
        get => _lastSerial;
        set
        {
            if (_lastSerial != value)
            {
                _lastSerial = value;
                LastSerialChanged.InvokeAsync(value);
            }
        }
    }

    [Parameter]
    public EventCallback<Item[]> ItemBoxChanged { get; set; }
    [Parameter]
    public EventCallback<EquipItem[]> EquipItemBoxChanged { get; set; }
    [Parameter]
    public EventCallback<CrystalItem[]> CrystalItemBoxChanged { get; set; }
    [Parameter]
    public EventCallback<uint> LastSerialChanged { get; set; }

    private string filterText = "";

    protected Task OnItemBoxChanged(ChangeEventArgs e)
    {
        if (e.Value != null)
        {
            ItemBox = (Item[])e.Value;
            return ItemBoxChanged.InvokeAsync(ItemBox);
        }
        else
            throw new ArgumentNullException("Changed value is not available.");
    }

    protected Task OnEquipItemBoxChanged(ChangeEventArgs e)
    {
        if (e.Value != null)
        {
            EquipItemBox = (EquipItem[])e.Value;
            return EquipItemBoxChanged.InvokeAsync(EquipItemBox);
        }
        else
            throw new ArgumentNullException("Changed value is not available.");
    }

    protected Task OnCrystalItemBoxChanged(ChangeEventArgs e)
    {
        if (e.Value != null)
        {
            CrystalItemBox = (CrystalItem[])e.Value;
            return ItemBoxChanged.InvokeAsync(CrystalItemBox);
        }
        else
            throw new ArgumentNullException("Changed value is not available.");
    }

    private void UpdateItemID(ushort _newId, ItemID _itemID, ItemType _itemType, bool _isCylinder = false)
    {
        _itemID.ID = _newId;
        _itemID.TypeID = _itemID.IsEmpty() && !(_itemID.TypeID == ItemType.Crystal && _isCylinder)? ItemType.None : _itemType;
    }

    private void UpdateItem(ushort _newId, Item _item)
    {
        bool isCylinder = false;
        if (_item.FullID.TypeID == ItemType.Crystal)
            isCylinder = ((CrystalItem)_item).IsCylinder;

        _item.FullID.ID = _newId;
        _item.FullID.TypeID = _item.FullID.IsEmpty() && !(_item.FullID.TypeID == ItemType.Crystal && isCylinder) ? ItemType.None : _item.FullID.TypeID;

        if (_item.FullID.TypeID == ItemType.Weapon      ||
            _item.FullID.TypeID == ItemType.HeadArmour  ||
            _item.FullID.TypeID == ItemType.TorsoArmour ||
            _item.FullID.TypeID == ItemType.ArmArmour   ||
            _item.FullID.TypeID == ItemType.LegArmour   ||
            _item.FullID.TypeID == ItemType.FootArmour  ||
            _item.FullID.TypeID == ItemType.Gem)
            DLS.XCDEUpdateSingleItemIdxLookup(_item, _item.FullID.TypeID);
    }

    private bool ItemFilter(Item item)
    {
        bool     finalResult = true;
        string[] searchTerms = filterText.Split(' ');
        bool[]   results     = new bool[searchTerms.Length];

        // Using Linq FirstOrDefault here will cause mad slowdown
        // IDs will match index in imported json & array anyway
        // If they do not then you have bigger problems
        string   itemName    = DLS.XCDEItems?[item.FullID.ID].Name ?? "";

        for (int i = 0; i < searchTerms.Length; i++)
        {
            results[i] = 
                item.Index.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)     ||
                item.FullID.ID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase) ||
                itemName.Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)                  ||
                item.Quantity.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)  ||
                item.SerialNo.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)  ||
                (searchTerms[i].Contains("Exist", StringComparison.OrdinalIgnoreCase) && item.Exists)  ||
                (searchTerms[i].Contains("Fav", StringComparison.OrdinalIgnoreCase) && item.Favourite) ||
                item.Unk_1.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)     ||
                item.Unk_2.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase);
        }

        foreach (bool b in results)
            finalResult &= b;

        return finalResult;
    }

    private bool ItemFilter(EquipItem item)
    {
        bool     finalResult = true;
        string[] searchTerms = filterText.Split(' ');
        bool[]   results     = new bool[searchTerms.Length];

        // Using Linq FirstOrDefault here will cause mad slowdown
        // IDs will match index in imported json & array anyway
        // If they do not then you have bigger problems
        string   itemName    = DLS.XCDEItems?[item.FullID.ID].Name ?? "";

        for (int i = 0; i < searchTerms.Length; i++)
        {
            results[i] = 
                item.Index.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)            ||
                item.FullID.ID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)        ||
                itemName.Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)                         ||
                item.Quantity.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)         ||
                item.SerialNo.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)         ||
                (searchTerms[i].Contains("Exist", StringComparison.OrdinalIgnoreCase) && item.Exists)         ||
                (searchTerms[i].Contains("Fav", StringComparison.OrdinalIgnoreCase) && item.Favourite)        ||
                item.Weight.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)           ||
                item.GemSlots.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)         ||
                item.Gems[0].ID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)       ||
                item.Gems[1].ID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)       ||
                item.Gems[2].ID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)       ||
                item.UniqueGems[0].ID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase) ||
                item.UniqueGems[1].ID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase) ||
                item.UniqueGems[2].ID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase) ||
                item.Unk_1.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)            ||
                item.Unk_2.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)            ||
                item.Unk_3.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase);
        }

        foreach (bool b in results)
            finalResult &= b;

        return finalResult;
    }

    private bool ItemFilter(CrystalItem item)
    {
        bool     finalResult = true;
        string[] searchTerms = filterText.Split(' ');
        bool[]   results     = new bool[searchTerms.Length];

        // Using Linq FirstOrDefault here will casue mad slowdown
        // IDs will match index in imported json & array anyway
        // If they do not then you have bigger problems
        string   itemName    = DLS.XCDEItems?[item.FullID.ID].Name ?? "";
        string   rankName    = DLS.XCDECrystalRanks?[item.Rank].Name ?? "";
        string   elementName = DLS.XCDECrystalElements?[item.Element].Name ?? "";

        for (int i = 0; i < searchTerms.Length; i++)
        {
            results[i] = 
                item.Index.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)                ||
                item.FullID.ID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)            ||
                itemName.Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)                             ||
                item.CrystalNameID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)        ||
                item.Quantity.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)             ||
                item.SerialNo.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)             ||
                (searchTerms[i].Contains("Exist", StringComparison.OrdinalIgnoreCase) && item.Exists)             ||
                (searchTerms[i].Contains("Fav", StringComparison.OrdinalIgnoreCase) && item.Favourite)            ||
                item.Rank.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)                 ||
                rankName.Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)                             ||
                item.Element.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)              ||
                elementName.Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)                          ||
                (searchTerms[i].Contains("Cylinder", StringComparison.OrdinalIgnoreCase) && item.IsCylinder)      ||
                item.BuffCount.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)            ||
                item.Buffs[0].BuffID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)      ||
                item.Buffs[0].Value.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)       ||
                item.Buffs[0].Probability.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase) ||
                item.Buffs[1].BuffID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)      ||
                item.Buffs[1].Value.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)       ||
                item.Buffs[1].Probability.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase) ||
                item.Buffs[2].BuffID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)      ||
                item.Buffs[2].Value.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)       ||
                item.Buffs[2].Probability.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase) ||
                item.Buffs[3].BuffID.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)      ||
                item.Buffs[3].Value.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)       ||
                item.Buffs[3].Probability.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase) ||
                item.Unk_1.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)                ||
                item.Unk_2.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase)                ||
                item.Unk_3.ToString().Contains(searchTerms[i], StringComparison.OrdinalIgnoreCase);
        }

        foreach (bool b in results)
            finalResult &= b;

        return finalResult;
    }
}