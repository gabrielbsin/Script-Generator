using ScriptGenerator.Life;
using System;
using System.Collections.Generic;
using static ScriptGenerator.Script;
using static ScriptGenerator.Tools.Opcodes;

namespace ScriptGenerator.Tools
{
    class Parser
    {
        private static List<Script> scripts = new List<Script>();
        private static Script activeScript = new Script();
        public static int MAP_ID = 0;

        public static void showAllScripts()
        {
            if (activeScript != null)
            {
                if (activeScript.GetScriptType() != ScriptType.None) scripts.Add(activeScript);
                activeScript = new Script();
            }

            scripts.Reverse();
            foreach (Script script in scripts)
            {
                script.showScriptBox();
            }
            scripts.Clear();
            LifeStorage.clear();
        }

        public static void parseOutbound(OutboundOpcodes opcode, Packet packet)
        {
            if (packet == null || activeScript == null)
            {
                return;
            }
            if (Program.DEBUG_PACKETS) Console.WriteLine("Reading Outbound Packet " + opcode);
            activeScript.handleOutboundPackets(opcode, packet);
        }

        public static void parseInbound(InboundOpcodes opcode, Packet packet, long timestamp)
        {
            if (packet == null || activeScript == null)
            {
                return;
            }
            if (opcode == InboundOpcodes.SET_FIELD)
            {
                int mapID = Handler.parseSetField(activeScript, packet);
                if (Util.hasMapScript(mapID))
                {
                    LifeStorage.clear();
                    dispose();
                    activeScript.setMapID(mapID);
                }
                return;
            }
            if (Program.DEBUG_PACKETS) Console.WriteLine("Reading Inbound Packet " + opcode);
            activeScript.handleInboundPackets(opcode, packet, timestamp);
        }

        public static void dispose()
        {
            if (activeScript != null)
            {
                if (activeScript.GetScriptType() != ScriptType.None && activeScript.getScript().Length > 0)
                {
                    scripts.Add(activeScript);
                }
            }
            activeScript = new Script();
        }

        public static Script getActiveScript()
        {
            return activeScript;
        }
    } 
}
