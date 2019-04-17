using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScriptGenerator.Tools
{
    class Util
    {
        public static bool isNumber(String str)
        {
            return Regex.IsMatch(str, "-?\\d+(\\.\\d+)?");
        }

        public static bool isExtendSPJob(int jobID)
        {
            return !isBeastTamer(jobID) && !isPinkBean(jobID);
        }

        public static bool isBeastTamer(int jobID)
        {
            return jobID / 1000 == 11;
        }

        public static bool isPinkBean(int jobID)
        {
            return jobID == 13000 || jobID == 13100;
        }

        public static T GetEnumObjectByValue<T>(int valueId)
        {
            return (T)Enum.ToObject(typeof(T), valueId);
        }

        public static T GetEnumObjectByValue<T>(long valueId)
        {
            return (T)Enum.ToObject(typeof(T), valueId);
        }

        public static String getMapName(int mapID)
        {
            string request = String.Format("{0}?id={1}", Program.MAP_URL, mapID);
            try
            {
                return GetString(request);
            }
            catch (WebException)
            {
                return "Unknown : Unknown";
            }
        }

        public static bool hasMapScript(int mapID)
        {
            String mapScript = getMapUserEnterScript(mapID);
            if (mapScript == null || mapScript.Equals("Unknown"))
            {
                return false;
            }
            return true;
        }

        public static String getMapUserEnterScript(int mapID)
        {
            string request = String.Format("{0}?id={1}", Program.MAP_SCRIPT_URL, mapID);
            try
            {
                return GetString(request);
            }
            catch (WebException)
            {
                return "Unknown";
            }
        }

        public static bool hasNpcScript(int npcID)
        {
            String npcScript = getNpcScript(npcID);
            if (npcScript == null || npcScript.Equals("Unknown"))
            {
                return false;
            }
            return true;
        }

        public static String getNpcScript(int npcID)
        {
            string request = String.Format("{0}?id={1}", Program.NPC_SCRIPT_URL, npcID);
            try
            {
                return GetString(request);
            }
            catch (WebException)
            {
                return "Unknown";
            }
        }

        public static String getNpcName(int npcID)
        {
            string request = String.Format("{0}?id={1}", Program.NPC_NAME_URL, npcID);
            try
            {
                return GetString(request);
            }
            catch (WebException)
            {
                return "Unknown";
            }
        }

        public static String getDirectionScriptName(int mapID, int key)
        {
            string request = String.Format("{0}?id={1}&key={2}", Program.DIRECTION_SCRIPT_URL, mapID, key);
            try
            {
                return GetString(request);
            }
            catch (WebException)
            {
                return "Unknown";
            }
        }

        public static String getQuestName(int questID)
        {
            string request = String.Format("{0}?id={1}", Program.QUEST_NAME_URL, questID);
            try
            {
                return GetString(request);
            }
            catch (WebException)
            {
                return "Unknown";
            }
        }

        public static String getPortalScriptName(int mapID, String portalName)
        {
            string request = String.Format("{0}?id={1}&name={2}", Program.PORTAL_SCRIPT_URL, mapID, portalName);
            try
            {
                return GetString(request);
            }
            catch (WebException)
            {
                return "Unknown";
            }
        }

        private static string GetString(string URL)
        {
            WebRequest request = WebRequest.Create(URL);

            request.Proxy = null;

            WebResponse myResponse = request.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.ASCII);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();
            return result;
        }
        public static String quotes(String var)
        {
            return String.Format("\"{0}\"", var);
        }
    }
}
