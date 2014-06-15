/**
 * MainWindow.cs
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

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tac
{
    class MainWindow : Window<TacPartCfgLogger>
    {
        private readonly string version;
        private readonly HashSet<Part> selectedParts = new HashSet<Part>();
        private Vector2 scrollPosition = Vector2.zero;

        private GUIStyle labelStyle;
        private GUIStyle versionStyle;

        public MainWindow()
            : base("TAC Part CFG Logger", 360, Screen.height * 0.6f)
        {
            this.Log("Constructor");
            version = Utilities.GetDllVersion(this);
        }

        protected override void DrawWindowContents(int windowId)
        {
            if (GUILayout.Button("Save All"))
            {
                this.Log("Saving...");
                string path = KSPUtil.ApplicationRootPath + Path.DirectorySeparatorChar + "_CfgOutput" + Path.DirectorySeparatorChar;
                Directory.CreateDirectory(path);

                foreach (var d in GameDatabase.Instance.root.AllConfigs)
                {
                    File.WriteAllText(path + d.url.Replace('/', '.') + ".cfg", d.config.ToString());
                }
            }

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            foreach (var d in GameDatabase.Instance.root.AllConfigs)
            {
                GUILayout.Label(d.name + "; " + d.type + "; " + d.url.Replace('/', '.'), labelStyle);
            }
            GUILayout.EndScrollView();

            GUILayout.Space(8);

            GUI.Label(new Rect(4, windowPos.height - 13, windowPos.width - 20, 12), "TAC Part CFG Logger v" + version, versionStyle);
        }

        protected override void ConfigureStyles()
        {
            base.ConfigureStyles();

            if (labelStyle == null)
            {
                labelStyle = new GUIStyle(GUI.skin.label);
                labelStyle.wordWrap = false;
                labelStyle.fontStyle = FontStyle.Normal;
                labelStyle.normal.textColor = Color.white;
                labelStyle.alignment = TextAnchor.MiddleLeft;

                versionStyle = Utilities.GetVersionStyle();
            }
        }
    }
}
