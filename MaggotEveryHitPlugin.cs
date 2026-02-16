using BepInEx;
using HarmonyLib;

namespace MaggotEveryHitMod
{
    [BepInAutoPlugin(id: ModId, name: ModName, version: ModVersion)]
    public partial class MaggotEveryHitPlugin : BaseUnityPlugin
    {
        private const string ModId = "io.github.danatron1.MaggotEveryHitMod";
        private const string ModName = "MaggotEveryHit";
        private const string ModVersion = "1.0.0";

        internal static MaggotEveryHitPlugin instance;
        private readonly Harmony harmony = new(ModId);


        HeroController hc => HeroController.instance;

        private void Awake()
        {
            instance = this;
            ModSettings.Initialize(Config);

            harmony.PatchAll();

            ModHooks.TakeHealthHook += PlayerHit;

            Log($"Plugin {Name} {Version} initialized");
        }

        private static int PlayerHit(PlayerData data, int damage)
        {
            if (ModSettings.MaggotEveryHit && !instance.hc.cState.isMaggoted) instance.hc.SetIsMaggoted(true);
            return damage;
        }

        public static void Log(string message) => instance.Logger.LogInfo(message);
    }
}
