﻿using System;
using Eto.Forms;
using Kuriimu2.EtoForms.Forms;

namespace Kuriimu2.EtoForms.Gtk
{
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new Application(Eto.Platforms.Gtk).Run(new MainForm());
        }
    }
}
