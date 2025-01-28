using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public string faseAtual;
    public int vidasAtual;
    public float[] checkpointPos = new float[3];
    public bool checkpoint;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            Soldier player = FindObjectOfType<Soldier>();
            faseAtual = player.GetFase();
            vidasAtual = player.GetVida();
            checkpointPos[0] = player.transform.position.x;
            checkpointPos[1] = player.transform.position.y;
            checkpoint = false;
        }
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.Load();
        if(data != null)
        {
            faseAtual = data.fase;
            vidasAtual = data.vida;
            SceneManager.LoadScene(faseAtual);
            Time.timeScale = 1.0f;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(faseAtual);
    }

    public void RestartGame()
    {
        SaveSystem.Delete();
        SceneManager.LoadScene(faseAtual);
    }

    public void SaveGame()
    {
        GameData data = new GameData(vidasAtual, faseAtual, checkpointPos, checkpoint);
        SaveSystem.Save(data);
    }

    public void Checkpoint(float x, float y, int vida)
    {
        checkpoint = true;
        checkpointPos[0] = x;
        checkpointPos[1] = y;
        vidasAtual = vida;
        
        SaveGame();
    }
}
