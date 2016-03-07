#region Directives

using System.IO;
using UnityEditor;
using UnityEngine;

#endregion

/// <summary>
/// Unity Initializer 
/// Initializes the project folder heirarchy
/// </summary>
public class UnityInitializer : EditorWindow
{
    //Main Assets Directory
    private DirectoryInfo _assetsDirectory;

    //Name of readme Files to add to sub directories 
    private const string ReadMe = "README.md";

    // Add menu named "My Window" to the Window menu
    [MenuItem("Utilities/Initialize Project")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        var window = (UnityInitializer) GetWindow(typeof (UnityInitializer));
        window.Show();
        window.Initialize();
    }

    /// <summary>
    /// Initialize
    /// Runs the initilization of the project hierarchy
    /// </summary>
    public void Initialize()
    {
        _assetsDirectory = new DirectoryInfo(Application.dataPath);
        AddDirectory("Editor");
        AddDirectory("Font");
        AddDirectory("Scenes");
        AddDirectory("Scripts");
        AddDirectory("Art");
        AddDirectory("Materials");
        AddDirectory("Models");
        AddDirectory("Prefabs");
        AddDirectory("Plugins");
        AddDirectory("Resources");
        AddDirectory("Animations");
        Close();
    }

    /// <summary>
    /// AddDirectory
    /// Adds the new directory if Needed
    /// </summary>
    /// <param name="dirName">Directory Name</param>
    private void AddDirectory(string dirName)
    {
        var directoryPath = _assetsDirectory.Name + "/" + dirName;
        var directoryNeedsCreation = !Directory.Exists(directoryPath);
        if (directoryNeedsCreation)
        {
            _assetsDirectory.CreateSubdirectory(dirName);
        }

        AddReadMe(directoryPath);
    }

    /// <summary>
    /// AddReadMe
    /// Adds a readme if not already in existence. 
    /// 
    /// This is a necessary step to allow cross platform development with git.
    /// Git will not delete the folder on other systems because it doesn't track folders just files.
    /// </summary>
    private void AddReadMe(string directoryPath)
    {
        var filePath = directoryPath + "/" + ReadMe;
        var readmeNeedsCreate = !File.Exists(filePath);
        if (readmeNeedsCreate)
        {
            File.Create(filePath);
        }
    }
}