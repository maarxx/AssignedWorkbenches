using HarmonyLib;
using System.Reflection;
using Verse;

namespace AssignedWorkbenches
{
    [StaticConstructorOnStartup]
    class Main
    {
        static Main()
        {
            Log.Message("Hello from Harmony in scope: com.github.harmony.rimworld.maarx.assignedworkbenches");
            var harmony = new Harmony("com.github.harmony.rimworld.maarx.assignedworkbenches");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
