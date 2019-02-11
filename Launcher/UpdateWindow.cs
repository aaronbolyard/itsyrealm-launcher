using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HeyRed.MarkdownSharp;

namespace ItsyRealm.Launcher
{
	public partial class UpdateWindow : Form
	{
		static readonly string Html = @"
		<html>
			<head>
				<title>ItsyRealm Patchnotes</title>
				<link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css'>
			</head>
			<body>
				{0}
			</body>
		</html>";

		public UpdateWindow(Release release)
		{
			InitializeComponent();

			var notes = String.Format("{0}\n\nVersion {1}", release.PatchNotes, release.Version.ToVersionString());
			Markdown markdown = new Markdown();
			var html = markdown.Transform(notes);
			patchNotesViewer.DocumentText = "0";
			patchNotesViewer.Document.Write(String.Format(Html, html));
		}

		private void updateButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
