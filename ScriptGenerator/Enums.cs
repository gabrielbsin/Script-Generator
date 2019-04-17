using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGenerator
{
    public static class Enums
    {
        public enum UIType : int
        {
            UI_ITEM = 0, UI_EQUIP = 1, UI_STAT = 2, UI_SKILL = 3, UI_MINIMAP = 4, UI_KEYCONFIG = 5, UI_QUESTINFO = 6, UI_USERLIST = 7, UI_MESSENGER = 8, UI_MONSTERBOOK = 9, UI_USERINFO = 10, UI_SHORTCUT = 11, UI_MENU = 12, UI_QUESTALARM = 13, UI_PARTYHP = 14, UI_QUESTTIMER = 15, UI_QUESTTIMERACTION = 16, UI_MONSTERCARNIVAL = 17, UI_ITEMSEARCH = 18, UI_ENERGYBAR = 19, UI_GUILDBOARD = 20, UI_PARTYSEARCH = 21, UI_ITEMMAKE = 22, UI_CONSULT = 23, UI_CLASSCOMPETITION = 24, UI_RANKING = 25, UI_FAMILY = 26, UI_FAMILYCHART = 27, UI_OPERATORBOARD = 28, UI_OPERATORBOARDSTATE = 29, UI_MEDALQUESTINFO = 30, UI_WEBEVENT = 31, UI_SKILLEX = 32, UI_REPAIRDURABILITY = 33, UI_CHATWND = 34, UI_BATTLERECORD = 35, UI_GUILDMAKEMARK = 36, UI_GUILDMAKE = 37, UI_GUILDRANK = 38, UI_CREATE_PREMIUMADVENTURER = 39, UI_WORLDMAP = 40, UI_WORLDMAPALARM = 41, UI_MAKING_SKILL = 42, UI_BAG1 = 43, UI_BAG2 = 44, UI_BAG3 = 45, UI_BAG4 = 46, UI_PVPMODERESULT = 47, UI_PVPPARTYHP = 48, UI_PVPRANKING = 49, UI_PVPLAUNCHER = 50, UI_ITEMPOT = 51, UI_EVENT2 = 52, UI_EVENT_WATERMELON = 53, UI_EVENT_BINGSU = 54, UI_EVENTLIST = 55, UI_EVENT_MONKEY = 56, UI_EVENT_PIRATE = 57, UI_EXP_PLUS_EVENT = 58, UI_EVENT_FRUIT = 59, UI_EVENT_ECO = 60, UI_EVENT_BASE = 61, UI_FIELDITEM = 62, UI_FIELDITEMINVENTORY = 63, UI_BAG5 = 64, UI_STEALMAN = 65, UI_STEALLIST = 66, UI_LEVELUPGUIDE = 67, UI_ASWANSTATE = 68, UI_ASWANRESULT = 69, UI_ASWANSTART = 70, UI_ASWANSIEGEGAUGE = 71, UI_DOJANGRESULT = 72, UI_CROSSHUNTER = 73, UI_DEATHCOUNT = 74, UI_CASHITEMALARM = 75, UI_BOARDGAME = 76, UI_PVPLAUNCHERHARDCORE = 77, UI_PVPHARDCORERESULT = 78, UI_PVPHARDCOREFIELDINFO = 79, UI_BINGO = 80, UI_LARKNESS = 81, UI_MESORANGER = 82, UI_KAISERTRANSFORM = 83, UI_HYPERSKILL = 84, UI_DRESSROOM = 85, UI_BAG6 = 86, UI_ENTRY = 87, UI_LARKNESSHELP = 88, UI_KAISERTRANSFORMHELP = 89, UI_SOULRECHARGE = 90, UI_AGGRORANK = 91, UI_SOULRECHARGE_HELP = 92, UI_COLLECTEVENT_MUSEUM = 93, UI_DEATHCOUNTINFO = 94, UI_EVENTNOTICE = 95, UI_DAMAGERANK = 96, UI_SUMMONEVENT_REWARD = 97, UI_MYSTIC_FIELD = 98, UI_MAPLESCHOOL = 99, UI_EVOLVING_SYSTEM = 100, UI_INS_BAG1 = 101, UI_INS_BAG2 = 102, UI_YUTGAME = 103, UI_JEWELCRAFT = 104, UI_YUT_ACHIEVEMENT = 105, UI_VALUEPACK = 106, UI_HALLOWEENCANDY_RANKING = 107, UI_GETREWARD = 108, UI_MAPLESTYLE = 109, UI_INDIAN_ACHIEVEMENT = 110, UI_WAITQUEUE = 111, UI_ACTION_ACHIEVEMENT = 112, UI_CLOCK_GAUGE = 113, UI_ATTENDANCE_CHECK = 114, UI_SURPLUS = 115, UI_MENTORING = 116, UI_COLLECTEVENT_LOTTERY = 117, UI_ACHIEVEMENT_WINTER2012 = 118, UI_ROULETTE = 119, UI_COLLECTEVENT_LOTTERY_RESULT = 120, UI_UNIFIED_ACHIEVEMENTS = 121, UI_COOK_TANGYOON = 122, UI_COOK_MINIGAME = 123, UI_OVERLOAD_NOTICE = 124, UI_VIRTUALINVEN_BITSINVEN = 125, UI_VIRTUALINVEN_BITSCASE = 126, UI_BINGO_CASSANDRA = 127, UI_GOFARM = 128, UI_MULTI_YUT_GAME = 129, UI_FARM_CHAT = 130, UI_FARM_MON_INFO = 131, UI_FARM_RECOMMEND = 132, UI_FARM_ENTER_INGAME = 133, UI_FARM_GROUP_CHAT = 134, UI_FARM_LOCKER = 135, UI_COLLECTEVENT_MUSEUMS2 = 136, UI_LETTER = 137, UI_MEMO_IN_GAME = 138, UI_AVATAR_MEGAPHON = 139, UI_EGO_EQUIP = 140, UI_INHERITANCE = 141, UI_RHYTHM_START = 142, UI_RHYTHM_GAME = 143, UI_SOUL_MP_COUNT = 144, UI_SCENARIOINFO = 145, UI_SCENARIOSTART = 146, UI_ZERO_CASH_EQUIP = 147, UI_ZERO_SUB_GAUGE = 148, UI_CASHSHOP_PROMOTION_BANNER = 149, UI_ENCHANT = 150, UI_SCENARIOILLUCHAT = 151, UI_MIRROR_DUNGEON = 152, UI_RHYTHM_EXIT = 153, UI_10TH_ANNIVERSARY_LIVE = 154, UI_LINKSKILL = 155, UI_INTRUSION = 156, UI_STUDY_MAKINGSKILL = 157, UI_EVENT_NAME_TAG = 158, UI_MIRROR_READING = 159, UI_JOURNAL = 160, UI_AUCTION = 161, UI_SKILLFORZERO = 162, UI_MIRROR_DUNGEON_MINI = 163, UI_JOB_FREE_CHANGE = 164, UI_AUCTION_MANAGE = 165, UI_SCENARIOSTART_TOOLTIP = 166, UI_AUCTION_TOP_MESSAGE = 167, UI_TIME_GATE = 168, UI_LOCKEDSKILL_FORZERO = 169, UI_INVASION_SUPPORT = 170, UI_INVASION_SUPPORT_SUMMERY = 171, UI_INVASION_SUPPORT_ICON = 172, UI_MESOMARKET = 173, UI_UNIVERSERANKING = 174, UI_MONSTERBATTLECOLLECTION = 175, UI_LIMITGOODS_NOTICE = 176, UI_INVASION_SUPPORT_SETTING = 177, UI_HEKATON_MINIMAP = 178, UI_DIMENSION_LIBRARY = 179, UI_BOSSARENA_MATCH = 180, UI_BOSSARENA_SELECT = 181, UI_SOUL_COLLECTION = 182, UI_WORLD_TRANSFER = 183, UI_SOUL_WAITING_LINSE = 184, UI_PARTY_QUEST_RANKING = 185, UI_SOUL_DUNGEON = 186, UI_SOUL_DUNGEON_MINI_MAP = 187, UI_ITEMREPLACE = 188, UI_SAILING = 189, UI_VESSEL = 190, UI_EPISODEBOOK = 191, UI_ATTENDANCE_GHOST_EVENT = 192, UI_ATTENDANCE_MUSTACHE_EVENT = 193, UI_GHOSTPAINTS_EVENT = 194, UI_CONTENTS_SHORTCUT = 195, UI_EVENTGROUP_WAITQUEUE = 196, UI_COORDINATIONCONTEST = 197, UI_GUILDCONTENT_RANK = 198, UI_HUNDREDBINGO = 199, UI_HUNDREDBINGO_RANK = 200, UI_EQUIPMENT_ENCHANT = 201, UI_GROWTH_HELPER = 202, UI_GUILDMINI = 203, UI_INNERABILITY = 204, UI_AFREECATV = 205, UI_MANNEQUIN = 206, UI_TOWERRESULT = 207, UI_TOWERRANK = 208, UI_AFREECTV_ONAIR = 209, UI_BUTTERFLY_EVENT = 210, UI_HONEY_POINT = 211, UI_EVENT_RANKING = 212, UI_JOURNAL_EVENT = 213, UI_USERLIST_DEBUG = 214, UI_HYPER_UPGRADE_RECIPE_DECOMPOSER = 215, UI_PARTY_INVITATION = 216, UI_SMARTPHONE = 217, UI_SPINOFF_CHAPTER = 218, UI_MAPLEFRIENDS_DUNGEONRESULT = 219, UI_GROWTH_HELPER_DETAIL = 220, UI_STARPLANET_MINIGAME_RESULT = 221, UI_STARPLANET_MATCHING = 222, UI_STARPLANET_INVITATION_WAITING = 223, UI_STAR_USERLIST = 224, UI_SELECT_RACE = 225, UI_THEMEWORLD_HUNDREDBINGO = 226, UI_THEMEWORLD_HUNDREDBINGO_RANK = 227, UI_THEMEWORLD_HUNDREDRPS = 228, UI_WORLD_TRANSFER_FOR_SHININGSTAR = 229, UI_STARPLANET_BENEFIT_LIST = 230, UI_STARPLANET_NOTICE_GRADE_UP = 231, UI_STARPLANET_NOTICE_GRADE_DOWN = 232, UI_STARPLANET_NOTICE_SHININGSTAR = 233, UI_STARPLANET_MY_INFO = 234, UI_STARPLANET_TRENDSHOP = 235, UI_STARPLANET_RANKING = 236, UI_STARPLANET_RANKING_STARRANK = 237, UI_STARPLANET_CHANNEL_SELECT = 238, UI_STARPLANET_SKILL = 239, UI_STARPLANET_MINIGAME_QUEUE = 240, UI_STARPLANET_MISSION_RPS = 241, UI_STARPLANET_MINIGAME_CONFIG = 242, UI_DIRECTION_INPUT = 243, UI_BLOCKBUSTER_BLACK_HEAVEN = 244, UI_TALKTAG = 245, UI_TALKADD = 246, UI_TALKINVITE = 248, UI_THEMEWORLD_GROUPDANCE = 249, UI_JAGUAR_MANAGEMENT = 250, UI_MAZE_MAP = 251, UI_BATTLEPVP_CHAMPSELECT = 252, UI_BATTLEPVP_PLAYSCORE = 253, UI_BATTLEPVP_RESULT = 254, UI_BATTLEPVP_CHAMPSTAT = 255, UI_BATTLEPVP_STATCOREBAR = 256, UI_BATTLEPVP_SKILLBAR = 257, UI_CONTENTS_STAMP_BOOK = 258, UI_THEMEWORLD_HUNDREDBINGO_SELECT = 259, UI_STARPLANET_STATICSTIC = 260, UI_TEMPORARY_SKILLBAR = 261, UI_SPINOFF_GUITAR_RHYTHMGAME = 262, UI_PLANT_POT = 263, UI_RANDOM_MISSION = 264, UI_MONSTERSTORY = 265, UI_TRESURE_SELECT = 266, UI_CONTENTSMAP = 267, UI_AWESOMIUM_WND = 268, UI_TRESURE_RESULT = 269, UI_ITEMCOLLECITON_12TH_EVENT = 270, UI_ITEMCOLLECITON_FAQ = 271, UI_SECRET_DIARY = 272, UI_PIGGYBAR_GAUGE = 273, UI_BINGSOO_POT = 274, UI_BINGSOO_POT_MINI = 275, UI_NEWBATTLERECORD = 276, UI_NEWBATTLERECORDMINI = 277, UI_TOADSHAMMER = 278, UI_BAG_CON1 = 279, UI_BAG_CON2 = 280, UI_BAG_CON3 = 281, UI_BAG_CON4 = 282, UI_LOGIN_BANNER = 283, UI_DOJANGRANKING = 284, UI_BAG7 = 285, UI_GHOSTPARK_ENTER = 286, UI_GHOSTPARK_EXP = 287, UI_URUS_ENTER_THE_MAP = 288, UI_URUS_SKILL_LIST = 289, UI_URUS_FIELD_SCORE = 290, UI_URUS_USER_SCORE = 291, UI_URUS_RESULT = 292, UI_URUS_SHOP = 293, UI_URUS_SHOP_SUB = 294, UI_URUS_ENTER_THE_MAP_PARTY = 295, UI_URUS_USER_LIST = 296, UI_JAGUAR_ACTION_BAR = 297, UI_RUNNERMINIGAME = 298, UI_MINIGAME_PINBALL_EXIT = 299, UI_SKILL_RP_KR = 300, UI_DAYILYGIFT = 1100, UI_HOM_EVENT = 1101, UI_TOWER_CHAIR = 1102, UI_BLOCKBUSTER_SELECT_UI = 1103, UI_BLOCKBUSTER_HOFM = 1104, UI_MONSTERCOLLECTION_NEW = 1105, UI_BAITBAG = 1106, UI_RW_CYLINDER = 1107, UI_TRADEKING_SHOP = 1108, UI_TRADEKING_INVEN = 1109, UI_NAMECHANGE = 1110, UI_SLIDE_PUZZLE = 1111, UI_PLATFORM_STAGE_SELECT = 1112, UI_PLATFORM_STAGE_LEAVE = 1113, UI_PLATFORM_STAGE_OXYZEN = 1114, UI_DEMIAN_FIELD_DEBUFF = 1115, UI_DISGUISE = 1116, UI_WND_NO = 1117, UI_MATRIX_MAIN = 1130, UI_MATRIX_UPGRADE = 1131, UI_NOT_DEFINED = -1,
        }

        public enum InventoryOperation : int
        {
            ADD = 0, UPDATE_QUANTITY = 1, MOVE = 2, REMOVE = 3, ITEM_EXP = 4, UPDATE_BAG_POS = 5, UPDATE_BAG_QUANTITY = 6, UNK_1 = 7, UPDATE_ITEM_INFO = 9, UNK_2 = 9, UNK_3 = 10,
        }

        public enum Message : int
        {
            DROP_PICKUP_MESSAGE = 0,
            QUEST_RECORD_MESSAGE = 1,
            QUEST_RECORD_MESSAGE_ADD_VALID_CHECK = 2,
            CASH_ITEM_EXPIRE_MESSAGE = 3,
            INC_EXP_MESSAGE = 4,
            INC_SP_MESSAGE = 5,
            INC_POP_MESSAGE = 6,
            INC_MONEY_MESSAGE = 7,
            INC_GP_MESSAGE = 8,
            INC_COMMITMENT_MESSAGE = 9,
            GIVE_BUFF_MESSAGE = 10,
            GENERAL_ITEM_EXPIRE_MESSAGE = 11,
            SYSTEM_MESSAGE = 12,
            QUEST_RECORD_EX_MESSAGE = 14,
            WORLD_SHARE_RECORD_MESSAGE = 15,
            ITEM_PROTECT_EXPIRE_MESSAGE = 16,
            ITEM_EXPIRE_REPLACE_MESSAGE = 17,
            ITEM_ABILITY_TIME_LIMITED_EXPIRE_MESSAGE = 18,
            SKILL_EXPIRE_MESSAGE = 19,
            INC_NON_COMBAT_STAT_EXP_MESSAGE = 20,
            LIMIT_NON_COMBAT_STAT_EXP_MESSAGE = 22,
            ANDROID_MACHINE_HEART_ALSET_MESSAGE = 24,
            INC_FATIGUE_BY_REST_MESSAGE = 25,
            INC_PVP_POINT_MESSAGE = 26,
            PVP_ITEM_USE_MESSAGE = 27,
            WEDDING_PORTAL_ERROR = 28,
            INC_HARDCORE_EXP_MESSAGE = 29,
            NOTICE_AUTO_LINE_CHANGED = 30,
            ENTRY_RECORD_MESSAGE = 31,
            EVOLVING_SYSTEM_MESSAGE = 32,
            EVOLVING_SYSTEM_MESSAGE_WITH_NAME = 33,
            CORE_INVEN_OPERATION_MESSAGE = 34,
            NX_RECORD_MESSAGE = 35,
            BLOCKED_BEHAVIOR_MESSAGE = 36,
            INC_WP_MESSAGE = 37,
            MAX_WP_MESSAGE = 38,
            STYLISH_KILL_MESSAGE = 39,
            BARRIER_EFFECT_IGNORE_MESSAGE = 40,
            EXPIRED_CASH_ITEM_RESULT_MESSAGE = 41,
            COLLECTION_RECORD_MESSAGE = 42,
            RANDOM_CHANCE_MESSAGE = 43,
            EXPIRED_QUEST_RESULT_MESSAGE = 44
        }

        public enum Stats : long
        {
            SKIN = 0x1,
            FACE = 0x2,
            HAIR = 0x4,
            LEVEL = 0x10,
            SUBJOB = 0x20,
            STR = 0x40,
            DEX = 0x80,
            INTE = 0x100,
            LUK = 0x200,
            HP = 0x400,
            MHP = 0x800,
            MP = 0x1000,
            MMP = 0x2000,
            AP = 0x4000,
            SP = 0x8000,
            EXP = 0x10000,
            POP = 0x20000,
            MONEY = 0x40000,
            FATIGUE = 0x80000,
            CHARISMA_EXP = 0x100000,
            INSIGHT_EXP = 0x200000,
            WILL_EXP = 0x400000,
            CRAFT_EXP = 0x800000,
            SENSE_EXP = 0x1000000,
            CHARM_EXP = 0x2000000,
            DAY_LIMIT = 0x4000000,
            ALBA_ACTIVITY = 0x8000000,
            CHARACTER_CARD = 0x10000000,
            PVP1 = 0x20000000,
            PVP2 = 0x40000000,
            EVENT_POINTS = 0x80000000
        }

        public enum UserEffectType : int
        {
            LevelUp = 0,
            SkillUse = 1,
            SkillUseBySummoned = 2,
            SkillAffected = 4,
            SkillAffected_Ex = 5,
            SkillAffected_Select = 6,
            SkillSpecialAffected = 7,
            Quest = 8,
            Pet = 9,
            SkillSpecial = 10,
            Resist = 11,
            ProtectOnDieItemUse = 12,
            PlayPortalSE = 13,
            JobChanged = 14,
            QuestComplete = 15,
            IncDecHPEffect = 16,
            BuffItemEffect = 17,
            SquibEffect = 18,
            MonsterBookCardGet = 19,
            LotteryUse = 20,
            ItemLevelUp = 21,
            ItemMaker = 22,
            ExpItemConsumed = 24,
            FieldExpItemConsumed = 25,
            ReservedEffect = 26,
            UnkAtm1 = 27,
            UpgradeTombItemUse = 28,
            BattlefieldItemUse = 29,
            UnkAtm2 = 30,
            AvatarOriented = 31,
            AvatarOrientedRepeat = 32,
            AvatarOrientedMultipleRepeat = 33,
            IncubatorUse = 34,
            PlaySoundWithMuteBGM = 35,
            PlayExclSoundWithDownBGM = 36,
            SoulStoneUse = 37,
            IncDecHPEffect_EX = 38,
            IncDecHPRegenEffect = 39,
            EffectUOL = 40,
            PvPRage = 41,
            PvPChampion = 42,
            PvPGradeUp = 43,
            PvPRevive = 44,
            JobEffect = 45,
            FadeInOut = 46,
            MobSkillHit = 47,
            AswanSiegeAttack = 48,
            BlindEffect = 49,
            BossShieldCount = 50,
            ResetOnStateForOnOffSkill = 51,
            JewelCraft = 52,
            ConsumeEffect = 53,
            PetBuff = 54,
            LotteryUIResult = 55,
            LeftMonsterNumber = 56,
            ReservedEffectRepeat = 57,
            RobbinsBomb = 58,
            SkillMode = 59,
            ActQuestComplete = 60,
            Point = 61,
            SpeechBalloon = 62,
            TextEffect = 63,
            SkillPreLoopEnd = 64,
            Aiming = 65,
            PickUpItem = 67,
            BattlePvP_IncDecHp = 68,
            BiteAttack_ReceiveSuccess = 69,
            BiteAttack_ReceiveFail = 70,
            IncDecHPEffect_Delayed = 71,
            Lightness = 72,
            SetUsed = 73
        }

        public enum InGameDirectionEventType : int
        {
            ForcedAction = 0,
            Delay = 1,
            EffectPlay = 2,
            ForcedInput = 3,
            PatternInputRequest = 4,
            CameraMove = 5,// automated send delay
            CameraOnCharacter = 6,
            CameraZoom = 7,// automated send delay
            CameraReleaseFromUserPoint = 8,
            VansheeMode = 9,
            FaceOff = 10,
            Monologue = 11,
            MonologueScroll = 12,
            AvatarLookSet = 13,
            RemoveAdditionalEffect = 14,
            // 15
            ForcedMove = 16,
            ForcedFlip = 17,
            InputUI = 18
            // 19
            // 20
            // 21
            // 22
        }

        public enum FieldEffectType : int
        {
            FromString = 0,
            Tremble = 1,
            ObjectStateByString = 2,
            DisableEffectObject = 3,
            Screen = 4,
            PlaySound = 5,
            MobHPTag = 6,
            ChangeBGM = 7,
            BGMVolumeOnly = 8,
            SetBGMVolume = 9,
            // 10
            // 11
            // 12
            // 13
            // 14
            RewardRoulette = 15,
            TopScreen = 16,
            BackScreen = 17,
            TopScreenEffect = 18,
            ScreenEffect = 19,
            ScreenFloatingEffect = 20,
            Blind = 21,
            SetGrey = 22,
            OnOffLayer = 23,
            OverlapScreen = 24,
            OverlapScreenDetail = 25,
            RemoveOverlapScreen = 26,
            ChangeColor = 27,
            StageClearExpOnly = 28,
            TopScreen_WithOrigin = 29,
            SpineScreen = 30,
            OffSpineScreen = 31
        }

        public enum ScriptMessageType : int
        {
            Say = 0,
            Say2 = 1,
            SayImage = 2,
            AskYesNo = 3,
            AskText = 4,
            AskNumber = 5,
            AskMenu = 6,
            InitialQuiz = 7,
            InitialSpeedQuiz = 8,
            IQQuiz = 9,
            AskAvatar = 10,
            AskAndroid = 11,
            // 12
            AskPet = 13,
            AskPetAll = 14,
            AskActionPetEvolution = 15,
            Script = 16,
            AskAccept = 17,
            // 18
            AskBoxText = 19,
            AskSlideMenu = 20,
            AskIngameDirection = 21,
            // 22
            PlayMovieClip = 23,
            AskCenter = 24,
            AskAvatar2 = 25,
            AskSelectMenu = 26,
            AskAngelicBuster = 27,
            SayIllustration = 28,
            SayDualIllustration = 29,
            AskYesNoIllustration = 30,
            AskAcceptIllustration = 31,
            AskMenuIllustration = 32,
            AskYesNoDualIllustration = 33,
            AskAcceptDualIllustration = 34,
            AskMenuDualIllustration = 35,
            AskSNN2 = 36,
            AskAvatarZero = 37,
            // 38
            // 39
            Monologue = 40,
            AskWeaponBox = 41,
            AskBoxTextBgImg = 42,
            AskUserSurvey = 43,
            SuccessCamera = 44,
            AskMixHair = 45,
            AskMixHairExZero = 46,
            OnAskCustomMixHair = 47,
            OnAskCustomMixHairAndProb = 48,
            OnAskMixHairNew = 49,
            OnAskMixHairNewExZero = 50,
            NpcAction = 51,
            OnAskScreenShinningStarMsg = 52,
            InputUI = 53,
            // 54
            OnAskNumberUseKeyPad = 55,
            OnSpinOffGuitarRhythmGame = 56,
            OnGhostParkEnter = 57
        }

        public enum ParamType : int
        {
            NotCancellable = 0x1,
            PlayerAsSpeaker = 0x2,
            OverrideSpeakerID = 0x4,
            FlipSpeaker = 0x8,
            PlayerAsSpeakerFlip = 0x10,
            BoxChat = 0x20,
            BoxChatAsPlayer = 0x22,
            BoxChatOverrideSpeaker = 0x24,
            FlipBoxChat = 0x28,
            FlipBoxChatAsPlayer = 0x30
        }

        public enum QuestRequestType : int
        {
            LostItem = 0,
            AcceptQuest = 1,
            CompleteQuest = 2,
            ResignQuest = 3,
            OpeningScript = 4,
            CompleteScript = 5,
            LaterStep = 6
        }
    }
}
