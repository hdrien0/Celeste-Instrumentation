using HarmonyLib;
using System.Reflection;
using Monocle;
using Celeste;

namespace Instrumentation.Patches
{
    [HarmonyPatch(typeof(LevelLoader), "StartLevel")]
    class Celeste_LevelLoader_StartLevel_Patch
    {
        [HarmonyPrefix]
        static bool Prefix(LevelLoader __instance)
        {
            typeof(LevelLoader).GetField("started", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(__instance, true);

            __instance.Level.LoadLevel(Player.IntroTypes.None, true);
            __instance.Level.Session.JustStarted = false;
            if (Engine.Scene == __instance)
            {
                Engine.Scene = __instance.Level;
            }
            return false;
        }
    }
}