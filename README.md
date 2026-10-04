# DeepCore Workshop Builder

**Engineering Better Gameplay**

DeepCore Workshop Builder is a Windows utility designed to simplify building and validating **DeepCore Mods packages for the Stationeers Steam Workshop**.

Instead of manually creating folders, renaming artwork, writing `About.xml`, and checking package contents, Workshop Builder prepares the package automatically.

## Features

- Automatically generates the Workshop Mod ID
- Supports manual Mod ID override
- Selects and validates the BepInEx plugin DLL
- Selects Workshop preview and thumbnail artwork
- Automatically renames artwork to the required Workshop filenames
- Validates thumbnail size before building
- Generates `About.xml` automatically
- Creates the correct Workshop package folder structure
- Displays a live Package Preview before building
- Verifies all required package files after building
- Protects existing packages with an overwrite warning
- Remembers the selected output folder
- Provides direct access to the completed package folder

## Generated Package Structure

A typical BepInEx Workshop package created by Workshop Builder looks like:

    DeepCoreMods.ExampleMod
    ├── DeepCoreMods.ExampleMod.dll
    └── About
        ├── About.xml
        ├── Preview.png
        └── thumb.png

Artwork extensions are preserved when the package is built.

## Workshop Validation

Before a package is created, Workshop Builder checks:

- Mod Name
- Mod ID
- Author
- Version
- Plugin DLL
- Preview image
- Thumbnail image
- Output folder
- Valid Mod ID folder characters
- Valid numeric version format
- Steam Workshop thumbnail size requirement

The completed package is then verified before being reported as successfully built.

## Installation

Download the latest release ZIP and extract it to a folder of your choice.

Run:

    DeepCoreWorkshopBuilder.exe

The application is published as a self-contained Windows x64 application.

## Basic Usage

1. Enter the Mod Name.
2. Confirm or edit the automatically generated Mod ID.
3. Enter the version and description.
4. Select the compiled BepInEx plugin DLL.
5. Select the Workshop Preview image.
6. Select the Workshop Thumbnail image.
7. Choose the output folder.
8. Review the Package Preview.
9. Click **BUILD WORKSHOP PACKAGE**.
10. Open the completed package using **OPEN PACKAGE FOLDER**.

The resulting folder is ready for the Stationeers Workshop upload process.

## Requirements

- Windows 64-bit
- Stationeers
- BepInEx-based Stationeers mod DLL

The released Workshop Builder application is self-contained and does not require a separate .NET installation.

## Version

**DeepCore Workshop Builder v1.0.0**

Initial public release.

## Author

**C0reSmith**

### DeepCore Mods

**Engineering Better Gameplay**
