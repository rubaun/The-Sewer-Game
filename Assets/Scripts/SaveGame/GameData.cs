[System.Serializable]

public class GameData
{
    public int vida;
    public string fase;
    public float[] checkpointPos;
    public bool checkpoint;

    public GameData(int vida, string fase, float[] checkpointPos,bool checkpoint)
    {
        this.vida = vida;
        this.fase = fase;
        this.checkpointPos = checkpointPos;
        this.checkpoint = false;
    }
}
