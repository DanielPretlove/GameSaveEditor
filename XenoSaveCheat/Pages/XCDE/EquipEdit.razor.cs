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
using XenoSaveCheat.Services;

namespace XenoSaveCheat.Pages.XCDE;

public partial class EquipEdit
{
    [Inject]
    public DataLookupService DLS { get; set; } = null!;
    
    [Parameter]
    public ItemID[]             Equipment         { get; set; } = new ItemID[6];
    [Parameter]
    public uint[]               Cosmetics         { get; set; } = new uint[6];
    [Parameter]
    public Character            CurrentChar       { get; set; } = Character.None;
    [Parameter]
    public NonPartyMemberItem[] NonPartyEquipment { get; set; } = new NonPartyMemberItem[6];

    [Parameter]
    public EventCallback<ItemID[]>             EquipmentChanged         { get; set; }
    [Parameter]
    public EventCallback<uint[]>               CosmeticsChanged         { get; set; }
    [Parameter]
    public EventCallback<NonPartyMemberItem[]> NonpartyEquipmentChanged { get; set; }

    protected new void OnParametersSet()
    {
        Equipment = new ItemID[6];
        for (int i = 0; i < Equipment.Length; i++)
            Equipment[i] = new();
        Cosmetics = new uint[6];
    }

    protected Task OnEquipmentChanged(ChangeEventArgs e)
    {
        if (e.Value != null)
        {
            Equipment = (ItemID[])e.Value;
            return EquipmentChanged.InvokeAsync(Equipment);
        }
        else
            throw new ArgumentNullException("Changed value is not available.");
        
    }

    protected Task OnCosmeticsChanged(ChangeEventArgs e)
    {
        if (e.Value != null)
        {
            Cosmetics = (uint[])e.Value;
            return  CosmeticsChanged.InvokeAsync(Cosmetics);
        }
        else
            throw new ArgumentNullException("Changed value is not available.");
    }
}