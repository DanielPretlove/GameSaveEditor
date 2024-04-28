/*
 * XenoSave
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

namespace dmm.XenoSave.XCDE;

public enum DisplaySetting : ushort
{
    Display,
    Hide
}

public enum MiniMapSetting : ushort
{
    Hide,
    Fixed,
    Rotate
}

public enum GameSetting : ushort
{
    On,
    Off
}

public enum BuffDebuffInfoSetting : ushort
{
    EveryTime,
    FirstTime
}

public enum OtherNotificatiionSetting : ushort
{
    Notify,
    DontNotify,
    Custom
}

public enum NotificationSetting : ushort
{
    Notify,
    DontNotify
}

public enum CameraInvertSetting : ushort
{
    Normal,
    Inverted
}

public enum AudioLanguage : ushort
{
    Japanese,
    English
}

public enum BGMSetting : ushort
{
    Arranged,
    Original
}

public enum MessageScrollSetting : ushort
{
    Automatic,
    Manual
}

public enum TextSpeedSetting : ushort
{
    Normal,
    Fast
}

public class Setting<T> : ISaveObject
{
    public const int SIZE = 2;
    public bool Modified { get; set; }
    public T? Property { get; set; }

    public void Create(ushort data)
    {
        Modified = data >> 15 == 1;
        Property = (T)(object)(ushort)(data & ~0x8000);
    }

    public void Create(byte[] data)
    {
        this.Create(BitConverter.ToUInt16(data));
    }

    public byte[] ToRawData()
    {
        return BitConverter.GetBytes((ushort)(Convert.ToUInt16(Property) | (ushort)((Modified ? 1 : 0) << 15)));
    }
}

/*
*   XCDESetting Structure
*   Settings file for Xenoblade Chronicles: Definitive Edition [WORK IN PROGRESS, PLEASE RESEARCH]
*
*   Offset  Type                    Name                        Description
*   0x000   uint8[0x8]              Unk_0x000                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
*   0x008   byte                    TitleBackground             = ??? A value > 0 causes Title Screen to use Post-game background...
*   0x009   uint8[0x3D]             Unk_0x009                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
*   0x046   Setting                 Controls                    = Display Settings -> Controls
*   0x048   Setting                 MiniMapDisplay              = Display Settings -> Mini-map Display
*   0x04A   Setting                 Unk_0x04A                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
*   0x04C   Setting                 ArtDescriptions             = Display Settings -> Art Descriptions
*   0x04E   Setting                 EnemyIcons                  = Display Settings -> Enemy Icons
*   0x050   Setting                 TravelGuidance              = Display Settings -> Travel Guidance
*   0x052   Setting                 ChanceArts                  = Display Settings -> Chance Arts
*   0x054   Setting                 Vibration                   = Game Settings ->  Vibration
*   0x056   Setting                 Unk_0x056                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
*   0x058   Setting                 Brightness                  = Brightness
*   0x05A   Setting                 UpdateNotificationIcons     = Notification Settings -> Update Notification Icons
*   0x05C   Setting                 BuffDebuffInfo              = Notification Settings -> Buff/Debuff Info
*   0x05E   Setting                 OtherNotifications          = Notification Settings -> Other Notifications
*   0x060   Setting                 BuffDebuffEffects           = Notification Settings -> Other Notifications -> Buff/Debuff Effects
*   0x062   Setting                 Locations                   = Notification Settings -> Other Notifications -> Locations
*   0x064   Setting                 Items                       = Notification Settings -> Other Notifications -> Items
*   0x066   Setting                 Achievements                = Notification Settings -> Other Notifications -> Achievements
*   0x068   Setting                 Stealing                    = Notification Settings -> Other Notifications -> Stealing
*   0x06A   Setting                 Quests                      = Notification Settings -> Other Notifications -> Quests
*   0x06C   Setting                 AffinityChart               = Notification Settings -> Other Notifications -> Affinity Chart
*   0x06E   Setting                 Tutorials                   = Notification Settings -> Other Notifications -> Tutorials
*   0x070   Setting                 Affinity                    = Notification Settings -> Other Notifications -> Affinity
*   0x072   Setting                 CameraInvertY               = Camera -> Invert Y-axis
*   0x074   Setting                 CameraInvertX               = Camera -> Invert X-Axis
*   0x076   Setting                 Unk_0x076                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
*   0x078   Setting                 CameraResetSpeed            = Camera -> Camera Reset Speed
*   0x07A   Setting                 CameraPanSpeed              = Camera -> Camera Pan Speed
*   0x07C   Setting                 CameraZoomSpeed             = Camera -> Camera Zoom Speed
*   0x07E   Setting                 GradientCorrection          = Camera -> Gradient Correction
*   0x080   Setting                 LanguageSelection           = Sound -> Language Selection
*   0x082   Setting                 FieldBGM                    = Sound -> Toggle Field BGM
*   0x084   Setting                 BattleBGM                   = Sound -> Toggle Battle BGM
*   0x086   Setting                 CutsceneVoiceVolume         = Sound -> Cutscene Voice Volume
*   0x088   Setting                 GameBGMVolume               = Sound -> Game BGM Volume
*   0x08A   Setting                 GameSEVolume                = Sound -> Game SE Volume
*   0x08C   Setting                 GameVoiceVolume             = Sound -> Game Voice Volume
*   0x08E   Setting                 SystemVolume                = Sound -> System Volume
*   0x090   Setting                 EventScrolling              = Messages -> Event Scrolling
*   0x092   Setting                 Subtitles                   = Messages -> Subtitles
*   0x094   Setting                 DialogueTextSpeed           = Messages -> Dialogue Text Speed
*   0x096   uint8[0x4E]             Unk_0x096                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
*   0x0E4   uint8[0x1E]             CutsceneUnlockStory         = Event Viewer: Flasg for Unlocked Story Cutscenes
*   0x102   uint8[0x7]              Unk_0x102                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
*   0x109   uint8[0x8]              CutsceneUnlockHeartToHeart  = Event Viewer: Flasg for Unlocked Heart-to-Heart Cutscenes
*   0x111   uint8[0x4]              CutsceneUnlockQuietMoment   = Event Viewer: Flasg for Unlocked Quiet Moment Cutscenes ??? Not sure if size = 0x4 or larger ???
*   0x115   uint8[0x583]            Unk_0x115                   = ???   [UNKNOWN, PLEASE INVESTIGATE]
*   0x164   EquipCosmeticsFlagSet   UnlockedEquipmentCosmetics  = Set of flags for all Unlocked Equipment Cosmetics
*/
public class XCDESettings : ISaveFile, ISaveObject
{
    public const int SIZE = 0x698;

    #region Misc/Unknown
    public byte[]   Unk_0x000       { get; set;} = new byte[0x8];
    public byte     TitleBackground { get; set; }
    public byte[]   Unk_0x009       { get; set; } = new byte[0x3D];
    #endregion

    #region Game Config Settings
    
    #region Display Settings
    public Setting<DisplaySetting>  Controls        { get; set; } = new Setting<DisplaySetting>();
    public Setting<MiniMapSetting>  MiniMapDisplay  { get; set; } = new Setting<MiniMapSetting>();
    public Setting<ushort>          Unk_0x04A       { get; set; } = new Setting<ushort>();
    public Setting<DisplaySetting>  ArtDescriptions { get; set; } = new Setting<DisplaySetting>();
    public Setting<DisplaySetting>  EnemyIcons      { get; set; } = new Setting<DisplaySetting>();
    public Setting<DisplaySetting>  TravelGuidance  { get; set; } = new Setting<DisplaySetting>();
    public Setting<DisplaySetting>  ChanceArts      { get; set; } = new Setting<DisplaySetting>();
    #endregion

    #region Game Settings
    public Setting<GameSetting> Vibration   { get; set; } = new Setting<GameSetting>();
    public Setting<ushort>      Unk_0x056   { get; set; } = new Setting<ushort>();
    #endregion

    #region Brightness
    public Setting<ushort> Brightness { get; set; } = new Setting<ushort>();
    #endregion

    #region Notification Settings
    public Setting<DisplaySetting>              UpdateNotificationIcons { get; set; } = new Setting<DisplaySetting>();
    public Setting<BuffDebuffInfoSetting>       BuffDebuffInfo          { get; set; } = new Setting<BuffDebuffInfoSetting>();
    public Setting<OtherNotificatiionSetting>   OtherNotifications      { get; set; } = new Setting<OtherNotificatiionSetting>();
    public Setting<DisplaySetting>              BuffDebuffEffects       { get; set; } = new Setting<DisplaySetting>();
    public Setting<NotificationSetting>         Locations               { get; set; } = new Setting<NotificationSetting>();
    public Setting<NotificationSetting>         Items                   { get; set; } = new Setting<NotificationSetting>();
    public Setting<NotificationSetting>         Achievements            { get; set; } = new Setting<NotificationSetting>();
    public Setting<NotificationSetting>         Stealing                { get; set; } = new Setting<NotificationSetting>();
    public Setting<NotificationSetting>         Quests                  { get; set; } = new Setting<NotificationSetting>();
    public Setting<NotificationSetting>         AffinityChart           { get; set; } = new Setting<NotificationSetting>();
    public Setting<NotificationSetting>         Tutorials               { get; set; } = new Setting<NotificationSetting>();
    public Setting<NotificationSetting>         Affinity                { get; set; } = new Setting<NotificationSetting>();
    #endregion

    #region Camera
    public Setting<CameraInvertSetting> CameraInvertY       { get; set; } = new Setting<CameraInvertSetting>();
    public Setting<CameraInvertSetting> CameraInvertX       { get; set; } = new Setting<CameraInvertSetting>();
    public Setting<ushort>              Unk_0x076           { get; set; } = new Setting<ushort>();
    public Setting<ushort>              CameraResetSpeed    { get; set; } = new Setting<ushort>();
    public Setting<ushort>              CameraPanSpeed      { get; set; } = new Setting<ushort>();
    public Setting<ushort>              CameraZoomSpeed     { get; set; } = new Setting<ushort>();
    public Setting<GameSetting>         GradientCorrection  { get; set; } = new Setting<GameSetting>();
    #endregion
    
    #region Sound
    public Setting<AudioLanguage>   LanguageSelection   { get; set; } = new Setting<AudioLanguage>();
    public Setting<BGMSetting>      FieldBGM            { get; set; } = new Setting<BGMSetting>();
    public Setting<BGMSetting>      BattleBGM           { get; set; } = new Setting<BGMSetting>();
    public Setting<ushort>          CutsceneVoiceVolume { get; set; } = new Setting<ushort>();
    public Setting<ushort>          GameBGMVolume       { get; set; } = new Setting<ushort>();
    public Setting<ushort>          GameSEVolume        { get; set; } = new Setting<ushort>();
    public Setting<ushort>          GameVoiceVolume     { get; set; } = new Setting<ushort>();
    public Setting<ushort>          SystemVolume        { get; set; } = new Setting<ushort>();
    #endregion

    #region Messages
    public Setting<MessageScrollSetting>    EventScrolling      { get; set; } = new Setting<MessageScrollSetting>();
    public Setting<GameSetting>             Subtitles           { get; set; } = new Setting<GameSetting>();
    public Setting<TextSpeedSetting>        DialogueTextSpeed   { get; set; } = new Setting<TextSpeedSetting>();
    #endregion

    #endregion

    #region Event Viewer/Unknown
    byte[] Unk_0x096                    { get; set; } = new byte[0x4E];
    byte[] CutsceneUnlockStory          { get; set; } = new byte[0x1E];
    byte[] Unk_0x102                    { get; set; } = new byte[0x7];
    byte[] CutsceneUnlockHeartToHeart   { get; set; } = new byte[0x8];
    byte[] CutsceneUnlockQuietMoment    { get; set; } = new byte[0x4];
    byte[] Unk_0x115                    { get; set; } = new byte[0x4F];
    #endregion

    #region Equipment Cosmetics/Unknown
    EquipCosmeticsFlagSet UnlockedEquipmentCosmetics    { get; set; }
    byte[] Unk_0x4F0                    { get; set; } = new byte[0x1A8];

    #endregion

    private static readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
    {
        { "Unk_0x000",                  0x0   },
        { "TitleBackground",            0x8   },
        { "Unk_0x009",                  0x9   },
        { "Controls",                   0x46  },
        { "MiniMapDisplay",             0x48  },
        { "Unk_0x04A",                  0x4A  },
        { "ArtDescriptions",            0x4C  },
        { "EnemyIcons",                 0x4E  },
        { "TravelGuidance",             0x50  },
        { "ChanceArts",                 0x52  },
        { "Vibration",                  0x54  },
        { "Unk_0x056",                  0x56  },
        { "Brightness",                 0x58  },
        { "UpdateNotificationIcons",    0x5A  },
        { "BuffDebuffInfo",             0x5C  },
        { "OtherNotifications",         0x5E  },
        { "BuffDebuffEffects",          0x60  },
        { "Locations",                  0x62  },
        { "Items",                      0x64  },
        { "Achievements",               0x66  },
        { "Stealing",                   0x68  },
        { "Quests",                     0x6A  },
        { "AffinityChart",              0x6C  },
        { "Tutorials",                  0x6E  },
        { "Affinity",                   0x70  },
        { "CameraInvertY",              0x72  },
        { "CameraInvertX",              0x74  },
        { "Unk_0x076",                  0x76  },
        { "CameraResetSpeed",           0x78  },
        { "CameraPanSpeed",             0x7A  },
        { "CameraZoomSpeed",            0x7C  },
        { "GradientCorrection",         0x7E  },
        { "LanguageSelection",          0x80  },
        { "FieldBGM",                   0x82  },
        { "BattleBGM",                  0x84  },
        { "CutsceneVoiceVolume",        0x86  },
        { "GameBGMVolume",              0x88  },
        { "GameSEVolume",               0x8A  },
        { "GameVoiceVolume",            0x8C  },
        { "SystemVolume",               0x8E  },
        { "EventScrolling",             0x90  },
        { "Subtitles",                  0x92  },
        { "DialogueTextSpeed",          0x94  },
        { "Unk_0x096",                  0x96  },
        { "CutsceneUnlockStory",        0xE4  },
        { "Unk_0x102",                  0x102 },
        { "CutsceneUnlockHeartToHeart", 0x109 },
        { "CutsceneUnlockQuietMoment",  0x111 },
        { "Unk_0x115",                  0x115 },
        { "UnlockedEquipmentCosmetics", 0x164 },
        { "Unk_0x4F0",                  0x4F0 }
    };

    public XCDESettings(byte[] data)
    {
        #region Misc/Unknown
        Unk_0x000 = data.GetByteSubArray(LOC["Unk_0x000"], (uint)(Unk_0x000.Length));
        TitleBackground = data[LOC["TitleBackground"]];
        Unk_0x009 = data.GetByteSubArray(LOC["Unk_0x009"], (uint)(Unk_0x009.Length));
        #endregion

        #region Game Config Settings

        #region Display Settings
        Controls.Create(data.GetByteSubArray(LOC["Controls"], Setting<object>.SIZE));
        MiniMapDisplay.Create(data.GetByteSubArray(LOC["MiniMapDisplay"], Setting<object>.SIZE));
        Unk_0x04A.Create(data.GetByteSubArray(LOC["Unk_0x04A"], Setting<object>.SIZE));
        ArtDescriptions.Create(data.GetByteSubArray(LOC["ArtDescriptions"], Setting<object>.SIZE));
        Unk_0x04A.Create(data.GetByteSubArray(LOC["Unk_0x04A"], Setting<object>.SIZE));
        ArtDescriptions.Create(data.GetByteSubArray(LOC["ArtDescriptions"], Setting<object>.SIZE));
        EnemyIcons.Create(data.GetByteSubArray(LOC["EnemyIcons"], Setting<object>.SIZE));
        TravelGuidance.Create(data.GetByteSubArray(LOC["TravelGuidance"], Setting<object>.SIZE));
        ChanceArts.Create(data.GetByteSubArray(LOC["ChanceArts"], Setting<object>.SIZE));
        #endregion

        #region Game Settings
        Vibration.Create(data.GetByteSubArray(LOC["Vibration"], Setting<object>.SIZE));
        Unk_0x056.Create(data.GetByteSubArray(LOC["Unk_0x056"], Setting<object>.SIZE));
        #endregion

        #region Brightness
        Brightness.Create(data.GetByteSubArray(LOC["Brightness"], Setting<object>.SIZE));
        #endregion

        #region Notification Settings
        UpdateNotificationIcons.Create(data.GetByteSubArray(LOC["UpdateNotificationIcons"], Setting<object>.SIZE));
        BuffDebuffInfo.Create(data.GetByteSubArray(LOC["BuffDebuffInfo"], Setting<object>.SIZE));
        OtherNotifications.Create(data.GetByteSubArray(LOC["OtherNotifications"], Setting<object>.SIZE));
        BuffDebuffEffects.Create(data.GetByteSubArray(LOC["BuffDebuffEffects"], Setting<object>.SIZE));
        Locations.Create(data.GetByteSubArray(LOC["Locations"], Setting<object>.SIZE));
        Items.Create(data.GetByteSubArray(LOC["Items"], Setting<object>.SIZE));
        Achievements.Create(data.GetByteSubArray(LOC["Achievements"], Setting<object>.SIZE));
        Stealing.Create(data.GetByteSubArray(LOC["Stealing"], Setting<object>.SIZE));
        Quests.Create(data.GetByteSubArray(LOC["Quests"], Setting<object>.SIZE));
        AffinityChart.Create(data.GetByteSubArray(LOC["AffinityChart"], Setting<object>.SIZE));
        Tutorials.Create(data.GetByteSubArray(LOC["Tutorials"], Setting<object>.SIZE));
        Affinity.Create(data.GetByteSubArray(LOC["Affinity"], Setting<object>.SIZE));
        #endregion

        #region Camera
        CameraInvertY.Create(data.GetByteSubArray(LOC["CameraInvertY"], Setting<object>.SIZE));
        CameraInvertX.Create(data.GetByteSubArray(LOC["CameraInvertX"], Setting<object>.SIZE));
        Unk_0x076.Create(data.GetByteSubArray(LOC["Unk_0x076"], Setting<object>.SIZE));
        CameraResetSpeed.Create(data.GetByteSubArray(LOC["CameraResetSpeed"], Setting<object>.SIZE));
        CameraPanSpeed.Create(data.GetByteSubArray(LOC["CameraPanSpeed"], Setting<object>.SIZE));
        CameraZoomSpeed.Create(data.GetByteSubArray(LOC["CameraZoomSpeed"], Setting<object>.SIZE));
        GradientCorrection.Create(data.GetByteSubArray(LOC["GradientCorrection"], Setting<object>.SIZE));
        #endregion

        #region Sound        
        LanguageSelection.Create(data.GetByteSubArray(LOC["LanguageSelection"], Setting<object>.SIZE));
        FieldBGM.Create(data.GetByteSubArray(LOC["FieldBGM"], Setting<object>.SIZE));
        BattleBGM.Create(data.GetByteSubArray(LOC["BattleBGM"], Setting<object>.SIZE));
        CutsceneVoiceVolume.Create(data.GetByteSubArray(LOC["CutsceneVoiceVolume"], Setting<object>.SIZE));
        GameBGMVolume.Create(data.GetByteSubArray(LOC["GameBGMVolume"], Setting<object>.SIZE));
        GameSEVolume.Create(data.GetByteSubArray(LOC["GameSEVolume"], Setting<object>.SIZE));
        GameVoiceVolume.Create(data.GetByteSubArray(LOC["GameVoiceVolume"], Setting<object>.SIZE));
        SystemVolume.Create(data.GetByteSubArray(LOC["SystemVolume"], Setting<object>.SIZE));
        #endregion

        #region Messages
        EventScrolling.Create(data.GetByteSubArray(LOC["EventScrolling"], Setting<object>.SIZE));
        Subtitles.Create(data.GetByteSubArray(LOC["Subtitles"], Setting<object>.SIZE));
        DialogueTextSpeed.Create(data.GetByteSubArray(LOC["DialogueTextSpeed"], Setting<object>.SIZE));
        #endregion

        #endregion

        #region Event Viewer/Unknown
        Unk_0x096 = data.GetByteSubArray(LOC["Unk_0x096"], (uint)Unk_0x096.Length);
        CutsceneUnlockStory = data.GetByteSubArray(LOC["CutsceneUnlockStory"], (uint)(CutsceneUnlockStory.Length));
        Unk_0x102 = data.GetByteSubArray(LOC["Unk_0x102"], (uint)Unk_0x102.Length);
        CutsceneUnlockHeartToHeart = data.GetByteSubArray(LOC["CutsceneUnlockHeartToHeart"], (uint)(CutsceneUnlockHeartToHeart.Length));
        CutsceneUnlockQuietMoment = data.GetByteSubArray(LOC["CutsceneUnlockQuietMoment"], (uint)(CutsceneUnlockQuietMoment.Length));
        Unk_0x115 = data.GetByteSubArray(LOC["Unk_0x115"], (uint)Unk_0x115.Length);
        #endregion

        #region Equipment Cosmetics/Unknown
        UnlockedEquipmentCosmetics = new EquipCosmeticsFlagSet(data.GetByteSubArray(LOC["UnlockedEquipmentCosmetics"], EquipCosmeticsFlagSet.SIZE));
        Unk_0x4F0 = data.GetByteSubArray(LOC["Unk_0x4F0"], ( uint)Unk_0x4F0.Length);
        #endregion
    }

    public byte[] ToRawData()
    {
        List<byte> result = new List<byte>();

        #region Misc/Unknown
        result.AddRange(Unk_0x000);
        result.Add(TitleBackground);
        result.AddRange(Unk_0x009);
        #endregion

        #region Game Config Settings

        #region Display Settings
        result.AddRange(Controls.ToRawData());
        result.AddRange(MiniMapDisplay.ToRawData());
        result.AddRange(Unk_0x04A.ToRawData());
        result.AddRange(ArtDescriptions.ToRawData());
        result.AddRange(EnemyIcons.ToRawData());
        result.AddRange(TravelGuidance.ToRawData());
        result.AddRange(ChanceArts.ToRawData());
        #endregion

        #region Game Settings
        result.AddRange(Vibration.ToRawData());
        result.AddRange(Unk_0x056.ToRawData());
        #endregion

        #region Brightness
        result.AddRange(Brightness.ToRawData());
        #endregion

        #region Notification Settings
        result.AddRange(UpdateNotificationIcons.ToRawData());
        result.AddRange(BuffDebuffInfo.ToRawData());
        result.AddRange(OtherNotifications.ToRawData());
        result.AddRange(BuffDebuffEffects.ToRawData());
        result.AddRange(Locations.ToRawData());
        result.AddRange(Items.ToRawData());
        result.AddRange(Achievements.ToRawData());
        result.AddRange(Stealing.ToRawData());
        result.AddRange(Quests.ToRawData());
        result.AddRange(AffinityChart.ToRawData());
        result.AddRange(Tutorials.ToRawData());
        result.AddRange(Affinity.ToRawData());
        #endregion

        #region Camera
        result.AddRange(CameraInvertY.ToRawData());
        result.AddRange(CameraInvertX.ToRawData());
        result.AddRange(Unk_0x076.ToRawData());
        result.AddRange(CameraResetSpeed.ToRawData());
        result.AddRange(CameraPanSpeed.ToRawData());
        result.AddRange(CameraZoomSpeed.ToRawData());
        result.AddRange(GradientCorrection.ToRawData());
        #endregion

        #region Sound
        result.AddRange(LanguageSelection.ToRawData());
        result.AddRange(FieldBGM.ToRawData());
        result.AddRange(BattleBGM.ToRawData());
        result.AddRange(CutsceneVoiceVolume.ToRawData());
        result.AddRange(GameBGMVolume.ToRawData());
        result.AddRange(GameSEVolume.ToRawData());
        result.AddRange(GameVoiceVolume.ToRawData());
        result.AddRange(SystemVolume.ToRawData());
        #endregion

        #region Messages
        result.AddRange(EventScrolling.ToRawData());
        result.AddRange(Subtitles.ToRawData());
        result.AddRange(DialogueTextSpeed.ToRawData());
        #endregion

        #endregion

        #region Event Viewer/Unknown
        result.AddRange(Unk_0x096);
        result.AddRange(CutsceneUnlockStory);
        result.AddRange(Unk_0x102);
        result.AddRange(CutsceneUnlockHeartToHeart);
        result.AddRange(CutsceneUnlockQuietMoment);
        result.AddRange(Unk_0x115);
        #endregion

        #region Equipment Cosmetics/Unknown
        result.AddRange(UnlockedEquipmentCosmetics.ToRawData());
        result.AddRange(Unk_0x4F0);
        #endregion

        return result.ToArray();
    }
}