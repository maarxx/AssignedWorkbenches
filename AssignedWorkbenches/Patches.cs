using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using Verse;
using Verse.AI;

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

    // RimWorld.WorkGiver_DoBill
    //public override Job JobOnThing(Pawn pawn, Thing thing, bool forced = false)
    [HarmonyPatch(typeof(WorkGiver_DoBill))]
    [HarmonyPatch("JobOnThing")]
    class Patch_WorkGiver_DoBill_JobOnThing
    {
        static bool Prefix(WorkGiver_DoBill __instance, Pawn pawn, Thing thing, bool forced, Job __result)
        {
            CompAssignableToPawn awbc = thing.TryGetComp<AssignedWorkbenchesComp>();
            if (awbc != null)
            {
                List<Pawn> assignedPawns = awbc.AssignedPawnsForReading;
                if (assignedPawns.Count == 0)
                {
                    return true;
                }
                else if (assignedPawns.Contains(pawn))
                {
                    return true;
                }
                __result = null;
                return false;
            }
            return true;
        }
    }
}
