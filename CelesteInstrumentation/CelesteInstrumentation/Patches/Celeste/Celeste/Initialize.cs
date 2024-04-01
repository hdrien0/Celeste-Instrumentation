using System;
using HarmonyLib;

namespace Instrumentation.Patches
{
	[HarmonyPatch(typeof(Celeste.Celeste), "Initialize")]
	class Celeste_Celeste_Initialize_Patch
	{
		[HarmonyPrefix]
		static void Prefix(Celeste.Celeste __instance)
		{
			Celeste.Celeste.PlayMode = Celeste.Celeste.PlayModes.Debug;
			__instance.TargetElapsedTime = TimeSpan.FromTicks(166667L / (long)Instrumentation.GlobalParameters.GamespeedMultiplier);
		}
	}
}