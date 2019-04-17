using ScriptGenerator.Tools;
using System;
using System.IO;
using System.Windows.Forms;
using static ScriptGenerator.Tools.Opcodes;

namespace ScriptGenerator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void mFileOpenMenu_Click(object sender, EventArgs e)
        {
            if (mOpenDialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string path in mOpenDialog.FileNames)
                {
                    OpenReadOnly(path);
                }
            }
        }

        public void OpenReadOnly(string pFilename)
        {
            using (FileStream stream = new FileStream(pFilename, FileMode.Open, FileAccess.Read))
            {
                BinaryReader reader = new BinaryReader(stream);
                ushort MapleSharkVersion = reader.ReadUInt16();
                ushort mBuild = MapleSharkVersion;
                byte mLocale = 8;
                if (MapleSharkVersion < 0x2000)
                {

                    ushort mLocalPort = reader.ReadUInt16();
                }
                else
                {
                    byte v1 = (byte)((MapleSharkVersion >> 12) & 0xF),
                           v2 = (byte)((MapleSharkVersion >> 8) & 0xF),
                           v3 = (byte)((MapleSharkVersion >> 4) & 0xF),
                           v4 = (byte)((MapleSharkVersion >> 0) & 0xF);
                    Console.WriteLine("Loading MSB file, saved by MapleShark V{0}.{1}.{2}.{3}", v1, v2, v3, v4);

                    if (MapleSharkVersion == 0x2012)
                    {
                        mLocale = (byte)reader.ReadUInt16();
                        mBuild = reader.ReadUInt16();
                        ushort mLocalPort = reader.ReadUInt16();
                    }
                    else if (MapleSharkVersion == 0x2014)
                    {
                        String mLocalEndpoint = reader.ReadString();
                        ushort mLocalPort = reader.ReadUInt16();
                        String mRemoteEndpoint = reader.ReadString();
                        ushort mRemotePort = reader.ReadUInt16();

                        mLocale = (byte)reader.ReadUInt16();
                        mBuild = reader.ReadUInt16();
                    }
                    else if (MapleSharkVersion == 0x2015 || MapleSharkVersion >= 0x2020)
                    {
                        String mLocalEndpoint = reader.ReadString();
                        ushort mLocalPort = reader.ReadUInt16();
                        String mRemoteEndpoint = reader.ReadString();
                        ushort mRemotePort = reader.ReadUInt16();

                        mLocale = reader.ReadByte();
                        mBuild = reader.ReadUInt16();

                        if (MapleSharkVersion >= 0x2021)
                        {
                            String mPatchLocation = reader.ReadString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("I have no idea how to open this MSB file. It looks to me as a version " + string.Format("{0}.{1}.{2}.{3}", v1, v2, v3, v4) + " MapleShark MSB file... O.o?!");
                        return;
                    }
                }

                while (stream.Position < stream.Length)
                {
                    long timestamp = reader.ReadInt64();
                    int size = MapleSharkVersion < 0x2027 ? reader.ReadUInt16() : reader.ReadInt32();
                    ushort opcode = reader.ReadUInt16();
                    bool outbound;

                    if (MapleSharkVersion >= 0x2020)
                    {
                        outbound = reader.ReadBoolean();
                    }
                    else
                    {
                        outbound = (size & 0x8000) != 0;
                        size = (ushort)(size & 0x7FFF);
                    }

                    byte[] buffer = reader.ReadBytes(size);

                    uint preDecodeIV = 0, postDecodeIV = 0;
                    if (MapleSharkVersion >= 0x2025)
                    {
                        preDecodeIV = reader.ReadUInt32();
                        postDecodeIV = reader.ReadUInt32();
                    }
                    Packet packet = new Packet(buffer);
                    if (outbound)
                    {
                        Parser.parseOutbound(Util.GetEnumObjectByValue<OutboundOpcodes>(opcode), packet);
                    } else
                    {
                        Parser.parseInbound(Util.GetEnumObjectByValue<InboundOpcodes>(opcode), packet, timestamp);
                    }
                }
            }
            Parser.showAllScripts();
            Text = String.Format("Script Generator | Created by MechAviv");
            Console.WriteLine("Loaded file: {0}", pFilename);
        }

        private void open_Click(object sender, EventArgs e)
        {
            if (mOpenDialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string path in mOpenDialog.FileNames)
                {
                    OpenReadOnly(path);
                }
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
    
