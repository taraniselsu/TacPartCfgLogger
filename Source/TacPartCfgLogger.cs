﻿/**
 * TacPartCfgLogger.cs
 *
 * Thunder Aerospace Corporation's Part CFG Logger for the Kerbal Space Program, by Taranis Elsu
 *
 * (C) Copyright 2013, Taranis Elsu
 *
 * Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com/. This
 * project is in no way associated with nor endorsed by Squad.
 *
 * This code is licensed under the Attribution-NonCommercial-ShareAlike 3.0 (CC BY-NC-SA 3.0)
 * creative commons license. See <http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode>
 * for full details.
 *
 * Attribution — You are free to modify this code, so long as you mention that the resulting
 * work is based upon or adapted from this code.
 *
 * Non-commercial - You may not use this work for commercial purposes.
 *
 * Share Alike — If you alter, transform, or build upon this work, you may distribute the
 * resulting work only under the same or similar license to the CC BY-NC-SA 3.0 license.
 *
 * Note that Thunder Aerospace Corporation is a ficticious entity created for entertainment
 * purposes. It is in no way meant to represent a real entity. Any similarity to a real entity
 * is purely coincidental.
 */

using KSP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tac
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    class TacPartCfgLogger : MonoBehaviour
    {
        private string configFilename;
        private MainWindow window;
        private ButtonWrapper button;

        void Awake()
        {
            this.Log("Awake");
            configFilename = IOUtils.GetFilePathFor(this.GetType(), "TacPartCfgLogger.cfg");
            window = new MainWindow();
            button = new ButtonWrapper(new Rect(Screen.width * 0.225f, 0, 32, 32),
                "ThunderAerospace/TacPartCfgLogger/Textures/button", "CL",
                "TAC Part CFG Logger", OnIconClicked);
        }

        void Start()
        {
            this.Log("Start");
            Load();

            button.Visible = true;
        }

        void OnDestroy()
        {
            this.Log("OnDestroy");
            Save();
            button.Destroy();
        }

        private void Load()
        {
            if (File.Exists<TacPartCfgLogger>(configFilename))
            {
                ConfigNode config = ConfigNode.Load(configFilename);
                button.Load(config);
                window.Load(config);
            }
        }

        private void Save()
        {
            ConfigNode config = new ConfigNode();
            button.Save(config);
            window.Save(config);

            config.Save(configFilename);
        }

        private void OnIconClicked()
        {
            window.ToggleVisible();
        }
    }
}
