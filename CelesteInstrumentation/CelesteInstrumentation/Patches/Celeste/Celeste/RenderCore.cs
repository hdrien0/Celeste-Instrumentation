using HarmonyLib;

namespace Instrumentation.Patches
{
	[HarmonyPatch(typeof(Celeste.Celeste), "RenderCore")]
	class Celeste_Celeste_RenderCore_Patch
	{
		[HarmonyPrefix]
		static bool Prefix()
		{
			if (Instrumentation.GlobalParameters.Headless)
			{
				return false;
			}
			return true;
		}
	}
}
