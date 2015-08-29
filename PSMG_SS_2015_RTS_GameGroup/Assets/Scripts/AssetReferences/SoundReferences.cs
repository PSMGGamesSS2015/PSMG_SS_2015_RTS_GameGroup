using System.Diagnostics.CodeAnalysis;

namespace Assets.Scripts.AssetReferences
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SoundReferences
    {
        #region Theme

        public const string BackgroundTheme = "Theme/BackgroundTheme";
        public const string MainTheme = "Theme/MainTheme";
        public const string WonTheme = "Theme/WonLevel";
        public const string ForestTheme = "Theme/ForestTheme";
        public const string CaveTheme = "Theme/CaveTheme";
        public const string MountainTheme = "Theme/MountainTheme";
        public const string MountainsAtNightTheme = "Theme/MountainsAtNightTheme";
        public const string StarWarsTheme = "Theme/StarWarsTheme";
        public const string KoboldingenTheme = "Theme/KoboldingenTheme";

        #endregion

        #region Sounds

        public const string Bodenschalter = "Sounds/FX_bodenschalter";
        public const string BombExplosion = "Sounds/FX_bombe_explodiert";
        public const string BushRustle1 = "Sounds/FX_buschrascheln1";
        public const string BushRustle2 = "Sounds/FX_buschrascheln2";
        public const string TorchLight1 = "Sounds/FX_fackel_entzuenden1";
        public const string TorchLight2 = "Sounds/FX_fackel_entzuenden2";
        public const string ShieldWood1 = "Sounds/FX_holzschild1";
        public const string ShieldWood2 = "Sounds/FX_holzschild2";
        public const string ShieldMetal1 = "Sounds/FX_metallschild1";
        public const string ShieldMetal2 = "Sounds/FX_metallschild2";
        public const string ShieldMetal3 = "Sounds/FX_metallschild3";
        public const string Puddle1 = "Sounds/FX_pfuetze1";
        public const string ShipCanon = "Sounds/FX_schiffskanone";
        public const string DoorOpen = "Sounds/Tuer_oeffnen_II";
        public const string WallTrigger = "Sounds/FX_wandschalter";
        public const string TorchFireV1 = "Sounds/feuerrascheln_carolin";
        public const string TorchFireV2 = "Sounds/feuerrascheln_v2";
        public const string MetallDrum = "Sounds/Metalltrommel";
        public const string FireBurning = "Sounds/Rascheln";
        public const string Walking = "Sounds/Ritter_Gehen";
        public const string UI_Feedback = "Sounds/Feedback";

        #endregion

        #region Imp

        public const string ImpGoing = "Imp/FX_imp_schlendern";
        public const string ImpSetupLadder = "Imp/FX_leiter_aufstellen";
        public const string ImpAttack1 = "Imp/FX_speerstich1";
        public const string ImpAttack2 = "Imp/FX_speerstich2";
        public const string ImpWork = "Imp/Goblins_arbeit";
        public const string ImpDamage = "Imp/Goblins_aua";
        public const string ImpDeath = "Imp/Goblins_death";

        public static readonly string[] ImpCommantVariants =
        {
            "Imp/Goblins_hach",
            "Imp/Goblins_hihihi",
            "Imp/Goblins_hmm",
            "Imp/Goblins_hmmhehe"
        };

        public static readonly string[] ImpSelectedVariants =
        {
            "Imp/Goblins_ich",
            "Imp/Goblins_ihr_wuenscht",
            "Imp/Goblins_ja_2",
            "Imp/Goblins_ja_meister",
            "Imp/Goblins_ja",
            "Imp/Goblins_jiaa",
            "Imp/Goblins_mmmn_2",
            "Imp/Goblins_mmmn",
            "Imp/Goblins_was_begehrt_ihr",
            "Imp/Goblins_was_gibts 2",
            "Imp/Goblins_was_gibts",
            "Imp/Goblins_was_liegt an"
        };

        #endregion

        #region Troll

        public static readonly string[] TrollAttackVariants =
        {
            "Troll/Troll_attack1",
            "Troll/Troll_attack2",
            "Troll/Troll_attack3"
        };

        public const string TrollDeath = "Troll/Troll_death";

        public static readonly string[] TrollSelectedVariants
            =
        {
            "Troll/Troll_draufhaun",
            "Troll/Troll_oarr",
            "Troll/Troll_oeoea",
            "Troll/Troll_rahahaha",
            "Troll/Troll_uooa",
            "Troll/Troll_wooaa"
        };

        #endregion

        #region Dragon

        public const string DragonWing1 = "Dragon/Dragon_fluegel1";
        public const string DragonWing2 = "Dragon/Dragon_fluegel2";
        public const string DragonDeath = "Dragon/Dragon_death";
        public const string DragonBurr = "Dragon/Dragon_burrr";

        public static readonly string[] DragonSelectedVariants =
        {
            "Dragon/Dragon_horr",
            "Dragon/Dragon_mhm",
            "Dragon/Dragon_mhmn",
            "Dragon/Dragon_brrr",
            "Dragon/Dragon_attack1",
            "Dragon/Dragon_attack2",
            "Dragon/Dragon_attack3"
        };

        public const string DragonComment6 = "Dragon/Dragon_schnief1";
        public const string DragonComment7 = "Dragon/Dragon_schnief2";
        public const string DragonCry = "Dragon/Dragon_schnief3";
        public const string DragonWoahh = "Dragon/Dragon_woaah";
        public const string DragonComment10 = "Dragon/Dragon_woooah_lang";

        #endregion

        #region King

        public const string KingAttack1 = "King/King_attack1";
        public const string KingAttack2 = "King/King_attack2";
        public const string KingAttack3 = "King/King_attack3";
        public const string KingDeath = "King/King_death";
        public const string KingSpeech = "King/King_bis_zu_mir";

        #endregion

        #region Knight

        public static readonly string[] KnightDeadVariants =
        {
            "Knight/Knight_death",
            "Knight/Knight_uiuiui"
        };

        public static readonly string[] KnightAttackVariants =
        {
            "Knight/Knight_attack1",
            "Knight/Knight_attack2",
            "Knight/Knight_attack3"
        };

        public static readonly string[] KnightSelectedVariants =
        {
            "Knight/Knight_ha",
            "Knight/Knight_halloo",
            "Knight/Knight_hohoho",
            "Knight/Knight_hohoo",
            "Knight/Knight_ouiouioui"
        };

        public static readonly string[] KnightBeingAttackedVariants =
        {
            "Knight/Knight_en_garde",
            "Knight/Knight_wenn_ich_bitten_darf"
        };

        #endregion

        #region Soundausgabe Level 1

        public const string SoundLvl1_01_LevelStarted = "Sprachausgabe/Level_1/1_01_Level_01_gestartet";
        public const string SoundLvl1_02 = "Sprachausgabe/Level_1/1_02_Kobold_auswaehlen";
        public const string SoundLvl1_03_AssignProfession = "Sprachausgabe/Level_1/1_03_Beruf_zuweisen";
        public const string SoundLvl1_04_CowardTrained = "Sprachausgabe/Level_1/1_04_Feigling_ausgebildet";

        public const string SoundLvl1_05_SuspensionBridgesCrossed =
            "Sprachausgabe/Level_1/1_05_Beide_Haengebruecken_ueberquert";

        public const string SoundLvl1_07 = "Sprachausgabe/Level_1/1_07_Leitern_gefunden";
        public const string SoundLvl1_08 = "Sprachausgabe/Level_1/1_08_Felsbrocken_erreicht";
        public const string SoundLvl1_09 = "Sprachausgabe/Level_1/1_09_Waffen_gefunden";
        public const string SoundLvl1_10 = "Sprachausgabe/Level_1/1_10_Troll_gesichtet";
        public const string SoundLvl1_11 = "Sprachausgabe/Level_1/1_11_Karte_erfolgreich_abgeschlossen";

        #endregion

        #region Soundausgabe Level 2

        public const string SoundLvl2_01 = "Sprachausgabe/Level_2/2_01_Level02_gestartet";
        public const string SoundLvl2_02 = "Sprachausgabe/Level_2/2_02_Dunkelheit";
        public const string SoundLvl2_04 = "Sprachausgabe/Level_2/2_04_Hanteln_gefunden";
        public const string SoundLvl2_05 = "Sprachausgabe/Level_2/2_05_Brummwespe";

        #endregion

        #region Soundausgabe Level 3

        #endregion

        #region Soundausgabe Level 4

        #endregion

        #region Soundausgabe Level 5

        public const string SoundLvl5_01 = "Sprachausgabe/Level_5/5_01_Spieler_betritt_die_Karte";
        public const string SoundLvl5_02 = "Sprachausgabe/Level_5/5_02_Spieler_steht_an_verschlossenem_Tor";
        public const string SoundLvl5_03 = "Sprachausgabe/Level_5/5_03_Imp_sammelt_Fackeln_auf";
        public const string SoundLvl5_04 = "Sprachausgabe/Level_5/5_04_Imp_sammelt_Zaubertrank_auf";
        public const string SoundLVl5_05 = "Sprachausgabe/Level_5/5_05_Kanone_feuert";

        #endregion

        #region Soundausgabe Level 6

        public const string SoundLvl6_01 = "Sprachausgabe/Level_6/6_01_Spieler_betritt_kleines_Backstuebchen";
        public const string SoundLvl6_02 = "Sprachausgabe/Level_6/6_02_Tuer_Sprengen_fehlgeschlagen";
        public const string SoundLvl6_03 = "Sprachausgabe/Level_6/6_03_Kuchen_fast_fertig";
        public const string SoundLvl6_04_KnightSaliva = "Sprachausgabe/Level_6/6_04_Kuchen_fertig";

        public const string SoundLvl6_05_KnightEatingCake =
            "Sprachausgabe/Level_6/6_05_Tor_geht_auf_Ritter_holt_sich_Kuchen_und_isst";

        public const string SoundLvl6_06 = "Sprachausgabe/Level_6/6_06_Imps_sind_bei_der_Haengebruecke";
        public const string SoundLvl6_07 = "Sprachausgabe/Level_6/6_07_Imp_befreien_Prinzessin";
        public const string SoundLvl6_08 = "Sprachausgabe/Level_6/6_08_Koenig_springt_aus_Torte";

        #endregion

        #region Soundausgabe Level 7

        public const string SoundLvl7_01 = "Sprachausgabe/Level_7/7_01_Imps_kommen_im_Dorf_an";

        #endregion
    }
}