////////////////////////////////////////////////////////////////////////////////
// Launcher/LauncherClient.cs
//
// This file is a part of the ItsyRealm launcher.
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
////////////////////////////////////////////////////////////////////////////////
using ICSharpCode.SharpZipLib.Zip;
using IniParser;
using IniParser.Model;
using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace ItsyRealm.Launcher
{
	/// <summary>
	/// Stores information about the installed version of ItsyRealm
	/// and exposes methods to download and update the game.
	/// </summary>
	public class LauncherClient : IDisposable
	{
		ReleasePlatform mPlatform;

#if ITSYREALM_BUILD_DEBUG
		public static string Domain => "http://localhost:5000";
#else
		public static string Domain = "https://itsyrealm.com";
#endif

		GameVersion? mGameVersion;
		GameVersion mLauncherVersion;

		GameVersion? mServerGameVersion;
		GameVersion? mServerLauncherVersion;

		Release mLauncherRelease;
		Release mBuildRelease;

		LauncherAction mAction = LauncherAction.Play;

		bool mRequestedElevation = false;

		/// <summary>
		/// Gets the action to perform when launching the client. Defaults to <see cref="LauncherAction.Play">Play</see>
		/// </summary>
		public LauncherAction Action => mAction;

		/// <summary>
		/// Creates a LauncherClient with the provided command line arguments.
		/// </summary>
		/// <param name="arguments">A list of arguments.</param>
		public LauncherClient(string[] arguments)
		{
			for (int i = 0; i < arguments.Length; ++i)
			{
				if (arguments[i].ToLowerInvariant() == "/action")
				{
					if (i < arguments.Length - 1)
					{
						LauncherAction action;
						if (Enum.TryParse(arguments[i + 1], true, out action))
						{
							mAction = action;
						}
					}
				}
				else if (arguments[i].ToLowerInvariant() == "/elevated")
				{
					mRequestedElevation = true;
				}
			}

			if (IntPtr.Size == 8)
			{
				mPlatform = ReleasePlatform.Win64;
			}
			else
			{
				mPlatform = ReleasePlatform.Win32;
			}

			BuildDirectories();
			Refresh();

			try
			{
				Connect();
			}
			catch
			{
				// Just don't bother updating the game.
			}
		}

		void BuildDirectories()
		{
			Directory.CreateDirectory(GetGamePath(null));
			Directory.CreateDirectory(GetLauncherPath(null));
		}

		void Connect()
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

			var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
			mLauncherVersion = new GameVersion()
			{
				Major = assemblyVersion.Major,
				Minor = assemblyVersion.Minor,
				Revision = assemblyVersion.MajorRevision,
				Tag = GameVersionTag.Release
			};

			Release launcher = Release.Fetch(ReleaseType.Launcher);
			mServerLauncherVersion = launcher.Version;
			mLauncherRelease = launcher;

			Release build = Release.Fetch(ReleaseType.Build);
			mServerGameVersion = build.Version;
			mBuildRelease = build;
		}

		void Refresh()
		{
			string gameVersionPath = GetLauncherPath("manifest.ini");
			if (File.Exists(gameVersionPath))
			{
				FileIniDataParser parser = new FileIniDataParser();
				var manifest = parser.ReadFile(gameVersionPath);
				var versionString = manifest.GetKey("version");

				GameVersion version;
				if (GameVersion.TryParse(versionString, out version))
				{
					mGameVersion = version;
				}
			}
		}

		/// <summary>
		/// Performs the play action.
		/// 
		/// If there's a new launcher, downloads the latest launcher and runs it.
		///
		/// Otherwise, it downloads the latest game, if necessary. Then it runs the game.
		/// </summary>
		public void Play()
		{
			if (CopyLauncher())
			{
				if (mServerLauncherVersion.HasValue && mServerLauncherVersion.Value.IsNewer(mLauncherVersion))
				{
					DownloadLauncher();
					return;
				}
				else if (!mGameVersion.HasValue || (mServerGameVersion.HasValue && mGameVersion.HasValue && mServerGameVersion.Value.IsNewer(mGameVersion.Value)))
				{
					if (mBuildRelease == null)
					{
						MessageBox.Show("Couldn't download latest game build.");
						return;
					}
					else
					{
						DownloadGame();
					}
				}

				StartGame();
			}
		}

		void DownloadLauncher()
		{
			using (WebClient client = new WebClient())
			{
				string address = string.Format("{0}/api/download/launcher/get/{1}/{2}", Domain, mPlatform.ToString(), mServerLauncherVersion.ToString());
				string file = GetLauncherPath("Update.exe");

				DownloadProgressWindow window = new DownloadProgressWindow(client, mLauncherRelease);
				client.DownloadFileAsync(new Uri(address), file);
				Application.Run(window);
				if (window.Success)
				{
					Process.Start(file, "/action updatelauncher");
				}
			}
		}

		bool CopyLauncher()
		{
			string filename = GetLauncherPath("Launcher.exe");
			var currentFilename = Process.GetCurrentProcess().MainModule.FileName;

			if (filename != currentFilename)
			{
				if (!File.Exists(filename))
				{
					if (MessageBox.Show(
						"Do you wish to install the launcher? " +
						"This will install the launcher and create shortcuts. " +
						"You can remove it any time from the Start Menu.",
						"Confirm Installation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					{
						return false;
					}
				}
			
				File.Copy(currentFilename, filename, true);
				InstallLauncher();
			}

			return true;
		}

		void InstallLauncher()
		{
			Shortcut.MakeShortcut(
				"ItsyRealm",
				GetLauncherPath("Launcher.exe"),
				GetLauncherPath(null),
				"/action play",
				"Play ItsyRealm!");

			Shortcut.MakeShortcut(
				"Configure ItsyRealm",
				GetLauncherPath("Launcher.exe"),
				GetLauncherPath(null),
				"/action configure",
				"Configure ItsyRealm options.");

			Shortcut.MakeShortcut(
				"Uninstall ItsyRealm",
				GetLauncherPath("Launcher.exe"),
				GetLauncherPath(null),
				"/action uninstall",
				"Remove ItsyRealm and all associated files.");
		}

		void DownloadGame()
		{
			Application.Run(new UpdateWindow(mBuildRelease));

			using (WebClient client = new WebClient())
			{
				string address = string.Format("{0}/api/download/build/get/{1}/{2}", Domain, mPlatform.ToString(), mServerGameVersion.Value.ToString());
				string file = GetLauncherPath("Game.zip");

				DownloadProgressWindow window = new DownloadProgressWindow(client, mBuildRelease);
				client.DownloadFileAsync(new Uri(address), file);
				Application.Run(window);
				if (window.Success)
				{
					FastZip zip = new FastZip();
					Directory.Delete(GetGamePath(null), true);
					zip.ExtractZip(file, GetGamePath(null), null);

					File.Delete(file);

					File.WriteAllText(GetLauncherPath("manifest.ini"), String.Format("version = {0}", mServerGameVersion.Value.ToString()));
				}
			}
		}

		void StartGame()
		{
			string arguments = "";
			{
				string settingsPath = GetLauncherPath("settings.ini");
				if (File.Exists(settingsPath))
				{
					FileIniDataParser parser = new FileIniDataParser();
					var data = parser.ReadFile(settingsPath);
					
					if (data.GetKey("debug")?.ToLowerInvariant() == "on")
					{
						arguments += " /debug";
					}
					
					if (data.GetKey("anonymous")?.ToLowerInvariant() == "on")
					{
						arguments += " /f:anonymous";
					}
				}
			}

			if (File.Exists(GetGamePath("love.exe")))
			{
				ProcessStartInfo info = new ProcessStartInfo()
				{
					FileName = GetGamePath("love.exe"),
					Arguments = String.Format("--fused {0}{1}", "itsyrealm.love", arguments),
					WorkingDirectory = GetGamePath(null)
				};

				Process.Start(info);
			}
			else
			{
				MessageBox.Show("Could not run game: latest build not downloaded.");
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct OVERLAPPED
		{
			public uint internalLow;
			public uint internalHigh;
			public uint offsetLow;
			public uint offsetHigh;
			public IntPtr hEvent;
		}

		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern bool LockFileEx(SafeFileHandle handle, uint flags, uint reserved, uint countLow, uint countHigh, ref OVERLAPPED overlapped);

		private const uint LOCKFILE_EXCLUSIVE_LOCK = 0x00000002;

		static void Lock(FileStream stream, ulong offset, ulong count)
		{
			uint countLow = (uint)count;
			uint countHigh = (uint)(count >> 32);

			OVERLAPPED overlapped = new OVERLAPPED()
			{
				internalLow = 0,
				internalHigh = 0,
				offsetLow = (uint)offset,
				offsetHigh = (uint)(offset >> 32),
				hEvent = IntPtr.Zero,
			};

			if (!LockFileEx(stream.SafeFileHandle, LOCKFILE_EXCLUSIVE_LOCK, 0, countLow,
				countHigh, ref overlapped))
			{
				//TODO: throw an exception
			}
		}

		/// <summary>
		/// Performs the update launcher action.
		/// 
		/// Deletes the old launcher and copies the executable.
		/// </summary>
		public void UpdateLauncher()
		{
			string filename = GetLauncherPath("Launcher.exe");
			using (var file = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Read))
			{
				Lock(file, 0, (ulong)file.Length);
				file.Close();

				File.Delete(filename);
				File.Copy(Process.GetCurrentProcess().MainModule.FileName, filename);

				Process.Start(filename);
			}
		}

		/// <summary>
		/// Shows a panel to configure the game.
		/// </summary>
		public void Configure()
		{
			string settingsPath = GetLauncherPath("settings.ini");

			IniData data;
			FileIniDataParser parser = new FileIniDataParser();
			if (File.Exists(settingsPath))
			{
				data = parser.ReadFile(settingsPath);
			}
			else
			{
				data = new IniData();
			}

			var window = new ConfigureWindow(data);
			Application.Run(window);

			parser.WriteFile(settingsPath, data);
		}

		[Flags]
		enum MoveFileFlags
		{
			MOVEFILE_REPLACE_EXISTING = 0x00000001,
			MOVEFILE_COPY_ALLOWED = 0x00000002,
			MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004,
			MOVEFILE_WRITE_THROUGH = 0x00000008,
			MOVEFILE_CREATE_HARDLINK = 0x00000010,
			MOVEFILE_FAIL_IF_NOT_TRACKABLE = 0x00000020
		}

		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);

		public void Uninstall()
		{
			if (!mRequestedElevation && MessageBox.Show("Are you sure you wish to uninstall ItsyRealm?", "Uninstall ItsyRealm", MessageBoxButtons.YesNo) == DialogResult.No)
			{
				return;
			}

			bool isCurrentlyElevated;
			using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
			{
				WindowsPrincipal principal = new WindowsPrincipal(identity);
				isCurrentlyElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
			}

			if (!isCurrentlyElevated && !mRequestedElevation)
			{
				try
				{
					Process.Start(new ProcessStartInfo(GetLauncherPath("Launcher.exe"))
					{
						Arguments = "/action uninstall /elevated",
						Verb = "runas"
					});
				}
				catch
				{
					// Nothing. User cancelled or something.
				}
			}
			else if (mRequestedElevation)
			{
				try
				{
					if (Directory.Exists(GetGamePath(null)))
					{
						Directory.Delete(GetGamePath(null), true);
					}

					foreach (var item in Directory.EnumerateFiles(GetLauncherPath(null)))
					{
						if (item != GetLauncherPath("Launcher.exe"))
						{
							File.Delete(item);
						}
					}

					foreach (var directory in Directory.EnumerateDirectories(GetLauncherPath(null)))
					{
						Directory.Delete(directory, true);
					}

					var shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
					shortcutPath = Path.Combine(shortcutPath, "ItsyRealm");
					if (Directory.Exists(shortcutPath))
					{
						Directory.Delete(shortcutPath, true);
					}

					if (MessageBox.Show("Do you want to delete the save game data? This action cannot be reversed.", "Uninstall ItsyRealm", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						var saveGamePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
						saveGamePath = Path.Combine(saveGamePath, "ItsyRealm");

						if (Directory.Exists(saveGamePath))
						{
							Directory.Delete(saveGamePath, true);
						}
					}

					MoveFileEx(GetLauncherPath("Launcher.exe"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
					MoveFileEx(GetLauncherPath(null), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Uninstall Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		/// <summary>
		/// Gets a game path.
		/// </summary>
		/// <param name="resource">The resource to build a path to.</param>
		/// <returns>The absolute path to the game resource.</returns>
		string GetGamePath(string resource)
		{
			string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			if (resource == null)
			{
				return Path.Combine(rootPath, "ItsyRealmLauncher", "game");
			}

			return Path.Combine(rootPath, "ItsyRealmLauncher", "game", resource);
		}

		/// <summary>
		/// Gets a launcher path.
		/// </summary>
		/// <param name="resource">The resource to build a path to.</param>
		/// <returns>The absolute path to the launcher resource.</returns>
		string GetLauncherPath(string resource)
		{
			string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			if (resource == null)
			{
				return Path.Combine(rootPath, "ItsyRealmLauncher", "launcher");
			}

			return Path.Combine(rootPath, "ItsyRealmLauncher", "launcher", resource);
		}

		public void Dispose()
		{
			// Nothing.
		}
	}
}
