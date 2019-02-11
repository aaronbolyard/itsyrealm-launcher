////////////////////////////////////////////////////////////////////////////////
// Launcher/LauncherAction.cs
//
// This file is a part of the ItsyRealm launcher.
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
////////////////////////////////////////////////////////////////////////////////

namespace ItsyRealm.Launcher
{
	/// <summary>
	/// Describes the action the launcher will be performing.
	/// </summary>
	public enum LauncherAction
	{
		/// <summary>
		/// Updates the game, if necessary, and launches it.
		/// </summary>
		Play,

		/// <summary>
		/// Exposes configuration options, like debug mode and analytics.
		/// </summary>
		Configure,

		/// <summary>
		/// Removes the launcher and the game. Optionally removes the game save data.
		/// </summary>
		Uninstall,

		/// <summary>
		/// Update the launcher.
		/// </summary>
		UpdateLauncher,

		/// <summary>
		/// Do nothing and quit.
		/// </summary>
		Quit
	}
}
