using HarmonyLib;
using System;
using Celeste;

namespace Instrumentation.Patches
{
	[HarmonyPatch(typeof(SaveData), "TotalCassettes" , MethodType.Getter)]
	class Celeste_SaveData_TotalCassettes_Getter_Patch
	{
		[HarmonyPrefix]
		static bool Prefix()
		{
			return false;
		}

		[HarmonyPostfix]
		static int Postfix(int __result, SaveData __instance)
		{
			int result;
			try
			{
				int num = 0;
				for (int i = 0; i <= __instance.MaxArea; i++)
				{
					if (!AreaData.Get(i).Interlude && __instance.Areas[i].Cassette)
					{
						num++;
					}
				}
				result = num;
			}
			catch (Exception)
			{
				result = 0;
			}
			return result;
		}
	}
}
