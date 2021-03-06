﻿using Sandbox;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage;
using VRageMath;
using CoreOs;
using System.Collections;

namespace core
{
    public class mainframe
    {
        IMyGridTerminalSystem GridTerminalSystem; // comment me out in space engineers
        bool VSTUDIO = false;

        /// RECOMMERED:
        ///
        // To use an EXTERNAL CONFIG SCREEN you have to rename one LCD on your GRID with the following ID
        // (and only with this ID!)
        const string SIOS_CONFIG_SCREEN_ID = "_SIOS_CONFIG_";
        // To use a Debugger Screen you have to set the desired Screens PUBLIC TITLE to the following ID:
        const string SIOS_DEBUG_SCREEN_ID = "_SIOS_CORE_DEBUG_";
        ///<SUMMARY>
        // Mandatory Blockname of your Mainframe
        const string SIOS_PROGRAMBLOCK_ID = "_SIOS_CORE_MAINFRAME_";
        // Mandatory Blockname of your EDI Defence Mainframe
        const string SIOS_ADDON_EDI_ID = "_SIOS_EDI_ADDON_";
        // Mandatory Blockname of your TVI Trading Mainframe
        const string SIOS_ADDON_TVI_ID = "_SIOS_TVI_ADDON_";

        Dictionary<string, string> RUNNING_CONFIGURATION;
        Dictionary<IMyInteriorLight, Color> LIGHT_CONFIGURATION;
        Dictionary<IMyTerminalBlock, String> BLOCK_INVENTORY;


        string platformID = "";
        bool INSTALL_ENABLED = false;
        bool hideVersionInfoOnScreen = false;
        bool allowBeaconRename = false;
        // Creator Consts
        const string CREATOR = "Mahtrok";
        const string CO_CREATOR = "cccdemon";
        const string TITLE = "$IOS <CORE> ";
        const string version = "v1.1";
        const string created = "10-09-15";
        const string updated = "10-24-15";
        const string contact = "Mahtrok@mahtrok.de";
        const string company = "Exodus Systems - 2015";

        const string LF = "\n";
        const string TAB = "    ";
        const string BLANK = " ";
        const char DIVIDER = '|';
        const char LIST_DIVIDER = ',';
        const char CONFIG_DIVIDER = '\n';
        const char CONFIG_SEPARATOR = ':';
        const char SUFFIX = ']';
        const string PREFIX = "[";

        // Rename Consts
        const string ASSEMBLER_ID = "$Asse";
        const string REFINERY_ID = "$Refi";
        const string CARGO_ID = "$Carg";
        const string CONNECTOR_ID = "$Conn";
        const string COLLECTOR_ID = "$Coll";
        const string BATTERY_ID = "$Batt";
        const string GRAVITY_ID = "$Grav";
        const string REACTOR_ID = "$Reac";
        const string SORTER_ID = "$Sort";
        const string OXYGEN_ID = "$OxyG";
        const string OXY_FARM_ID = "$OxyF";
        const string OXY_TANK_ID = "$OxyT";
        const string AIR_VENT_ID = "$Vent";
        const string DOOR_ID = "$Door";
        const string STORAGE_ID = "$Stor";
        const string PRODUCTION_ID = "$Prod";
        const string SPEAKER_ID = "$Spea";
        const string LIGHT_ID = "$Light";
        const string TERMINAL_ID = "$Ctrl";
        const string SENSOR_ID = "$Sen";
        const string PANEL_ID = "$LCD";
        const string INFO_ID = "$Info";
        const string PROGRAM_ID = "$Prog";
        const string TIMER_ID = "$Time";
        const string MISSILE_TURRET_ID = "$MTur";
        const string GATTLING_TURRET_ID = "$GTur";
        const string TURRET_ID = "$ITur";
        const string THRUSTER_ID = "$Thru";
        const string NO_CONFIG_ID = "$NA";
        const string TRADER_ID = "$IOS <TRADER>";
        const string UPDATE_ID = "$IOS <UPDATE>";

        // Screen/Alarm Color Config
        Color conditionRedBG = new Color(40, 0, 0);
        Color conditionOrangeBG = new Color(40, 10, 0);
        Color panelDefaultBG = new Color(40, 40, 40);
        Color conditionRedFC = new Color(255, 255, 255);
        Color conditionOrangeFC = new Color(255, 255, 255);
        Color panelDefaultFC = new Color(0, 0, 0);
        Color conditionRedLight = new Color(255, 0, 0);
        Color conditionOrangeLight = new Color(255, 110, 0);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // THERE IS NO NEED TO CHANGE ANYTHING BELOW THIS LINE, AS LONG AS YOU DO NOT EXACTLY KNOW WHAT  //
        // YOU ARE DOING!                                                                                                                                                              //
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        bool DEBUG_ENABLED = false;
        bool BOOTED = false;
        bool SECURITY_ACTIVE = false;
        bool INITIALIZED = false;
        // ??
        bool ENABLED = true;

        /// <Command API>
        const string API_RELOAD_CONFIG = "API_RELOAD_CONFIG";
        const string API_RAISE_ALARM = "API_RAISE_ALARM";
        const string API_DISABLE_ALARM = "API_DISABLE_ALARM";
        const string API_SHUTDOWN_SHIP = "API_SHUTDOWN_SHIP";
        const string API_BOOT_SHIP = "API_BOOT_SHIP";
        const string API_DISABLE_BLOCK = "API_DISABLE_BLOCK";
        const string API_ENABLE_BLOCK = "API_ENABLE_BLOCK";
        const string API_ATTACK_DETECTED = "API_ATTACK_DETECTED";
        const string API_ATTACK_DEFENDET = "API_ATTACK_DEFENDET";
        const string API_COLLISION_ALERT = "API_COLLISION_ALERT";
        const string API_STATUS_GREEN = "API_STATUS_GREEN";
        const string API_STATUS_ORANGE = "API_STATUS_ORANGE";
        const string API_STATUS_RED = "API_STATUS_RED";
        const string API_CANT_PRESURISE = "API_CANT_PRESURISE";

        const string API_EDI_LOGON = "API_EDI_LOGON";
        const string API_EDI_LOGOFF = "API_EDI_LOGOFF";

        const string API_TVI_LOGON = "API_TVI_LOGON";
        const string API_TVI_LOGOFF = "API_TVI_LOGOFF";

        const string API_COM_LOGON = "API_COM_LOGON";
        const string API_COM_LOGOFF = "API_COM_LOGOFF ";
        /// </Command API>
        Dictionary<string, string> API_COMMANDS;



        /// Config Text Consts
        const string KEY_PLATTFORM_ID = "C_PLATTFORM_ID";
        const string KEY_PLATFORM_ROLE_ID = "C_PLATFORM_ROLE_ID";
        const string KEY_CONDITION = "C_CONDITION";
        const string KEY_INSTALL_ENABLED = "C_INSTALL_ENABLED";
        const string KEY_HIDE_VERSION_INFO = "C_HIDE_VERSION_INFO";
        const string KEY_ALLOW_BEACON_RENAME = "C_ALLOW_BEACON_RENAME";
        const string KEY_ACTIVATE_SECURITY = "C_ACTIVATE_SECURITY";
        const string KEY_LCD_BG_COLOR = "C_LCD_BG_COLOR";
        const string KEY_LCD_FONT_COLOR = "C_LCD_FONT_COLOR";
        const string KEY_DEBUG_ENABLED = "C_DEBUG_ENABLED";
        const string KEY_EDI_INSTALLED = "C_EDI_INSTALLED";
        const string KEY_ATI_INSTALLED = "C_ATI_INSTALLED";


        string VALUE_PLATTFORM_ID = "0";
        string VALUE_PLATFORM_ROLE_ID = "0";
        string VALUE_CONDITION = "green";
        string VALUE_INSTALL_ENABLED = "false";
        string VALUE_HIDE_VERSION_INFO = "true";
        string VALUE_ALLOW_BEACON_RENAME = "false";
        string VALUE_ACTIVATE_SECURITY = "false";
        string VALUE_LCD_BG_COLOR = "40,40,40";
        string VALUE_LCD_FONT_COLOR = "255,255,255";
        string VALUE_DEBUG_ENABLED = "false";
        string VALUE_EDI_INSTALLED = "false";
        string VALUE_ATI_INSTALLED = "false";




        List<string> debugMessages;
        List<IMyAssembler> assemblers;
        List<IMyRefinery> refineries;
        List<IMyConveyorSorter> sorters;
        List<IMyOxygenGenerator> oxygens;
        List<IMyOxygenTank> oxytanks;
        List<IMyAirVent> airvents;
        List<IMyBatteryBlock> batteries;
        List<IMyReactor> reactors;
        List<IMyInventory> storage;
        List<IMyInventory> production;
        List<IMySoundBlock> speakers;
        List<IMyInteriorLight> lights;
        List<IMyDoor> doors;
        List<IMySensorBlock> sensors;
        List<IMyTerminalBlock> terminals;
        List<IMyTextPanel> panels;
        List<IMyTextPanel> debugger;
        List<IMyProgrammableBlock> programs;
        List<IMyProgrammableBlock> traders;
        List<IMyTimerBlock> timers;
        List<IMyLargeGatlingTurret> gatlings;
        List<IMyLargeMissileTurret> missiles;
        List<IMyLargeInteriorTurret> turrets;

        List<MARole> platformRoles;

        IMyTextPanel config;
        IMyTimerBlock securityTimer;
        IMyProgrammableBlock core;
        IMyProgrammableBlock EDI;

        int platformRoleID = 0;
        int airVentCnt = 0;
        string condition = "green";
        bool ediInstalled = false;
        bool tmviInstalled = false;


        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*                                                                            GETTER                                                                                  */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        int GetRandomPlatformID()
        {
            Random _rnd = new Random();
            return _rnd.Next(1000);
        }
        string GetTimeStamp()
        {
            return PREFIX + System.DateTime.Now.Hour.ToString() + ":"
            + System.DateTime.Now.Minute.ToString() + ":"
            + System.DateTime.Now.Second.ToString() + SUFFIX;
        }
        string GetVersionInfo()
        {
            return TITLE + BLANK + version + " by " + CREATOR + ", last Update: " + updated + LF;
        }
        string GetContactData()
        {
            return LF + "Creator: " + CREATOR + LF + "E-Mail: " + contact + LF + company;
        }
        string GetCoContactData()
        {
            return LF + "CoAutor: " + CO_CREATOR + LF;
        }
        string GetDebugInfo()
        {
            string _info = GetVersionInfo();
            for (int i = 0; i < debugMessages.Count; i++)
            {
                _info += debugMessages[i];
            }
            return _info;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*                                                                       MAIN METHOD                                                                           */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void start(string argument)
        {
            Main(argument);
        }
        void Main(string argument)
        {
            debugMessages = new List<string>();
            if (argument == "reinstall")
            {
                if (VALUE_PLATTFORM_ID != "0")
                {
                    INITIALIZED = false;
                    BOOTED = false;
                    Uninstall();
                    BLOCK_INVENTORY = new Dictionary<IMyTerminalBlock, string>();
                    Install();
                    INSTALL_ENABLED = false;
                } else {
                    Debug("ERROR: reinstall only available after install");
                }

            }
            if (argument == "install")
            {
                INITIALIZED = false;
                BOOTED = false;
                if (!INSTALL_ENABLED)
                {
                    BLOCK_INVENTORY = new Dictionary<IMyTerminalBlock, string>();
                    if (VALUE_PLATTFORM_ID == "0") {
                        debugMessages.Clear();
                        Install();
                        INSTALL_ENABLED = true;
                    } else {
                        debugMessages.Clear();
                        Debug("ERROR: You need to uninstall first!");
                    }
                    
                }
            }

            ///////////////////// INITIALIZATION /////////////////////
            if (!INITIALIZED) {
                _generateConfig();
                Init();
                DisplayDebug();
                Debug("System Initialized");
            } else {
                Debug("System already Initialized");
            }
            DisplayDebug();
            ////////////////////// DEBUGGER ///////////////////////
            ProcessArgument(argument);
            ///////////////////// INSTALLATION /////////////////////
            ////////////////// GETTING ALL BLOCKs /////////////////
            if (!BOOTED && INITIALIZED)
            {
                Debug("GETTING BLOCKS");
                GetBlocks();
                Debug("Generating Blockconfig");
            }
            Debug("STORING DATA");
            StoreExternalConfigData();
            Debug("DATA STORED");
            DisplayDebug();
            /////////////////////// DISPLAY /////////////////////////
            Display();
            ///////////////////// RESET SCRIPT ////////////////////
            //Reset();
            if (argument == "uninstall")
            {
                if (VALUE_PLATTFORM_ID == "0") {
                    Debug("ERROR: $SIOS is not installed!");
                } else {
                    debugMessages.Clear();
                    Debug("Uninstalling");
                    Uninstall();
                }
            }

            DisplayDebug();
        }

        void ProcessArgument(string _argument)
        {
            _argument = _argument.Trim();

            if (_argument == "reload")
            {
                LoadExternalConfigData();
            }
            if (_argument == "clear") {
                debugMessages.Clear();
            }

            else if (_argument.StartsWith("delete: "))
            {
                _argument = _argument.Substring(8);
                Delete(_argument);
            }
            else if (_argument.StartsWith("replace: "))
            {
                _argument = _argument.Substring(9);
                Replace(_argument);
            }
            else if (_argument.StartsWith("API_"))
            {
                ProcessAPIArgument(_argument);
            } else {
                Debug("Damn, argument is not valid");
            }


        }
        void Delete(string _toDelete)
        {
            List<IMyTerminalBlock> _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(_blocks);
            for (int i = 0; i < _blocks.Count; i++)
            {
                _blocks[i].SetCustomName(_blocks[i].CustomName.Replace(_toDelete, ""));
            }
        }
        void Replace(string _argument)
        {
            List<IMyTerminalBlock> _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(_blocks);
            string[] _temp = _argument.Split(',');
            if (_temp.Length >= 1)
            {
                if (_temp[1].StartsWith(BLANK))
                    _temp[1] = _temp[1].Substring(1);
                for (int i = 0; i < _blocks.Count; i++)
                {
                    _blocks[i].SetCustomName(_blocks[i].CustomName.Replace(_temp[0], _temp[1]));
                }
            }
        }
        void Reset()
        {
            INSTALL_ENABLED = false;
            ENABLED = true;
            BOOTED = false;
            airVentCnt = 0;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*                                                                       INITIALIZATION                                                                           */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        void _initAPI()
        {
            API_COMMANDS = new Dictionary<string, string>();
            
            API_COMMANDS.Add(API_RELOAD_CONFIG, API_RELOAD_CONFIG);
            API_COMMANDS.Add(API_RAISE_ALARM, API_RAISE_ALARM);
            API_COMMANDS.Add(API_DISABLE_ALARM, API_DISABLE_ALARM);
            API_COMMANDS.Add(API_SHUTDOWN_SHIP, API_SHUTDOWN_SHIP);
            API_COMMANDS.Add(API_BOOT_SHIP, API_BOOT_SHIP);
            API_COMMANDS.Add(API_DISABLE_BLOCK, API_DISABLE_BLOCK);
            API_COMMANDS.Add(API_ENABLE_BLOCK, API_ENABLE_BLOCK);
            API_COMMANDS.Add(API_ATTACK_DETECTED, API_ATTACK_DETECTED);
            API_COMMANDS.Add(API_ATTACK_DEFENDET, API_ATTACK_DEFENDET);
            API_COMMANDS.Add(API_COLLISION_ALERT, API_COLLISION_ALERT);
            API_COMMANDS.Add(API_STATUS_GREEN, API_STATUS_GREEN);
            API_COMMANDS.Add(API_STATUS_ORANGE, API_STATUS_ORANGE);
            API_COMMANDS.Add(API_STATUS_RED, API_STATUS_RED);
            API_COMMANDS.Add(API_CANT_PRESURISE, API_CANT_PRESURISE);
            API_COMMANDS.Add(API_EDI_LOGON, API_EDI_LOGON);
            API_COMMANDS.Add(API_EDI_LOGOFF, API_EDI_LOGOFF);
            API_COMMANDS.Add(API_TVI_LOGON, API_TVI_LOGON);
            API_COMMANDS.Add(API_TVI_LOGOFF, API_TVI_LOGOFF);
            API_COMMANDS.Add(API_COM_LOGON, API_COM_LOGON);
            API_COMMANDS.Add(API_COM_LOGOFF, API_COM_LOGOFF);
        }

        void _FindDebugScreen()
        {
            List<IMyTerminalBlock> _debugs = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(_debugs);
                for (int i = 0; i < _debugs.Count; i++)
                {
                    if (_debugs[i].CustomName.Contains(SIOS_DEBUG_SCREEN_ID))
                    {
                        debugger.Add((IMyTextPanel)_debugs[i]);
                        VALUE_DEBUG_ENABLED = true.ToString();
                        DEBUG_ENABLED = true;
                    }
                }
        }

        void _FindConfigScreen()
        {
            int _screens = 0;
            List<IMyTerminalBlock> _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(_blocks);
            Debug("Textpanels found: " + _blocks.Count);
            for (int i = 0; i < _blocks.Count; i++)
            {
                Debug("Found Textpanel with name: " + _blocks[i].CustomName);
                if (_blocks[i].CustomName.Contains(SIOS_CONFIG_SCREEN_ID))
                {
                    Debug("Textpanels which contains SIOS_CONFIG_SCREEN_ID: " + SIOS_CONFIG_SCREEN_ID);
                    _screens++; // there should`nt be more than 1!
                    config = (IMyTextPanel)_blocks[i];
 
                    config.SetValue("BackgroundColor", panelDefaultBG);
                    config.SetValue("FontColor", panelDefaultFC);
                    config.SetValue("FontSize", 0.8f);
                    StoreExternalConfigData();
                }
            }
            if (_screens == 0) Debug("No Configscreen found");

            if (_screens > 1) Debug("WARNING: TO MANY CONFIGSCREENS FOUND");

        }
        void _FindMyseLF() {
           List<IMyTerminalBlock> _blocks = new List<IMyTerminalBlock>();

            GridTerminalSystem.GetBlocksOfType<IMyProgrammableBlock>(_blocks);
            for (int i = 0; i < _blocks.Count; i++)
            {
                if (_blocks[i].CustomName.Contains(SIOS_PROGRAMBLOCK_ID))
                {
                    core = (IMyProgrammableBlock)_blocks[i];
                    Debug("Found myseLF - Generating awareness - I know who I am. I am!");
                }
            }
        }
        void _generateConfig()
        {
            RUNNING_CONFIGURATION = new Dictionary<string, string>();
            RUNNING_CONFIGURATION.Add(KEY_PLATTFORM_ID, VALUE_PLATTFORM_ID);
            RUNNING_CONFIGURATION.Add(KEY_PLATFORM_ROLE_ID, VALUE_PLATFORM_ROLE_ID);
            RUNNING_CONFIGURATION.Add(KEY_CONDITION, VALUE_CONDITION );
            RUNNING_CONFIGURATION.Add(KEY_INSTALL_ENABLED, VALUE_INSTALL_ENABLED);
            RUNNING_CONFIGURATION.Add(KEY_HIDE_VERSION_INFO, VALUE_HIDE_VERSION_INFO);
            RUNNING_CONFIGURATION.Add(KEY_ALLOW_BEACON_RENAME, VALUE_ALLOW_BEACON_RENAME);
            RUNNING_CONFIGURATION.Add(KEY_ACTIVATE_SECURITY, VALUE_ACTIVATE_SECURITY);
            RUNNING_CONFIGURATION.Add(KEY_LCD_BG_COLOR, VALUE_LCD_BG_COLOR);
            RUNNING_CONFIGURATION.Add(KEY_LCD_FONT_COLOR, VALUE_LCD_FONT_COLOR);
            RUNNING_CONFIGURATION.Add(KEY_DEBUG_ENABLED, VALUE_DEBUG_ENABLED);
            RUNNING_CONFIGURATION.Add(KEY_EDI_INSTALLED, VALUE_EDI_INSTALLED);
            RUNNING_CONFIGURATION.Add(KEY_ATI_INSTALLED, VALUE_ATI_INSTALLED);

            INITIALIZED = true;
        }
        void Init()
        {

            debugger = new List<IMyTextPanel>();
            InitRoles();
            _FindDebugScreen();
            _FindMyseLF();
            _initAPI();
            assemblers = new List<IMyAssembler>();
            refineries = new List<IMyRefinery>();
            sorters = new List<IMyConveyorSorter>();
            batteries = new List<IMyBatteryBlock>();
            reactors = new List<IMyReactor>();
            oxygens = new List<IMyOxygenGenerator>();
            oxytanks = new List<IMyOxygenTank>();
            airvents = new List<IMyAirVent>();
            storage = new List<IMyInventory>();
            production = new List<IMyInventory>();
            speakers = new List<IMySoundBlock>();
            sensors = new List<IMySensorBlock>();
            lights = new List<IMyInteriorLight>();
            doors = new List<IMyDoor>();
            terminals = new List<IMyTerminalBlock>();
            panels = new List<IMyTextPanel>();
            programs = new List<IMyProgrammableBlock>();
            traders = new List<IMyProgrammableBlock>();
            timers = new List<IMyTimerBlock>();
            gatlings = new List<IMyLargeGatlingTurret>();
            missiles = new List<IMyLargeMissileTurret>();
            turrets = new List<IMyLargeInteriorTurret>();
            _FindConfigScreen();
            INSTALL_ENABLED = true;
            INITIALIZED = true;

        }
        void GetBlocks()
        {
            List<IMyTerminalBlock> _blocks = new List<IMyTerminalBlock>();
            LIGHT_CONFIGURATION = new Dictionary<IMyInteriorLight, Color>();
            GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(_blocks);
            for (int i = 0; i < _blocks.Count; i++)
            {
                if (_blocks[i].CustomName.Contains(platformID))
                {
                    ///////////////////// GETTING ASSEMBLERS /////////////////////
                    if (_blocks[i].CustomName.Contains(ASSEMBLER_ID))
                    {
                        IMyAssembler _assembler = (IMyAssembler)_blocks[i];
                        assemblers.Add(_assembler);
                        storage.Add(_assembler.GetInventory(0));
                        production.Add(_assembler.GetInventory(1));
                    }
                    ////////////////////// GETTING REFINERIES //////////////////////
                    if (_blocks[i].CustomName.Contains(REFINERY_ID))
                    {
                        IMyRefinery _refinery = (IMyRefinery)_blocks[i];
                        refineries.Add(_refinery);
                        storage.Add(_refinery.GetInventory(0));
                        production.Add(_refinery.GetInventory(1));
                    }
                    ////////////////// GETTING CONVEYOR SORTERS /////////////////
                    if (_blocks[i].CustomName.Contains(SORTER_ID))
                        sorters.Add((IMyConveyorSorter)_blocks[i]);
                    /////////////////////// GETTING BATTERIES //////////////////////
                    if (_blocks[i].CustomName.Contains(BATTERY_ID))
                        batteries.Add((IMyBatteryBlock)_blocks[i]);
                    /////////////////////// GETTING REACTORS //////////////////////
                    if (_blocks[i].CustomName.Contains(REACTOR_ID))
                    {
                        IMyReactor _reactor = (IMyReactor)_blocks[i];
                        reactors.Add(_reactor);
                        storage.Add(_reactor.GetInventory(0));
                    }
                    /////////////////////// GETTING OXYGENS //////////////////////
                    if (_blocks[i].CustomName.Contains(OXYGEN_ID))
                    {
                        IMyOxygenGenerator _oxygen = (IMyOxygenGenerator)_blocks[i];
                        oxygens.Add(_oxygen);
                    }
                    ////////////////////// GETTING OXYTANKS //////////////////////
                    if (_blocks[i].CustomName.Contains(OXY_TANK_ID))
                    {
                        IMyOxygenTank _oxytank = (IMyOxygenTank)_blocks[i];
                        oxytanks.Add(_oxytank);
                    }
                    /////////////////////// GETTING AIRVENTS //////////////////////
                    if (_blocks[i].CustomName.Contains(AIR_VENT_ID))
                    {
                        IMyAirVent _vent = (IMyAirVent)_blocks[i];
                        airvents.Add(_vent);
                    }
                    ///////////////////// GETTING INVENTORIES /////////////////////
                    if (_blocks[i].CustomName.Contains(STORAGE_ID))
                        storage.Add(((IMyInventoryOwner)_blocks[i]).GetInventory(0));
                    /////////////////// GETTING INTERIOR LIGHTS ////////////////////
                    if (_blocks[i].CustomName.Contains(LIGHT_ID)) {
                        lights.Add((IMyInteriorLight)_blocks[i]);
                        Color _color = ((IMyInteriorLight)_blocks[i]).GetValue<Color>("Color");
                        LIGHT_CONFIGURATION.Add((IMyInteriorLight)_blocks[i], _color);
                    }
                    /////////////////// GETTING SOUND BLOCKS ////////////////////
                    if (_blocks[i].CustomName.Contains(SPEAKER_ID))
                        speakers.Add((IMySoundBlock)_blocks[i]);
                    /////////////////////// GETTING DOORS //////////////////////////
                    if (_blocks[i].CustomName.Contains(DOOR_ID))
                        doors.Add((IMyDoor)_blocks[i]);
                    ///////////////////// GETTING SENSORS //////////////////////////
                    if (_blocks[i].CustomName.Contains(SENSOR_ID))
                        sensors.Add((IMySensorBlock)_blocks[i]);
                    ///////////////////// GETTING TERMINALS ///////////////////////
                    if (_blocks[i].CustomName.Contains(TERMINAL_ID))
                        terminals.Add(_blocks[i]);
                    ///////////////////// GETTING TEXT PANELS /////////////////////
                    if (_blocks[i].CustomName.Contains(PANEL_ID))
                    {
                        IMyTextPanel _panel = (IMyTextPanel)_blocks[i];
                        if (_panel.GetPublicTitle().Contains(SIOS_DEBUG_SCREEN_ID))
                        {
                            //debugger.Add(_panel);
                            //DEBUG_ENABLED = true;
                        }
                        else
                        {
                            if (!_panel.CustomName.Contains(SIOS_CONFIG_SCREEN_ID) && _panel.GetPublicTitle() == INFO_ID)
                            {
                               // _panel.ShowPublicTextOnScreen();
                               // _panel.SetValue("FontSize", 0.8f);
                               // panels.Add(_panel);
                            }
                        }
                    }
                    ////////////////////// GETTING PROGRAMS //////////////////////
                    if (_blocks[i].CustomName.Contains(PROGRAM_ID))
                    {
                        IMyProgrammableBlock _program = (IMyProgrammableBlock)_blocks[i];
                        if (_program.CustomName.Contains(SIOS_PROGRAMBLOCK_ID))
                            core = _program;
                        else if (_program.CustomName.Contains(SIOS_ADDON_EDI_ID))
                            EDI = _program;
                        else if (_program.CustomName.Contains(TRADER_ID))
                            traders.Add(_program);
                        else
                            programs.Add(_program);
                    }
                    ///////////////////// GETTING TIMERBLOCKS ////////////////////
                    if (_blocks[i].CustomName.Contains(TIMER_ID))
                    {
                        if (_blocks[i].CustomName.Contains(UPDATE_ID))
                        {
                            IMyTimerBlock _update = (IMyTimerBlock)_blocks[i];
                            _update.GetActionWithName("Start").Apply(_update);
                        }
                        else
                            timers.Add((IMyTimerBlock)_blocks[i]);
                    }
                    /////////////////// GETTING GATLING TURRETS /////////////////
                    if (_blocks[i].CustomName.Contains(GATTLING_TURRET_ID))
                    {
                        gatlings.Add((IMyLargeGatlingTurret)_blocks[i]);
                    }
                    if (_blocks[i].CustomName.Contains(MISSILE_TURRET_ID))
                    {
                        missiles.Add((IMyLargeMissileTurret)_blocks[i]);
                    }
                    if (_blocks[i].CustomName.Contains(TURRET_ID))
                    {
                        turrets.Add((IMyLargeInteriorTurret)_blocks[i]);
                    }
                }
            }
            Debug("Found: " + lights.Count + " lights");
            BOOTED = true;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*                                                                   AUTO INSTALLATION                                                                     */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void _FormatBlock(List<IMyTerminalBlock> _blockList, string _blocktype)
        {
            string _standardText = PREFIX + VALUE_PLATTFORM_ID + DIVIDER;
            string _blockText = _standardText;
            string _oldName;
            string _blockTextEnd;
            for (int i = 0; i < _blockList.Count; i++)
            {
                _oldName = _blockList[i].CustomName;
                _blockTextEnd = SUFFIX.ToString();
                    switch (_blocktype)
                    {
                        case "IMyAssembler":
                            _blockText = _standardText + ASSEMBLER_ID + DIVIDER + PRODUCTION_ID + _blockTextEnd; break;
                        case "IMyRefinery":
                            _blockText = _standardText + REFINERY_ID + DIVIDER + PRODUCTION_ID + _blockTextEnd; break;
                        case "IMyConveyorSorter":
                            _blockText = _standardText + SORTER_ID + DIVIDER + STORAGE_ID + _blockTextEnd; break;
                        case "IMyBatteryBlock":
                            _blockText = _standardText + BATTERY_ID + DIVIDER + GRAVITY_ID + _blockTextEnd; break;
                        case "IMyGravityGenerator":
                            _blockText = _standardText + GRAVITY_ID + _blockTextEnd; break;
                        case "IMyReactor":
                            _blockText = _standardText + REACTOR_ID + DIVIDER + STORAGE_ID + _blockTextEnd; break;
                        case "IMyOxygenGenerator":
                            _blockText = _standardText + OXYGEN_ID + _blockTextEnd; break;
                        case "IMyOxygenFarm":
                            _blockText = _standardText + OXY_FARM_ID + _blockTextEnd; break;
                        case "IMyOxygenTank":
                            _blockText = _standardText + OXY_TANK_ID + _blockTextEnd; break;
                        case "IMyAirVent":
                            _blockText = _standardText + AIR_VENT_ID + DIVIDER + "$" + airVentCnt.ToString("0000") + _blockTextEnd;
                            airVentCnt++;
                            break;
                        case "IMyCargoContainer":
                            _blockText = _standardText + CARGO_ID + DIVIDER + STORAGE_ID + _blockTextEnd; break;
                        case "IMyShipConnector":
                            _blockText = _standardText + CONNECTOR_ID + DIVIDER + STORAGE_ID + _blockTextEnd; break;
                        case "IMyCollector":
                            _blockText = _standardText + COLLECTOR_ID + DIVIDER + STORAGE_ID + _blockTextEnd; break;
                        case "IMySoundBlock":
                            _blockText = _standardText + SPEAKER_ID + _blockTextEnd; break;
                        case "IMyInteriorLight":
                            _blockText = _standardText + LIGHT_ID + DIVIDER + _blockTextEnd;
                            break;
                        case "IMyDoor":
                            _blockText = _standardText + DOOR_ID + _blockTextEnd; break;
                        case "IMySensorBlock":
                            _blockText = _standardText + SENSOR_ID + _blockTextEnd; break;
                        case "IMyButtonPanel":
                            _blockText = _standardText + TERMINAL_ID + _blockTextEnd; break;
                        case "IMyShipController":
                            _blockText = _standardText + TERMINAL_ID + _blockTextEnd; break;
                        case "IMyTextPanel":
                            //_blockText = _blockText + PANEL_ID + _blockTextEnd;
                            _blockText = _oldName;
                            break;
                        case "IMyProgrammableBlock":
                            //_blockText = _blockText + PROGRAM_ID + _blockTextEnd; break;
                            _blockText =_oldName;
                            break;
                        case "IMyTimerBlock":
                            _blockText = _standardText + TIMER_ID + _blockTextEnd; break;
                        case "IMyLargeGatlingTurret":
                            _blockText = _standardText + GATTLING_TURRET_ID + _blockTextEnd; break;
                        case "IMyLargeMissileTurret":
                            _blockText = _standardText + MISSILE_TURRET_ID + _blockTextEnd; break;
                        case "IMyLargeInteriorTurret":
                            _blockText = _standardText + TURRET_ID + _blockTextEnd; break;
                        default:
                            _blockText = _standardText + " Unknown Block" + DIVIDER + _oldName + _blockTextEnd; break;
                    }
                        BLOCK_INVENTORY.Add(_blockList[i], _oldName);
                        _blockList[i].SetCustomName(_blockText);
            }
        }
        void Install()
        {
  
            VALUE_PLATTFORM_ID = GetRandomPlatformID().ToString();
            
            ///////////////////// INSTALL ASSEMBLERS /////////////////////
            List<IMyTerminalBlock> _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyAssembler>(_blocks);
            _FormatBlock(_blocks, "IMyAssembler");
            ////////////////////// INSTALL REFINERIES //////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyRefinery>(_blocks);
            _FormatBlock(_blocks, "IMyRefinery");
            ////////////////// INSTALL CONVEYOR SORTERS /////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyConveyorSorter>(_blocks);
            _FormatBlock(_blocks, "IMyConveyorSorter");
            /////////////////////// INSTALL BATTERIES //////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyBatteryBlock>(_blocks);
            _FormatBlock(_blocks, "IMyBatteryBlock");
            /////////////////////// INSTALL Gravity Generator //////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyGravityGenerator>(_blocks);
            _FormatBlock(_blocks, "IMyGravityGenerator");
            /////////////////////// INSTALL REACTORS //////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyReactor>(_blocks);
            _FormatBlock(_blocks, "IMyReactor");
            /////////////////////// INSTALL OXYGENS //////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyOxygenGenerator>(_blocks);
            _FormatBlock(_blocks, "IMyOxygenGenerator");
            ////////////////////// INSTALL OXYFARMS //////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyOxygenFarm>(_blocks);
            _FormatBlock(_blocks, "IMyOxygenFarm");
            ////////////////////// INSTALL OXYTANKS /////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyOxygenTank>(_blocks);
            _FormatBlock(_blocks, "IMyOxygenTank");
            ////////////////////// INSTALL AIRVENTS ///////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyAirVent>(_blocks);
            _FormatBlock(_blocks, "IMyAirVent");
            ///////////////////// INSTALL CONTAINERS /////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyCargoContainer>(_blocks);
            _FormatBlock(_blocks, "IMyCargoContainer");
            ///////////////////// INSTALL CONNECTORS /////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyShipConnector>(_blocks);
            _FormatBlock(_blocks, "IMyShipConnector");
            ///////////////////// INSTALL COLLECTORS /////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyCollector>(_blocks);
            _FormatBlock(_blocks, "IMyCollector");
            ///////////////////// INSTALL SOUNDBLOCKS /////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMySoundBlock>(_blocks);
            _FormatBlock(_blocks, "IMySoundBlock");
            /////////////////// INSTALL INTERIOR LIGHT ////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyInteriorLight>(_blocks);
            _FormatBlock(_blocks, "IMyInteriorLight");
            /////////////////////// INSTALL DOORS ///////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyDoor>(_blocks);
            _FormatBlock(_blocks, "IMyDoor");
            /////////////////////// INSTALL SENSORS ///////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMySensorBlock>(_blocks);
            _FormatBlock(_blocks, "IMySensorBlock");
            ////////////////////// INSTALL TERMINALS /////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyButtonPanel>(_blocks);
            _FormatBlock(_blocks, "IMyButtonPanel");
            ///////////////////// INSTALL SHIP CONTROLLS /////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyShipController>(_blocks);
            _FormatBlock(_blocks, "IMyShipController");
            ///////////////////// INSTALL TEXT PANELS /////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(_blocks);
            _FormatBlock(_blocks, "IMyTextPanel");
            ////////////////// INSTALL PROGRAM BLOCKS ///////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyProgrammableBlock>(_blocks);
            _FormatBlock(_blocks, "IMyProgrammableBlock");
            //////////////////// INSTALL TIMER BLOCKS /////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyTimerBlock>(_blocks);
            _FormatBlock(_blocks, "IMyTimerBlock");
            //////////////////// INSTALL GATLING TURRETS /////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyLargeGatlingTurret>(_blocks);
            _FormatBlock(_blocks, "IMyLargeGatlingTurret");
            //////////////////// INSTALL MISSILE TURRETS /////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyLargeMissileTurret>(_blocks);
            _FormatBlock(_blocks, "IMyLargeMissileTurret");
            //////////////////// INSTALL INTERIOR TURRETS /////////////////////
            _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyLargeInteriorTurret>(_blocks);
            _FormatBlock(_blocks, "IMyLargeInteriorTurret");
            // JUST ADD NEW LINES FOR NEW BLOCKTYPES
            INSTALL_ENABLED = true;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*                                                                       DEINSTALLATION                                                                       */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        void Uninstall()
        {
            Init();
            SetCondition("green");
            Debug("Uninstalling...");
            List<IMyTerminalBlock> _blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(_blocks);
            for (int i = 0; i < _blocks.Count; i++)
            {
                if (BLOCK_INVENTORY.ContainsKey(_blocks[i])) {
                    _blocks[i].SetCustomName(BLOCK_INVENTORY[_blocks[i]]);
                }
            }
            INSTALL_ENABLED = false;
            VALUE_PLATTFORM_ID = "0";
            StoreExternalConfigData();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*                                                           EXTERNAL DATA PROCESSING                                                            */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        void LoadExternalConfigData()
        {
            // Load from screen
            string _file = config.GetPublicText();
            Debug(config.GetPublicText());
            string[] lines = _file.Split(CONFIG_DIVIDER);
            Debug("Element Count of LINES" + lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] row = lines[i].Split(CONFIG_SEPARATOR);
                Debug("Searching for Key:" + row[0]);
                if (RUNNING_CONFIGURATION.ContainsKey(row[0])) {
                    Debug("Running config contains key, overwriting value");
                    RUNNING_CONFIGURATION[row[0]] = row[1].Trim();
                }

            }
            Debug(config.GetPublicText());
            StoreExternalConfigData();
            _reactOnConfigChange();

        }
        void _reactOnConfigChange()
        {
            // now i need to read the config and change the whatever on whatever config 
        }


        void StoreExternalConfigData()
        {
            // write to screen
            string _file;
            _file = GetTimeStamp() + ":" + GetVersionInfo() ;
            _file += KEY_PLATTFORM_ID + ":" + RUNNING_CONFIGURATION[KEY_PLATTFORM_ID] + LF;
            _file += KEY_PLATFORM_ROLE_ID + ":" + RUNNING_CONFIGURATION[KEY_PLATFORM_ROLE_ID] + LF;
            _file += KEY_CONDITION + ":" + RUNNING_CONFIGURATION[KEY_CONDITION] + LF;
            _file += KEY_INSTALL_ENABLED + ":" + RUNNING_CONFIGURATION[KEY_INSTALL_ENABLED] + LF;
            _file += KEY_HIDE_VERSION_INFO + ":" + RUNNING_CONFIGURATION[KEY_HIDE_VERSION_INFO] + LF;
            _file += KEY_ALLOW_BEACON_RENAME + ":" + RUNNING_CONFIGURATION[KEY_ALLOW_BEACON_RENAME] + LF;
            _file += KEY_ACTIVATE_SECURITY + ":" + RUNNING_CONFIGURATION[KEY_ACTIVATE_SECURITY] + LF;
            _file += KEY_LCD_BG_COLOR + ":" + RUNNING_CONFIGURATION[KEY_LCD_BG_COLOR] + LF;
            _file += KEY_LCD_FONT_COLOR + ":" + RUNNING_CONFIGURATION[KEY_LCD_FONT_COLOR] + LF;
            _file += KEY_DEBUG_ENABLED + ":" + RUNNING_CONFIGURATION[KEY_DEBUG_ENABLED] + LF;
            _file += KEY_EDI_INSTALLED + ":" + RUNNING_CONFIGURATION[KEY_EDI_INSTALLED] + LF;
            _file += KEY_ATI_INSTALLED + ":" + RUNNING_CONFIGURATION[KEY_ATI_INSTALLED] + LF;
            config.WritePublicText(_file);

        }

        void Display()
        {

            string _info = GetVersionInfo();
            if (hideVersionInfoOnScreen)
                _info = "";
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].WritePublicText(_info);
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*                                                               CONDITION MANAGEMENT                                                                */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        void SetCondition(string _condition)
        {
            condition = _condition;
            StoreExternalConfigData();
            Debug("New Condition: [" + _condition + "]");
            Debug("Switching Lights: " + lights.Count);

            Color lcdBGColor = new Color(0, 0, 0);
            Color fontColor = new Color(255, 255, 255);
            Color lightColor = new Color(255, 255, 255);
            Debug("Switch to condition: " + _condition);
            switch (_condition)
            {
                case "green":
                    lcdBGColor = panelDefaultBG;
                    fontColor = panelDefaultFC;
                    for (int i = 0; i < lights.Count; i++)
                    {
                        string _name = lights[i].CustomName;
                        Color color = LIGHT_CONFIGURATION[lights[i]];
                        lights[i].SetValue("Color", color);
                        Debug("OLD COLOR WAS:" + color.ToString());
                    }
                    VALUE_CONDITION = "green";
                    break;
                case "orange":
                    lcdBGColor = conditionOrangeBG;
                    fontColor = conditionOrangeFC;
                    lightColor = conditionOrangeLight;
                    for (int i = 0; i < lights.Count; i++)
                    {
                        lights[i].SetValue("Color", lightColor);
                    }
                    VALUE_CONDITION = "orange";
                    break;
                case "red":
                    Debug("Switching Lights to condition red");
                    lcdBGColor = conditionRedBG;
                    fontColor = conditionRedFC;
                    lightColor = conditionRedLight;
                    Debug("Switching Lights:" + lightColor.ToString());

                    for (int i = 0; i < lights.Count; i++)
                    {
                        lights[i].SetValue("Color", lightColor);
                    }
                    VALUE_CONDITION = "red";
                    break;
                default:
                    lcdBGColor = panelDefaultBG;
                    fontColor = panelDefaultFC;
                    break;
            }
            Debug("SETTING COLOR");
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].SetValue("BackgroundColor", lcdBGColor);
                panels[i].SetValue("FontColor", fontColor);
            }
            RUNNING_CONFIGURATION[KEY_CONDITION] = VALUE_CONDITION;
            StoreExternalConfigData();
        }
        void InitRoles()
        {
            platformRoles = new List<MARole>();
            AddRole("Trading Outpost");
            AddRole("Trading Vessel");
            AddRole("Refinery");
            AddRole("Factory");
            AddRole("Production Center");
            AddRole("Service Station");
            AddRole("Shipyard");
            AddRole("Military Installation");
            AddRole("Listening Post");
            AddRole("Science Platform");
            AddRole("Observatory");
            Debug("Roles: " + platformRoles.Count + "\n");
        }
        void AddRole(string _id)
        {
            platformRoles.Add(new MARole(_id));
        }
        public struct MARole
        {
            public string ID;
            public MARole(string _id)
            {
                ID = _id;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*                                                                          DEBUGGER                                                                              */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        void Debug(string _msg)
        {
            debugMessages.Add(GetTimeStamp() + ": " + _msg + LF);
        }
        void DisplayDebug()
        {
            try
            {
                for (int i = 0; i < debugger.Count; i++)
                {
                    if (debugger[i] != null)
                    {
                        debugger[i].ShowPublicTextOnScreen();
                        debugger[i].SetValue("BackgroundColor", new Color(0, 0, 255));
                        debugger[i].SetValue("FontColor", new Color(255, 255, 0));
                        debugger[i].SetValue("FontSize", 0.6f);
                        debugger[i].WritePublicText(GetDebugInfo());
                    }
                    else
                        throw new ArgumentNullException("Missing Block!");
                }

            }
            catch (NullReferenceException ex)
            {
                Debug(ex.ToString());
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*                                                                          API                                                                              */
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // if a Argument starts with API:
        void ProcessAPIArgument(string _argument)
        {
            Debug("API CALL DETECTED!");
            Debug(_argument);
            if (API_COMMANDS.ContainsKey(_argument)){
                Debug("Argument defined for API CALL:" + _argument);
                switch (_argument)
                {
                    case API_RELOAD_CONFIG:
                        Debug("Argument:" + API_RELOAD_CONFIG);
                        LoadExternalConfigData();
                        break;
                    case API_RAISE_ALARM:
                        Debug("Argument:" + API_RAISE_ALARM);
                        break;
                    case API_DISABLE_ALARM:
                        Debug("Argument:" + API_DISABLE_ALARM);
                        SetCondition("green");
                        break;
                    case API_SHUTDOWN_SHIP:
                        Debug("Argument:" + API_SHUTDOWN_SHIP);
                        // disableShip
                        break;
                    case API_BOOT_SHIP:
                        Debug("Argument:" + API_BOOT_SHIP);
                        // enable ship
                        break;
                    case API_DISABLE_BLOCK:
                        Debug("Argument:" + API_DISABLE_BLOCK);
                        // dont know what we should do - need more arguments
                        break;
                    case API_ENABLE_BLOCK:
                        Debug("Argument:" + API_ENABLE_BLOCK);
                        // dont know what we should do - need more arguments

                        break;
                    case API_ATTACK_DETECTED:
                        Debug("Argument:" + API_CANT_PRESURISE);
                        SetCondition("red");
                        break;

                    case API_ATTACK_DEFENDET:
                        Debug("Argument:" + API_ATTACK_DEFENDET);
                        // dont know what we should do - need more arguments
                        SetCondition("green");
                        break;
                    case API_COLLISION_ALERT:
                        Debug("Argument:" + API_COLLISION_ALERT);
                        SetCondition("orange");
                        break;
                    case API_STATUS_GREEN:
                        Debug("Argument:" + API_STATUS_GREEN);
                        RUNNING_CONFIGURATION[KEY_CONDITION] = "red";
                        SetCondition("green");
                        break;
                    case API_STATUS_ORANGE:
                        Debug("Argument:" + API_STATUS_ORANGE);
                        SetCondition("orange");
                        break;
                    case API_STATUS_RED:
                        Debug("Argument:" + API_STATUS_RED);
                        RUNNING_CONFIGURATION[KEY_CONDITION] = "red";
                        SetCondition("red");
                        break;
                    case API_CANT_PRESURISE:
                        Debug("Argument:" + API_CANT_PRESURISE);
                        break;
                    default:
                        break;
                }


            } else {
                Debug("Argument is not a valid API Command");
            }
        }

   




     



   }
}
