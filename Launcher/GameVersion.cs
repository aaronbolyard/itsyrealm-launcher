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
	/// A SemVer with tag for game resources.
	/// </summary>
	public struct GameVersion
	{
		/// <summary>
		/// Theh major version.
		/// 
		/// For the final release, this would be 1. Expansion packs increase this number.
		/// </summary>
		public int Major;

		/// <summary>
		/// Minor version.
		/// 
		/// For the alpha/beta releases, this goes up every completed tier. Same for expansion packs.
		/// </summary>
		public int Minor;

		/// <summary>
		/// Revision.
		/// 
		/// This is the date for alpha versions; for releases, this goes up with bug fixes.
		/// </summary>
		public int Revision;

		/// <summary>
		/// A <see cref="GameVersionTag">GameVersionTag</see>.
		/// </summary>
		public GameVersionTag Tag;

		/// <summary>
		/// Returns true if this version is newer than the provided version.
		/// </summary>
		/// <param name="other">The version to compare against.</param>
		/// <returns>True if this version is newer, false otherwise.</returns>
		public bool IsNewer(GameVersion other)
		{
			if (Major > other.Major)
			{
				return true;
			}
			else if (Major == other.Major)
			{
				if (Minor > other.Minor)
				{
					return true;
				}
				else if (Minor == other.Minor)
				{
					if (Revision > other.Revision)
					{
						return true;
					}
					else
					{
						return Tag > other.Tag;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Converts the <see cref="GameVersion">GameVersion</see> to a version string that can be parsed by <see cref="">
		/// </summary>
		/// <returns></returns>
		public string ToVersionString()
		{
			return String.Format("{0}.{1}.{2}-{3}", Major, Minor, Revision, Tag.ToString().ToLowerInvariant());
		}

		/// <summary>
		/// Parses a <see cref="GameVersion">GameVersion</see> from a string.
		/// 
		/// The string should be in the format <code>major.minor.revision-tag</code>.
		/// </summary>
		/// <param name="value">The string value to parse.</param>
		/// <param name="result">The resulting game verision.</param>
		/// <returns>True on success, false on failure. Failure occurs if the value parameter is not in the correct format.</returns>
		public static bool TryParse(string value, out GameVersion result)
		{
			result = new GameVersion();

			string[] components = value.Split('.');
			if (components.Length == 3)
			{
				string major = components[0];
				if (!Int32.TryParse(major, out result.Major) || result.Major < 0)
				{
					result = new GameVersion();
					return false;
				}

				string minor = components[1];
				if (!Int32.TryParse(minor, out result.Minor) || result.Minor < 0)
				{
					result = new GameVersion();
					return false;
				}

				string[] revisionTag = components[2].Split('-');
				if (revisionTag.Length == 2)
				{
					string revision = revisionTag[0];
					if (!Int32.TryParse(revision, out result.Revision) || result.Revision < 0)
					{
						result = new GameVersion();
						return false;
					}

					string tag = revisionTag[1];
					if (!Enum.TryParse(tag, true, out result.Tag))
					{
						result = new GameVersion();
						return false;
					}
				}
			}

			return true;
		}

		public override bool Equals(object obj)
		{
			if (Object.ReferenceEquals(this, obj))
			{
				return true;
			}
			else if (obj is GameVersion)
			{
				var other = (GameVersion)obj;
				return Major == other.Major &&
					   Minor == other.Minor &&
					   Revision == other.Revision &&
					   Tag == other.Tag;
			}

			return false;
		}

		public override int GetHashCode()
		{
			return Major.GetHashCode() ^ Minor.GetHashCode() ^ Revision.GetHashCode() ^ Tag.GetHashCode();
		}

		public override string ToString()
		{
			return ToVersionString();
		}
	}
}
