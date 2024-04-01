using HarmonyLib;
using Celeste;

namespace Instrumentation.Patches
{
	[HarmonyPatch(typeof(Player), "Added")]
	class Celeste_Player_Added_Patch
	{
		[HarmonyPrefix]
		static void Prefix(Player __instance)
		{
			InputsManager.Initialize();
			__instance.Components.Add(new PlayerStateTracker(__instance));
		}
	}
}