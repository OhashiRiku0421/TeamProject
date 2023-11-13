public struct ScoreData
{
    public readonly float ClearTime;

    public readonly int PLayerLife;

    public readonly int BetLife;

    public readonly int ItemCount;

    public ScoreData(float clearTime, int playerLife, int itemCount, int betLife)
    {
        ClearTime = clearTime;
        PLayerLife = playerLife;
        ItemCount = itemCount;
        BetLife = betLife;
    }
}
