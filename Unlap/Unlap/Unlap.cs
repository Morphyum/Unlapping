using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Unlap
{
    public class Unlap
    {
        public static void Init() {
            var harmony = HarmonyInstance.Create("de.morphyum.Unlapping");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
