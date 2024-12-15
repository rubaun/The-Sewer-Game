using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    
    
        public Vector3 playerPosition;
        public List<GameObject> inventario;
        public List<GameObject> vidas;
    

    public void SavePlayer(List<GameObject> vidas, List<GameObject> inventario)
    {
        SaveData data = new SaveData();
        data.playerPosition = transform.position;
        data.vidas = vidas;
        data.inventario = inventario;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savePlayer.json", json);
    }

    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/savePlayer.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            transform.position = data.playerPosition;
            vidas = data.vidas;
            inventario = data.inventario;
        }

    }
}

