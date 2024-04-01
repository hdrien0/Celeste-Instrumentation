using HarmonyLib;
using Monocle;

namespace Instrumentation.Patches
{
    [HarmonyPatch(typeof(Engine), "RawDeltaTime", MethodType.Getter)]
    class Monocle_Engine_RawDeltaTime_Getter_Patch
    {
        [HarmonyPrefix]
        static bool Prefix()
        {
            return false;
        }

        [HarmonyPostfix]
        static float Postfix(float __result)
        {
            return 0.0166667f;
        }
    }
}
