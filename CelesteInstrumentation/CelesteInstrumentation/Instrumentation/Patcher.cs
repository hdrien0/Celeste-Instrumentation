using HarmonyLib;

namespace Instrumentation
{
    public class Patcher
    {
        public static void ApplyPatches()
        {
            Harmony harmony = new Harmony("CelesteInstrumentation");

            harmony.PatchAll();
        }
    }
}
