using ScriptGenerator.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ScriptGenerator
{
    static class Program
    {
        public static String MAP_URL = "";
        public static String MAP_SCRIPT_URL = "";
        public static String NPC_SCRIPT_URL = "";
        public static String NPC_NAME_URL = "";
        public static String DIRECTION_SCRIPT_URL = "";
        public static String PORTAL_SCRIPT_URL = "";
        public static String QUEST_NAME_URL = "";
        public static bool DEBUG_PACKETS = false;
        public static int VERSION = -1;

        public static frmMain form;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new frmMain();
            Application.Run(form);
        }
    }
}
