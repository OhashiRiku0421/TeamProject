using System;

/// <summary>
/// Pauseを管理するクラス
/// </summary>
public class PauseSystem
{

    private static PauseSystem _instance = new();

    public static PauseSystem Instance => _instance;

    private Action _onPause;
    private Action _onResume;

    /// <summary>
    /// Pauseのアクションに登録
    /// </summary>
    public void Register(IPause pause)
    {
        _onPause += pause.Pause;
        _onResume += pause.Resume;
    }

    /// <summary>
    ///  Pauseのアクションを解除
    /// </summary>
    public void Unregister(IPause pause)
    {
        _onPause -= pause.Pause;
        _onResume -= pause.Resume;
    }

    /// <summary>
    /// Pauseを実行する
    /// </summary>
    public void Pause()
    {
        _onPause?.Invoke();
    }

    /// <summary>
    /// Pauseを解除する
    /// </summary>
    public void Resume()
    {
        _onResume?.Invoke();
    }
}