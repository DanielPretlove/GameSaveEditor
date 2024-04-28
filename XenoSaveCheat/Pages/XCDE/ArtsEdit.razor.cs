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

namespace XenoSaveCheat.Pages.XCDE;

public partial class ArtsEdit
{
    [Inject]
    public DataLookupService DLS { get; set; } = null!;
    
    [Parameter]
    public ushort[] Arts { get; set; } = new ushort[9];

    [Parameter]
    public ushort[] MonadoArts { get; set; } = new ushort[9];

    [Parameter]
    public EventCallback<ushort[]> ArtsChanged { get; set; }

    [Parameter]
    public EventCallback<ushort[]> MonadoArtsChanged { get; set; }

    protected new void OnParametersSet()
    {
        Arts = new ushort[9];
        MonadoArts = new ushort[9];
    }

    private Task OnArtsChanged(ChangeEventArgs e)
    {
        if (e.Value != null)
        {
            Arts = (ushort[])e.Value;
            return ArtsChanged.InvokeAsync(Arts);
        }
        else
            throw new ArgumentNullException("Changed value is not available.");

    }

    private Task OnMonadoArtsChanged(ChangeEventArgs e)
    {
        if (e.Value != null)
        {
            MonadoArts = (ushort[])e.Value;
            return MonadoArtsChanged.InvokeAsync(MonadoArts);
        }
        else
            throw new ArgumentNullException("Changed value is not available.");
        
    }
}