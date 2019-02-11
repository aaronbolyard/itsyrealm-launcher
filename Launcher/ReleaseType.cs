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
	/// A type of release.
	/// </summary>
	public enum ReleaseType
	{
		/// <summary>
		/// Represents a game build for a desktop platform.
		/// </summary>
		Build,

		/// <summary>
		/// Represents a launcher for a desktop platform.
		/// </summary>
		Launcher,

		/// <summary>
		/// Represents a resource build for a mobile platform.
		/// </summary>
		Resource
	}
}
