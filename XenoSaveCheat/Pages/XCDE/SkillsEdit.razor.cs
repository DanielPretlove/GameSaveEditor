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

public partial class SkillsEdit
{
    [Inject]
    public DataLookupService DLS { get; set; }= null!;

    private uint _selectedSkillTreeIndex;

    [Parameter]
    public Character CurrentChar             { get; set; } = Character.None;    
    [Parameter]
    public uint      SelectedSkillTreeIndex
    {
        get =>  _selectedSkillTreeIndex;
        set
        {
            if (_selectedSkillTreeIndex != value)
            {
                _selectedSkillTreeIndex = value;
                SelectedSkillTreeIndexChanged.InvokeAsync(value);
            }
        }
    }
    [Parameter]
    public uint[]    SkillTreeSPs            { get; set; } = new uint[5];
    [Parameter]
    public uint[]    SkillTreeUnlockedSkills { get; set; } = new uint[5];
    [Parameter]
    public bool      SkillTree4Unlocked      { get; set; } = false;
    [Parameter]
    public bool      SkillTree5Unlocked      { get; set; } = false;
    [Parameter]
    public byte[]    SkillLinkIDs            { get; set; } = new byte[35];

    [Parameter]
    public EventCallback<uint>   SelectedSkillTreeIndexChanged  { get; set; }
    [Parameter]
    public EventCallback<uint[]> SkillTreeSPsChanged            { get; set; }
    [Parameter]
    public EventCallback<uint[]> SkillTreeUnlockedSkillsChanged { get; set; }
    [Parameter]
    public EventCallback<bool>   SkillTree4UnlockedChanged      { get; set; }
    [Parameter]
    public EventCallback<bool>   SkillTree5UnlockedChanged      { get; set; }
    [Parameter]
    public EventCallback<byte[]> SkillLinkIDsChanged            { get; set; }

    protected new void OnParametersSet()
    {
        SkillTreeSPs = new uint[5];
        SkillTreeUnlockedSkills = new uint[5];
        SkillLinkIDs = new byte[35];
    }

    protected Task OnSkillTreeSPsChanged(ChangeEventArgs e)
    {
        if (e.Value != null)
        {
            SkillTreeSPs = (uint[])e.Value;
            return SkillTreeSPsChanged.InvokeAsync(SkillTreeSPs);
        }
        else
            throw new ArgumentNullException("Changed value is not available.");
    }

    protected Task OnSkillTreeUnlockedSkillsChanged(ChangeEventArgs e)
    {
        if (e.Value != null)
        {
            SkillTreeUnlockedSkills = (uint[])e.Value;
            return SkillTreeUnlockedSkillsChanged.InvokeAsync(SkillTreeUnlockedSkills);
        }
        else
            throw new ArgumentNullException("Changed value is not available.");
    }

    protected Task OnSkillTree4UnlockedChanged(bool value)
    {
        SkillTree4Unlocked = value;
        return SkillTree4UnlockedChanged.InvokeAsync(SkillTree4Unlocked);
    }

    protected Task OnSkillTree5UnlockedChanged(bool value)
    {
        SkillTree5Unlocked = value;
        return SkillTree5UnlockedChanged.InvokeAsync(SkillTree5Unlocked);
    }

    protected Task OnSkillLinkIDsChanged(ChangeEventArgs e)
    {
        if (e.Value != null)
        {
            SkillLinkIDs = (byte[])e.Value;
            return SkillLinkIDsChanged.InvokeAsync(SkillLinkIDs);
        }
        else
            throw new ArgumentNullException("Changed value is not available.");
    }
}