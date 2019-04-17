using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGenerator.Life
{
    public class LifeStorage
    {
        private static Dictionary<int, Npc> mNpc = new Dictionary<int, Npc>();

        public static Npc getNpcByObjectID(int objectID)
        {
            Npc npc = null;
            if (mNpc.TryGetValue(objectID, out npc))
            {
                return npc;
            }
            return npc;
        }

        public static void addNpc(int objectID, Npc npc)
        {
            Npc old = null;
            if (mNpc.TryGetValue(objectID, out old))
            {
                if (old != null)
                {
                    mNpc.Remove(objectID);
                }
            }
            mNpc.Add(objectID, npc);
        }

        public static void removeNpc(int objectID)
        {
            mNpc.Remove(objectID);
        }

        public static void clear()
        {
            mNpc.Clear();
        }
    }
}
