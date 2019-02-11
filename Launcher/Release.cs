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
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace ItsyRealm.Launcher
{
	public class Release
	{
		ReleaseType mType = ReleaseType.Build;
		GameVersion mVersion = new GameVersion();
		Dictionary<ReleasePlatform, string> mPlatformChecksums = new Dictionary<ReleasePlatform, string>();
		string mPatchNotes = "";

		/// <summary>
		/// Gets an enumerable list of platforms supported by this release.
		/// </summary>
		public IEnumerable<ReleasePlatform> Platforms => mPlatformChecksums.Keys;

		/// <summary>
		/// Gets the <see cref="ReleaseType">ReleaseType</see> describing this release.
		/// </summary>
		public ReleaseType Type => mType;

		/// <summary>
		/// Gets the patch notes, in Markdown format, detailing the release.
		/// </summary>
		public string PatchNotes => mPatchNotes;

		/// <summary>
		/// Gets the version of the release.
		/// </summary>
		public GameVersion Version => mVersion;

		private Release()
		{
			// Nothing.
		}

		/// <summary>
		/// Fetches a new release of the provided type, optionally at the specific version.
		/// </summary>
		/// <param name="releaseType">The <see cref="ReleaseType">ReleaseType</see> to fetch.</param>
		/// <param name="releaseVersion">The optional version to fetch.</param>
		/// <returns>The release on success, null on failure. Failure can occur if no versionwas found.</returns>
		public static Release Fetch(ReleaseType releaseType, GameVersion? releaseVersion = null)
		{
			string url;
			if (releaseVersion.HasValue)
			{
				url = String.Format("{0}/api/download/{1}/version/{2}", LauncherClient.Domain, releaseType.ToString().ToLowerInvariant(), releaseVersion.Value.ToVersionString());
			}
			else
			{
				url = String.Format("{0}/api/download/{1}/version", LauncherClient.Domain, releaseType.ToString().ToLowerInvariant());
			}

			using (var client = new WebClient())
			{
				string value = client.DownloadString(url);
				JObject json = JObject.Parse(value);

				ReleaseType fetchedReleaseType;
				if (!Enum.TryParse((string)json["type"], true, out fetchedReleaseType) || releaseType != fetchedReleaseType)
				{
					return null;
				}

				GameVersion fetchedVersion;
				if (!GameVersion.TryParse((string)json["version"], out fetchedVersion) ||
				    (releaseVersion.HasValue && !releaseVersion.Value.Equals(fetchedVersion)))
				{
					return null;
				}

				string patchNotes = (string)json["patchNotes"];

				var release = new Release()
				{
					mVersion = fetchedVersion,
					mPatchNotes = patchNotes,
					mType = fetchedReleaseType
				};

				foreach (var download in json["downloads"])
				{
					ReleasePlatform platform;
					if (Enum.TryParse((string)download["platform"], true, out platform))
					{
						release.mPlatformChecksums.Add(platform, (string)download["checksum"]);
					}
				}

				return release;
			}
		}
	}
}
