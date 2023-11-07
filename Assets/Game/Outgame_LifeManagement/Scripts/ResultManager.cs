using UnityEngine;

public class ResultManager : MonoBehaviour, ISetLife
{
    private int _originLife = ExternalLifeManager.Life;
    private int _getLife = 10;
    private int _lostLife = 20;
    private int _bonusLife = 30;
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
