using HarmonyLib;
using System.Reflection;
using Monocle;
using System;
using Celeste;

namespace Instrumentation.Patches
{
    [HarmonyPatch(typeof(SpotlightWipe), MethodType.Constructor, new Type[] { typeof(Scene), typeof(bool), typeof(Action) })]
    class Celeste_SpotlightWipe_Ctor_Patch
    {
        [HarmonyPrefix]
        static bool Prefix(SpotlightWipe __instance, ref Scene scene, ref bool wipeIn, ref Action onComplete)
        {
            typeof(SpotlightWipe).GetField("Duration", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(__instance, 0f);
            typeof(SpotlightWipe).GetField("Modifier", BindingFlags.Static | BindingFlags.Public)?.SetValue(null, 5f);
            return false;
        }
    }
}