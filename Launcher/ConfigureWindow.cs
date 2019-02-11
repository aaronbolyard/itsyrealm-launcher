using IniParser.Model;
using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace ItsyRealm.Launcher
{
	public partial class ConfigureWindow : Form
	{
		IniData mData;

		public ConfigureWindow(IniData data)
		{
			InitializeComponent();

			mData = data;

			if (mData.GetKey("debug")?.ToLowerInvariant() == "on")
			{
				enableDebugModeCheckbox.Checked = true;
			}

			if (mData.GetKey("anonymous")?.ToLowerInvariant() == "on")
			{
				enableAnonymousModeCheckbox.Checked = true;
			}
		}

		private void enableDebugModeCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			if (enableAnonymousModeCheckbox.Checked)
			{
				mData.Global.SetKeyData(new KeyData("debug") { Value = "on" });
			}
			else
			{
				mData.Global.SetKeyData(new KeyData("debug") { Value = "off" });
			}
		}

		private void enableAnonymousModeCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			if (enableAnonymousModeCheckbox.Checked)
			{
				mData.Global.SetKeyData(new KeyData("anonymous") { Value = "on" });
			}
			else
			{
				mData.Global.SetKeyData(new KeyData("anonymous") { Value = "off" });
			}
		}

		private void openScreenshotsButton_Click(object sender, EventArgs e)
		{
			string path;
			{
				string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				path = Path.Combine(rootPath, "ItsyRealm");
			}

			if (Directory.Exists(path))
			{
				Process.Start(new ProcessStartInfo()
				{
					FileName = path,
					UseShellExecute = true,
					Verb = "open"
				});
			}
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
