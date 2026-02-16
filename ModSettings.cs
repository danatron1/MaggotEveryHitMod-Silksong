using BepInEx.Configuration;

namespace MaggotEveryHitMod
{
    public static class ModSettings
    {
        public static bool AllWaterMaggotWater;
        public static bool MaggotEveryHit;

        public static void Initialize(ConfigFile config)
        {
            AllWaterMaggotWater = config.Bind("Settings", "All water is maggot water", true, "Replace all normal water with maggot water?").Value;
            MaggotEveryHit = config.Bind("Settings", "Maggot every hit", true, "Inflict the maggot debuff whenever the player takes damage").Value;
        }
    }
}
