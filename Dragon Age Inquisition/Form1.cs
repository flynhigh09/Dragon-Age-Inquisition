using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using PS3Lib.NET;
using PS3Lib;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Web;
using System.Diagnostics;

namespace Dragon_Age_Inquisition
{
    public partial class Form1 : Form
    {
        #region Main Variables
        public static PS3API PS3 = new PS3API();
        public static string TITLE;
        public static bool Connected = false;
        public static bool isCONNECTED = false;
        public static bool isATTACHED = false;
        private int Api = 0;
        private static int NumberOffsets = 0;
        private static uint ZeroOffset;
        private bool Checkinout;
        private static int connected = 0;
        public static int apiDLL = 0;
        public static string IPAddrStr = "";
        public static string settFile = "";
        public static string Codesf = "";
        public static uint gstart = 0U;
        public static uint progresss = 0U;
      
        public static uint Health = 0U;
        public static uint InfiniteItemUsage = 0U;
        public static uint ExpMultiplier = 0U;
        public static uint Infabilitypoints = 0U;
        public static uint InfabilitypointsandMana = 0U;
        public static uint Inquisitionexpmultiplier = 0U;
        public static uint Nocooldown = 0u;
        public static uint codefunctionaddress = 0u;
        public static uint code_function_address2 = 0u;
        public static uint code_function_address1 = 0u;
        public static uint codefunction_address = 0u;
        public static uint codefunctionaddress2 = 0u;
        #endregion

        #region  Call Form Functions
        public struct IBArg
        {
            public string label;
            public string defStr;
            public string retStr;
        };
        public IBArg[] CallIBox(IBArg[] a)
        {
            InputBox ib = new InputBox();
            ib.Arg = a;
            ib.fmHeight = Height;
            ib.fmWidth = Width;
            ib.fmLeft = Left;
            ib.fmTop = Top;
            ib.TopMost = true;
            ib.fmForeColor = ForeColor;
            ib.fmBackColor = BackColor;
            ib.Show();

            while (ib.ret == 0)
            {
                a = ib.Arg;
                Application.DoEvents();
            }

            a = ib.Arg;
            if (ib.ret == 1)
                return a;
            else if (ib.ret == 2)
                return null;
            return null;
        }
        /* ForeColor and BackColor */
        public static Color ncBackColor = Color.Black;
        public static Color ncForeColor = Color.FromArgb(200, 25, 125);
        /* Keybind arrays */
        public static Keys[] keyBinds = new Keys[8];
        public static string[] keyNames = new string[] {"Connect", "Attach", "Disconnect","Save All", "Set All", "Load All",};
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                Checkinout = true;
                Opacity = 0;
                ScreenTimer.Enabled = true;
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (e.Cancel == true)
                return;
            if (Opacity > 0)
            {
                Checkinout = false;
                ScreenTimer.Enabled = true;
                e.Cancel = true;
            }
        }
        private void ScreenTimer_Tick(object sender, EventArgs e)
        {
            if (Checkinout == false)
            {
                Opacity -= (ScreenTimer.Interval / 1000.0);
                if (Opacity > 0)
                    ScreenTimer.Enabled = true;
                else
                {
                    ScreenTimer.Enabled = false;
                    Close();
                }
            }
            else
            {
                Opacity += (ScreenTimer.Interval / 750.0);
                ScreenTimer.Enabled = (Opacity < 1.0);
                Checkinout = (Opacity < 1.0);
            }
        }

        public static int ConnectPS3()
        {
            try
            {
                if (apiDLL == 0) //TMAPI
                {
                    if (PS3.ConnectTarget())
                        return 1;
                }
                else //CCAPI
                {
                    if (IPAddrStr != "" && PS3.ConnectTarget(IPAddrStr))
                        return 1;
                }
            }
            catch
            {
            }

            return 0;
        }
        public static int AttachPS3()
        {
            if (PS3.AttachProcess())
                return 2;

            return 0;
        }
        public static void SaveOptions()
        {
            using (System.IO.StreamWriter fd = new System.IO.StreamWriter(settFile, false))
            {
                //KeyBinds
                for (int x = 0; x < keyBinds.Length; x++)
                {
                    string key = keyBinds[x].GetHashCode().ToString();
                    fd.WriteLine(key);
                }
                //Colors
                fd.WriteLine(ncBackColor.Name);
                fd.WriteLine(ncForeColor.Name);

                //API
                fd.WriteLine(apiDLL.ToString());

                //Save Ip
                fd.WriteLine(IPAddrStr.ToString());
            }
        }
        private void AllButtons_MouseEnter(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var name = currentButton.Name;
            currentButton.ForeColor = ncBackColor;
            currentButton.BackColor = ncForeColor;
        }
        private void AllButtons_MouseLeave(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var name = currentButton.Name;
            currentButton.ForeColor = ncForeColor;
            currentButton.BackColor = ncBackColor;
        }
        public void NotConnected()
        {
            Connect.ForeColor = Color.OrangeRed;
            Connect.Text = ("Not Connected");
        }
        public void IsConnected()
        {
            Connect.ForeColor = Color.Chartreuse;
            Connect.Text = ("Connected");
        }
        public void NotAttached()
        {
            Attached.ForeColor = Color.OrangeRed;
            Attached.Text = ("Not Attached");
        }
        public void IsAttached()
        {
            Attached.ForeColor = Color.DodgerBlue;
            Attached.Text = ("Attached");
        }
        public static uint Hook(uint Sb, uint Bh)
        {
            if (Sb > Bh)
                return 0x4C000000 - (Sb - Bh);
            else if (Sb < Bh)
                return Bh - Sb + 0x48000000;
            else
                return 0x48000000;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Closing(object sender, EventArgs e)
        {
            Checkinout = true;
            if (PS3.GetCurrentAPI() == SelectAPI.ControlConsole)
                PS3.DisconnectTarget();
            Connect.Text = "Disconnected";
            AddressTimer.Stop();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.MouseEnter += AllButtons_MouseEnter;
                button.MouseLeave += AllButtons_MouseLeave;
            }
            string VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            TITLE = this.Text; this.Text = this.Text + " - v" + VERSION;

            settFile = Application.StartupPath + "\\ps3.ini";
            int x = 0;
            if (File.Exists(settFile))
            {
                string[] settLines = File.ReadAllLines(settFile);
                try
                {
                    for (x = 0; x < keyBinds.Length; x++)
                        keyBinds[x] = (Keys)int.Parse(settLines[x]);

                    ncBackColor = Color.FromArgb(int.Parse(settLines[x], System.Globalization.NumberStyles.HexNumber)); BackColor = ncBackColor; x++;
                    ncForeColor = Color.FromArgb(int.Parse(settLines[x], System.Globalization.NumberStyles.HexNumber)); ForeColor = ncForeColor; x++;
                    apiDLL = int.Parse(settLines[x]); x++;
                    IPAddrStr = int.Parse(settLines[x]).ToString(); x++;
                }
                catch
                {
                }
            }
            PS3.ChangeAPI((apiDLL == 0) ? SelectAPI.TargetManager : SelectAPI.ControlConsole);
            if (apiDLL == 0)
                PS3.PS3TMAPI_NET();

            int ctrl = 0;
            for (ctrl = 0; ctrl < Controls.Count; ctrl++)
            {
                Controls[ctrl].BackColor = ncBackColor;
                Controls[ctrl].ForeColor = ncForeColor;
            }
            int ctrl1 = 0;
            for (ctrl1 = 0; ctrl1 < toolStrip1.Controls.Count; ctrl1++)
            {
                toolStrip1.Controls[ctrl1].BackColor = ncBackColor;
                toolStrip1.Controls[ctrl1].ForeColor = ncForeColor;
            }   
        }
        private void Options_Click(object sender, EventArgs e)
        {
            OptionForm oForm = new OptionForm();
            oForm.Show();
        }
        private void Connect_Click(object sender, EventArgs e)
        {
            Connect.Text = "Connecting..";
            try
            {
                if (apiDLL == 0)
                {
                    if (PS3.ConnectTarget())
                    {
                        PS3.AttachProcess();
                        isCONNECTED = true;
                        Connect.Text = "Connected";
                        Ps3.Connect(); Ps3.Attach();
                        Game.Text = Ps3.GetGame();
                        IsConnected(); IsAttached();
                    }
                }
                else
                {
                    IBArg[] ibArg = new IBArg[1];
                    ibArg[0].defStr = (IPAddrStr == "") ? "192.168.1.0" : IPAddrStr;
                    ibArg[0].label = " PS3  IP";
                    ibArg = CallIBox(ibArg);

                    if (ibArg == null)
                    {
                        Connect.Text = "Cancelled Connecting";
                        return;
                    }
                    IPAddrStr = ibArg[0].retStr;
                    if (ibArg[0].retStr != "")
                    {
                        if (PS3.ConnectTarget(ibArg[0].retStr))
                        {
                            PS3.AttachProcess();
                            isCONNECTED = true;
                            Connect.Text = "Connected";
                            IsConnected(); IsAttached();
                        }
                    }
                }
                if (isCONNECTED == false)
                {
                    Connect.Text = "Failed to connect to PS3";
                    NotConnected();
                    isCONNECTED = false;
                }
            }
            catch
            {
                Connect.Text = "Failed to connect to PS3";
                NotConnected();
                isCONNECTED = false;
            }
        }
        private void SaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Ini Files|*.ini|All Files|*.*";
                saveFileDialog.ShowDialog();
                if ((saveFileDialog.FileName) == "")
                {
                    MessageBox.Show("Closed");
                }
                if (!File.Exists((saveFileDialog.FileName)))
                {
                    using (File.Create((saveFileDialog.FileName))) ;
                }
                IniParser iniParser = new IniParser((saveFileDialog.FileName));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "Health", Health.ToString("X"));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "Items Usage", InfiniteItemUsage.ToString("X"));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "Exp Multiplier", ExpMultiplier.ToString("X"));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "Infability Points", Infabilitypoints.ToString("X"));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "Inquisition Exp Multiplier", Inquisitionexpmultiplier.ToString("X"));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "No Cooldown", Nocooldown.ToString("X"));
                iniParser.SaveSettings();
                MessageBox.Show("Succesfully Saved Addresses for Update " + Update.Value);
            }
            catch
            {
                MessageBox.Show("Save Aborted!");
            }
        }
        private void LoadAll_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Ini Files|*.ini|All Files|*.*";
                openFileDialog.ShowDialog();
                IniParser iniParser = new IniParser(openFileDialog.FileName);
                string fileName = openFileDialog.FileName;
                Health = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "Health"), NumberStyles.HexNumber);
                InfiniteItemUsage = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "Infinite Items"), NumberStyles.HexNumber);
                ExpMultiplier = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "Exp Multiplier"), NumberStyles.HexNumber);
                Infabilitypoints = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "Ability Points"), NumberStyles.HexNumber);
                Inquisitionexpmultiplier = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "Inquisition Exp Multiplier"), NumberStyles.HexNumber);
                Nocooldown = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "No Cooldown"), NumberStyles.HexNumber);
                gstart = 1U;
                AddressTimer.Start();
                PROG.Visible = true;
                SaveAll.Enabled = true;

                if (Health > 0U && (InfiniteItemUsage > 0U && ExpMultiplier > 0U))
                {
                    MessageBox.Show("Addresses Loaded For " + Update.Value);
                    PROG.Visible = true;
                    SaveAll.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Attention", "Save May Not Exist for " + Update.Value + ", or Is Corrupted");
                }
            }
            catch
            {
            }
        }
        private void InfHealth_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes_Found_branch_address = StringBAToBA("7CA318147C6328147C641814786300204800000838600000382100B048397260");// Search For Bytes
                Health = Search(bytes_Found_branch_address, 0x01280000, 0x1000000, 4);

                byte[] bytes_function_address = StringBAToBA("000000000000000000000000000000000000000000000000");// Search For Code Spot
                uint codefunctionaddress = Search(bytes_function_address, 0x018BDA00, 0x1000000, 4);

                uint end_function = codefunctionaddress + 0x14;
                uint Health_back_too_address = Health + 0x04;

                byte[] Healthcode = StringBAToBA("2C0303D04082000C83C403B493C40B947CA31814");//Set Health Code Bytes in found spot
                PS3.SetMemory(codefunctionaddress, Healthcode);

                PS3.Extension.WriteUInt32(end_function, Hook(end_function, Health_back_too_address));
                PS3.Extension.WriteUInt32(Health, Hook(Health, codefunctionaddress));

                RichTextBox richTextBox3 = this.Output;
                string str3 = richTextBox3.Text + "Health: 0x" + Health.ToString("X");
                richTextBox3.Text = str3;

                InfHealth.Text = "Inf Health Set";
            }
            catch (Exception)
            {
                InfHealth.Text = "Not Found";
                throw;
            }
        }
        private void EXPMultiplier_Click(object sender, EventArgs e)
        {
            DialogResult Expmessage = MessageBox.Show("Do you want to Continue", "MAY FREEZE", MessageBoxButtons.OKCancel);
            if (Expmessage == DialogResult.OK)
            {
                try
                {
                    byte[] Found_branch = StringBAToBA("C0250024EFE1F82A48CAEB29"); // Search For Bytes
                    ExpMultiplier = Search(Found_branch, 0x005D0000, 0x1000000, 4);

                    byte[] function_address = StringBAToBA("000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");// Search For Code Spot
                    uint code_function_address1 = Search(function_address, 0x017E0000, 0x1000000, 4);

                    byte[] EXP = StringBAToBA("880500212C000001408200208805003A2C0000C2408200143C0042C8900500D0C02500D0FFFF0072C02500244E800020");
                    PS3.SetMemory(code_function_address1, EXP);                               /*^Multiplier^*/

                    PS3.Extension.WriteUInt32(ExpMultiplier, Hook(ExpMultiplier, code_function_address1));

                    RichTextBox richTextBox5 = this.Output;
                    string str5 = richTextBox5.Text + "\nExp Multiplier: 0x" + ExpMultiplier.ToString("X");
                    richTextBox5.Text = str5;

                    EXPMultiplier.Text = "Multiplier Set";
                }
                catch (Exception)
                {
                    EXPMultiplier.Text = "Not Found";
                }
            }
            if (Expmessage == DialogResult.Cancel)
            {
                MessageBox.Show("Retry");
            }
        }
        private void Infiniteabilitypoints_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes_Found_branch_address = StringBAToBA("D03F002441820030889F0020");// Search For Bytes
                Infabilitypoints = Search(bytes_Found_branch_address, 0x005D0000, 0x00080000, 4);

                byte[] bytes_function_address = StringBAToBA("000000000000000000000000000000000000000000000000");// Search For Code Spot
                uint code_function_address2 = Search(bytes_function_address, 0x01757700, 0x1000000, 4);

                uint end_function2 = code_function_address2 + 0x14;
                uint abilitypoints_back_too_address = Infabilitypoints;

                byte[] Infiniteability = StringBAToBA("D03F00242C1800014082000C3C8042C6909F0024");
                PS3.SetMemory(code_function_address2, Infiniteability);

                PS3.Extension.WriteUInt32(end_function2, Hook(end_function2, abilitypoints_back_too_address + 0x04));
                PS3.Extension.WriteUInt32(Infabilitypoints, Hook(Infabilitypoints, code_function_address2));

                RichTextBox richTextBox7 = this.Output;
                string str7 = richTextBox7.Text + "\nAbility Points: 0x" + Infabilitypoints.ToString("X");
                richTextBox7.Text = str7;

                Infiniteabilitypoints.Text = "Ability Points Set";
            }
            catch (Exception)
            {
                Infiniteabilitypoints.Text = "Not Found";
                throw;
            }
        }
        private void InfItems_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes_Found_branch_address = StringBAToBA("7C8620142C0400004080000838800000");// Search For Bytes
                InfiniteItemUsage = Search(bytes_Found_branch_address, 0x00400000, 0x1000000, 4);

                byte[] Items = StringBAToBA("388000632C0400004080000838800000"); //Set Health Code Bytes in found spot
                PS3.SetMemory(InfiniteItemUsage, Items);

                RichTextBox richTextBox4 = this.Output;
                string str4 = richTextBox4.Text + "\nInfinite Items: 0x" + InfiniteItemUsage.ToString("X");
                richTextBox4.Text = str4;

                InfItems.Text = "Inf Items Set";
            }
            catch (Exception)
            {
                InfItems.Text = "Not Found";
                throw;
            }
        }
        private void InquisitionEXPmultiplier_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes_Found = StringBAToBA("93C6001C6383000080BC000063A40000");// Search For Bytes
                Inquisitionexpmultiplier = Search(bytes_Found, 0x00C00000, 0x1000000, 4);

                byte[] bytes_function = StringBAToBA("000000000000000000000000000000000000000000000000000000000000000000000000");// Search For Code Spot
                uint codefunction_address = Search(bytes_function, 0x0177B900, 0x1000000, 4);

                uint end_of_function = codefunction_address + 0x20;
                uint back_too_address = Inquisitionexpmultiplier;

                byte[] code = StringBAToBA("8066003C2C0303E8408200148066001C7C63F0501C6300647FC3F21493C6001C");
                PS3.SetMemory(codefunction_address, code);

                PS3.Extension.WriteUInt32(end_of_function, Hook(end_of_function, back_too_address + 0x04));
                PS3.Extension.WriteUInt32(Inquisitionexpmultiplier, Hook(Inquisitionexpmultiplier, codefunction_address));

                RichTextBox richTextBox8 = this.Output;
                string str8 = richTextBox8.Text + "\nInquisition Exp: 0x" + Inquisitionexpmultiplier.ToString("X");
                richTextBox8.Text = str8;

                InquisitionEXPmultiplier.Text = "Inquisition EXP Set";
            }
            catch (Exception)
            {
                InquisitionEXPmultiplier.Text = "Not Found";
                throw;
            }
        }
        private void NoCooldown_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes_Found_branch_address = StringBAToBA("EC23082A2C030000D03F00104082001C");// Search For Bytes
                Nocooldown = Search(bytes_Found_branch_address, 0x004E0000, 0x1000000, 4);

                byte[] Items = StringBAToBA("C03F00142C030000D03F00104082001C"); //Set Health Code Bytes in found spot
                PS3.SetMemory(Nocooldown, Items);

                RichTextBox richTextBox9 = this.Output;
                string str9 = richTextBox9.Text + "\nNo Cooldown: 0x" + Nocooldown.ToString("X");
                richTextBox9.Text = str9;

                NoCooldown.Text = "No Cooldown Set";
            }
            catch (Exception)
            {
                NoCooldown.Text = "Not Found";
                throw;
            }
        }

        #region Set All
        public void Getall()
        {
            if (Infmana.Checked)
            {
                NewThread(new Action(InfabilitypointsandnMana)); // Adds ability + Mana
                //NewThread(new Action(MaxXPMultiplier));
                NewThread(new Action(nfiniteItemUsage));               
                NewThread(new Action(InfiniteHP));
                NewThread(new Action(Inquisitionmultiplier));
                NewThread(new Action(Cooldown));
            }
            else
            {
                NewThread(new Action(abilitypoints)); // Just ability
                //NewThread(new Action(MaxXPMultiplier));
                NewThread(new Action(nfiniteItemUsage));                
                NewThread(new Action(InfiniteHP));
                NewThread(new Action(Inquisitionmultiplier));
                NewThread(new Action(Cooldown));
            }
        }
        private void SetAll_Click(object sender, EventArgs e)
        {
            Health = 0;
            InfiniteItemUsage = 0;
            ExpMultiplier = 0;
            Infabilitypoints = 0;
            InfabilitypointsandMana = 0;
            Inquisitionexpmultiplier = 0;
            Nocooldown = 0;
            gstart = 0U;
            progresss = 0U;
            AddressTimer.Start();
        }

        private void AddressTimer_Tick(object sender, EventArgs e)
        {
            if ((int) gstart == 0)
            {
                NewThread(new Action(this.Getall));
                ++gstart;
            }
            AddressTimer.Start();
            PROG.Visible = true;
            PROG.Value = (int) progresss;
            if (Health > 4194304U)
            {
                InfHealth.Enabled = true;
            }
            if (InfiniteItemUsage > 4194304U)
            {
                InfItems.Enabled = true;
            }
            if (ExpMultiplier > 4194304U)
            {
                Infiniteabilitypoints.Enabled = true;
            }
            if (Infabilitypoints > 4194304U)
            {
                EXPMultiplier.Enabled = true;
            }
            if (Inquisitionexpmultiplier > 4194304U)
            {
                InquisitionEXPmultiplier.Enabled = true;
            }
            if (Nocooldown > 4194304U)
            {
                NoCooldown.Enabled = true;
            }

            //((Health <= 0U || InfiniteItemUsage <= 0U || ExpMultiplier <= 0U || Infabilitypoints <= 0U && (Inquisitionexpmultiplier <= 0U || Nocooldown <= 0U)))
            if ((Health <= 0U || InfiniteItemUsage <= 0U ||
                 (ExpMultiplier <= 0U || Infabilitypoints <= 0U && (Inquisitionexpmultiplier <= 0U || Nocooldown <= 0U))))
                return;
            SaveAll.Enabled = true;
            SetAll.Text = "";

            if ((int) Health == 69)
            {
                RichTextBox richTextBox = this.Output;
                string str = richTextBox.Text + "ERROR: Finding: XP";
                richTextBox.Text = str;
                Health = 0U;
            }
            if ((int) InfiniteItemUsage == 69)
            {
                RichTextBox richTextBox = this.Output;
                string str = richTextBox.Text + "ERROR: Finding: SkillsPoints";
                richTextBox.Text = str;
                InfiniteItemUsage = 0U;
            }
            if ((int) ExpMultiplier == 69)
            {
                RichTextBox richTextBox = this.Output;
                string str = richTextBox.Text + "ERROR: Finding: Ranks";
                richTextBox.Text = str;
                ExpMultiplier = 0U;
            }
            if ((int) Infabilitypoints == 69)
            {
                RichTextBox richTextBox = this.Output;
                string str = richTextBox.Text + "ERROR: Finding: Max Arrows";
                richTextBox.Text = str;
                Infabilitypoints = 0U;
            }
            if ((int) Inquisitionexpmultiplier == 69)
            {
                RichTextBox richTextBox = this.Output;
                string str = richTextBox.Text + "ERROR: Finding: Max Arrows";
                richTextBox.Text = str;
                Inquisitionexpmultiplier = 0U;
            }
            if ((int) Nocooldown == 69)
            {
                RichTextBox richTextBox = this.Output;
                string str = richTextBox.Text + "ERROR: Finding: Max Arrows";
                richTextBox.Text = str;
                Nocooldown = 0U;
            }
            RichTextBox richTextBox1 = this.Output;
            string str1 = richTextBox1.Text + "Dragon Age: Inquisition v " + Update.Value + "\n Dumper By flynhigh09 \n";
            richTextBox1.Text = str1;

            RichTextBox richTextBox2 = this.Output;
            string str2 = richTextBox2.Text + "~~~~~~~~~~~~~~~~~~~~\n";
            richTextBox2.Text = str2;

            RichTextBox richTextBox3 = this.Output;
            string str3 = richTextBox3.Text + "Health: 0x" + Health.ToString("X");
            richTextBox3.Text = str3;

            RichTextBox richTextBox4 = this.Output;
            string str4 = richTextBox4.Text + "\nInfinite Items: 0x" + InfiniteItemUsage.ToString("X");
            richTextBox4.Text = str4;

            RichTextBox richTextBox5 = this.Output;
            string str5 = richTextBox5.Text + "\nExp Multiplier: 0x" + ExpMultiplier.ToString("X");
            richTextBox5.Text = str5;

            RichTextBox richTextBox7 = this.Output;
            string str7 = richTextBox7.Text + "\nAbility Points: 0x" + Infabilitypoints.ToString("X");
            richTextBox7.Text = str7;

            RichTextBox richTextBox8 = this.Output;
            string str8 = richTextBox8.Text + "\nInquisition Exp: 0x" + Inquisitionexpmultiplier.ToString("X");
            richTextBox8.Text = str8;

            RichTextBox richTextBox9 = this.Output;
            string str9 = richTextBox9.Text + "\nNo Cooldown: 0x" + Nocooldown.ToString("X");
            richTextBox9.Text = str9;

            RichTextBox richTextBox6 = this.Output;
            string str6 = richTextBox6.Text + "\n-------------------------------------";
            richTextBox2.Text = str6;

            gstart = 0U;
            PROG.Value = PROG.Maximum;
            SetAll.Text = "Done";
            AddressTimer.Stop();
            PROG.Visible = false;
        }
        public void InfiniteHP()
        {
            byte[] bytes_Found_branch_address = StringBAToBA("7CA318147C6328147C641814786300204800000838600000382100B048397260");// Search For Bytes
            Health = Search(bytes_Found_branch_address, 0x01280000, 0x1000000, 4);

            byte[] bytes_function_address = StringBAToBA("000000000000000000000000000000000000000000000000");// Search For Code Spot
            codefunctionaddress = Search(bytes_function_address, 0x018BDA00, 0x1000000, 4);

            uint end_function = codefunctionaddress + 0x14;
            uint Health_back_too_address = Health;

            byte[] Healthcode = StringBAToBA("2C0303D04082000C83C403B493C40B947CA31814");//Set Health Code Bytes in found spot
            PS3.SetMemory(codefunctionaddress, Healthcode);

            PS3.Extension.WriteUInt32(end_function, Hook(end_function, Health_back_too_address + 0x04));
            PS3.Extension.WriteUInt32(Health, Hook(Health, codefunctionaddress));
            progresss += 10U;
        }
        public void MaxXPMultiplier()
        {
            byte[] Found_branch = StringBAToBA("C0250024EFE1F82A48CAEB29");// Search For Bytes
            ExpMultiplier = Search(Found_branch, 0x005D0000, 0x1000000, 4);

            byte[] function_address = StringBAToBA("000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");// Search For Code Spot
            code_function_address1 = Search(function_address, 0x017E0000, 0x1000000, 4);

            byte[] EXP = StringBAToBA("880500212C000001408200208805003A2C0000C2408200143C0042C8900500D0C02500D0FFFF0072C02500244E800020 ");
            PS3.SetMemory(code_function_address1, EXP);                               /*^Multiplier^*/

            PS3.Extension.WriteUInt32(ExpMultiplier, Hook(ExpMultiplier, code_function_address1));
            progresss += 10U;
        }
        public void Inquisitionmultiplier()
        {
            byte[] bytes_Found = StringBAToBA("93C6001C6383000080BC000063A40000");// Search For Bytes
            Inquisitionexpmultiplier = Search(bytes_Found, 0x00C00000, 0x1000000, 4);

            byte[] bytes_function = StringBAToBA("000000000000000000000000000000000000000000000000000000000000000000000000");// Search For Code Spot
            codefunction_address = Search(bytes_function, 0x0177B900, 0x1000000, 4);

            uint end_of_function = codefunction_address + 0x20;
            uint back_too_address = Inquisitionexpmultiplier;

            byte[] code = StringBAToBA("8066003C2C0303E8408200148066001C7C63F0501C6300647FC3F21493C6001C");
            PS3.SetMemory(codefunction_address, code);

            PS3.Extension.WriteUInt32(end_of_function, Hook(end_of_function, back_too_address + 0x04));
            PS3.Extension.WriteUInt32(Inquisitionexpmultiplier, Hook(Inquisitionexpmultiplier, codefunction_address));
            progresss += 10U;
        }
        public void abilitypoints()
        {
            byte[] bytes_Found_branch_address = StringBAToBA("D03F002441820030889F0020");// Search For Bytes
            Infabilitypoints = Search(bytes_Found_branch_address, 0x005D0000, 0x00080000, 4);

            byte[] bytes_function_address = StringBAToBA("000000000000000000000000000000000000000000000000");// Search For Code Spot
            code_function_address2 = Search(bytes_function_address, 0x01757700, 0x1000000, 4);

            uint end_function2 = code_function_address2 + 0x14;
            uint abilitypoints_back_too_address = Infabilitypoints;

            byte[] abilitypoints = StringBAToBA("D03F00242C1800014082000C3C8042C6909F0024");
            PS3.SetMemory(code_function_address2, abilitypoints);

            PS3.Extension.WriteUInt32(end_function2, Hook(end_function2, abilitypoints_back_too_address + 0x04));
            PS3.Extension.WriteUInt32(Infabilitypoints, Hook(Infabilitypoints, code_function_address2));
            progresss += 10U;
        }
        public void InfabilitypointsandnMana()
        {
            byte[] bytes_Found_branch_address = StringBAToBA("D03F002441820030889F0020");// Search For Bytes
            InfabilitypointsandMana = Search(bytes_Found_branch_address, 0x005D0000, 0x00080000, 4);

            byte[] bytes_function_address = StringBAToBA("0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");// Search For Code Spot
            codefunctionaddress2 = Search(bytes_function_address, 0x018ADC00, 0x1000000, 4);

            uint end_function2 = codefunctionaddress2 + 0x14;
            uint abilitypoints_back_too_address = InfabilitypointsandMana;

            byte[] abilitypoints = StringBAToBA("D03F00242C1800014082000C3C8042C6909F0024A09F00202C043F014082000C3C8042C8909F00244E800020");
            PS3.SetMemory(codefunctionaddress2, abilitypoints);

            PS3.Extension.WriteUInt32(end_function2, Hook(end_function2, abilitypoints_back_too_address));
            PS3.Extension.WriteUInt32(InfabilitypointsandMana, Hook(InfabilitypointsandMana, codefunctionaddress2));
            progresss += 10U;
        }
        public void nfiniteItemUsage()
        {
            byte[] bytes_Found_branch = StringBAToBA("7C8620142C0400004080000838800000");// Search For Bytes
            InfiniteItemUsage = Search(bytes_Found_branch, 0x00400000, 0x1000000, 4);

            byte[] Items = StringBAToBA("388000632C0400004080000838800000");//Set Health Code Bytes in found spot
            PS3.SetMemory(InfiniteItemUsage, Items);
            progresss += 10U;
        }
        public void Cooldown()
        {
            byte[] Found_branch_address = StringBAToBA("EC23082A2C030000D03F00104082001C");// Search For Bytes
            Nocooldown = Search(Found_branch_address, 0x00400000, 0x1000000, 4);

            byte[] Cooldown = StringBAToBA("C03F00142C030000D03F00104082001C");
            PS3.SetMemory(Nocooldown, Cooldown);
            progresss += 10U;
        }
        #endregion

        #region ><>< Helpers ><><
        public void NewThread(Action task)
        {
            new Thread((() => task()))
            {
                Name = task.Method.Name
            }

            .Start();
        }
        public static string ByteAToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }
        private string StringFix(string h)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= h.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(h.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
            }
            sb.Replace((char)0x00, ' ');
            return sb.ToString();
        }
        // * Converts array at offset of length size to a ulong
        public static ulong ByteArrayToLong(byte[] array, int offset, int size)
        {
            int pos = 0, x = 0;
            ulong result = 0;
            //foreach (byte by in array)
            for (x = size; x > 0; x--)
            {
                if ((x - 1 + offset) >= array.Length)
                    result += 0;
                else
                    result += (ulong)((ulong)array[x - 1 + offset] << pos);
                pos += 8;
            }
            return result;
        }
        public static byte[] StringBAToBA(string str)
        {
            if (str == null || (str.Length % 2) == 1)
                return new byte[0];
            byte[] ret = new byte[str.Length / 2];
            for (int x = 0; x < str.Length; x += 2)
                ret[x / 2] = byte.Parse(sMid(str, x, 2), System.Globalization.NumberStyles.HexNumber);
            return ret;
        }
        public static string sMid(string text, int index, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", length, "length must be > 0");
            else if (length == 0 || text.Length == 0)
                return "";
            else if (text.Length < (length + index))
                return text;
            else
                return text.Substring(index, length);
        }
        public static uint ContainsSequences(byte[] toSearch, byte[] toFind, uint StartOffset, int bytes)
        {
            for (int i = 0; (i + toFind.Length) < toSearch.Length; i += bytes)
            {
                bool flag = true;
                for (int j = 0; j < toFind.Length; j++)
                {
                    if (toSearch[i + j] != toFind[j])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    NumberOffsets++;
                    int num3 = ((int)StartOffset) + i;
                    return (uint)num3;
                }
            }
            return 0;
        }
        public static uint Search(byte[] Search, uint Start, int Length, int bytes)
        {          
            byte[] ReadBytes = PS3.Extension.ReadBytes(Start, Length);
            uint num = ContainsSequences(ReadBytes, Search, Start, bytes);
            if (num.Equals(ZeroOffset))
            {                
                return 0;             
            }
            else
            {
                int counter = 0;
                foreach (int value in Search)
                    if (value == 1) ++counter;
                uint num2 = num + ((uint) counter);
                return num2;
            }
        }
        public static uint SearchTmapi(byte[] Search, uint Start, int Length, int bytes)
        {
            byte[] ReadBytes = Ps3.ReadInt(Start, Length);
            uint num = ContainsSequences(ReadBytes, Search, Start, bytes);
            if (num.Equals(ZeroOffset))
            {
                return 0; //not found
            }
            else
            {
                int counter = 0;
                foreach (int value in Search)
                    if (value == 1) ++counter;
                uint num2 = num + ((uint)counter);
                return num2;
            }
        }
        public uint SpecifiedSearch(byte[] ToSearchIn, byte[] FindWhat, uint UsedOffsetToGetBytesFrom, int OffsetNumber)
        {
            int num1 = 0;
            uint num2 = 0U;
            int num3 = 0;
        label_14:
            while (num3 + FindWhat.Length < ToSearchIn.Length)
            {
                bool flag = true;
                int num4 = 0;
                int num5 = FindWhat.Length - 1;
                for (int index = num4; index <= num5; ++index)
                {
                    if ((int)ToSearchIn[num3 + index] != (int)FindWhat[index])
                    {
                        if (!false)
                        {
                            num3 += 4;
                            goto label_14;
                        }
                        else
                        {
                            ++num1;
                            num2 = UsedOffsetToGetBytesFrom + (uint)num3;
                            if (num1.Equals(OffsetNumber))
                                return num2;
                            num3 += 4;
                            goto label_14;
                        }
                    }
                }
                if (!flag)
                {
                    num3 += 4;
                }
                else
                {
                    ++num1;
                    num2 = UsedOffsetToGetBytesFrom + (uint)num3;
                    num3 += 4;
                    if (num1.Equals(OffsetNumber))
                        return num2;
                }
            }
            return num2;
        }
        public static byte[] strToArray(string hex)
        {
            string str = hex.Replace("0x", "").Replace(" ", "");
            if (str.Length % 2 == 1)
                throw new Exception("Binary cannot have an odd number of digits");
            byte[] numArray = new byte[str.Length / 2];
            string[] strArray = hex.Split(new char[1]
            {
                ' '
            });
            int index = 0;
            foreach (string s in strArray)
            {
                numArray[index] = (byte)int.Parse(s, NumberStyles.HexNumber);
                ++index;
            }
            return numArray;
        }
        public static uint FindAddress(string UniqueBytes, uint startOffset = 1694498816U, uint difference = 0U, uint maxoffset = 2415919104U, uint skip = 4U, uint size = 65536U)
        {
            byte[] numArray1 = strToArray(UniqueBytes);
            int length1 = UniqueBytes.Replace(" ", "").Length / 2;
            byte[] numArray2 = new byte[length1];
            uint num1 = 0U;
            while (num1 < maxoffset - startOffset)
            {
                byte[] length2 = new byte[(int)(IntPtr)size];
                Ps3.ReadBytes(startOffset + num1, length2);
                uint num2 = 0U;
                while (num2 < size - 4U)
                {
                    int num3 = length1;
                    for (int index = 0; index < length1; ++index)
                    {
                        if ((int)length2[(int)(IntPtr)(checked((long)(ulong)unchecked((long)num2 + (long)index)))] != (int)numArray1[index])
                        {
                            --num3;
                            break;
                        }
                    }
                    if (num3 == length1)
                        return startOffset + num1 + num2 + difference;
                    num2 += skip;
                }
                num1 += size;
            }
            return 69U;
        }
        public static uint FindAddressCcapi(string UniqueBytes, uint startOffset = 1694498816U, uint difference = 0U, uint maxoffset = 2415919104U, uint skip = 4U, uint size = 65536U)
        {
            byte[] numArray1 = strToArray(UniqueBytes);
            int length1 = UniqueBytes.Replace(" ", "").Length / 2;
            byte[] numArray2 = new byte[length1];
            uint num1 = 0U;
            while (num1 < maxoffset - startOffset)
            {
                byte[] length2 = new byte[(int)(IntPtr)size];
                PS3.CCAPI.GetMemory(startOffset + num1, length2);
                uint num2 = 0U;
                while (num2 < size - 4U)
                {
                    int num3 = length1;
                    for (int index = 0; index < length1; ++index)
                    {
                        if ((int)length2[(int)(IntPtr)(checked((long)(ulong)unchecked((long)num2 + (long)index)))] !=(int)numArray1[index])
                        {
                            --num3;
                            break;
                        }
                    }
                    if (num3 == length1)
                        return startOffset + num1 + num2 + difference;
                    num2 += skip;
                }
                num1 += size;
            }
            return 69U;
        }
        private static int ScanBytes(byte[] bytesToScan, byte[] BytePattern, uint startOffset = 0U, uint MaxSearchLength = 2415919103U, uint skip = 4U)
        {
            int num = -1;
            if ((int)MaxSearchLength == int.MaxValue)
                MaxSearchLength = (uint)bytesToScan.Length;
            for (int index1 = (int)startOffset;
                (long)index1 < (long)(MaxSearchLength - startOffset) - (long)BytePattern.Length - 1L;
                ++index1)
            {
                if ((int)bytesToScan[index1] == (int)BytePattern[0])
                {
                    int index2 = 1;
                    while (index2 < BytePattern.Length &&
                           (int)bytesToScan[index1 + index2] == (int)BytePattern[index2])
                    {
                        ++index2;
                        if ((int)bytesToScan[index1 + index2] == (int)BytePattern[index2] &&
                            index2 == BytePattern.Length - 1)
                            return (int)((long)startOffset + (long)index1);
                    }
                }
            }
            return num;
        }
        #endregion
    }
}
