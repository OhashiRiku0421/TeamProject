using UnityEngine;

public class ResultManager : MonoBehaviour, ISetLife
{   /// <summary>インゲームに持ち込んだライフ</summary>
    private int _originLife = ExternalLifeManager.Life;
    /// <summary>インゲームで手に入れたライフ</summary>
    private int _getLife = 10;
    /// <summary>インゲームで失ったライフ</summary>
    private int _lostLife = 20;
    /// <summary>ボーナスライフ</summary>
    private int _bonusLife = 30;
    /// <summary>最終的に手に入ったライフ</summary>
    private int _currentLife = 40;

    public int OriginLife { get => _originLife; }
    public int GetLife { get => _getLife; }
    public int LostLife {  get => _lostLife; }
    public int BonusLife {  get => _bonusLife; }
    public int CurrentLife { get => _currentLife; }

    public void SetLife(int currentPlayerLife, int itemScore)
    {
        _getLife = itemScore;
        _lostLife = _originLife - currentPlayerLife;
    }
}
