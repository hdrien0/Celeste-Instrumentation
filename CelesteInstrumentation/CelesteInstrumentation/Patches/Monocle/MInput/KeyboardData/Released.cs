using HarmonyLib;
using Monocle;
using System;
using Microsoft.Xna.Framework.Input;

namespace Instrumentation.Patches
{
    [HarmonyPatch(typeof(MInput.KeyboardData), "Released", new Type[] { typeof(Keys) })]
    class Monocle_MInput_KeyboardData_Released_Patch
    {
        [HarmonyPostfix]
        static bool Postfix(bool __result, Keys key)
        {
            return InputsManager.Released(key);
        }
    }
}
