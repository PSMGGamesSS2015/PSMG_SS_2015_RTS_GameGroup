using Assets.Scripts.AssetReferences;

namespace Assets.Scripts.Config
{
    /// <summary>
    /// The LevelConfig class contains a static array of LevelConfigs that stores
    /// basic information about the individual levels such as the number of imps
    /// that can exist in the level at a time.
    /// </summary>

    public class LevelConfig  {
        private readonly LevelType levelType;

        public enum LevelType
        {
            Menu,
            InGame,
            Narrative
        }

        public LevelConfig(string name, LevelType levelType, string[] playList)
        {
            Name = name;
            Type = levelType;
            PlayList = playList;
        }

        public LevelConfig(int maxImps, float spawnInterval, string name, int[] maxProfessions, LevelType levelType, string[] playList)
        {
            MaxImps = maxImps;
            SpawnInterval = spawnInterval;
            Name = name;
            MaxProfessions = maxProfessions;
            Type = levelType;
            PlayList = playList;
        }

        public int MaxImps { get; private set; }

        public float SpawnInterval { get; private set; }

        public string Name { get; private set; }

        public int[] MaxProfessions { get; private set; }

        public LevelType Type { get; private set; }

        public string[] PlayList { get; set; }

        /// <summary>
        /// This is a globally usable array of level configurations.
        /// </summary>
        public static LevelConfig[] Levels = 
        {               
            new LevelConfig(0, 0f, SceneReferences.MainMenu, new[] {0,0,0,0,0,0}, LevelType.Menu, new []{SoundReferences.MainTheme}),
            new LevelConfig(SceneReferences.StarWarsIntro, LevelType.Narrative, new []{SoundReferences.StarWarsTheme}), 
            new LevelConfig(6, 4.0f, SceneReferences.Level01Koboldingen, new[] {0,6,0,0,0,0}, LevelType.InGame, new []{SoundReferences.MountainTheme, SoundReferences.ForestTheme, SoundReferences.KoboldingenTheme}),           
            new LevelConfig(6, 4.0f, SceneReferences.Level02CherryTopMountains, new[] {1,6,3,1,2,1}, LevelType.InGame, new []{SoundReferences.MountainsAtNightTheme, SoundReferences.MountainTheme, SoundReferences.ForestTheme, SoundReferences.CaveTheme}),
            new LevelConfig(6, 4.0f, SceneReferences.Level05CastleGlazeArrival, new[] {1,6,10,10,10,0}, LevelType.InGame, new []{SoundReferences.KoboldingenTheme, SoundReferences.ForestTheme, SoundReferences.MountainTheme}),
            new LevelConfig(6, 4.0f, SceneReferences.Level06CastleGlazeDungenon, new[] {1,6,0,1,1,0,0}, LevelType.InGame, new []{SoundReferences.CaveTheme, SoundReferences.KoboldingenTheme, SoundReferences.MountainTheme}),
        };
    }
}
