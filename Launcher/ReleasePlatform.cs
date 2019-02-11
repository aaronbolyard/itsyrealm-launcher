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
	/// Represents a platform supported by releases.
	/// </summary>
	public enum ReleasePlatform
	{
		/// <summary>
		/// Windows (32-bit).
		/// </summary>
		Win32,

		/// <summary>
		/// Windows (64-bit).
		/// </summary>
		Win64,

		/// <summary>
		/// Linux (32-bit).
		/// </summary>
		Linux32,

		/// <summary>
		/// Linux (64-bit).
		/// </summary>
		Linux64,

		/// <summary>
		/// macOS.
		/// </summary>
		MacOS
	}
}
