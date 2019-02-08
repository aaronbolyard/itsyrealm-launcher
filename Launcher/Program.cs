////////////////////////////////////////////////////////////////////////////////
// Launcher/Program.cs
//
// This file is a part of the ItsyRealm launcher.
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;

namespace ItsyRealm.Launcher
{
	static class Program
	{
		[STAThread]
		static void Main(string[] arguments)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			using (var client = new LauncherClient(arguments))
			{
				// Bla bla bla.
			}
		}
	}
}