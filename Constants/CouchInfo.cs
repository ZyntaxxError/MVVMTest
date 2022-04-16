using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.Constants
{
    internal static class CouchInfo
    {
        /// <summary>
        /// Default assigned HU for internal couch structure of "Exact IGRT Couch"
        /// </summary>
        internal const int INTERNAL_HU = -1000;
        /// <summary>
        /// Default assigned HU for external couch structure of "Exact IGRT Couch"
        /// </summary>
        internal const int EXTERNAL_HU = -300;
        internal const string InternalShortId = "CouchInt";
        internal const string ExternalShortId = "CouchSurf";
        internal const string TopModel = "Exact IGRT Couch";
        internal const string TopModelStrictAddOn = "thin";

        internal const string TopModelThinId = "Exact_IGRT_Couch_Top_thin"; // Id for adding thin couch when auto planning
        internal const string TopModelMediumId = "Exact_IGRT_Couch_Top_medium"; // Id for adding medium couch when auto planning
        internal const string TopModelThickId = "Exact_IGRT_Couch_Top_thick"; // Id for adding thick couch when auto planning
        internal const double COUCH_LIMIT_MAX_LONG = 1605;



        public enum AutoPlanCouchTop
        {
            Exact_IGRT_Couch_Top_thin,
            Exact_IGRT_Couch_Top_medium,
            Exact_IGRT_Couch_Top_thick
        }

        internal const int ExternalWidthMm = 530;
        internal const int InternalWidthMm = 520;
        /// <summary>
        /// External thickness of couch top model "Exact IGRT Couch Top, Thin"
        /// </summary>
        internal const int ThinExternalThicknessMm = 50;

        internal const int MediumExternalThicknessMm = 62;  // probably 62.5

        internal const int ThickExternalThicknessMm = 75;
        /// <summary>
        internal const int ThinInternalThicknessMm = 42;
        internal const int MediumInternalThicknessMm = 54;  // probably 42.5
        internal const int ThickInternalThicknessMm = 67;

        internal const int GeometricToleranceMm = 1;
        internal const int ShellThicknessTop = 4;
        internal const double ShellThicknessLat = 4.5;


        /// <summary>
        /// Enum of index positions for couch top "Exact IGRT Couch" and the couch 
        /// long coordinate in cm when respective index is positioned at isocenter.<br/>
        /// Example: int lng = (int)CouchInfo.CouchIndexLongCoordAtIsoCm.H2;
        /// </summary>
        public enum CouchLongCoord
        {
            MechanicalMin = 155,
            End = 670,
            H4 = 840,
            H3 = 980,
            H2 = 1120,
            H1 = 1260,
            Index0 = 1400,
            F1 = 1540,
            MechanicalMax = 1605,
            F2 = 1680,
            F3 = 1820,
            F4 = 1960,
            F5 = 2100,
            F6 = 2240,
            F7 = 2380,
            F8 = 2520
        }




        /// <summary>
        /// From dose planning instruction:
        /// Nedan anges gränser för placering av isocenter, det är viktigt att ta ca 2 cm marginal från dessa så att matchningsförflyttningar kan utföras. 
        //        Longitudinellt:
        //maxvärde 160,5 cm, då finns det 93 cm från britsens kant till isocenter.
        //Vertikalt:
        //Minvärde -56 cm, då är det ssd 156 cm till britstoppen.
        //Maxvärde +40 cm, då är SSD 134 cm till britsens undersida.
        //Vid vrt 0 cm är britsen 130 cm över golvet.
        //Lateralt
        //Minvärde -25 cm.
        //Maxvärde +25 cm.
        //I förhållande till fixationer:
        //Vingbräda: Isocenter maximalt 40 cm från vingbrädans kaudala kant
        //Maskplatta: 
        //Isocenter maximalt 40 cm från nackkuddens kaudla kant
        //Avståndet från den kraniella delen av nackkuddens kant till britsens topp är 28 cm
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>



    }
}
