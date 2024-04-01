using HarmonyLib;
using Monocle;

namespace Instrumentation.Patches
{
	[HarmonyPatch(typeof(Celeste.OverworldLoader), "Begin")]
	class Celeste_OverworldLoader_Begin_Patch
	{
		[HarmonyPrefix]
		static bool Prefix()
		{
			Socket.Initialize();
			Engine.Scene = new WaitingScene();
			return false;
		}
	}
}
