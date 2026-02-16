using HarmonyLib;
using System;

namespace MaggotEveryHitMod;

[HarmonyPatch]
internal static class ModHooks
{
    public static event Func<PlayerData, int, int>? TakeHealthHook;

    [HarmonyPatch(typeof(PlayerData), nameof(PlayerData.TakeHealth))]
    [HarmonyPrefix]
    private static void PlayerData_TakeHealth(PlayerData __instance, ref int amount, ref bool hasBlueHealth, ref bool allowFracturedMaskBreak)
    {
        if (TakeHealthHook != null)
        {
            amount = TakeHealthHook.Invoke(__instance, amount);
        }
    }

    [HarmonyPatch(typeof(UnMaggotRegion), nameof(UnMaggotRegion.Start))]
    [HarmonyPrefix]
    private static void UnMaggotRegion_Start(UnMaggotRegion __instance)
    {
        if (!ModSettings.AllWaterMaggotWater) return;
        __instance.maggotRegion = __instance.gameObject.AddComponent<MaggotRegion>();
    }

    [HarmonyPatch(typeof(MaggotRegion), nameof(MaggotRegion.Start))]
    [HarmonyPrefix]
    private static void MaggotRegion_Start(MaggotRegion __instance)
    {
        if (!ModSettings.AllWaterMaggotWater) return;
        __instance.overrideActive = new TeamCherry.SharedUtils.OverrideBool
        {
            IsEnabled = true,
            Value = true
        };
    }
}
