using HarmonyLib;
using Monocle;
using System;
using Microsoft.Xna.Framework.Input;

namespace Instrumentation.Patches
{
    [HarmonyPatch(typeof(MInput.KeyboardData), "Pressed", new Type[] { typeof(Keys) })]
    class Monocle_MInput_KeyboardData_Pressed_Patch
    {

        [HarmonyPostfix]
        static bool Postfix(bool __result, Keys key)
        {
            return InputsManager.Pressed(key);
        }
    }
}