using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WriteData : MonoBehaviour
{
    private string _filePath;

    private void Start()
    {
#if UNITY_EDITOR
        _filePath = Path.Combine(Application.persistentDataPath, "zabojstwa.txt");
#else
        string exeFolder = Application.dataPath;
        string parentFolder = Directory.GetParent(exeFolder).FullName;
        _filePath = Path.Combine(parentFolder, "zabojstwa.txt");
#endif

        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "=== Kiedy zabity ===\n");
        }
    }

    /*
    public void WriteLine(string text)
    {
        File.AppendAllText(_filePath, text + "\n");
    }
    */
}
