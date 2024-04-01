using HarmonyLib;
using Celeste;

namespace Instrumentation.Patches
{
	[HarmonyPatch(typeof(Player), "Die")]
	class Celeste_Player_Die_Patch
	{
		[HarmonyPrefix]
		static bool Prefix(Player __instance)
		{
			if (Instrumentation.SessionParameters.EndOnDeath)
			{
				Instrumentation.EndSession(__instance);
				return false;
			}
			return true;
		}
	}
}