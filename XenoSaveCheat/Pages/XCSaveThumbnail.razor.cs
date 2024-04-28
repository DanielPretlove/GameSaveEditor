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
using XenoSaveCheat.Services;

namespace XenoSaveCheat.Pages;

public partial class XCSaveThumbnail
{
    [Inject]
    public dmm.XenoSaveCheatCore.Core Core { get; set; } = null!;

    private string _file = string.Empty;
    private string thumbBase64 = string.Empty;

    [Parameter]
    public string File { get => _file; set => _file = value.Replace('-', '.'); }
    [Parameter]
    public int Width { get; set; } = dmm.XenoSave.XCSaveThumbnail.WIDTH;
    [Parameter]
    public int Height { get; set; }= dmm.XenoSave.XCSaveThumbnail.HEIGHT;

    protected override void OnParametersSet()
    {
        thumbBase64 = (Core.SAVEDATA.ContainsKey(File)) ? string.Format("data:image/bmp;base64,{0}", Convert.ToBase64String(((dmm.XenoSave.XCSaveThumbnail)Core.SAVEDATA[File]).BMP)) : "";
    }
}