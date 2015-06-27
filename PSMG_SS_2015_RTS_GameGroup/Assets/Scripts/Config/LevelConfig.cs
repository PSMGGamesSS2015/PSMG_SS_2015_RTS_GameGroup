using Assets.Scripts.AssetReferences;

namespace Assets.Scripts.Config
{
    /// <summary>
    /// The LevelConfig class contains a static array of LevelConfigs that stores
    /// basic information about the individual levels such as the number of imps
    /// that can exist in the level at a time.
    /// </summary>

    public class LevelConfig  {

        public LevelConfig (int maxImps, float spawnInterval, string name, int[] maxProfessions) {
            this.MaxImps = maxImps;
            this.SpawnInterval = spawnInterval;
            this.Name = name;
            this.MaxProfessions = maxProfessions;
        }

        public int MaxImps { get; private set; }

        public float SpawnInterval { get; private set; }

        public string Name { get; private set; }

        public int[] MaxProfessions { get; private set; }

        /// <summary>
        /// This is a globally usable array of level configurations.
        /// </summary>
        public static LevelConfig[] Levels = 
        {                                 
            new LevelConfig(4, 4.0f, SceneReferences.TestScene, new[] {4,3,3,3,0,0,0,0}),
            new LevelConfig(4, 4.0f, SceneReferences.Level01Koboldingen, new[] {4,3,3,3,0,0,0,0})              
        };
    }
}
