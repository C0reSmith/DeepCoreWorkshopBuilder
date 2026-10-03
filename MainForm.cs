using System.Drawing;
using System.IO;


namespace DeepCoreWorkshopBuilder
{
    public partial class MainForm : Form
    {
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

                using Image image = Image.FromFile(dialog.FileName);

                FileInfo imageFile = new FileInfo(dialog.FileName);

                lblPreviewStatus.Text =
                    $"✓ {imageFile.Name} — {image.Width} × {image.Height}";
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

                using Image image = Image.FromFile(dialog.FileName);

                FileInfo imageFile = new FileInfo(dialog.FileName);

                lblThumbnailStatus.Text =
                    $"✓ {imageFile.Name} — {image.Width} × {image.Height}";
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
                    return;
                }
            }

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

            // Keep the original preview image filename.
            string previewFileName =
                Path.GetFileName(txtPreviewPath.Text.Trim());

            // Build the destination path inside the Workshop package.
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

            // Keep the original thumbnail filename.
            string thumbnailFileName =
                Path.GetFileName(txtThumbnailPath.Text.Trim());

            // Build the destination path inside the Workshop package.
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

                return;
            }

            lblBuildStatus.Text =
                "✓ Workshop package built successfully";

            btnOpenPackageFolder.Enabled = true;

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
    }
}
