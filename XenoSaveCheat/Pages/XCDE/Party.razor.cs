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

public partial class Party
{
    [Inject]
    public dmm.XenoSaveCheatCore.Core Core { get; set; } = null!;

    private string _file = string.Empty;

    [Parameter]
    public string File { get => _file; set => _file = value.Replace('-', '.'); }

    private dmm.XenoSave.XCDE.XCDESave Save { get; set; } = null!;

    protected override void OnParametersSet() => Save = (dmm.XenoSave.XCDE.XCDESave)Core.SAVEDATA[File];
}