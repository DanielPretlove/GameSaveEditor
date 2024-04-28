/*

0x8xxx bit seems to be Modified flag for any Setting

Most Settings are boolean, some have 3 choices, some are numerical range
Range 0-1000
For 6-Bar ranges:
0 Bars  0
1 Bar   166
2 Bars  333
3 Bars  500
4 Bars  666
5 Bars  833
6 Bars  1000
*/

bitfield Setting
{
    Value    : 15;
    Modified : 1;
};

bitfield Cutscenes
{
	Unused                      : 1;
	BionisandMechonis           : 1;
	SeizingtheFuture            : 1;
	ShulkandReyn                : 1;
	Colony9                     : 1;
	ColonelVangarre             : 1;
	FioraandDunban              : 1;
	ToShulksSide                : 1;
	APeacefulView               : 1;
	TheMonadosRevelation        : 1;
	YouForgotSomething          : 1;
	TephraCave                  : 1;
	MagMellRuins                : 1;
	TheSentriesAwaken           : 1;
	Colony9inDanger             : 1;
	TheMechonAttack             : 1;
	PuttingUpaFight             : 1;
	TotheLab                    : 1;
	TheSneeringFace             : 1;
	LaunchingtheMobileArtillery : 1;
	ReynsPromise                : 1;
	TheHerooftheHoms            : 1;
	FiorasResolve               : 1;
	TheSwordofFutureSight       : 1;
	MetalFace                   : 1;
	Loss                        : 1;
	TheYouthsSetOut             : 1;
	TheBrokenSeal               : 1;
	TotheBionisKnee             : 1;
	Guidance                    : 1;
	TheTrueMonado               : 1;
	ReyninDanger                : 1;
	AnUnwantedFuture            : 1;
	ChasedbyArachnos            : 1;
	PowerUnlocked               : 1;
	TheMonadosWarning           : 1;
	TheDistantMechonis          : 1;
	TheBionisLeg                : 1;
	TheVision                   : 1;
	TheBoyattheOasis            : 1;
	TotheHomsCamp               : 1;
	SharlaandJuju               : 1;
	TheTragedyofColony6         : 1;
	ANewAlly                    : 1;
	TheAbandonedBuggy           : 1;
	WhereDidJujuGo              : 1;
	ShulksPower                 : 1;
	PursuingtheMechon           : 1;
	TheTalkingFace              : 1;
	JujuKidnapped               : 1;
	ThePromise                  : 1;
	Colony6                     : 1;
	TheUndergroundPath          : 1;
	FollowingGunfire            : 1;
	RendezvouswithOtharon       : 1;
	AMessyPlan                  : 1;
	AttheCentralPit             : 1;
	XordtheEntertainer          : 1;
	ChangetheFutureTogether     : 1;
	Tenacity                    : 1;
	UnfinishedBusiness          : 1;
	WhoWaitsatPrisonIsland      : 1;
	SatorlMarsh                 : 1;
	TheNoponMerchant            : 1;
	SoitisWritten               : 1;
	SeparateWays                : 1;
	ImminentDespair             : 1;
	ChosenbytheMonado           : 1;
	JourneyInsidetheBionis      : 1;
	FaceNemesis                 : 1;
	MaknaForest                 : 1;
	TheGirlfromtheVision        : 1;
	Alvis                       : 1;
	MeliaAwakens                : 1;
	FrontierVillage             : 1;
	MeliasOrders                : 1;
	RikitheLegendaryHeropon     : 1;
	AProphetsInstinct           : 1;
	MoreTelethiaTrouble         : 1;
	AnUnbowedSpirit             : 1;
	ANewProphecy                : 1;
	MeliasPromise               : 1;
	UpwardsfromFrontierVillage  : 1;
	ErythSea                    : 1;
	PartingwithMelia            : 1;
	AlcamoththeImperialCapital  : 1;
	IntheAudienceChamber        : 1;
	TheDivineSeer               : 1;
	IntheTomb                   : 1;
	TheTwoConsorts              : 1;
	InquisitoroftheBioniteOrder : 1;
	TheLegendoftheMonado        : 1;
	TheHighEntiaTomb            : 1;
	TheirFutureSovereign        : 1;
	TheTombsGuard               : 1;
	TowardstheCeremonyHall      : 1;
	InquisitorTyrea             : 1;
	TheBionisWish               : 1;
	AnAudiencewiththeEmperor    : 1;
	TheMechonInvade             : 1;
	TheEmperorsDecision         : 1;
	InPursuitoftheEmperor       : 1;
	AncientCreaturesSwarm       : 1;
	FightingtheAncientCreatures : 1;
	ReasonfortheAggression      : 1;
	TheGiantAwakens             : 1;
	AtthePrisonGates            : 1;
	PrisonIsland                : 1;
	TheMonadosShackles          : 1;
	OutsideEstablishedHarmony   : 1;
	FioraLives                  : 1;
	DunbansConsideration        : 1;
	TimetoGo                    : 1;
	PathtoGalahadFortress       : 1;
	RidingtheShortRangePod      : 1;
	ValakMountain               : 1;
	TheIceBoundRuins            : 1;
	Tremors                     : 1;
	OurOldFriendMumkhar         : 1;
	EgiltheLeaderofMechonis     : 1;
	GettingFioraBack            : 1;
	TheSwordoftheMechonis       : 1;
	SwordValley                 : 1;
	TheSupplyConvoy             : 1;
	MeetingDicksonAgain         : 1;
	InfiltratingtheFortress     : 1;
	MumkharBarstheWay           : 1;
	AMadmanBreathesHisLast      : 1;
	TheFortressRearEntrance     : 1;
	InGalahadFortress           : 1;
	SeveringtheEtherSupply      : 1;
	ASuccessfulPlan             : 1;
	FaceNemesisAttacks          : 1;
	ControlledMachines          : 1;
	FiorasRequest               : 1;
	AnotherShulk                : 1;
	SeekingWater                : 1;
	TheFirstKiss                : 1;
	IWillProtectYou             : 1;
	ReynandSharla               : 1;
	FindingShulk                : 1;
	ProfoundJoy                 : 1;
	DunbanandRiki               : 1;
	HiddenMachinaVillage        : 1;
	FiorasNewBody               : 1;
	MiqolsRequest               : 1;
	TheAlliedForceofBionis      : 1;
	WhatHappensNext             : 1;
	ToTakeDownEgilOr            : 1;
	AMajesticView               : 1;
	MechonisField               : 1;
	TheImmobilisedLift          : 1;
	IWontLetAnyoneElseDie       : 1;
	JadeFace                    : 1;
	ReunionwithGadolt           : 1;
	EncounterwithVanea          : 1;
	MechonisCentralFactory      : 1;
	Advance                     : 1;
	AgnirathaMechonisCapital    : 1;
	LeavingtheRuins             : 1;
	MemoriesofanAncientAge      : 1;
	AnUnheardVoice              : 1;
	TheFewRemainingSouls        : 1;
	GadoltsReturn               : 1;
	ConfrontingEgil             : 1;
	TheGoldenMechon             : 1;
	APromiseFulfilled           : 1;
	MechonisAwakens             : 1;
	TheJunksSwoopsIn            : 1;
	ShulksChange                : 1;
	TheApocryphaGenerator       : 1;
	InsidetheFlow               : 1;
	EgilsPain                   : 1;
	ShulksChoice                : 1;
	AWorldsDeathandRebirth      : 1;
	MeynethsWords               : 1;
	EgilsPassing                : 1;
	KallianChanges              : 1;
	BoundlessGrief              : 1;
	FeelingsShared              : 1;
	Colony6inDanger             : 1;
	ShulkAscends                : 1;
	TheWilltoSurvive            : 1;
	ACommonDestiny              : 1;
	Sortie                      : 1;
	TheBionisInterior           : 1;
	ItThatEliminatesImpureLife  : 1;
	BelovedSister               : 1;
	ReturntoPrisonIsland        : 1;
	TheWarmUpAct                : 1;
	BeforetheCathedralsAltar    : 1;
	ToSeizetheFuture            : 1;
	DiscipleDickson             : 1;
	APowerBornfromHope          : 1;
	TheTeacherisSurpassed       : 1;
	ZanzasWelcome               : 1;
	TheLieoftheWorld            : 1;
	TheThirdMonado              : 1;
	AWorldWithoutGods           : 1;
	TheEmpressandtheAssassin    : 1;
	RoaroftheTelethia           : 1;
	MeliaandTyrea               : 1;
	TheWorldOneYearLater        : 1;
	StepAsideRiki               : 1;
	ChildrenoftheHeropon        : 1;
	AttheCompanionsCape         : 1;
	HopeoftheHighEntia          : 1;
	FlockingTelethia            : 1;
	ACapitalChanged             : 1;
	SavingTeelan                : 1;
	TheFogKingRises             : 1;
	ReunionwithTyrea            : 1;
	BeyondtheRamparts           : 1;
	TheAncientTownofGranDell    : 1;
	OneMind                     : 1;
	TheMonstersTransform        : 1;
	GranDellinPeril             : 1;
	BefoggedArmus               : 1;
	InPursuitofResearch         : 1;
	GaelgarMakesHisMove         : 1;
	UpinSmoke                   : 1;
	TheRampartsareBreached      : 1;
	MarchoftheFogbeasts         : 1;
	MakingfortheCape            : 1;
	MartialResolve              : 1;
	ForFamily                   : 1;
	ALeadersReturn              : 1;
	ADecisionisMade             : 1;
	FacingtheFogKing            : 1;
	TheRiftSwells               : 1;
	TheFutureisSaved            : 1;
	HeadingforColony6           : 1;
	FuelledbyBitterness         : 1;
};

bitfield EquipCosmeticByte
{
    Unlocked1 : 1;
    IsNotNew1 : 1;
    Unlocked2 : 1;
    IsNotNew2 : 1;
    Unlocked3 : 1;
    IsNotNew3 : 1;
    Unlocked4 : 1;
    IsNotNew4 : 1;
};

struct XCDESettings
{
	// Misc/Unknown
    u8        Unk_0x000[0x8];
    u8        TitleBackground;
    u8        Unk_0x009[0x3D];

    // Display Settings
    Setting   Controls;
    Setting   MiniMapDisplay;
    Setting   Unk_0x4A;
    Setting   ArtDescriptions;
    Setting   EnemyIcons;
    Setting   TravelGuidance;
    Setting   ChanceArts;

    // Game Settings
    Setting   Vibration;
    Setting   Unk_0x056;

    // Brightness
    Setting   Brightness;

    // Notification Sttings
    Setting   UpdateNotificationIcons;
    Setting   BuffDebuffInfo;
    Setting   OtherNotifications;
    Setting   BuffDebuffEffects;
    Setting   Locations;
    Setting   Items;
    Setting   Achievements;
    Setting   Stealing;
    Setting   Quests;
    Setting   AffinityChart;
    Setting   Tutorials;
    Setting   Affinity;

    // Camera
    Setting   CameraInvertY;
    Setting   CameraInvertX;
    Setting   Unk_0x076;
    Setting   CameraResetSpeed;
    Setting   CameraPanSpeed;
    Setting   CameraZoomSpeed;
    Setting   GradientCorrection;

    // Sound
    Setting   LanguageSelection;
    Setting   FieldBGM;
    Setting   BattleBGM;
    Setting   CutsceneVoiceVolume;
    Setting   GameBGMVolume;
    Setting   GameSEVolume;
    Setting   GameVoiceVolume;
    Setting   SystemVolume;

    // Messages
    Setting   EventScrolling;
    Setting   Subtitles;
    Setting   DialogueTextSpeed;
    
	// Event Viewer/Unknown
    u8        Unk_0x096[0x4E];
    Cutscenes CutscenesMainStory;
    u8        Unk_0x102[0x7];
    u8        CutsceneUnlockHeartToHeart[0x8];
    u8        CutSceneUnlockQuietMoment[0x4];  // Length = ???
    u8        Unk_0x115[0x4F];

	// Equipment Cosmetics/Unknown
    EquipCosmeticByte CosmeticSet[0x38C];
    u8                Unk_0x4F0[0x1A8];
};

XCDESettings Settings @ 0x0;