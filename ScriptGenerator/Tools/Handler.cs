using System;
using static ScriptGenerator.Enums;
using static ScriptGenerator.Enums.InGameDirectionEventType;
using static ScriptGenerator.Enums.FieldEffectType;
using static ScriptGenerator.Enums.ScriptMessageType;
using static ScriptGenerator.Enums.QuestRequestType;
using static ScriptGenerator.Enums.UserEffectType;
using static ScriptGenerator.Script;
using System.Collections;
using ScriptGenerator.Life;

namespace ScriptGenerator.Tools
{
    class Handler
    {
        public static void handleInventoryOperation(Packet packet)
        {
            packet.ReadByte();// excl request
            int operations = packet.ReadByte();
            packet.ReadByte();
            if (operations > 0)
            {
                Parser.getActiveScript().addComment(String.Format("Inventory Operation with {0} operations.", operations));
            }
            //InventoryOperation operation = Util.GetEnumObjectByValue<InventoryOperation>(packet.ReadByte());
        }
        public static void handleMessage(Packet packet)
        {
            Message type = Util.GetEnumObjectByValue<Message>(packet.ReadByte());
            switch (type)
            {
                case Message.QUEST_RECORD_EX_MESSAGE:
                    int questID = packet.ReadInt();
                    String qrValue = packet.ReadString();
                    switch (questID)
                    {
                        case 16119:
                            if (!Parser.getActiveScript().isWaitForScriptAnswer() && Parser.getActiveScript().GetScriptType() == ScriptType.MapScript)
                            {
                                Parser.dispose();
                            }
                            break;
                        default:
                            Parser.getActiveScript().addComment(String.Format("Update Quest Record EX | Quest ID: [{0}] | Data: {1}", questID, qrValue));
                            break;
                    }
                    break;
                case Message.QUEST_RECORD_MESSAGE:
                    questID = packet.ReadInt();
                    int questAct = packet.ReadByte();
                    if (questAct == 1)// Start Quest
                    {
                        qrValue = packet.ReadString();
                        if (qrValue.Length == 0)
                        {
                            Parser.getActiveScript().addLine(String.Format("sm.startQuest({0})", questID));
                        }
                        else
                        {
                            Parser.getActiveScript().addLine(String.Format("sm.createQuestWithQRValue({0}, {1})", questID, Util.quotes(qrValue)));
                        }
                    }
                    else if (questAct == 2)// Complete Quest
                    {
                        Parser.getActiveScript().addLine(String.Format("sm.completeQuest({0})", questID));
                    }
                    break;
                case Message.INC_EXP_MESSAGE:
                    packet.ReadByte();
                    Parser.getActiveScript().addLine(String.Format("sm.giveExp({0})", packet.ReadLong()));
                    break;
                case Message.SYSTEM_MESSAGE:
                    Parser.getActiveScript().addLine(String.Format("sm.systemMessage({0})", Util.quotes(packet.ReadString())));
                    break;
                // ignored:
                //case QuestComplete:
                //    break;
                default:
                    Parser.getActiveScript().addComment(String.Format("Unhandled Message [{0}] Packet: {1}", type, packet.ToString()));
                    break;
            }
        }
        public static void handleStatChanged(Packet packet)
        {
            packet.ReadByte();
            packet.ReadByte();
            Stats type = Util.GetEnumObjectByValue<Stats>(packet.ReadLong());
            switch (type)
            {
                // ignored:
                case Stats.EVENT_POINTS:
                    break;
                default:
                    if (type != 0)
                    {
                        Parser.getActiveScript().addComment(String.Format("Unhandled Stat Changed [{0}] Packet: {1}", type, packet.ToString()));
                    }
                    break;
            }
        }
        public static void handleUserEffect(Packet packet)
        {
            UserEffectType type = Util.GetEnumObjectByValue<UserEffectType>(packet.ReadByte());
            switch (type)
            {
                case FadeInOut:
                    Parser.getActiveScript().addLine(String.Format("sm.fadeInOut({0}, {1}, {2}, {3})", packet.ReadInt(), packet.ReadInt(), packet.ReadInt(), packet.ReadByte()));
                    break;
                case SpeechBalloon:
                    Parser.getActiveScript().addLine(String.Format("sm.speechBalloon({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})", packet.ReadBoolPy(), packet.ReadInt(), packet.ReadInt(), Util.quotes(packet.ReadString()), packet.ReadInt(), packet.ReadInt(), packet.ReadInt(), packet.ReadInt(), packet.ReadInt(), packet.ReadInt(), packet.ReadInt()));
                    break;
                case TextEffect:
                    String text = packet.ReadString();
                    int letterDelay = packet.ReadInt();
                    int boxDuration = packet.ReadInt();
                    int clientPos = packet.ReadInt();
                    int x = packet.ReadInt();
                    int y = packet.ReadInt();
                    int align = packet.ReadInt();
                    int lineSpace = packet.ReadInt();
                    int enterType = packet.ReadInt();
                    int leaveType = packet.ReadInt();
                    int textType = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.createFieldTextEffect({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})", Util.quotes(text), letterDelay, boxDuration, clientPos, x, y, align, lineSpace, textType, enterType, leaveType));
                    break;
                case ReservedEffect:
                    packet.ReadByte();
                    packet.ReadInt();
                    packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.reservedEffect({0})", Util.quotes(packet.ReadString())));
                    break;
                case AvatarOriented:
                    Parser.getActiveScript().addLine(String.Format("sm.avatarOriented({0})", Util.quotes(packet.ReadString())));
                    break;
                case PlayExclSoundWithDownBGM:
                    Parser.getActiveScript().addLine(String.Format("sm.playExclSoundWithDownBGM({0}, {1})", Util.quotes(packet.ReadString()), packet.ReadInt()));
                    break;
                // ignored:
                case Quest:
                case QuestComplete:
                    break;
                default:
                    Parser.getActiveScript().addComment(String.Format("Unhandled User Effect [{0}] Packet: {1}", type, packet.ToString()));
                    break;
            }
        }
        public static void handleUserQuestRequest(int requestType, int questID, bool initedLifes)
        {
            QuestRequestType type = Util.GetEnumObjectByValue<QuestRequestType>(requestType);
            switch (type)
            {
                case OpeningScript:
                    Parser.getActiveScript().setQuestID(questID, true, initedLifes);
                    break;
                case CompleteScript:
                    Parser.getActiveScript().setQuestID(questID, false, initedLifes);
                    break;
                case LostItem:
                case AcceptQuest:
                case CompleteQuest:
                case ResignQuest:
                case LaterStep:
                    break;
                default:
                    Parser.getActiveScript().addComment(String.Format("Unhandled quest request [{0}]", type));
                    break;
            }
        }
        public static bool hasParam(int param, ParamType paramType)
        { 
            return (param & (int) paramType) != 0;
        }

        public static void handleScriptMessageParam(int param)
        {
            if (hasParam(param, ParamType.NotCancellable))
            {
                Console.WriteLine("Not cancellable");
                Parser.getActiveScript().addLine(String.Format("sm.removeEscapeButton()"));
                param ^= (int) ParamType.NotCancellable;
            }
            if (hasParam(param, ParamType.PlayerAsSpeaker))
            {
                Console.WriteLine("PlayerAsSpeaker");
                Parser.getActiveScript().addLine(String.Format("sm.setPlayerAsSpeaker()"));
                param ^= (int)ParamType.PlayerAsSpeaker;
            }
            if (hasParam(param, ParamType.OverrideSpeakerID))
            {
                Console.WriteLine("OverrideSpeakerID");
                Parser.getActiveScript().addLine(String.Format("sm.flipDialogue()"));
                param ^= (int)ParamType.OverrideSpeakerID;
            }
            if (hasParam(param, ParamType.FlipSpeaker))
            {
                Console.WriteLine("FlipSpeaker");
                Parser.getActiveScript().addLine(String.Format("sm.flipSpeaker()"));
                param ^= (int)ParamType.FlipSpeaker;
            }
            if (hasParam(param, ParamType.PlayerAsSpeakerFlip))
            {
                Console.WriteLine("PlayerAsSpeakerFlip");
                Parser.getActiveScript().addLine(String.Format("sm.flipDialoguePlayerAsSpeaker()"));
                param ^= (int)ParamType.PlayerAsSpeakerFlip;
            }
            if (hasParam(param, ParamType.BoxChat))
            {
                Console.WriteLine("BoxChat");
                Parser.getActiveScript().addLine(String.Format("sm.setBoxChat()"));
                param ^= (int)ParamType.BoxChat;
            }
            if (hasParam(param, ParamType.BoxChatAsPlayer))
            {
                Console.WriteLine("BoxChatAsPlayer");
                Parser.getActiveScript().addLine(String.Format("sm.boxChatPlayerAsSpeaker()"));
                param ^= (int)ParamType.BoxChatAsPlayer;
            }
            if (hasParam(param, ParamType.BoxChatOverrideSpeaker))
            {
                Console.WriteLine("BoxChatOverrideSpeaker");
                Parser.getActiveScript().addLine(String.Format("sm.setBoxOverrideSpeaker()"));
                param ^= (int)ParamType.BoxChatOverrideSpeaker;
            }
            if (hasParam(param, ParamType.FlipBoxChat))
            {
                Console.WriteLine("FlipBoxChat");
                Parser.getActiveScript().addLine(String.Format("sm.flipBoxChat()"));
                param ^= (int)ParamType.FlipBoxChat;
            }
            if (hasParam(param, ParamType.FlipBoxChatAsPlayer))
            {
                Console.WriteLine("FlipBoxChatAsPlayer");
                Parser.getActiveScript().addLine(String.Format("sm.flipBoxChatPlayerAsSpeaker()"));
                param ^= (int)ParamType.FlipBoxChatAsPlayer;
            }
        }
        public static void handleScriptMessage(Packet packet)
        {
            int speakerType = packet.ReadByte();
            int speakerID = packet.ReadInt();
            if (packet.ReadBool())
            {
                int overrideNpc = packet.ReadInt();
                if (overrideNpc != 0)
                {
                    speakerID = overrideNpc;
                }
            }
            ScriptMessageType type = Util.GetEnumObjectByValue<ScriptMessageType>(packet.ReadByte());
            int param = packet.ReadShort();
            int color = packet.ReadByte();
            switch (type)
            {
                case Say:
                case AskMenu:
                case AskAccept:
                case AskYesNo:
                case AskText:
                case AskBoxText:
                case SayIllustration:
                    if (hasParam(param, ParamType.OverrideSpeakerID))
                    {
                        int overrideNpc = packet.ReadInt();
                        if (overrideNpc != 0)
                        {
                            speakerID = overrideNpc;
                        }
                    }
                    break;
            }
            switch (param)
            {
                case 0x24:
                    Parser.getActiveScript().addLine(String.Format("sm.setNpcOverrideBoxChat({0})", speakerID));
                    break;
                case 0x25:
                    Parser.getActiveScript().addLine(String.Format("sm.setIntroBoxChat({0})", speakerID));
                    break;
                default:
                    Parser.getActiveScript().addLine(String.Format("sm.setSpeakerID({0})", speakerID));
                    handleScriptMessageParam(param);
                    if (color != 0)
                    {
                        Parser.getActiveScript().addLine(String.Format("sm.setColor({0})", color));
                    }
                    break;
            }
            if (speakerType != 4) Parser.getActiveScript().addLine(String.Format("sm.setSpeakerType({0})", speakerType));
            String text;
            bool isPrev, isNext;
            switch (type)
            {
                case Say:
                    text = packet.ReadString();
                    isPrev = packet.ReadBool();
                    isNext = packet.ReadBool();
                    if (text.Contains(Environment.NewLine))
                    {
                        text.Replace(Environment.NewLine, "\r\n");
                    }
                    if (isPrev && isNext)
                    {
                        Parser.getActiveScript().addLine(String.Format("sm.sendSay({0})", Util.quotes(text)));
                    } else if (isPrev && !isNext) {
                        Parser.getActiveScript().addLine(String.Format("sm.sendPrev({0})", Util.quotes(text)));
                    } else if (!isPrev && isNext) {
                        Parser.getActiveScript().addLine(String.Format("sm.sendNext({0})", Util.quotes(text)));
                    } else {
                        Parser.getActiveScript().addLine(String.Format("sm.sendSayOkay({0})", Util.quotes(text)));
                    }
                    break;
                case AskYesNo:
                    text = packet.ReadString();
                    if (text.Contains(Environment.NewLine))
                    {
                        text.Replace(Environment.NewLine, "\r\n");
                    }
                    Parser.getActiveScript().addLine(String.Format("if sm.sendAskYesNo({0}):", Util.quotes(text)));
                    break;
                case AskAccept:
                    text = packet.ReadString();
                    if (text.Contains(Environment.NewLine))
                    {
                        text.Replace(Environment.NewLine, "\r\n");
                    }
                    Parser.getActiveScript().addLine(String.Format("if sm.sendAskAccept({0}):", Util.quotes(text)));
                    break;
                case AskMenu:
                    text = packet.ReadString();
                    if (text.Contains(Environment.NewLine))
                    {
                        text.Replace(Environment.NewLine, "\r\n");
                    }
                    Parser.getActiveScript().addLine(String.Format("selection = sm.sendNext({0})", Util.quotes(text)));
                    break;
                case SayIllustration:
                    text = packet.ReadString();
                    isPrev = packet.ReadBool();
                    isNext = packet.ReadBool();
                    int faceIndex = packet.ReadInt();
                    String isLeft = packet.ReadBoolPy();
                    if (isPrev && isNext)
                    {
                        Parser.getActiveScript().addLine(String.Format("sm.sendSayIllustration({0}, {1}, {2})", Util.quotes(text), faceIndex, isLeft));
                    }
                    else if (isPrev && !isNext)
                    {
                        Parser.getActiveScript().addLine(String.Format("sm.sendSayPrevIllustration({0}, {1}, {2})", Util.quotes(text), faceIndex, isLeft));
                    }
                    else if (!isPrev && isNext)
                    {
                        Parser.getActiveScript().addLine(String.Format("sm.sendSayNextIllustration({0}, {1}, {2})", Util.quotes(text), faceIndex, isLeft));
                    }
                    else
                    {
                        Parser.getActiveScript().addLine(String.Format("sm.sendSayOkayIllustration({0}, {1}, {2})", Util.quotes(text), faceIndex, isLeft));
                    }
                    break;
                default:
                    Parser.getActiveScript().addComment(String.Format("Unhandled Script Message [{0}] Packet: {1}", type, packet.ToString()));
                    break;
            }
            Parser.getActiveScript().setWaitForAnswer(true);
        }
        public static void handleFieldEffect(Packet packet)
        {
            FieldEffectType type = Util.GetEnumObjectByValue<FieldEffectType>(packet.ReadByte());
            String str;
            int x, y;
            switch (type)
            {
                case ObjectStateByString:
                    Parser.getActiveScript().addLine(String.Format("sm.objectStateByString({0})", Util.quotes(packet.ReadString())));
                    break;
                case Tremble:
                    Parser.getActiveScript().addLine(String.Format("sm.tremble({0}, {1}, {2})", packet.ReadByte(), packet.ReadInt(), packet.ReadShort()));
                    break;
                case PlaySound:
                    str = packet.ReadString();
                    x = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.playSound({0}, {1})", Util.quotes(str), x));
                    break;
                case ChangeBGM:
                    str = packet.ReadString();
                    x = packet.ReadInt();
                    y = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.changeBGM({0}, {1}, {2})", Util.quotes(str), x, y));
                    break;
                case SetBGMVolume:
                    Parser.getActiveScript().addLine(String.Format("sm.setBGMVolume({0}, {1})", packet.ReadInt(), packet.ReadInt()));
                    break;
                case BackScreen:
                    str = packet.ReadString();
                    x = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.showFieldBackgroundEffect({0}, {1})", Util.quotes(str), x));
                    break;
                case ScreenEffect:
                    str = packet.ReadString();
                    x = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.showFieldEffect({0}, {1})", Util.quotes(str), x));
                    break;
                case Blind:
                    int enable = packet.ReadByte();
                    x = packet.ReadShort();
                    int color = packet.ReadShort();
                    int unk1 = packet.ReadShort();
                    int unk2 = packet.ReadShort();
                    int time = packet.ReadInt();
                    int unk3 = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.blind({0}, {1}, {2}, {3}, {4}, {5}, {6})", enable, x, color, unk1, unk2, time, unk3));
                    break;
                case OnOffLayer:
                    byte layerType = packet.ReadByte();
                    int term = packet.ReadInt();
                    String key = packet.ReadString();
                    if (layerType == 0)
                    {
                        x = packet.ReadInt();
                        y = packet.ReadInt();
                        int z = packet.ReadInt();
                        String effect = packet.ReadString();
                        int origin = packet.ReadInt();
                        int unk4 = packet.ReadByte();
                        int unk5 = packet.ReadInt();
                        int unk6 = packet.ReadByte();
                        Parser.getActiveScript().addLine(String.Format("sm.OnOffLayer_On({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})", term, Util.quotes(key), x, y, z, Util.quotes(effect), origin, unk4, unk5, unk6));
                    }
                    else if (layerType == 1)
                    {
                        x = packet.ReadInt();
                        y = packet.ReadInt();
                        Parser.getActiveScript().addLine(String.Format("sm.OnOffLayer_Move({0}, {1}, {2}, {3})", term, Util.quotes(key), x, y));
                    }
                    else if (layerType == 2)
                    {
                        x = packet.ReadByte();
                        Parser.getActiveScript().addLine(String.Format("sm.OnOffLayer_Off({0}, {1}, {2})", term, Util.quotes(key), x));
                    }
                    break;
                case OverlapScreen:
                    x = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.showFade({0})", x));
                    break;
                case OverlapScreenDetail:
                    int duration = packet.ReadInt();
                    int fadeInTime = packet.ReadInt();
                    int fadeOutTime = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.showFadeTransition({0}, {1}, {2})", duration, fadeInTime, fadeOutTime));
                    break;
                case RemoveOverlapScreen:
                    x = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.removeOverlapScreen({0})", x));
                    break;
                case StageClearExpOnly:
                    x = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.showClearStageExpWindow({0})", x));
                    break;
                case SpineScreen:
                    Parser.getActiveScript().addLine(String.Format("sm.spineScreen({0}, {1}, {2}, {3}, {4}, {5}, {6})", packet.ReadBoolPy(), packet.ReadBoolPy(), packet.ReadBoolPy(), packet.ReadInt(), Util.quotes(packet.ReadString()), Util.quotes(packet.ReadString()), packet.ReadBool() ? Util.quotes(packet.ReadString()) : "None"));
                    break;
                case OffSpineScreen:
                    String layer = packet.ReadString();
                    int spineType = packet.ReadInt();
                    if (spineType == 1)
                    {
                        Parser.getActiveScript().addLine(String.Format("sm.offSpineScreenAlpha({0}, {1})", Util.quotes(layer), packet.ReadInt()));
                    }
                    else if (spineType == 2) {
                        Parser.getActiveScript().addLine(String.Format("sm.offSpineScreenAni({0}, {1})", Util.quotes(layer), Util.quotes(packet.ReadString())));
                    }
                    else {
                        Parser.getActiveScript().addLine(String.Format("sm.offSpineScreenImmediate({0})", Util.quotes(layer)));
                    }
                    break;
                default:
                    Parser.getActiveScript().addComment(String.Format("Unhandled Field Effect [{0}] Packet: {1}", type, packet.ToString()));
                    break;
            }
        }
        public static void handleIngameDirectionEvent(Packet packet)
        {
            InGameDirectionEventType type = Util.GetEnumObjectByValue<InGameDirectionEventType>(packet.ReadByte());
            int duration = 0, x = 0, y = 0, z = -2, unk1 = 0;
            String str, unk2 = "False";
            Npc npc = null;
            switch (type)
            {
                case ForcedAction:
                    int action = packet.ReadInt();
                    if (action <= 1895)
                    {
                        duration = packet.ReadInt();
                    }
                    Parser.getActiveScript().addLine(String.Format("sm.forcedAction({0}, {1})", action, duration));
                    break;
                case Delay:
                    Parser.getActiveScript().addLine(String.Format("sm.sendDelay({0})", packet.ReadInt()));
                    Parser.getActiveScript().setWaitForAnswer(true);
                    break;
                case EffectPlay:
                    str = packet.ReadString();
                    duration = packet.ReadInt();
                    x = packet.ReadInt();
                    y = packet.ReadInt();
                    if (packet.ReadBool())
                    {
                        z = packet.ReadInt();
                    }
                    int npcVal = -2;
                    if (packet.ReadBool())
                    {
                        npcVal = packet.ReadInt();
                        npc = LifeStorage.getNpcByObjectID(npcVal);
                        unk2 = packet.ReadBool() ? "False" : "True";
                        unk1 = packet.ReadByte();
                    }
                    Parser.getActiveScript().addLine(String.Format("sm.showEffect({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", Util.quotes(str), duration, x, y, z, npc == null ? String.Format("{0}", npcVal) : npc.ObjectName, unk2, unk1));
                    break;
                case ForcedInput:
                    Parser.getActiveScript().addLine(String.Format("sm.forcedInput({0})", packet.ReadInt()));
                    break;
                case PatternInputRequest:
                    str = packet.ReadString();
                    int act = packet.ReadInt();
                    int requestCount = packet.ReadInt();
                    int time = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.patternInputRequest({0}, {1}, {2}, {3})", Util.quotes(str), act, requestCount, time));
                    break;
                case CameraMove:
                    bool back = packet.ReadBool();
                    int speed = packet.ReadInt();
                    if (!back)
                    {
                        x = packet.ReadInt();
                        y = packet.ReadInt();
                    }
                    Parser.getActiveScript().addLine(String.Format("sm.moveCamera({0}, {1}, {2}, {3})", Script.PythonBool(back), speed, x, y));
                    Parser.getActiveScript().setWaitForAnswer(true);
                    break;
                case CameraOnCharacter:
                    Parser.getActiveScript().addLine(String.Format("sm.setCameraOnNpc({0})", packet.ReadInt()));
                    break;
                case CameraZoom:
                    duration = packet.ReadInt();
                    int scale = packet.ReadInt();
                    int timePos = packet.ReadInt();
                    x = packet.ReadInt();
                    y = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.zoomCamera({0}, {1}, {2}, {3}, {4})", duration, scale, timePos, x, y));
                    Parser.getActiveScript().setWaitForAnswer(true);
                    break;
                case VansheeMode:
                    Parser.getActiveScript().addLine(String.Format("sm.hideUser({0})", packet.ReadBoolPy()));
                    break;
                case FaceOff:
                    Parser.getActiveScript().addLine(String.Format("sm.faceOff({0})", packet.ReadInt()));
                    break;
                case InGameDirectionEventType.Monologue:
                    str = packet.ReadString();
                    unk2 = packet.ReadBoolPy();
                    Parser.getActiveScript().addLine(String.Format("sm.sayMonologue({0}, {1})", Util.quotes(str), unk2));
                    Parser.getActiveScript().setWaitForAnswer(true);
                    break;
                case AvatarLookSet:
                    str = "[";
                    x = packet.ReadByte();
                    for (int i = 0;i < x; i++)
                    {
                        str += String.Format("{0}, ", packet.ReadInt());
                    }
                    str += "]";
                    str.Replace(", ]", "]");
                    Parser.getActiveScript().addLine(String.Format("sm.avatarLookSet({0})", str));
                    break;
                case RemoveAdditionalEffect:
                    Parser.getActiveScript().addLine(String.Format("sm.removeAdditionalEffect()"));
                    break;
                case ForcedMove:
                    str = Script.PythonBool(packet.ReadInt() == 1);
                    x = packet.ReadInt();
                    Parser.getActiveScript().addLine(String.Format("sm.forcedMove({0}, {1})", str, x));
                    break;
                case ForcedFlip:
                    str = Script.PythonBool(packet.ReadInt() == -1);
                    Parser.getActiveScript().addLine(String.Format("sm.forcedFlip({0})", str));
                    break;
                default:
                    Parser.getActiveScript().addComment(String.Format("Unhandled Ingame Direction Event [{0}] Packet: {1}", type, packet.ToString()));
                    break;
            }
        }
        public static bool isExclRequestStatChanged(Packet packet)
        {
            byte exclRequestSent = packet.ReadByte();
            byte unk = packet.ReadByte();
            long statFlag = packet.ReadLong();
            if (exclRequestSent != 0 || unk != 0 || statFlag != 0)
            {
                return false;
            }
            return true;
        }
        public static int parseSetField(Script script, Packet packet)
        {
            int mapID, portal;
            packet.ReadBytes(23);
            bool bCharacterData = packet.ReadBool();
            short notifierCheck = packet.ReadShort();
            if (notifierCheck > 0)
            {
                packet.ReadString();
                for (int i = 0; i < notifierCheck; i++)
                {
                    packet.ReadString();
                }
            }
            if (bCharacterData)
            {
                packet.ReadBytes(12);// damage calc
                long dbFlag = packet.ReadLong();// dbFlag
                packet.ReadByte();//combat orders
                packet.ReadBytes(13);// pet active skill cooltime

                int count = packet.ReadByte();
                for (int i = 0; i < count; i++)
                {
                    packet.ReadInt();
                }
                count = packet.ReadInt();
                for (int i = 0; i < count; i++)
                {
                    packet.ReadInt();
                    packet.ReadLong();
                }
                bool read = packet.ReadBool();
                if (read)
                {
                    packet.ReadByte();
                    count = packet.ReadInt();
                    for (int i = 0; i < count; i++)
                    {
                        packet.ReadLong();
                    }
                }
                packet.ReadBytes(42);
                int jobID = packet.ReadShort();
                packet.ReadBytes(26);
                if (Util.isExtendSPJob(jobID))
                {
                    int size = packet.ReadByte();
                    packet.ReadBytes(size * 5);
                }
                else
                {
                    packet.ReadShort();
                }
                packet.ReadBytes(20);
                mapID = packet.ReadInt();
                portal = packet.ReadByte();
            }
            else
            {
                packet.ReadByte();
                mapID = packet.ReadInt();
                portal = packet.ReadByte();
            }
            if (Parser.MAP_ID != 0 && Parser.getActiveScript().GetScriptType() != ScriptType.None)
            {
                Parser.getActiveScript().addLine(String.Format("sm.warp({0}, {1})", mapID, portal));
            }
            return mapID;
        }
    }
}
