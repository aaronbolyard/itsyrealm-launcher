using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ItsyRealm.Launcher
{
	public partial class DownloadProgressWindow : Form
	{
		WebClient mClient;
		bool mSuccess = false;

		public bool Success => mSuccess;

		public DownloadProgressWindow(WebClient client, Release release)
		{
			InitializeComponent();

			client.DownloadProgressChanged += Client_DownloadProgressChanged;
			client.DownloadFileCompleted += Client_DownloadFileCompleted;

			downloadLabel.Text = String.Format("Downloading {0} version {1}...", release.Type.ToString().ToLowerInvariant(), release.Version.ToString());
			mClient = client;
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			mClient.CancelAsync();
		}

		private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message);
				mSuccess = false;
			}
			else
			{
				mSuccess = true;
			}

			Close();
		}

		private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			progressBar.Minimum = 0;
			progressBar.Maximum = 100;
			progressBar.Value = e.ProgressPercentage;
		}
	}
}
