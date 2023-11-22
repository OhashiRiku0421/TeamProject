using UniRx;

public class ScoreSystem
{

    private float _clearTime;

    private static int _playerLife;

    private static int _betLife;

    private ReactiveProperty<int> _itemCount = new();

    public IReactiveProperty<int> ItemCount => _itemCount;

    public static ScoreData Score { get; private set; }

    /// <summary>
    /// リザルトに必要なライフを静的な変数に保存しておく
    /// </summary>
    public void GameResult()
    {
        Score = new(_clearTime, _playerLife, _itemCount.Value, _betLife);
    }

    /// <summary>
    /// リザルトの計算に必要なライフを保存しておく
    /// </summary>
    public static void SetLife(int playerLife, int betLife)
    {
        _playerLife = playerLife;
        _betLife = betLife;
    }

    /// <summary>
    /// アイテムの獲得数をカウント
    /// </summary>
    public void GetItem()
    {
        _itemCount.Value++;
    }
}