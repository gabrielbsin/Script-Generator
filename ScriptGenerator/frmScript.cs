using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptGenerator
{
    public partial class frmScript : Form
    {
        public frmScript()
        {
            InitializeComponent();
        }

        public void setInformation(Script scriptInfo)
        {
            Text = scriptInfo.getTitle();
            scriptName.Text = scriptInfo.getScriptName();
            String importUI = scriptInfo.isUIScript() ? "from net.swordie.ms.enums import UIType\r\n\r\n" : "";
            script.Text = scriptInfo.getCredits() + importUI + scriptInfo.getScript();
            //StartPosition = FormStartPosition.CenterParent;
        }
    }
}
