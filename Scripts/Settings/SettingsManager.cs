using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] GameObject postProcessing;
    [SerializeField] bool graphicsOnAwake;
    [SerializeField] bool graphics;
    [SerializeField] TMP_Text graphicsText;
    [SerializeField] string onText = "Graphics: On";
    [SerializeField] string offText = "Graphics: Off";
    
    private void Awake()
    {
        Load();
    }

    /// <summary>
    /// Loads settings
    /// </summary>
    private void Load()
    {
        SettingsData data = LoadData();

        if (data == null) return;

        graphics = data.graphics;

        if (graphicsText != null)
        {
            if (data.graphics) graphicsText.text = onText;
            else graphicsText.text = offText;
        }

        UpdateGraphics();
    }

    /// <returns>
    /// Returns the loaded data from the file, returns null if the file wasn't found.
    /// </returns>
    private SettingsData LoadData()
    {
        string path = Application.persistentDataPath + "/settings.data";
        if(File.Exists(path))
        {
            // specify the binary formatter and binary file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            // saves the settings in a binary file
            SettingsData loaded = formatter.Deserialize(fs) as SettingsData;
            fs.Close();

            return loaded;
        }

        return null;
    }
    
    /// <summary>
    /// Save settings data in a file
    /// </summary>
    public void SaveData()
    {
        SettingsData data = new SettingsData(graphics);
        
        // specify the binary formatter and binary file
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.data";

        FileStream fs = new FileStream(path, FileMode.Create);

        // saves the settings in a binary file
        formatter.Serialize(fs, data);
        fs.Close();
    }

    /// <summary>
    /// Changes the graphics value from the settings menu
    /// </summary>
    public void Graphics()
    {
        graphics = !graphics;

        if (graphics) graphicsText.text = onText;
        else graphicsText.text = offText;

        UpdateGraphics();
    }

    private void UpdateGraphics()
    {
        if (graphicsOnAwake)
        {
            postProcessing.SetActive(graphics);
        }
    }
}
