////////////////////////////////////////////////////////////////////////////////
// Launcher/LauncherClient.cs
//
// This file is a part of the ItsyRealm launcher.
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
////////////////////////////////////////////////////////////////////////////////
using System;

namespace ItsyRealm.Launcher
{
	/// <summary>
	/// Stores information about the installed version of ItsyRealm
	/// and exposes methods to download and update the game.
	/// </summary>
	public class LauncherClient : IDisposable
	{
		/// <summary>
		/// Creates a LauncherClient with the provided command line arguments.
		/// </summary>
		/// <param name="arguments">A list of arguments.</param>
		public LauncherClient(string[] arguments)
		{
			// Nothing.
		}

		public void Dispose()
		{
			// Nothing.
		}
	}
}
