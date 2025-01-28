using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private static string savePath;
    private static bool gameLoaded = false;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/save.json";
    }

    public static void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Jogo salvo em " + savePath);
    }

    public static GameData Load()
    {
        if (File.Exists(savePath))
        {
            
            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Jogo carregado de " + savePath);
            gameLoaded = true;
            return data;
        }
        else
        {
            Debug.LogWarning("Salvamento do Jogo não existe em " + savePath);
            return null;
        }
    }

    public static void Delete()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Salvamento do Jogo deletado em " + savePath);
        }
        else
        {
            Debug.LogWarning("Salvamento do Jogo não existe em " + savePath);
        }
    }

    public static bool SaveExists()
    {
        return File.Exists(savePath);
    }

    public static bool GameLoaded()
    {
        return gameLoaded;
    }
}
