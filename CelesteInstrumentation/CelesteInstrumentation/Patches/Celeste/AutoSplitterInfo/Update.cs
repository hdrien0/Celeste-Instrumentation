using HarmonyLib;
using Celeste;

namespace Instrumentation.Patches
{
	[HarmonyPatch(typeof(AutoSplitterInfo), "Update")]
	class Celeste_AutoSplitterInfo_Update_Patch
	{
		[HarmonyPrefix]
		static bool Prefix(AutoSplitterInfo __instance)
		{
			return false;
		}
	}
}

