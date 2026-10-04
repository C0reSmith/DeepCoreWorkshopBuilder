using System.Drawing;
using System.IO;


namespace DeepCoreWorkshopBuilder
{
    public partial class MainForm : Form
    {
        // Tracks whether the user has manually edited the Mod ID.
        private bool modIdManuallyEdited = false;

        // Prevents automatic Mod ID updates from being mistaken
        // for a manual user edit.
        private bool updatingModIdAutomatically = false;

        public MainForm()
        {
            InitializeComponent();

            // Load the saved default author.
            txtAuthor.Text = Properties.Settings.Default.DefaultAuthor;

            // Load the saved output folder.
            txtOutputFolder.Text =
                Properties.Settings.Default.DefaultOutputFolder;
        }

        private void btnBrowseDll_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog();

            dialog.Title = "Select Plugin DLL";
            dialog.Filter = "Plugin DLL (*.dll)|*.dll";
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtDllPath.Text = dialog.FileName;

             // The selected plugin DLL has changed.
                MarkPackageChanged();

                FileInfo dllFile = new FileInfo(dialog.FileName);

                double sizeKb = dllFile.Length / 1024.0;

                lblDllStatus.Text =
                    $"✓ {dllFile.Name} — {sizeKb:F1} KB";
            }
        }

        private void btnBrowsePreview_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog();

            dialog.Title = "Select Workshop Preview Image";
            dialog.Filter =
                "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|" +
                "PNG Files (*.png)|*.png|" +
                "JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg";

            dialog.CheckFileExists = true;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPreviewPath.Text = dialog.FileName;

             // The selected preview image has changed.
                MarkPackageChanged();

                using Image image = Image.FromFile(dialog.FileName);

                FileInfo imageFile = new FileInfo(dialog.FileName);

                string previewExtension =
                Path.GetExtension(dialog.FileName);

                lblPreviewStatus.Text =
                    $"✓ {imageFile.Name} — {image.Width} × {image.Height} → Preview{previewExtension}";
            }
        }

        private void btnBrowseThumbnail_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog();

            dialog.Title = "Select Workshop Thumbnail";
            dialog.Filter =
                "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|" +
                "PNG Files (*.png)|*.png|" +
                "JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg";

            dialog.CheckFileExists = true;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtThumbnailPath.Text = dialog.FileName;

             // The selected thumbnail image has changed.
                MarkPackageChanged();

                using Image image = Image.FromFile(dialog.FileName);

                FileInfo imageFile = new FileInfo(dialog.FileName);

                string thumbnailExtension =
                Path.GetExtension(dialog.FileName);

                lblThumbnailStatus.Text =
                    $"✓ {imageFile.Name} — {image.Width} × {image.Height} → thumb{thumbnailExtension}";
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description =
                    "Select where the Workshop package will be created.";

                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFolder.Text = folderDialog.SelectedPath;

                    Properties.Settings.Default.DefaultOutputFolder =
                        folderDialog.SelectedPath;

                    Properties.Settings.Default.Save();

                }
            }
        }

        private void btnBuildPackage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtModName.Text))
            {
                MessageBox.Show(
                    "Please enter a Mod Name.",
                    "Missing Mod Name",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtModName.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtModId.Text))
            {
                MessageBox.Show(
                    "Please enter a Mod ID.",
                    "Missing Mod ID",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtModId.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDllPath.Text) ||
                !File.Exists(txtDllPath.Text))
            {
                MessageBox.Show(
                    "Please select a valid plugin DLL.",
                    "Missing Plugin DLL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            if (string.IsNullOrWhiteSpace(txtPreviewPath.Text) ||
                !File.Exists(txtPreviewPath.Text))
            {
                MessageBox.Show(
                    "Please select a valid preview image.",
                    "Missing Preview Image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            if (string.IsNullOrWhiteSpace(txtThumbnailPath.Text) ||
                !File.Exists(txtThumbnailPath.Text))
            {
                MessageBox.Show(
                    "Please select a valid thumbnail image.",
                    "Missing Thumbnail",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // ------------------------------------------------------------
            // VALIDATE THUMBNAIL FILE SIZE
            // Steam Workshop requires thumb.png to be below 1 MB.
            // ------------------------------------------------------------

            FileInfo thumbnailInfo =
                new FileInfo(txtThumbnailPath.Text.Trim());

            const long oneMegabyte = 1024 * 1024;

            if (thumbnailInfo.Length >= oneMegabyte)
            {
                double sizeInMb =
                    thumbnailInfo.Length / (1024.0 * 1024.0);

                MessageBox.Show(
                    $"The thumbnail image is too large.\n\n" +
                    $"Current size: {sizeInMb:F2} MB\n" +
                    $"Maximum allowed: Below 1 MB\n\n" +
                    "Please select a smaller thumbnail image.",
                    "Thumbnail Too Large",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                lblBuildStatus.Text =
                    "Build stopped - thumbnail exceeds 1 MB.";

                return;
            }

            if (string.IsNullOrWhiteSpace(txtOutputFolder.Text) ||
                !Directory.Exists(txtOutputFolder.Text))
            {
                MessageBox.Show(
                    "Please select a valid output folder.",
                    "Missing Output Folder",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // ------------------------------------------------------------
            // WORKSHOP READINESS - VALID MOD ID
            // ------------------------------------------------------------

            // The Mod ID is also used as the Workshop package folder name,
            // so it must not contain invalid Windows filename characters.
            if (txtModId.Text.Trim().IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                MessageBox.Show(
                    "The Mod ID contains characters that cannot be used " +
                    "in a Workshop package folder name.\n\n" +
                    $"Mod ID: {txtModId.Text.Trim()}",
                    "Invalid Mod ID",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                lblBuildStatus.Text =
                    "Build stopped - invalid Mod ID.";

                return;
            }

            // Windows folder names must not end with a period or space.
            string modId = txtModId.Text.Trim();

            if (modId.EndsWith("."))
            {
                MessageBox.Show(
                    "The Mod ID cannot end with a period.\n\n" +
                    $"Mod ID: {modId}",
                    "Invalid Mod ID",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                lblBuildStatus.Text =
                    "Build stopped - invalid Mod ID.";

                return;
            }

            // ------------------------------------------------------------
            // WORKSHOP READINESS - VERSION
            // ------------------------------------------------------------

            if (string.IsNullOrWhiteSpace(txtVersion.Text))
            {
                MessageBox.Show(
                    "Please enter a Version number.",
                    "Missing Version",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                lblBuildStatus.Text =
                    "Build stopped - version is missing.";

                txtVersion.Focus();
                return;
            }

            // Version must use the DeepCore numeric format:
            // Major.Minor.Patch, for example 1.0.0.
            string version = txtVersion.Text.Trim();

            string[] versionParts = version.Split('.');

            bool validVersion =
                versionParts.Length == 3 &&
                int.TryParse(versionParts[0], out _) &&
                int.TryParse(versionParts[1], out _) &&
                int.TryParse(versionParts[2], out _);

            if (!validVersion)
            {
                MessageBox.Show(
                    "Please enter the Version in numeric format.\n\n" +
                    "Example: 1.0.0\n\n" +
                    "Do not include the \"v\" prefix.",
                    "Invalid Version",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                lblBuildStatus.Text =
                    "Build stopped - invalid version.";

                txtVersion.Focus();
                return;
            }

            // ------------------------------------------------------------
            // WORKSHOP READINESS - AUTHOR
            // ------------------------------------------------------------

            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show(
                    "Please enter an Author name.",
                    "Missing Author",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                lblBuildStatus.Text =
                    "Build stopped - author is missing.";

                txtAuthor.Focus();
                return;
            }

            // ------------------------------------------------------------
            // BUILD STARTING
            // ------------------------------------------------------------

            lblBuildStatus.Text = "Building Workshop package...";

            btnBuildPackage.Enabled = false;
            btnNewPackage.Enabled = false;
            btnOpenPackageFolder.Enabled = false;

            Application.DoEvents();

            // ------------------------------------------------------------
            // BUILD WORKSHOP PACKAGE
            // ------------------------------------------------------------

            // Create a safe folder name from the Mod ID.
            string packageFolderName = txtModId.Text.Trim();

            // Build the final Workshop package path.
            string packageFolder = Path.Combine(
                txtOutputFolder.Text.Trim(),
                packageFolderName);

            // ------------------------------------------------------------
            // CHECK IF PACKAGE FOLDER ALREADY EXISTS
            // ------------------------------------------------------------

            if (Directory.Exists(packageFolder))
            {
                DialogResult result = MessageBox.Show(
                    $"The Workshop package folder already exists:\n\n" +
                    $"{packageFolder}\n\n" +
                    "Do you want to overwrite it?",
                    "Workshop Package Already Exists",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                // No is the safe/default choice.
                if (result != DialogResult.Yes)
                {
                    lblBuildStatus.Text = "Build cancelled.";

                    // Restore the buttons because the build was cancelled.
                    btnBuildPackage.Enabled = true;
                    btnNewPackage.Enabled = true;

                    return;
                }
            }

            try
            {
                // Create the package folder if necessary.
                Directory.CreateDirectory(packageFolder);

                // ------------------------------------------------------------
                // COPY PLUGIN DLL
                // ------------------------------------------------------------

                // Keep the original DLL filename.
                string dllFileName = Path.GetFileName(txtDllPath.Text.Trim());

            // Build the destination path inside the Workshop package.
            string destinationDllPath = Path.Combine(
                packageFolder,
                dllFileName);

            // Copy the DLL into the package.
            // true = overwrite the existing DLL if the user already approved overwrite.
            File.Copy(
                txtDllPath.Text.Trim(),
                destinationDllPath,
                true);

            // Update the build status.
            lblBuildStatus.Text =
                $"DLL copied: {dllFileName}";
            // ------------------------------------------------------------
            // CREATE ABOUT FOLDER
            // ------------------------------------------------------------

            string aboutFolder = Path.Combine(
                packageFolder,
                "About");

            Directory.CreateDirectory(aboutFolder);

            lblBuildStatus.Text =
                "About folder created.";

            // ------------------------------------------------------------
            // COPY PREVIEW IMAGE
            // ------------------------------------------------------------

            // Get the original preview image extension.
            string previewExtension =
                Path.GetExtension(txtPreviewPath.Text.Trim());

            // Rename the copied preview image to "Preview"
            // while keeping its original file type.
            string previewFileName =
                "Preview" + previewExtension;

            // Build the destination path inside the About folder.
            string destinationPreviewPath = Path.Combine(
                aboutFolder,
                previewFileName);

            // Copy the preview image into the package.
            // true = overwrite if the user already approved overwrite.
            File.Copy(
                txtPreviewPath.Text.Trim(),
                destinationPreviewPath,
                true);

            // Update the build status.
            lblBuildStatus.Text =
                $"Preview image copied: {previewFileName}";

            // ------------------------------------------------------------
            // COPY THUMBNAIL IMAGE
            // ------------------------------------------------------------

            // Get the original thumbnail image extension.
            string thumbnailExtension =
                Path.GetExtension(txtThumbnailPath.Text.Trim());

            // Rename the copied thumbnail image to "thumb"
            // while keeping its original file type.
            string thumbnailFileName =
                "thumb" + thumbnailExtension;

            // Build the destination path inside the About folder.
            string destinationThumbnailPath = Path.Combine(
                aboutFolder,
                thumbnailFileName);

            // Copy the thumbnail into the package.
            // true = overwrite if the user already approved overwrite.
            File.Copy(
                txtThumbnailPath.Text.Trim(),
                destinationThumbnailPath,
                true);

            // Update the build status.
            lblBuildStatus.Text =
                $"Thumbnail copied: {thumbnailFileName}";


            // ------------------------------------------------------------
            // CREATE ABOUT.XML
            // ------------------------------------------------------------

            string aboutXmlPath = Path.Combine(
                aboutFolder,
                "About.xml");

            var aboutXml = new System.Xml.Linq.XDocument(
                new System.Xml.Linq.XDeclaration("1.0", "utf-8", null),

                new System.Xml.Linq.XElement(
                    "ModMetadata",

                    new System.Xml.Linq.XAttribute(
                        System.Xml.Linq.XNamespace.Xmlns + "xsi",
                        "http://www.w3.org/2001/XMLSchema-instance"),

                    new System.Xml.Linq.XAttribute(
                        System.Xml.Linq.XNamespace.Xmlns + "xsd",
                        "http://www.w3.org/2001/XMLSchema"),

                    new System.Xml.Linq.XElement(
                        "Name",
                        txtModName.Text.Trim()),

                    new System.Xml.Linq.XElement(
                        "Author",
                        txtAuthor.Text.Trim()),

                    new System.Xml.Linq.XElement(
                        "Version",
                        txtVersion.Text.Trim()),

                    new System.Xml.Linq.XElement(
                        "Description",
                        txtDescription.Text.Trim())
                )
            );

            aboutXml.Save(aboutXmlPath);

            lblBuildStatus.Text =
                "About.xml created.";


            // ------------------------------------------------------------
            // BUILD COMPLETE
            // ------------------------------------------------------------

            // ------------------------------------------------------------
            // VERIFY PACKAGE - PLUGIN DLL
            // ------------------------------------------------------------

            if (!File.Exists(destinationDllPath))
            {
                MessageBox.Show(
                    "The plugin DLL could not be found in the finished Workshop package.",
                    "Package Verification Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                lblBuildStatus.Text =
                    "Build failed - plugin DLL verification failed.";

                btnOpenPackageFolder.Enabled = false;

                // Restore the build controls so the user can correct
                // the problem and try the build again.
                btnBuildPackage.Enabled = true;
                btnNewPackage.Enabled = true;

                return;
            }

            // ------------------------------------------------------------
            // VERIFY PACKAGE - ABOUT.XML
            // ------------------------------------------------------------

            if (!File.Exists(aboutXmlPath))
            {
                MessageBox.Show(
                    "About.xml could not be found in the finished Workshop package.",
                    "Package Verification Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                lblBuildStatus.Text =
                    "Build failed - About.xml verification failed.";

                btnOpenPackageFolder.Enabled = false;

                // Restore the build controls so the user can correct
                // the problem and try the build again.
                btnBuildPackage.Enabled = true;
                btnNewPackage.Enabled = true;

                return;
            }

            // ------------------------------------------------------------
            // VERIFY PACKAGE - PREVIEW IMAGE
            // ------------------------------------------------------------

            if (!File.Exists(destinationPreviewPath))
            {
                MessageBox.Show(
                    "The preview image could not be found in the finished Workshop package.",
                    "Package Verification Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                lblBuildStatus.Text =
                    "Build failed - preview image verification failed.";

                btnOpenPackageFolder.Enabled = false;

                // Restore the build controls so the user can correct
                // the problem and try the build again.
                btnBuildPackage.Enabled = true;
                btnNewPackage.Enabled = true;

                return;
            }

            // ------------------------------------------------------------
            // VERIFY PACKAGE - THUMBNAIL IMAGE
            // ------------------------------------------------------------

            if (!File.Exists(destinationThumbnailPath))
            {
                MessageBox.Show(
                    "The thumbnail image could not be found in the finished Workshop package.",
                    "Package Verification Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                lblBuildStatus.Text =
                    "Build failed - thumbnail image verification failed.";

                btnOpenPackageFolder.Enabled = false;

                // Restore the build controls so the user can correct
                // the problem and try the build again.
                btnBuildPackage.Enabled = true;
                btnNewPackage.Enabled = true;

                return;
            }

            lblBuildStatus.Text =
                "✓ Workshop package built successfully";

            btnOpenPackageFolder.Enabled = true;

            // Restore the build controls now that the build is complete.
            btnBuildPackage.Enabled = true;
            btnNewPackage.Enabled = true;

            // ------------------------------------------------------------
            // BUILD SUMMARY
            // ------------------------------------------------------------

            MessageBox.Show(
                $"Workshop package built and verified successfully.\n\n" +
                $"Mod: {txtModName.Text.Trim()}\n" +
                $"Version: {txtVersion.Text.Trim()}\n\n" +
                $"Verified:\n" +
                $"✓ Plugin DLL\n" +
                $"✓ About.xml\n" +
                $"✓ Preview image\n" +
                $"✓ Thumbnail image\n\n" +
                $"Package:\n{packageFolder}",
                "Workshop Package Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }

            catch (Exception ex)
            {
                lblBuildStatus.Text =
                    "Build failed - an unexpected error occurred.";

                btnBuildPackage.Enabled = true;
                btnNewPackage.Enabled = true;
                btnOpenPackageFolder.Enabled = false;

                MessageBox.Show(
                    "The Workshop package could not be completed.\n\n" +
                    $"Error: {ex.Message}",
                    "Workshop Build Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnOpenPackageFolder_Click(object sender, EventArgs e)
        {
            string packageFolder = Path.Combine(
                txtOutputFolder.Text.Trim(),
                txtModId.Text.Trim());

            if (Directory.Exists(packageFolder))
            {
                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = packageFolder,
                        UseShellExecute = true
                    });
            }
        }

        private void btnNewPackage_Click(object sender, EventArgs e)
        {
            // Clear mod-specific details.
            txtModName.Clear();
            txtModId.Clear();
            txtDescription.Clear();

            // Clear selected plugin DLL.
            txtDllPath.Clear();
            lblDllStatus.Text = "No DLL selected";

            // Clear selected artwork.
            txtPreviewPath.Clear();
            lblPreviewStatus.Text = "No preview image selected";

            txtThumbnailPath.Clear();
            lblThumbnailStatus.Text = "No thumbnail selected";

            // Keep Author, Version and remembered Output Folder.

            // Reset build state.
            lblBuildStatus.Text = "Ready to build";
            btnOpenPackageFolder.Enabled = false;

            // Allow automatic Mod ID generation again for the next package.
            modIdManuallyEdited = false;

            // Put the cursor ready for the next mod.
            txtModName.Focus();
        }

        private void txtModName_TextChanged(object sender, EventArgs e)
        {
            // The package details have changed since the last build.
            MarkPackageChanged();

            // If the user has manually changed the Mod ID,
            // leave their custom value alone.
            if (modIdManuallyEdited)
                return;

            string modName = txtModName.Text.Trim();

            // Remove spaces to create a clean DeepCore Mod ID.
            string cleanName = modName.Replace(" ", "");

            updatingModIdAutomatically = true;

            if (string.IsNullOrWhiteSpace(cleanName))
            {
                txtModId.Clear();
            }
            else
            {
                txtModId.Text =
                    $"DeepCoreMods.{cleanName}";
            }

            updatingModIdAutomatically = false;
        }

        private void txtModId_TextChanged(object sender, EventArgs e)
        {
            // Ignore changes made by the automatic Mod ID generator.
            if (updatingModIdAutomatically)
                return;

            // Any other change was made manually by the user.
            modIdManuallyEdited = true;

            // The package details have changed since the last build.
            MarkPackageChanged();
        }

        private void txtVersion_TextChanged(object sender, EventArgs e)
        {
            MarkPackageChanged();
        }
        private void MarkPackageChanged()
        {
            lblBuildStatus.Text = "Ready to build";
            btnOpenPackageFolder.Enabled = false;
        }

        private void txtAuthor_TextChanged(object sender, EventArgs e)
        {
            MarkPackageChanged();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            MarkPackageChanged();
        }
    }
}
