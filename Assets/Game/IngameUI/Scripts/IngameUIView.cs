using UnityEngine;

/// <summary>
/// MVPのView
/// </summary>
public class IngameUIView : MonoBehaviour
{
    [SerializeField]
    public static readonly GameTimerUI _gameTimerUI;
    public GameTimerUI GameTimerUI => _gameTimerUI;

    [SerializeField]
    private PlayerLifeUI _playerLifeUI;
    public PlayerLifeUI PlayerLifeUI => _playerLifeUI;

    [SerializeField]
    private ItemCountUI _itemCountUI;
    public ItemCountUI ItemCountUI => _itemCountUI;

    public void Intialized()
    {
        _gameTimerUI.Intialized();
        _playerLifeUI.Intialized();
        _itemCountUI.Intialized();
    }
}
