using System.IO;
using UnityEngine;

/// <summary>
/// Manages the interaction between Unity and text files
/// </summary>
[System.Serializable]
public class TextFileManager 
{
    public string logName; // Variable that contains the name of the log being used (assigned in inspector)
    public string[] logContents; // Keeps track of the logs that have been made to ensure everything is up to date

    // Try create the file (if not already present) and read the contents to initlialize logContents
    public void Start()
    {
        CreateFile(logName);
        ReadFileContents(logName);
    }

    /// <summary>
    /// Used by the reader to create a new file
    /// </summary>
    /// <param name="fileName">The name of the file/database</param>
    public void CreateFile(string fileName) 
    {
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";

        if (File.Exists(dirPath) == false) 
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources");
            File.WriteAllText(dirPath, fileName + "\n");
        }
    }

    /// <summary>
    /// Used by the reader to read the current file contents
    /// </summary>
    /// <param name="fileName">The file/database to read from</param>
    /// <returns></returns>
    public string[] ReadFileContents(string fileName) 
    {
        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        string[] tContents = new string[0];

        if (File.Exists(dirPath) == true) 
        {
            tContents = File.ReadAllLines(dirPath);
        }
        else return null;

        logContents = tContents;
        return tContents;
    }

    /// <summary>
    /// Used by the reader to save a key and value pair to the file/database, or add a new one if not present
    /// </summary>
    /// <param name="fileName">The file/database to save to</param>
    /// <param name="key">The key you wish to save to/add</param>
    /// <param name="value">The value you wish to save with the key</param>
    /// <param name="isTimestamped">Do you want it timestamped?</param>
    public void AddKeyValuePair(string fileName, string key, string value, bool isTimestamped) 
    {
        // Read file to update logContents
        ReadFileContents(fileName);

        string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
        string tContents = key + "," + value;
        string timestamp = "Time Stamp: " + System.DateTime.Now;

        // If the file exists
        if (File.Exists(dirPath) == true) 
        {
            for (int i = 0; i < logContents.Length; i++) 
            {
                // Check if log contains given key, if so replace the value in temp variable
                if (logContents[i].Contains(key) == true) 
                {
                    if (isTimestamped == true) 
                    {
                        logContents[i] = timestamp + " - " + tContents;
                    } else {
                        logContents[i] = tContents;
                    }
                    File.WriteAllLines(dirPath, logContents);
                }
                // Else if the key is not found, create it by appending it to end of log
                else
                {
                    if (isTimestamped == true) 
                    {
                        File.AppendAllText(dirPath, timestamp + " - " + tContents);
                    } else {
                        File.AppendAllText(dirPath, tContents);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Used by the reader to grab the value from a given key, the reader converts the value based on the method used
    /// </summary>
    /// <param name="key">The key corrosponding to the value desired</param>
    /// <returns></returns>
    public string LocateStringByKey(string key) 
    {
        // Read log to update logContents
        ReadFileContents(logName);

        string t = "";

        // Iterate through all keys
        foreach (string s in logContents) 
        {
            if (s.Contains(key) == true) 
            {
                // Split up the line, and get the last item, which will be the value
                string[] splitString = s.Split (",".ToCharArray ());
                t = splitString[splitString.Length - 1];
            }
        }
        return t;
    }

    // ** Not currently in use ** //
    // public void AddFileLine(string fileName, string fileContents)
    // {
    //     ReadFileContents(fileName);
    //     string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
    //     string tContents = fileContents + "\n";
    //     string timestamp = "Time Stamp: " + System.DateTime.Now;
    //     if (File.Exists(dirPath) == true)
    //     {
    //         File.AppendAllText(dirPath, timestamp + " - " + tContents);
    //     }
    // }
    
}