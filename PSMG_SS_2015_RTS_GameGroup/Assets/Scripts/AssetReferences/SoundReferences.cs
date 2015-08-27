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
        public const string BushBurn = "Sounds/FX_busch_abbrennen";
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
        public const string Puddle2 = "Sounds/FX_pfuetze2";
        public const string Puddle3 = "Sounds/FX_pfuetze3";
        public const string ShipCanon = "Sounds/FX_schiffskanone";
        public const string DoorOpen = "Sounds/FX_tuer_oeffnen";
        public const string DoorOpen_II = "Sounds/Tuer_oeffnen_II";
        public const string WallTrigger = "Sounds/FX_wandschalter";
        public const string WaterDrops1 = "Sounds/Tropfen1";
        public const string WaterDrops2 = "Sounds/Tropfen2";
        public const string WaterDrops3 = "Sounds/Tropfen3";
        public const string WaterDrops4 = "Sounds/Tropfen4";
        public const string WaterDrops5 = "Sounds/Tropfen5";
        public const string TorchFireV1 = "Sounds/feuerrascheln_carolin";
        public const string TorchFireV2 = "Sounds/feuerrascheln_v2";
        public const string MetallDrum = "Metalltrommel";
        public const string FireBurning = "Rascheln";
        public const string Walking = "Ritter_Gehen";
        public const string HeavyWalking = "Schweres_Gehen";
        public const string UI_Feedback = "Feedback";

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

        public const string TrollAttack1 = "Troll/Troll_attack1";
        public const string TrollAttack2 = "Troll/Troll_attack2";
        public const string TrollAttack3 = "Troll/Troll_attack3";
        public const string TrollDeath = "Troll/Troll_death";
        public const string TrollComment1 = "Troll/Troll_draufhaun";
        public const string TrollComment2 = "Troll/Troll_oarr";
        public const string TrollComment3 = "Troll/Troll_oeoea";
        public const string TrollComment4 = "Troll/Troll_rahahaha";
        public const string TrollComment5 = "Troll/Troll_uooa";
        public const string TrollComment6 = "Troll/Troll_wooaa";

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

        public const string KnightAttack1 = "Knight/Knight_attack1";
        public const string KnightAttack2 = "Knight/Knight_attack2";
        public const string KnightAttack3 = "Knight/Knight_attack3";
        public const string KnightDeath = "Knight/Knight_death";
        public const string KnightEnGarde = "Knight/Knight_en_garde";
        public const string KnightHA = "Knight/Knight_ha";
        public const string KnightHallo = "Knight/Knight_halloo";
        public const string KnightHohoho = "Knight/Knight_hohoho";
        public const string KnightHohoo = "Knight/Knight_hohoo";
        public const string KnightOuioui = "Knight/Knight_ouiouioui";
        public const string KnightUiui = "Knight/Knight_uiuiui";
        public const string KnightWennIchDarf = "Knight/Knight_wenn_ich_bitten_darf";

        #endregion

        #region Soundausgabe Level 1

        public const string SoundLvl1_01 = "Sprachausgabe/Level_1/1_01_Level_01_gestartet";
        public const string SoundLvl1_02 = "Sprachausgabe/Level_1/1_02_Kobold_auswaehlen";
        public const string SoundLvl1_03 = "Sprachausgabe/Level_1/1_03_Beruf_zuweisen";
        public const string SoundLvl1_04 = "Sprachausgabe/Level_1/1_04_Feigling_ausgebildet";
        public const string SoundLvl1_05 = "Sprachausgabe/Level_1/1_05_Beide_Haengebruecken_ueberquert";
        public const string SoundLvl1_07 = "Sprachausgabe/Level_1/1_07_Leitern_gefunden";
        public const string SoundLvl1_08 = "Sprachausgabe/Level_1/1_08_Felsbrocken_erreicht";
        public const string SoundLvl1_09 = "Sprachausgabe/Level_1/1_09_Waffen_gefunden";
        public const string SoundLvl1_10 = "Sprachausgabe/Level_1/1_10_Troll_gesichtet";
        public const string SoundLvl1_11 = "Sprachausgabe/Level_1/1_11_Karte_erfolgreich_abgeschlossen";

        #endregion

        #region Soundausgabe Level 2

        public const string SoundLvl2_01 = "Sprachausgabe/Level_2/2_01_Level02_gestartet";
        public const string SoundLvl2_02 = "Sprachausgabe/Level_2/2_02_Dunkelheit";
        public const string SoundLvl2_03 = "Sprachausgabe/Level_2/2_03_Weiter_Abgrund";
        public const string SoundLvl2_04 = "Sprachausgabe/Level_2/2_04_Hanteln_gefunden";
        public const string SoundLvl2_05 = "Sprachausgabe/Level_2/2_05_Brummwespe";
        public const string SoundLvl2_06 = "Sprachausgabe/Level_2/2_06_Levelende_mit_einem_Kobold_erreicht";
        public const string SoundLvl2_07 = "Sprachausgabe/Level_2/2_07_Kruemelspur_gefunden";

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
        public const string SoundLvl6_05_KnightEatingCake = "Sprachausgabe/Level_6/6_05_Tor_geht_auf_Ritter_holt_sich_Kuchen_und_isst";
        public const string SoundLvl6_06 = "Sprachausgabe/Level_6/6_06_Imps_sind_bei_der_Haengebruecke";
        public const string SoundLvl6_07 = "Sprachausgabe/Level_6/6_07_Imp_befreien_Prinzessin";
        public const string SoundLvl6_08 = "Sprachausgabe/Level_6/6_08_Koenig_springt_aus_Torte";

        #endregion

        #region Soundausgabe Level 7

        public const string SoundLvl7_01 = "Sprachausgabe/Level_7/7_01_Imps_kommen_im_Dorf_an";

        #endregion
    }
}