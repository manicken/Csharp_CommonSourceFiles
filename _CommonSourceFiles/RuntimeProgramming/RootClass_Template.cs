using System;
using System.Text;
using System.Windows.Forms;
using Microsan;

//using OxyPlot;
//using OxyPlot.Series;
//using OxyPlot.WindowsForms;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO.Ports;

namespace MyNamespace
{ 
    public class RootClass
    {
		private static object RootObject = null;
		
        /// <summary> The main entry point for the runtime compile code. </summary>
        public static void RootMain(object rootObject)
        {
            RootObject = rootObject;
        }
    }
}