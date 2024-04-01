using HarmonyLib;
using Monocle;
using System;
using Microsoft.Xna.Framework.Input;

namespace Instrumentation.Patches
{
    [HarmonyPatch(typeof(MInput.KeyboardData), "Check", new Type[] { typeof(Keys) })]
    class Monocle_MInput_KeyboardData_Check_Patch
    {

        [HarmonyPostfix]
        static bool Postfix(bool __result, Keys key)
        {
            return InputsManager.Check(key);
        }
    }
}
