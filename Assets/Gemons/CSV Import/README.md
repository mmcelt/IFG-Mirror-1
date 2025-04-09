# CSV Import

## Table of Contents

- [Overview](#overview)
- [Installation Instructions](#installation)
- [Requirements](#requirements)
- [Limitations](#limitations)
- [Workflows](#workflows)
- [Advanced Topics](#advanced-topics)
- [Reference](#reference)
- [Samples](#samples)
- [Tutorials](#tutorials)

## Overview

CSV Import is a Unity package designed to allow developers to easily import and parse CSV files within Unity projects via an editor menu. It takes the CSV file and parses all rows of the CSV into separated `.asset` files in a target folder. The structure of this files is defined by a user provided `ScriptableObject` class as described in the unity documentation https://docs.unity3d.com/Manual/class-ScriptableObject.html.

The advantage of having the data stored in scriptable objects is that you can combine data imported from `.csv` files and data like asset references manually set in the Unity Editor. The contained example illustrates how item data for a fantasy game can be loaded from a `.csv` file and then the image references for every item can be assigned in the Unity Editor. This allows an easy split of updating numeric values for balancing and design in Unity Editor.

### Features

- **Easy Import:** Import CSV files directly into Unity.
- **Dynamic Parsing:** Automatically parse CSV files based on headers.
- **Customizable:** Flexible API to customize the data parsing and handling.
- **Data Management:** Easily manage, access and edit imported data within Unity.
- **Collaboration:** Easily work together by working separately on the numeric data of the `.csv` file and additional fields handled in the Unity Editor.

## Installation

Install the unity package as explained in the UPM manual: https://docs.unity3d.com/Manual/upm-ui-install.html

### Requirements

- Tested with: Unity 2023.2.17f1
- Will work with earlier Unity versions as long as Unity Editor menu API is the same.

### Limitations

- The column headers in the `.csv` file need to have identical names to the fields or properties in the target `ScriptableObject` data class. This is needed because the target fields are selected automatically without the need for additional mapping files or manual selection during import.
- Only `string`, `int` and `float` data types are currently being parsed correctly. Enums are best handled as additional property in side the target 'ScriptableObject' data class.
- The import folder is currently hardcoded to be the folder containing also the source `.csv` file. Thus it is recommended to have a ***Data*** folder containing the source and imported asset files.

### Workflows

1. Open your Unity project.
2. Download the CSV Import package via the package manager.
3. Prepare a `ScriptableObject` class that is a child class of `CsvImportBase` that contains all the field you need to import from a CSV file. 
    - Note that this class needs to implement the `IFormattable` interface of the parent class
    - This class also needs to implement the `UpdateFromImportedData()` method of the parent class. This method handles the updating of fields for already existing files. This is useful in case your class has fields like asset refernces that are manually assigned in the editor and should not be overwritten during import.
    - The main reason you need to derive from `CsVImportBase` is for easy searchability of the target data classes. A search for all `ScriptableObjet` classes would yield a long list.
4. Open the new Editor menu under "Assets/Gemons/Import CSV".
5. Drag and Drop the .csv file you want to import 
    - The menu will show you where the data will be imported to.
6. Decide if you want to delete existing files or update them using the checkbox. If this box is not checked and an asset with the same name already exists the importer will use the `UpdateFromImportedData()` method of your class to update the asset. There you can decide that not all fields should be overwritten during import.
7. Select the ScriptableObject class you just created that acts as target for the import.
8. Click `Import` and all rows of the file will be imported as separated `.asset` files into the target folder next to the selected `.csv` file.
9. Inspect the folder with the `.asset` files. You may now use these files as data containers for your game.
    - One way to do this is to add a list field to a `MonoBehavior` script and simply add the `.asset` files in the editor. This could look like this: `public List<ItemDataImportExample> ItemList;`

## Advanced Topics


## Reference


## Samples

Sample data is included in the ***Demo*** folder of the package. You may use the `ItemData.csv` to test the package using the flow described in the [Workflows](#workflows) section. Use the `ItemDataImportExample.cs` class as target for the import. 

## Tutorials