using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static string filePath = Application.persistentDataPath + "/Saved Levels";

    public static void Save(TileLevelMaker settings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);

        TileLevelMakerData data = new TileLevelMakerData(settings);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void Load(TileLevelMaker settings)
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            TileLevelMakerData data = formatter.Deserialize(stream) as TileLevelMakerData;

            stream.Close();

            data.LoadTo(settings);
        }
        else
        {
            Debug.Log("Save file not found");
        }
    }
}

[System.Serializable]
public class TileLevelMakerData
{
    public TileTracker.SquareType[,] board;

    public TileLevelMakerData(TileLevelMaker settings)
    {
        board = settings.board;
    }

    public void LoadTo(TileLevelMaker settings)
    {    if (board != null)             
        {
            settings.board = board;
        }
    }
}