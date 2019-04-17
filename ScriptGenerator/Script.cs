using ScriptGenerator.Life;
using ScriptGenerator.Tools;
using System;
using System.Windows.Forms;
using static ScriptGenerator.Enums;
using static ScriptGenerator.Tools.Opcodes;
using static ScriptGenerator.Tools.Opcodes.InboundOpcodes;
using static ScriptGenerator.Tools.Opcodes.OutboundOpcodes;
namespace ScriptGenerator
{
    public class Script
    {
        public enum ScriptType {None, MapScript, QuestScript, NpcScript, PortalScript, ItemScript, DirectionScript};

        private String sTitle, sScriptName, sObjectName, sCredits, sScript;
        private ScriptType type;
        private int id = -1;
        private int objectCounter = 1;
        private frmScript scriptForm;
        private bool read = false, flagged = false, waitForScriptAnswer = false, initedLifeObjects = false, uiLocked = false, hasUIScript = false;
        public Script()
        {
            type = ScriptType.None;
            sTitle = "ScriptGenerator";
            sScriptName = "";
            sObjectName = "";
            sCredits = "";
            sScript = "";
        }

        // Recv Opcodes
        public void handleOutboundPackets(OutboundOpcodes opcode, Packet packet)
        {
            switch (opcode)
            {
                case USER_PORTAL_SCRIPT_REQUEST:
                    packet.ReadByte();
                    String portalName = packet.ReadString();
                    String script = Util.getPortalScriptName(Parser.MAP_ID, portalName);
                    if(!script.Equals("Unknown") && !isWaitForScriptAnswer())
                    {
                        bool initedLifes = initedLifeObjects;
                        Parser.dispose();
                        Parser.getActiveScript().setPortal(Parser.MAP_ID, script, initedLifes);
                    }
                    break;
                case OutboundOpcodes.DIRECTION_NODE_COLLISION:
                    int key = packet.ReadInt();
                    if (!Util.getDirectionScriptName(Parser.MAP_ID, key).Equals("Unknown"))
                    {
                        bool initedLifes = initedLifeObjects;
                        Parser.dispose();
                        Parser.getActiveScript().setDirection(Parser.MAP_ID, key, initedLifes);
                    }
                    break;
                case USER_QUEST_REQUEST:

                    int requestType = packet.ReadByte();
                    int questID = packet.ReadInt();
                    if (questID == 7707) return;
                    if (!isWaitForScriptAnswer())
                    {
                        bool inited = initedLifeObjects;
                        // should I check the type and then dispose ?
                        Parser.dispose() ;
                        Handler.handleUserQuestRequest(requestType, questID, inited);
                        read = true;
                    }
                    break;
                case USER_SELECT_NPC:
                    //if (isDisposed())
                    //{
                        int objectID = packet.ReadInt();
                        Npc npc = LifeStorage.getNpcByObjectID(objectID);
                        if (npc != null)
                        {
                            if (Util.hasNpcScript(npc.TemplateID))
                            {
                            bool initedLifes = initedLifeObjects;
                            Parser.dispose();
                                Parser.getActiveScript().setNpcID(npc.TemplateID, initedLifes);
                            }
                        }
                    //}
                    break;
                case USER_SCRIPT_MESSAGE_ANSWER:
                    ScriptMessageType scriptMessageType = Util.GetEnumObjectByValue<ScriptMessageType>(packet.ReadByte());
                    Console.WriteLine("Script MEssage Type {0}", scriptMessageType);
                    if (scriptMessageType == ScriptMessageType.AskYesNo || scriptMessageType == ScriptMessageType.AskAccept)
                    {
                        addComment("Response is " + (packet.ReadBool() ? "Yes" : "No"));
                    }

                    if (scriptMessageType == ScriptMessageType.AskMenu)
                    {
                        packet.ReadByte();
                        if (packet.Length >= 4)
                        {
                            addComment(String.Format("if selection == {0}:", packet.ReadInt()));
                        }
                    }
                    setWaitForAnswer(false);
                    break;
            }
        }

        // Send Opcodes
        public void handleInboundPackets(InboundOpcodes opcode, Packet packet, long timestamp)
        {
            // Format: addLine(String.Format("sm.method({0}, {1})",  0, 1));
            if (waitForScriptAnswer && opcode != InboundOpcodes.EFFECT)
            {
                return;
            }
            int x, y, size, objectID, templateID;
            Npc npc = null;
            UIType uiType;
            switch (opcode)
            {
                case PROGRESS_MESSAGE_FONT:
                    addLine(String.Format("sm.progressMessageFont({0}, {1}, {2}, {3}, {4})", packet.ReadInt(), packet.ReadInt(), packet.ReadInt(), packet.ReadInt(), Util.quotes(packet.ReadString())));
                    break;
                case RANDOM_TELEPORT_KEY:
                    addLine(String.Format("sm.sendRandomTeleportKey({0})", packet.ReadByte()));
                    break;
                case ADD_POPUP_SAY:
                    addLine(String.Format("sm.addPopUpSay({0}, {1}, {2}, {3})", packet.ReadInt(), packet.ReadInt(), Util.quotes(packet.ReadString()), Util.quotes(packet.ReadString())));
                    break;
                case USER_SET_FIELD_FLOATING:
                    addLine(String.Format("sm.setFieldFloating({0}, {1}, {2}, {3})", packet.ReadInt(), packet.ReadInt(), packet.ReadInt(), packet.ReadInt()));
                    break;
                case SET_MAP_TAGGED_OBJECT_VISISBLE:
                    size = packet.ReadByte();
                    for (int i = 0; i < size; i++)
                    {
                        addLine(String.Format("sm.setMapTaggedObjectVisible({0}, {1}, {2}, {3})", Util.quotes(packet.ReadString()), packet.ReadBoolPy(), packet.ReadInt(), packet.ReadInt()));
                    }
                    break;
                case NPC_SET_FORCE_FLIP:
                    npc = LifeStorage.getNpcByObjectID(packet.ReadInt());
                    if (npc != null)
                    {
                        String left = PythonBool(packet.ReadInt() == -1);
                        addLine(String.Format("sm.flipNpcByObjectId({0}, {1})", npc.ObjectName, left));
                    }
                    break;
                case TELEPORT:
                    packet.ReadByte();
                    packet.ReadByte();
                    packet.ReadInt();// char id
                    addLine(String.Format("sm.teleportInField({0}, {1})", packet.ReadShort(), packet.ReadShort()));
                    break;
                case SET_MAP_TAGGED_OBJECT_ANIMATION:
                    size = packet.ReadInt();
                    for (int i = 0; i < size; i++)
                    {
                        addLine(String.Format("sm.setMapTaggedObjectAnimation({0}, {1})", Util.quotes(packet.ReadString()), packet.ReadInt()));
                    }
                    break;
                case EMOTION:
                    addLine(String.Format("sm.localEmotion({0}, {1}, {2})", packet.ReadInt(), packet.ReadInt(), PythonBool(packet.ReadBool())));
                    break;
                case FUNCKEY_SET_BY_SCRIPT:
                    bool add = packet.ReadBool();
                    int action = packet.ReadInt();
                    x = 0;
                    if (add)
                    {
                        x = packet.ReadInt();
                    }
                    addLine(String.Format("sm.setFuncKeyByScript({0}, {1}, {2})", PythonBool(add), action, x));
                    break;
                case SESSION_VALUE:
                    String key = packet.ReadString();
                    npc = LifeStorage.getNpcByObjectID(Int32.Parse(packet.ReadString()));
                    if (npc != null)
                    {
                        addLine(String.Format("sm.sendSessionValue({0}, {1})", Util.quotes(key), npc.TemplateID.ToString()));
                    }
                    break;
                case OPEN_UI_WITH_OPTION:
                    uiType = Util.GetEnumObjectByValue<UIType>(packet.ReadInt());
                    if (Util.isNumber(uiType.ToString()))
                    {
                        addLine(String.Format("sm.openUIWithOption({0}, {1})", uiType, packet.ReadInt()));
                    }
                    else
                    {
                        addLine(String.Format("sm.openUIWithOption(UIType.{0}, {1})", uiType, packet.ReadInt()));
                    }
                    hasUIScript = true;
                    break;
                case OPEN_UI:
                    uiType = Util.GetEnumObjectByValue<UIType>(packet.ReadInt());
                    if (Util.isNumber(uiType.ToString()))
                    {
                        addLine(String.Format("sm.openUI({0})", uiType));
                    } else
                    {
                        addLine(String.Format("sm.openUI(UIType.{0})", uiType));
                    }
                    hasUIScript = true;
                    break;
                case CLOSE_UI:
                    uiType = Util.GetEnumObjectByValue<UIType>(packet.ReadInt());
                    if (Util.isNumber(uiType.ToString()))
                    {
                        addLine(String.Format("sm.closeUI({0})", uiType));
                    }
                    else
                    {
                        addLine(String.Format("sm.closeUI(UIType.{0})", uiType));
                    }
                    hasUIScript = true;
                    break;
                case SCRIPT_PROGRESS_MESSAGE:
                    addLine(String.Format("sm.chatScript({0})", Util.quotes(packet.ReadString())));
                    break;
                case NPC_SET_FORCE_MOVE:
                    npc = LifeStorage.getNpcByObjectID(packet.ReadInt());
                    if (npc != null)
                    {
                        String left = PythonBool(packet.ReadInt() == -1);
                        int distance = packet.ReadInt();
                        int speed = packet.ReadInt();
                        addLine(String.Format("sm.moveNpcByObjectId({0}, {1}, {2}, {3})", npc.ObjectName, left, distance, speed));
                    }
                    break;
                case MAP_SCRIPT_FLAG:
                    if (!flagged)
                    {
                        read = true;
                        flagged = true;
                    }
                    break;
                case CHANGE_SKILL_RECORD_RESULT:
                    packet.ReadByte();// exclRequestSent
                    packet.ReadByte();// showResult
                    packet.ReadByte();// rmeove link skill
                    int skillSize = packet.ReadShort();
                    for (int i = 0; i < skillSize; i++)
                    {
                        int skillID = packet.ReadInt();
                        int currentSLV = packet.ReadInt();
                        int masterSLV = packet.ReadInt();
                        if (currentSLV <= 0 || masterSLV <= 0)
                        {
                            addLine(String.Format("sm.removeSkill({0})", skillID));
                        }
                        else
                        {
                            addLine(String.Format("sm.giveSkill({0}, {1}, {2})", skillID, currentSLV, masterSLV));
                        }
                    }
                    break;
                case EFFECT:
                    Handler.handleUserEffect(packet);
                    break;
                case INVENTORY_OPERATION:
                    Handler.handleInventoryOperation(packet);
                    break;
                case SCRIPT_MESSAGE:
                    Handler.handleScriptMessage(packet);
                    break;
                case VIDEO_BY_SCRIPT_2:
                    addLine(String.Format("sm.playURLVideoByScript(\"{0}\")", packet.ReadString()));
                    setWaitForAnswer(true);
                    break;
                case FIELD_EFFECT:
                    Handler.handleFieldEffect(packet);
                    break;
                case IN_GAME_DIRECTION_EVENT:
                    Handler.handleIngameDirectionEvent(packet);
                    break;
                case InboundOpcodes.BROADCAST_MSG:
                    byte broadcastType = packet.ReadByte();
                    if (broadcastType == 4)
                    {
                        byte unk = packet.ReadByte();
                        if (unk == 0)
                        {
                            initedLifeObjects = true;
                        }
                    }
                    break;
                case NPC_ENTER_FIELD:
                    objectID = packet.ReadInt();
                    if (LifeStorage.getNpcByObjectID(objectID) != null)
                    {
                        return;
                    }
                    templateID = packet.ReadInt();
                    x = packet.ReadShort();
                    y = packet.ReadShort();
                    npc = new Npc(objectID, templateID);
                    npc.X = x;
                    npc.Y = y;
                    npc.ObjectName = String.Format("OBJECT_{0}", objectCounter++);
                    LifeStorage.addNpc(objectID, npc);
                    if (initedLifeObjects) addLine(String.Format("{3} = sm.sendNpcEnterField({0}, {1}, {2})", templateID, x, y, npc.ObjectName));
                    break;
                case NPC_LEAVE_FIELD:
                    npc = LifeStorage.getNpcByObjectID(packet.ReadInt());
                    if (npc == null) return;
                    LifeStorage.removeNpc(npc.ObjectID);
                    if (initedLifeObjects) addLine(String.Format("sm.sendNpcLeaveField({0})", npc.ObjectName));
                    break;
                case NPC_CHANGE_CONTROLLER:
                    bool controller = packet.ReadBool();
                    objectID = packet.ReadInt();
                    npc = LifeStorage.getNpcByObjectID(objectID);
                    if (npc == null)
                    {
                        if (!controller) return;
                        templateID = packet.ReadInt();
                        x = packet.ReadShort();
                        y = packet.ReadShort();
                        npc = new Npc(objectID, templateID);
                        npc.X = x;
                        npc.Y = y;
                        npc.ObjectName = String.Format("OBJECT_{0}", objectCounter++);
                        LifeStorage.addNpc(objectID, npc);
                        if (initedLifeObjects) addLine(String.Format("{3} = sm.sendNpcController({0}, {1}, {2})", templateID, x, y, npc.ObjectName));
                    }
                    else
                    {
                        if (initedLifeObjects) addLine(String.Format("sm.sendNpcController({0}, {1})", npc.ObjectName, PythonBool(controller)));
                        if (!controller) LifeStorage.removeNpc(objectID);
                    }
                    break;
                case NPC_SET_SPECIAL_ACTION:
                    npc = LifeStorage.getNpcByObjectID(packet.ReadInt());
                    if (npc != null)
                    {
                        String npcEffect = packet.ReadString();
                        int duration = packet.ReadInt();
                        addLine(String.Format("sm.showNpcSpecialActionByObjectId({0}, \"{1}\", {2})", npc.ObjectName, npcEffect, duration));
                    }
                    break;
                case CUR_NODE_EVENT_END:
                    addLine(String.Format("sm.curNodeEventEnd({0})", packet.ReadBoolPy()));
                    break;
                case SET_TEMPORARY_SKILL_SET:
                    addLine(String.Format("sm.setTemporarySkillSet({0})", packet.ReadInt()));
                    break;
                case SET_IN_GAME_DIRECTION_MODE:
                    bool lockUI = false, blackFrame = false, forceMouseOver = false, showUI = false;
                    lockUI = packet.ReadBool();
                    uiLocked = lockUI;
                    blackFrame = packet.ReadBool();
                    if (lockUI)
                    {
                        forceMouseOver = packet.ReadBool();
                        showUI = packet.ReadBool();
                    }
                    addLine(String.Format("sm.setInGameDirectionMode({0}, {1}, {2}, {3})", PythonBool(lockUI), PythonBool(blackFrame), PythonBool(forceMouseOver), PythonBool(showUI)));
                    if (!lockUI)
                    {
                        //addComment("Set field decoding not working yet so check if ur character warped to somewhere else");
                        //Parser.dispose();
                    }
                    break;
                case SET_STAND_ALONE_MODE:
                    addLine(String.Format("sm.setStandAloneMode({0})", packet.ReadBoolPy()));
                    break;
                case MESSAGE:
                    Handler.handleMessage(packet);
                    break;
                case MOB_CRC_KEY_CHANGED:
                    if (type == ScriptType.MapScript)
                    {
                        if (!waitForScriptAnswer && flagged)
                        {
                            //Parser.dispose();
                        }
                    }
                    break;
                case STAT_CHANGED:
                    Handler.handleStatChanged(packet);
                    break;
                default:
                    if (!isIgnoredInbound(opcode) && read)
                    {
                        addLine(String.Format("# [{0}] [{1}]", opcode.ToString(), packet.ToString()));
                    }
                    break;
            }
        }

        public bool isIgnoredInbound(InboundOpcodes opcode)
        {
            switch (opcode)
            {
                case ALIVE_REQ:
                case InboundOpcodes.SECURITY_PACKET:
                case TEMPORARY_STAT_RESET:
                case TEMPORARY_STAT_SET:
                case InboundOpcodes.NPC_MOVE:
                case InboundOpcodes.MOB_MOVE:
                case InboundOpcodes.MOB_CONTROL_ACK:
                case InboundOpcodes.DROP_ENTER_FIELD:
                case InboundOpcodes.DROP_LEAVE_FIELD:
                case MOB_ENTER_FIELD:
                case MOB_LEAVE_FIELD:
                case MOB_CHANGE_CONTROLLER:
                case MOB_STAT_SET:
                case MOB_STAT_RESET:
                case MOB_HP_INDICATOR:
                case AVATAR_MEGAPHONE_RES:
                case SET_AVATAR_MEGAPHONE:
                case UNIVERSE_BOSS_IMPOSSIBLE:
                case InboundOpcodes.PRIVATE_SERVER_PACKET:
                case MONSTER_COLLECTION_RESULT:
                case CHARACTER_HONOR_EXP:
                case SKILL_COOLTIME_SET_M:
                case USER_ENTER_FIELD:
                case InboundOpcodes.PARTY_RESULT:
                case SET_QUICK_MOVE_INFO:
                case COMPLETE_SPECIAL_CHECK_SUCCESS:
                case ANTI_MACRO_RESULT:
                case CLEAR_AVATAR_MEGAPHONE:
                    return true;
            }
            if (Util.isNumber(opcode.ToString()))
            {
                return true;
            }
            return false;
        }

        public void addCredits(String line)
        {
            this.sCredits += "# " + line + "\r\n";
        }

        public void addComment(String line)
        {
            addLine("# " + line);
        }

        public void addLine(String line)
        {
            if (read) this.sScript += line + "\r\n";
            //Console.WriteLine("Adding Line [{0}] to [{1}]", line, sObjectName);
        }

        public void newLine()
        {
            this.sScript += "\r\n";
        }

        public void setWaitForAnswer(bool enable)
        {
            this.waitForScriptAnswer = enable;
            if (!enable) addLine("\r\n");
        }

        public void showScriptBox()
        {
            if (type != ScriptType.None)
            {
                scriptForm = new frmScript();
                scriptForm.setInformation(this);
                //scriptForm.StartPosition = FormStartPosition.CenterScreen;
                scriptForm.Show();
            }
        }

        public void setScriptDefault()
        {
            this.sTitle = String.Format("{0} | {1} | {2}", sTitle, sScriptName, type);
            this.sScript = "";
            this.sCredits = "";

            addCredits("Created by MechAviv");
            if (type == ScriptType.QuestScript) addCredits("Quest ID :: " + id);
            if (type == ScriptType.NpcScript) addCredits(String.Format("[{1}]  |  [{0}]", id, sObjectName));
            else if (type != ScriptType.QuestScript) addCredits(String.Format("ID :: [{0}]", id));
            if (type != ScriptType.QuestScript) addCredits(Util.getMapName(Parser.MAP_ID));
            if (type != ScriptType.NpcScript && type != ScriptType.MapScript) addCredits(sObjectName);
            newLine();
        }

        public void setQuestID(int questID, bool start, bool initedLifes)
        {
            this.id = questID;
            this.sObjectName = Util.getQuestName(questID);
            this.sScriptName = String.Format("q{0}{1}.py", questID, (start ? "s" : "e"));
            this.type = ScriptType.QuestScript;
            this.read = true;
            this.initedLifeObjects = initedLifes;
            setScriptDefault();
        }

        public void setMapID(int mapID)
        {
            this.id = mapID;
            Parser.MAP_ID = mapID;
            this.sObjectName = Util.getMapName(mapID);
            this.sScriptName = String.Format("{0}.py", Util.getMapUserEnterScript(mapID));
            this.type = ScriptType.MapScript;
            setScriptDefault();
        }

        public void setNpcID(int npcID, bool initedLifes)
        {
            this.id = npcID;
            this.sObjectName = Util.getNpcName(npcID);
            this.sScriptName = String.Format("{0}.py", Util.getNpcScript(npcID));
            this.type = ScriptType.NpcScript;
            this.read = true;
            this.initedLifeObjects = initedLifes;
            setScriptDefault();
        }

        public void setDirection(int mapID, int key, bool initedLifes)
        {
            this.id = mapID;
            this.sObjectName = Util.getMapName(mapID);
            this.sScriptName = String.Format("{0}.py", Util.getDirectionScriptName(mapID, key));
            this.type = ScriptType.DirectionScript;
            this.read = true;
            this.initedLifeObjects = initedLifes;
            setScriptDefault();
        }

        public void setPortal(int mapID, String scriptName, bool initedLifes)
        {
            this.id = mapID;
            this.sObjectName = Util.getMapName(mapID);
            this.sScriptName = String.Format("{0}.py", scriptName);
            this.type = ScriptType.PortalScript;
            this.read = true;
            this.initedLifeObjects = initedLifes;
            setScriptDefault();
        }

        public static String PythonBool(bool b)
        {
            return b ? "True" : "False";
        }

        public String getTitle()
        {
            return sTitle;
        }

        public String getScriptName()
        {
            return sScriptName;
        }

        public String getObjectName()
        {
            return sObjectName;
        }

        public int getId()
        {
            return id;
        }

        public String getScript()
        {
            return sScript;
        }

        public String getCredits()
        {
            return sCredits;
        }

        public ScriptType GetScriptType()
        {
            return type;
        }

        public bool isWaitForScriptAnswer()
        {
            return waitForScriptAnswer;
        }

        public bool isUIScript()
        {
            return hasUIScript;
        }
    }
}
