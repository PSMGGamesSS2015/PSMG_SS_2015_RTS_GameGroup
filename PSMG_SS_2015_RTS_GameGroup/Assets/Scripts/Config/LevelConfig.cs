using Assets.Scripts.AssetReferences;

namespace Assets.Scripts.Config
{
    /// <summary>
    /// The LevelConfig class contains a static array of LevelConfigs that stores
    /// basic information about the individual levels such as the number of imps
    /// that can exist in the level at a time.
    /// </summary>

    public class LevelConfig  {

        public enum LevelType
        {
            Menu,
            InGame,
            Narrative
        }

        public LevelConfig (int maxImps, float spawnInterval, string name, int[] maxProfessions, LevelType levelType) {
            MaxImps = maxImps;
            SpawnInterval = spawnInterval;
            Name = name;
            MaxProfessions = maxProfessions;
            Type = levelType;
        }

        public int MaxImps { get; private set; }

        public float SpawnInterval { get; private set; }

        public string Name { get; private set; }

        public int[] MaxProfessions { get; private set; }

        public LevelType Type { get; private set; }

        /// <summary>
        /// This is a globally usable array of level configurations.
        /// </summary>
        public static LevelConfig[] Levels = 
        {               
            new LevelConfig(1, 0f, SceneReferences.Level00MainMenu, new[] {0,0,0,0,0,0,0,0}, LevelType.Menu),                                   
            new LevelConfig(6, 4.0f, SceneReferences.Level01Koboldingen, new[] {0,10,0,0,0,0,0,0}, LevelType.InGame),           
            new LevelConfig(6, 4.0f, SceneReferences.Level02CherryTopMountains, new[] {10,10,10,10,0,0,0,0}, LevelType.InGame),
            new LevelConfig(6, 4.0f, SceneReferences.Level03CinnamonWood, new[] {10,10,10,10,10,0,10,0}, LevelType.InGame),
            new LevelConfig(6, 4.0f, SceneReferences.Level04TrollVillage, new[] {10,10,10,10,10,0,10,0}, LevelType.InGame),
        };
    }
}
