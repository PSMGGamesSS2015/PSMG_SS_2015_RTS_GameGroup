﻿using Assets.Scripts.AssetReferences;

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
            new LevelConfig(1, 0f, SceneReferences.MainMenu, new[] {0,0,0,0,0,0,0,0}, LevelType.Menu, new []{SoundReferences.MainTheme}),                                   
            new LevelConfig(SceneReferences.StarWarsIntro, LevelType.Narrative, new []{SoundReferences.StarWarsTheme}), 
            new LevelConfig(6, 4.0f, SceneReferences.Level01Koboldingen, new[] {0,10,10,10,0,0,0,0}, LevelType.InGame, new []{SoundReferences.MountainTheme}),           
            new LevelConfig(6, 4.0f, SceneReferences.Level02CherryTopMountains, new[] {10,10,10,10,10,10,10,0}, LevelType.InGame, new []{SoundReferences.MountainTheme}),
            new LevelConfig(6, 4.0f, SceneReferences.Level03CinnamonWood, new[] {10,10,10,10,10,0,10,0}, LevelType.InGame, new []{SoundReferences.ForestTheme}),
            new LevelConfig(6, 4.0f, SceneReferences.Level04TrollVillage, new[] {10,10,10,10,10,0,10,0}, LevelType.InGame, new []{SoundReferences.ForestTheme}),
            new LevelConfig(6, 4.0f, SceneReferences.Level05CastleGlazeArrival, new[] {10,10,10,10,10,0,10,0}, LevelType.InGame, new []{SoundReferences.CaveTheme}),
        };
    }
}
