using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phwang.GlobalVariables
{
    public class GlobalVariableClass
    {
        static public FrontEnd.FrontEndRootClass frontEndRootObject { get; set; }
        static public Fabric.FabricRootClass fabricRootObject { get; set; }
        static public Theme.ThemeRootClass themeRootObject { get; set; }
        static public Engine.EngineRootClass engineRootObject { get; set; }
        static public Models.ModelRootClass modelRootObject { get; set; }

        public static void Initilization()
        {
            if (frontEndRootObject == null)
            {
                fabricRootObject = new Fabric.FabricRootClass();
                frontEndRootObject = new FrontEnd.FrontEndRootClass();
                themeRootObject = new Theme.ThemeRootClass();
                engineRootObject = new Engine.EngineRootClass();

                modelRootObject = new Models.ModelRootClass();
            }
        }

        public static FrontEnd.FrontEndRootClass getGoRoot()
        {
            Initilization();
            return frontEndRootObject;
        }
    }
}
