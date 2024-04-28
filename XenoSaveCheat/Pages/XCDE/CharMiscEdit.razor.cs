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
using Microsoft.AspNetCore.Components;

namespace XenoSaveCheat.Pages.XCDE;

public partial class CharMiscEdit
{
    private uint _level;
    private uint _exp;
    private uint _ap;
    private uint _affinityCoins;
    private uint _expertModeLevel;
    private uint _expertModeExp;
    private uint _expertModeReserveExp;

    [Parameter]
    public uint Level
    {
        get => _level;
        set
        {
            if (_level != value)
            {
                _level = value;
                LevelChanged.InvokeAsync(value);
            }
        }
    }
    [Parameter]
    public uint EXP
    {
        get => _exp;
        set
        {
            if (_exp != value)
            {
                _exp = value;
                EXPChanged.InvokeAsync(value);
            }
        }
    }
    [Parameter]
    public uint AP
    {
        get => _ap;
        set
        {
            if (_ap != value)
            {
                _ap = value;
                APChanged.InvokeAsync(value);
            }
        }
    }
    [Parameter]
    public uint AffinityCoins
    {
        get => _affinityCoins;
        set
        {
            if (_affinityCoins != value)
            {
                _affinityCoins = value;
                AffinityCoinsChanged.InvokeAsync(value);
            }
        }
    }
    [Parameter]
    public uint ExpertModeLevel
    {
        get => _expertModeLevel;
        set
        {
            if (_expertModeLevel != value)
            {
                _expertModeLevel = value;
                ExpertModeLevelChanged.InvokeAsync(value);
            }
        }
    }
    [Parameter]
    public uint ExpertModeEXP
    {
        get => _expertModeExp;
        set
        {
            if (_expertModeExp != value)
            {
                _expertModeExp = value;
                ExpertModeEXPChanged.InvokeAsync(value);
            }
        }
    }
    [Parameter]
    public uint ExpertModeReserveEXP
    {
        get => _expertModeReserveExp;
        set
        {
            if (_expertModeReserveExp != value)
            {
                _expertModeReserveExp = value;
                ExpertModeReserveEXPChanged.InvokeAsync(value);
            }
        }
    }

    [Parameter]
    public EventCallback<uint> LevelChanged { get; set; }
    [Parameter]
    public EventCallback<uint> EXPChanged { get; set; }
    [Parameter]
    public EventCallback<uint> APChanged { get; set; }
    [Parameter]
    public EventCallback<uint> AffinityCoinsChanged { get; set; }
    [Parameter]
    public EventCallback<uint> ExpertModeLevelChanged { get; set; }
    [Parameter]
    public EventCallback<uint> ExpertModeEXPChanged { get; set; }
    [Parameter]
    public EventCallback<uint> ExpertModeReserveEXPChanged { get; set; }
}