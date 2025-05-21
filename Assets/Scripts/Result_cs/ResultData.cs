public class ResultData
{
    private static ResultData instance;
    public static ResultData Instance => instance ??= new ResultData();

    public int perfectCount;
    public int greatCount;
    public int goodCount;
    public int missCount;
    public int maxCombo;
    public int score;
    public float bpm;
}
