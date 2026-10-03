namespace DeepCoreWorkshopBuilder
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pnlHeader = new Panel();
            lblTagline = new Label();
            lblTitle = new Label();
            grpModDetails = new GroupBox();
            txtDescription = new TextBox();
            lblDescription = new Label();
            txtVersion = new TextBox();
            lblVersion = new Label();
            txtAuthor = new TextBox();
            lblAuthor = new Label();
            txtModId = new TextBox();
            lblModId = new Label();
            txtModName = new TextBox();
            lblModName = new Label();
            grpPlugin = new GroupBox();
            lblDllStatus = new Label();
            btnBrowseDll = new Button();
            txtDllPath = new TextBox();
            grpArtwork = new GroupBox();
            lblThumbnailStatus = new Label();
            btnBrowseThumbnail = new Button();
            txtThumbnailPath = new TextBox();
            lblThumbnail = new Label();
            lblPreviewStatus = new Label();
            btnBrowsePreview = new Button();
            txtPreviewPath = new TextBox();
            lblPreview = new Label();
            grpBuild = new GroupBox();
            btnNewPackage = new Button();
            btnOpenPackageFolder = new Button();
            lblBuildStatus = new Label();
            btnBuildPackage = new Button();
            btnBrowseOutput = new Button();
            txtOutputFolder = new TextBox();
            lblOutputFolder = new Label();
            pnlHeader.SuspendLayout();
            grpModDetails.SuspendLayout();
            grpPlugin.SuspendLayout();
            grpArtwork.SuspendLayout();
            grpBuild.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblTagline);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1084, 90);
            pnlHeader.TabIndex = 0;
            // 
            // lblTagline
            // 
            lblTagline.AutoSize = true;
            lblTagline.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTagline.Location = new Point(28, 55);
            lblTagline.Name = "lblTagline";
            lblTagline.Size = new Size(168, 17);
            lblTagline.TabIndex = 1;
            lblTagline.Text = "Engineering Better Gameplay";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(25, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(381, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "DeepCore Workshop Builder";
            // 
            // grpModDetails
            // 
            grpModDetails.Controls.Add(txtDescription);
            grpModDetails.Controls.Add(lblDescription);
            grpModDetails.Controls.Add(txtVersion);
            grpModDetails.Controls.Add(lblVersion);
            grpModDetails.Controls.Add(txtAuthor);
            grpModDetails.Controls.Add(lblAuthor);
            grpModDetails.Controls.Add(txtModId);
            grpModDetails.Controls.Add(lblModId);
            grpModDetails.Controls.Add(txtModName);
            grpModDetails.Controls.Add(lblModName);
            grpModDetails.Location = new Point(25, 110);
            grpModDetails.Name = "grpModDetails";
            grpModDetails.Size = new Size(500, 300);
            grpModDetails.TabIndex = 1;
            grpModDetails.TabStop = false;
            grpModDetails.Text = "Mod Details";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(120, 191);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(350, 75);
            txtDescription.TabIndex = 9;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(20, 195);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(70, 15);
            lblDescription.TabIndex = 8;
            lblDescription.Text = "Description:";
            // 
            // txtVersion
            // 
            txtVersion.Location = new Point(120, 151);
            txtVersion.Name = "txtVersion";
            txtVersion.Size = new Size(120, 23);
            txtVersion.TabIndex = 7;
            txtVersion.Text = "1.0.0";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Location = new Point(20, 155);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(48, 15);
            lblVersion.TabIndex = 6;
            lblVersion.Text = "Version:";
            // 
            // txtAuthor
            // 
            txtAuthor.Location = new Point(120, 111);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(350, 23);
            txtAuthor.TabIndex = 5;
            txtAuthor.Text = "C0reSmith";
            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Location = new Point(20, 115);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(47, 15);
            lblAuthor.TabIndex = 4;
            lblAuthor.Text = "Author:";
            // 
            // txtModId
            // 
            txtModId.Location = new Point(120, 71);
            txtModId.Name = "txtModId";
            txtModId.Size = new Size(350, 23);
            txtModId.TabIndex = 3;
            // 
            // lblModId
            // 
            lblModId.AutoSize = true;
            lblModId.Location = new Point(20, 75);
            lblModId.Name = "lblModId";
            lblModId.Size = new Size(49, 15);
            lblModId.TabIndex = 2;
            lblModId.Text = "Mod ID:";
            // 
            // txtModName
            // 
            txtModName.Location = new Point(120, 31);
            txtModName.Name = "txtModName";
            txtModName.Size = new Size(350, 23);
            txtModName.TabIndex = 1;
            // 
            // lblModName
            // 
            lblModName.AutoSize = true;
            lblModName.Location = new Point(20, 35);
            lblModName.Name = "lblModName";
            lblModName.Size = new Size(70, 15);
            lblModName.TabIndex = 0;
            lblModName.Text = "Mod Name:";
            // 
            // grpPlugin
            // 
            grpPlugin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpPlugin.Controls.Add(lblDllStatus);
            grpPlugin.Controls.Add(btnBrowseDll);
            grpPlugin.Controls.Add(txtDllPath);
            grpPlugin.Location = new Point(550, 110);
            grpPlugin.Name = "grpPlugin";
            grpPlugin.Size = new Size(500, 120);
            grpPlugin.TabIndex = 2;
            grpPlugin.TabStop = false;
            grpPlugin.Text = "Plugin DLL";
            // 
            // lblDllStatus
            // 
            lblDllStatus.AutoSize = true;
            lblDllStatus.Location = new Point(20, 78);
            lblDllStatus.Name = "lblDllStatus";
            lblDllStatus.Size = new Size(92, 15);
            lblDllStatus.TabIndex = 2;
            lblDllStatus.Text = "No DLL selected";
            // 
            // btnBrowseDll
            // 
            btnBrowseDll.Location = new Point(380, 39);
            btnBrowseDll.Name = "btnBrowseDll";
            btnBrowseDll.Size = new Size(90, 29);
            btnBrowseDll.TabIndex = 1;
            btnBrowseDll.Text = "Browse...";
            btnBrowseDll.UseVisualStyleBackColor = true;
            btnBrowseDll.Click += btnBrowseDll_Click;
            // 
            // txtDllPath
            // 
            txtDllPath.Location = new Point(20, 40);
            txtDllPath.Name = "txtDllPath";
            txtDllPath.ReadOnly = true;
            txtDllPath.Size = new Size(350, 23);
            txtDllPath.TabIndex = 0;
            // 
            // grpArtwork
            // 
            grpArtwork.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpArtwork.Controls.Add(lblThumbnailStatus);
            grpArtwork.Controls.Add(btnBrowseThumbnail);
            grpArtwork.Controls.Add(txtThumbnailPath);
            grpArtwork.Controls.Add(lblThumbnail);
            grpArtwork.Controls.Add(lblPreviewStatus);
            grpArtwork.Controls.Add(btnBrowsePreview);
            grpArtwork.Controls.Add(txtPreviewPath);
            grpArtwork.Controls.Add(lblPreview);
            grpArtwork.Location = new Point(550, 250);
            grpArtwork.Name = "grpArtwork";
            grpArtwork.Size = new Size(500, 195);
            grpArtwork.TabIndex = 3;
            grpArtwork.TabStop = false;
            grpArtwork.Text = "Workshop Artwork";
            // 
            // lblThumbnailStatus
            // 
            lblThumbnailStatus.AutoSize = true;
            lblThumbnailStatus.Location = new Point(20, 168);
            lblThumbnailStatus.Name = "lblThumbnailStatus";
            lblThumbnailStatus.Size = new Size(127, 15);
            lblThumbnailStatus.TabIndex = 7;
            lblThumbnailStatus.Text = "No thumbnail selected";
            // 
            // btnBrowseThumbnail
            // 
            btnBrowseThumbnail.Location = new Point(380, 138);
            btnBrowseThumbnail.Name = "btnBrowseThumbnail";
            btnBrowseThumbnail.Size = new Size(90, 29);
            btnBrowseThumbnail.TabIndex = 6;
            btnBrowseThumbnail.Text = "Browse...";
            btnBrowseThumbnail.UseVisualStyleBackColor = true;
            btnBrowseThumbnail.Click += btnBrowseThumbnail_Click;
            // 
            // txtThumbnailPath
            // 
            txtThumbnailPath.Location = new Point(20, 142);
            txtThumbnailPath.Name = "txtThumbnailPath";
            txtThumbnailPath.ReadOnly = true;
            txtThumbnailPath.Size = new Size(350, 23);
            txtThumbnailPath.TabIndex = 5;
            // 
            // lblThumbnail
            // 
            lblThumbnail.AutoSize = true;
            lblThumbnail.Location = new Point(20, 120);
            lblThumbnail.Name = "lblThumbnail";
            lblThumbnail.Size = new Size(71, 15);
            lblThumbnail.TabIndex = 4;
            lblThumbnail.Text = " Thumbnail:";
            // 
            // lblPreviewStatus
            // 
            lblPreviewStatus.AutoSize = true;
            lblPreviewStatus.Location = new Point(20, 92);
            lblPreviewStatus.Name = "lblPreviewStatus";
            lblPreviewStatus.Size = new Size(149, 15);
            lblPreviewStatus.TabIndex = 3;
            lblPreviewStatus.Text = "No preview image selected";
            // 
            // btnBrowsePreview
            // 
            btnBrowsePreview.Location = new Point(380, 57);
            btnBrowsePreview.Name = "btnBrowsePreview";
            btnBrowsePreview.Size = new Size(90, 29);
            btnBrowsePreview.TabIndex = 2;
            btnBrowsePreview.Text = "Browse...";
            btnBrowsePreview.UseVisualStyleBackColor = true;
            btnBrowsePreview.Click += btnBrowsePreview_Click;
            // 
            // txtPreviewPath
            // 
            txtPreviewPath.Location = new Point(20, 58);
            txtPreviewPath.Name = "txtPreviewPath";
            txtPreviewPath.ReadOnly = true;
            txtPreviewPath.Size = new Size(350, 23);
            txtPreviewPath.TabIndex = 1;
            // 
            // lblPreview
            // 
            lblPreview.AutoSize = true;
            lblPreview.Location = new Point(20, 35);
            lblPreview.Name = "lblPreview";
            lblPreview.Size = new Size(87, 15);
            lblPreview.TabIndex = 0;
            lblPreview.Text = "Preview Image:";
            // 
            // grpBuild
            // 
            grpBuild.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpBuild.Controls.Add(btnNewPackage);
            grpBuild.Controls.Add(btnOpenPackageFolder);
            grpBuild.Controls.Add(lblBuildStatus);
            grpBuild.Controls.Add(btnBuildPackage);
            grpBuild.Controls.Add(btnBrowseOutput);
            grpBuild.Controls.Add(txtOutputFolder);
            grpBuild.Controls.Add(lblOutputFolder);
            grpBuild.Location = new Point(25, 465);
            grpBuild.Name = "grpBuild";
            grpBuild.Size = new Size(1025, 120);
            grpBuild.TabIndex = 4;
            grpBuild.TabStop = false;
            grpBuild.Text = "Build Workshop Package";
            // 
            // btnNewPackage
            // 
            btnNewPackage.Location = new Point(864, 70);
            btnNewPackage.Name = "btnNewPackage";
            btnNewPackage.Size = new Size(106, 35);
            btnNewPackage.TabIndex = 6;
            btnNewPackage.Text = "NEW PACKAGE";
            btnNewPackage.UseVisualStyleBackColor = true;
            btnNewPackage.Click += btnNewPackage_Click;
            // 
            // btnOpenPackageFolder
            // 
            btnOpenPackageFolder.Enabled = false;
            btnOpenPackageFolder.Location = new Point(545, 70);
            btnOpenPackageFolder.Name = "btnOpenPackageFolder";
            btnOpenPackageFolder.Size = new Size(250, 35);
            btnOpenPackageFolder.TabIndex = 5;
            btnOpenPackageFolder.Text = "OPEN PACKAGE FOLDER";
            btnOpenPackageFolder.UseVisualStyleBackColor = true;
            btnOpenPackageFolder.Click += btnOpenPackageFolder_Click;
            // 
            // lblBuildStatus
            // 
            lblBuildStatus.AutoSize = true;
            lblBuildStatus.Location = new Point(290, 79);
            lblBuildStatus.Name = "lblBuildStatus";
            lblBuildStatus.Size = new Size(83, 15);
            lblBuildStatus.TabIndex = 4;
            lblBuildStatus.Text = "Ready to build";
            // 
            // btnBuildPackage
            // 
            btnBuildPackage.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBuildPackage.Location = new Point(20, 70);
            btnBuildPackage.Name = "btnBuildPackage";
            btnBuildPackage.Size = new Size(250, 35);
            btnBuildPackage.TabIndex = 3;
            btnBuildPackage.Text = "BUILD WORKSHOP PACKAGE";
            btnBuildPackage.UseVisualStyleBackColor = true;
            btnBuildPackage.Click += btnBuildPackage_Click;
            // 
            // btnBrowseOutput
            // 
            btnBrowseOutput.Location = new Point(880, 30);
            btnBrowseOutput.Name = "btnBrowseOutput";
            btnBrowseOutput.Size = new Size(90, 29);
            btnBrowseOutput.TabIndex = 2;
            btnBrowseOutput.Text = "Browse...";
            btnBrowseOutput.UseVisualStyleBackColor = true;
            btnBrowseOutput.Click += btnBrowseOutput_Click;
            // 
            // txtOutputFolder
            // 
            txtOutputFolder.Location = new Point(120, 31);
            txtOutputFolder.Name = "txtOutputFolder";
            txtOutputFolder.ReadOnly = true;
            txtOutputFolder.Size = new Size(750, 23);
            txtOutputFolder.TabIndex = 1;
            // 
            // lblOutputFolder
            // 
            lblOutputFolder.AutoSize = true;
            lblOutputFolder.Location = new Point(20, 35);
            lblOutputFolder.Name = "lblOutputFolder";
            lblOutputFolder.Size = new Size(84, 15);
            lblOutputFolder.TabIndex = 0;
            lblOutputFolder.Text = "Output Folder:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 721);
            Controls.Add(grpBuild);
            Controls.Add(grpArtwork);
            Controls.Add(grpPlugin);
            Controls.Add(grpModDetails);
            Controls.Add(pnlHeader);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1000, 700);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DeepCore Workshop Builder";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            grpModDetails.ResumeLayout(false);
            grpModDetails.PerformLayout();
            grpPlugin.ResumeLayout(false);
            grpPlugin.PerformLayout();
            grpArtwork.ResumeLayout(false);
            grpArtwork.PerformLayout();
            grpBuild.ResumeLayout(false);
            grpBuild.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTagline;
        private Label lblTitle;
        private GroupBox grpModDetails;
        private Label lblModId;
        private TextBox txtModName;
        private Label lblModName;
        private Label lblVersion;
        private TextBox txtAuthor;
        private Label lblAuthor;
        private TextBox txtModId;
        private TextBox txtDescription;
        private Label lblDescription;
        private TextBox txtVersion;
        private GroupBox grpPlugin;
        private Label lblDllStatus;
        private Button btnBrowseDll;
        private TextBox txtDllPath;
        private GroupBox grpArtwork;
        private Button btnBrowsePreview;
        private TextBox txtPreviewPath;
        private Label lblPreview;
        private Label lblThumbnailStatus;
        private Button btnBrowseThumbnail;
        private TextBox txtThumbnailPath;
        private Label lblThumbnail;
        private Label lblPreviewStatus;
        private GroupBox grpBuild;
        private Button btnBrowseOutput;
        private TextBox txtOutputFolder;
        private Label lblOutputFolder;
        private Label lblBuildStatus;
        private Button btnBuildPackage;
        private Button btnOpenPackageFolder;
        private Button btnNewPackage;
    }
}
