using HarmonyLib;

namespace ScalingPatch
{
    [HarmonyPatch(typeof(PatchQuest.Scaling))]
    [HarmonyPatch(nameof(PatchQuest.Scaling.BlasterDamageScale))]
    [HarmonyPatch(MethodType.Getter)]
    public static class BlasterDamageScalePatch
    {
        public static void Postfix(ref float __result)
        {
            __result *= PatchQuestTrainer.Settings.blasterDamageScaling;
        }
    }
}
