using Harmony;
using MM2;
using TMPro;
using UnityEngine.UI;

namespace Unlap
{
    class Patches
    {
        static string version = " +UL-0.2";
        [HarmonyPatch(typeof(SetUITextToVersionNumber), "Awake")]
        public static class SetUITextToVersionNumber_Awake_Patch
        {
            public static void Postfix(SetUITextToVersionNumber __instance) {
                Text component = __instance.GetComponent<Text>();
                if (component != null) {
                    component.text += version;
                }
                TextMeshProUGUI component2 = __instance.GetComponent<TextMeshProUGUI>();
                if (component2 != null) {
                    component2.text += version;
                }
            }
        }

        [HarmonyPatch(typeof(AISafetyFlagBehaviour), "SimulationUpdate")]
        public static class AISafetyFlagBehaviour_SimulationUpdate_Patch
        {
            public static void Postfix(AISafetyFlagBehaviour __instance, ref Vehicle ___mVehicle, ref RacingVehicle ___mRacingVehicle, ref bool ___mCanOverTake) {
                if (__instance.isInSafetyTrain && (___mRacingVehicle.GetLapsBehindLeader() > 0 || ___mRacingVehicle.timer.lap < ___mRacingVehicle.leader.timer.lap)) {
                    Vehicle safetyVehicle = Game.instance.vehicleManager.safetyVehicle;
                    SafetyVehicle safetyVehicle2 = safetyVehicle as SafetyVehicle;
                    safetyVehicle2.FlashGreenLights();
                    ___mCanOverTake = true;
                    ___mRacingVehicle.behaviourManager.SetCanAttackVehicle(true);
                    ___mVehicle.speedManager.GetController<SafetyCarSpeedController>().topSpeed = GameUtility.MilesPerHourToMetersPerSecond(200f);
                }
            }
        }
    }
}
