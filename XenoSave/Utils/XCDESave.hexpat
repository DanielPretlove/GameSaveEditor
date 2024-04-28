#pragma pattern_limit 262144

enum AffinityLink : u8
{
    None,
    Red,
    Orange,
    Yellow,
    green,
    Blue,
    Purple,
    Pink
};

enum ItemType : u16
{
    None = 0,
    Unk_1,
    Weapon,
    Gem,
    HeadArmour,
    TorsoArmour,
    ArmArmour,
    LegArmour,
    FootArmour,
    Crystal,
    Collectable,
    Material,
    KeyItem,
    ArtsManual
};

enum CrystalElement : u8
{
    None,
    Unk_1,
    Unk_2,
    Unk_3,
    Fire,
    Water,
    Electric,
    Ice,
    Wind,
    Earth,
    Mixed
};

enum Character : u16
{
    None = 0,
    Shulk,
    Reyn,
    Fiora,
    Dunban,
    Sharla,
    Riki,
    Melia,
    Fiora_2,
    Dickson,
    Mumkhar,
    Alvis,
    Dunban_2,
    Dunban_3,
    Kino,
    Nene,
    Wunwun,
    Tutu,
    Drydry,
    Fofora,
    Faifa,
    Hekasa,
    Setset,
    TeiTei,
    Nonona,
    Dekadeka,
    Evelen,
    Tentoo
};

enum ArtsLevelUnlocked : u8
{
    IV_Beginner,
    VII_Intermediate,
    Unk_2,
    X_Expert,
    XII_Master
};

bitfield RealTime
{
    Unk_B       : 4;
    Millisecond : 10;
    Second      : 6;
    Minute      : 6;
    Hour        : 6;
    Day         : 5;
    Unk_A       : 9;
    Month       : 4;
    Year        : 14;
};

struct ItemID
{
    u16      ID;
    ItemType TypeID;
};

struct Item
{
    u16      Index;
    ItemType Type;
    ItemID   FullID;
    u16      Quantity;
    u16      Unk_1;
    u32      SerialNo;
    bool     Exists;
    bool     Favourite;
    u16      Unk_2;
};

struct EquipItem
{
    Item   ItemBase;
    u8     Weight;
    u8     GemSlots;
    u16    Unk_3;
    ItemID Gem1;
    ItemID Gem1Alt;
    ItemID Gem2;
    ItemID Gem2Alt;
    ItemID Gem3;
    ItemID Gem3Alt;
};

struct CrystalBuff
{
    u16 BuffID;
    u8  Value;
    u8  Probability;
};

struct CrystalItem
{
    Item           ItemBase;
    u16            CrystalNameID;
    u8             Rank;
    CrystalElement Element;
    u8             Unk_3;
    bool           IsCylinder;
    u16            BuffCount;
    CrystalBuff    Buffs[4];
};

struct Party
{
    Character Characters[12];
    u8        PartyMembersCount;
};

struct PartyMember
{
    u32    Level;
    u32    EXP;
    u32    AP;
    u32    AffinityCoins;
    u8     Unk_1[4];
    ItemID Equipment[6];
    u8     Unk_2[4];
    u32    Cosmetics[6];
    u16    Arts[9];
    u16    MonadoArts[9];
    u8     Unk_3[0x0C];
    u32    SelectedSkillTreeIndex;
    u32    SkillTreeSPs[5];
    u32    SkillTreeUnlockedSkills[5];
    u8     Unk_4[0x20];
    u8     SkillLinkIDs[40];
    u32    ExpertModeLevel;
    u32    ExpertModeEXP;
    u32    ExpertModeReserveEXP;
    u8     Unk_5[0x40];
};

struct ArtsLevel
{
    u8                Level;
    ArtsLevelUnlocked MaxLevelUnlocked;
};

struct NonPartyMemberItem
{
    EquipItem   Item;
    u8          Unk_1[0x84];
};

struct NonPartyMember
{
    u8                 Unk_1[0x1C];
    NonPartyMemberItem Equipment[6];
    u8                 Unk_2[0x98];
};

bitfield TimeAttackScore
{
    AchievedRank   : 4;
    NSRewardRank   : 4;
    BonusNSReward2 : 1;
    BonusNSRevard1 : 1;
    Unk            : 20;
    IsNotNew       : 1;
    RewardAchieved : 1;
};

struct TimeAttackData
{
    TimeAttackScore Results[20];
    u8              Unk[0x50];
    u32             TimesMilliseconds[20];
};

struct XCDESave
{
    u8             Unk_0x000000[0x04];
    u32            TotalPlayTime;
    RealTime       SaveTimestamp;
    u32            Noponstone;
    s32            FCArtsCoins;
    u8             Unk_0x000018[0x038A];
    u8             Flags[0x024C];
    u8             Unk_0x0005EE[0x0517];
    AffinityLink   AffinityLinks[362];
    u8             Unk_0x000C6F[0x0185];
    u16            AreaAffinity[5];
    u8             Unk_0x000DFE[0x04];
    u16            PartyAffinity[21];
    u8             Unk_0x000E2C[0x06];
    u16            AvhievementCounters[200];
    u8             Unk_0x000FC2[0x2B4E];
    EquipItem      WeaponBox[500];
    EquipItem      HeadArmourBox[500];
    EquipItem      TorsoArmourBox[500];
    EquipItem      ArmArmourBox[500];
    EquipItem      LegArmourBox[500];
    EquipItem      FootArmourBox[500];
    CrystalItem    CrystalBox[500];
    CrystalItem    GemBox[500];
    Item           CollectableBox[500];
    Item           MaterialBox[500];
    Item           KeyItemBox[500];
    Item           ArtsManualBox[500];
    u8             Unk_0x03B5B0[0x0584];
    NonPartyMember NonPartyMembers[27];
    u8             Unk_0x044018[0x28F0];
    u32            ItemBoxSerials[13];
    u8             Unk_0x04693C[0x10276D];
    u8             Flags2[0x2C];
    u8             Unk_0x1490D5[0x0B13];
    float          CameraZoom;
    u8             Unk_0x149BEC[0x7F54];
    u32            Money;
    u8             Unk_0x151B44[0x04B4];
    TimeAttackData TimeAttack;
    u8             Unk_0x1520E8[0x0230];
    Party          Party;
    u8             Unk_0x152331[0x37];
    PartyMember    PartyMembers[16];
    ArtsLevel      ArtsLevels[188];
};

XCDESave savedata @ 0x0;