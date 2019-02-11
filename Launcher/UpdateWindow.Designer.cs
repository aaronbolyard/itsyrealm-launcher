namespace ItsyRealm.Launcher
{
	partial class UpdateWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.updateButton = new System.Windows.Forms.Button();
			this.patchNotesViewer = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// updateButton
			// 
			this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.updateButton.Location = new System.Drawing.Point(377, 566);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(75, 23);
			this.updateButton.TabIndex = 2;
			this.updateButton.Text = "Update";
			this.updateButton.UseVisualStyleBackColor = true;
			this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// patchNotesViewer
			// 
			this.patchNotesViewer.AllowNavigation = false;
			this.patchNotesViewer.AllowWebBrowserDrop = false;
			this.patchNotesViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.patchNotesViewer.IsWebBrowserContextMenuEnabled = false;
			this.patchNotesViewer.Location = new System.Drawing.Point(13, 13);
			this.patchNotesViewer.MinimumSize = new System.Drawing.Size(20, 20);
			this.patchNotesViewer.Name = "patchNotesViewer";
			this.patchNotesViewer.ScriptErrorsSuppressed = true;
			this.patchNotesViewer.Size = new System.Drawing.Size(439, 547);
			this.patchNotesViewer.TabIndex = 3;
			this.patchNotesViewer.WebBrowserShortcutsEnabled = false;
			// 
			// UpdateWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(464, 601);
			this.Controls.Add(this.patchNotesViewer);
			this.Controls.Add(this.updateButton);
			this.Name = "UpdateWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Update";
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button updateButton;
		private System.Windows.Forms.WebBrowser patchNotesViewer;
	}
}