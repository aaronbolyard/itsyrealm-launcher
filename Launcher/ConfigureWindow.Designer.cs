namespace ItsyRealm.Launcher
{
	partial class ConfigureWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureWindow));
			this.enableDebugModeCheckbox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.enableAnonymousModeCheckbox = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.openScreenshotsButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// enableDebugModeCheckbox
			// 
			this.enableDebugModeCheckbox.AutoSize = true;
			this.enableDebugModeCheckbox.Location = new System.Drawing.Point(13, 13);
			this.enableDebugModeCheckbox.Name = "enableDebugModeCheckbox";
			this.enableDebugModeCheckbox.Size = new System.Drawing.Size(88, 17);
			this.enableDebugModeCheckbox.TabIndex = 0;
			this.enableDebugModeCheckbox.Text = "Debug Mode";
			this.enableDebugModeCheckbox.UseVisualStyleBackColor = true;
			this.enableDebugModeCheckbox.CheckedChanged += new System.EventHandler(this.enableDebugModeCheckbox_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(287, 39);
			this.label1.TabIndex = 1;
			this.label1.Text = "Debug mode shows things like frames-per-second and logic\r\nupdates. You\'re also ab" +
    "le to perform some actions in-game\r\nthat you normally couldn\'t.";
			// 
			// enableAnonymousModeCheckbox
			// 
			this.enableAnonymousModeCheckbox.AutoSize = true;
			this.enableAnonymousModeCheckbox.Location = new System.Drawing.Point(12, 83);
			this.enableAnonymousModeCheckbox.Name = "enableAnonymousModeCheckbox";
			this.enableAnonymousModeCheckbox.Size = new System.Drawing.Size(105, 17);
			this.enableAnonymousModeCheckbox.TabIndex = 2;
			this.enableAnonymousModeCheckbox.Text = "Disable analytics";
			this.enableAnonymousModeCheckbox.UseVisualStyleBackColor = true;
			this.enableAnonymousModeCheckbox.CheckedChanged += new System.EventHandler(this.enableAnonymousModeCheckbox_CheckedChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 103);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(282, 39);
			this.label2.TabIndex = 3;
			this.label2.Text = "ItsyRealm collects data anonymously about things like play\r\ntime and game progres" +
    "s. If you wish to opt-out, no data\r\nwill be collected from your sessions.";
			// 
			// openScreenshotsButton
			// 
			this.openScreenshotsButton.Location = new System.Drawing.Point(13, 166);
			this.openScreenshotsButton.Name = "openScreenshotsButton";
			this.openScreenshotsButton.Size = new System.Drawing.Size(104, 23);
			this.openScreenshotsButton.TabIndex = 4;
			this.openScreenshotsButton.Text = "Open Screenshots";
			this.openScreenshotsButton.UseVisualStyleBackColor = true;
			this.openScreenshotsButton.Click += new System.EventHandler(this.openScreenshotsButton_Click);
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(217, 166);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 5;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// ConfigureWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(304, 201);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.openScreenshotsButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.enableAnonymousModeCheckbox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.enableDebugModeCheckbox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ConfigureWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Configure";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox enableDebugModeCheckbox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox enableAnonymousModeCheckbox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button openScreenshotsButton;
		private System.Windows.Forms.Button okButton;
	}
}