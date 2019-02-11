////////////////////////////////////////////////////////////////////////////////
// Launcher/LauncherClient.cs
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
	/// Represents a tag portion of a <see cref="GameVersion">GameVersion</see>.
	/// </summary>
	public enum GameVersionTag
	{
		/// <summary>
		/// Represents an alpha.
		/// </summary>
		Alpha,

		/// <summary>
		/// Represents a beta. This would be a demo.
		/// </summary>
		Beta,

		/// <summary>
		/// Represents a release; in other worlds, the goal!
		/// </summary>
		Release
	}
}
