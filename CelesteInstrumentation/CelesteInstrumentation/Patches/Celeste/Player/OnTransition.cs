using HarmonyLib;
using Celeste;

namespace Instrumentation.Patches
{
	[HarmonyPatch(typeof(Player), "OnTransition")]
	class Celeste_Player_OnTransition_Patch
	{
		[HarmonyPrefix]
		static void Prefix(Player __instance)
		{
			Instrumentation.OnTransition(__instance);
		}
	}
}

