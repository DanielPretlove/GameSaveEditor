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

namespace XenoSaveCheat.Pages;

public partial class XC3Save
{
    [Inject]
    public dmm.XenoSaveCheatCore.Core Core { get; set; } = null!;

    private string _file = string.Empty;

    [Parameter]
    public string File
    {
        get
        {
            return _file;
        }
        set
        {
            // For now need to replace dot with dash in save file for the loaded save file to load up in test page
            // e.g., http://localhost:5081/test/bf3game04-sav
            _file = value.Replace('-', '.');
        }
    }

    private dmm.XenoSave.XC3.XC3Save Save { get; set; } = null!;

    protected override void OnParametersSet() => Save = (dmm.XenoSave.XC3.XC3Save)Core.SAVEDATA[File];
}