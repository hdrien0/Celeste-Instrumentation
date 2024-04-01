namespace Instrumentation
{
   public static class Entry
   {
        public static void Execute()
        {
            try
            {
                Instrumentation.Initialize();
                if (Instrumentation.GlobalParameters.Enabled)
                {
                    Patcher.ApplyPatches();
                }
            }
            catch {}
        }
    }
}
