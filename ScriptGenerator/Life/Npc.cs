using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGenerator.Life
{
    public class Npc
    {
        private int objectID, templateID, x, y;
        private String objectName = "";

        public Npc(int objectID, int templateID)
        {
            this.objectID = objectID;
            this.templateID = templateID;
        }

        public int ObjectID { get => objectID; set => objectID = value; }
        public int TemplateID { get => templateID; set => templateID = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public string ObjectName { get => objectName; set => objectName = value; }
    }
}
