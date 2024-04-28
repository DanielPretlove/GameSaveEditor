/*
 * XenoSaveCheat
 * Copyright (C) 2023  damysteryman
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
using dmm.XenoSave.XCDE;

namespace XenoSaveCheat.Pages.XCDE;

public partial class TimeAttack
{
    [Inject]
    public dmm.XenoSaveCheatCore.Core Core { get; set; } = null!;
    [Inject]
    public DataLookupService DLS { get; set; } = null!;

    private string _file = string.Empty;
    private int SelectedType = 0;
    private dmm.XenoSave.XCDE.XCDESave Save { get; set; } = null!;

    [Parameter]
    public string File { get => _file; set => _file = value.Replace('-', '.'); }

    protected override void OnParametersSet() => Save = (dmm.XenoSave.XCDE.XCDESave)Core.SAVEDATA[File];

    private int CalculateNSReward(int[] _base, TimeAttackScore _score)
    {
        int result     = 0;
        int baseReward = _score.NSRewardRank == TimeAttackScore.Rank.None ? 0 : _base[(int)(_score.NSRewardRank) - 1];

        switch(_score.NSRewardRank)
        {
            case TimeAttackScore.Rank.S:
                result = baseReward
                       + (_score.BonusNSRewards[0] ? baseReward / 2 : 0)
                       + (_score.BonusNSRewards[1] ? baseReward : 0);
                break;
            case TimeAttackScore.Rank.A:
            case TimeAttackScore.Rank.B:
                result = baseReward
                       + (_score.BonusNSRewards[0] ? baseReward / 2 : 0)
                       + (_score.BonusNSRewards[1] ? baseReward * 2 : 0);
                break;
            case TimeAttackScore.Rank.C:
                result = baseReward
                       + (_score.BonusNSRewards[0] ? baseReward : 0)
                       + (_score.BonusNSRewards[1] ? baseReward * 2 : 0);
                break;
        }

        return result;
    }

    private string GetBonus1PercentDisplay(TimeAttackScore.Rank _rank)
    {
        int result = 0;
        switch(_rank)
        {
            case TimeAttackScore.Rank.S:
            case TimeAttackScore.Rank.A:
            case TimeAttackScore.Rank.B:
                result = 50;
                break;
            case TimeAttackScore.Rank.C:
                result = 100;
                break;
        }
        return $"+{result}%";
    }

    private string GetBonus2PercentDisplay(TimeAttackScore.Rank _rank)
    {
        int result = 0;
        switch(_rank)
        {
            case TimeAttackScore.Rank.S:
                result = 100;
                break;
            case TimeAttackScore.Rank.A:
            case TimeAttackScore.Rank.B:
            case TimeAttackScore.Rank.C:
                result = 200;
                break;
        }
        return $"+{result}%";
    }
}