/// <summary>
/// Pause用のインターフェース
/// </summary>
public interface IPause
{
    /// <summary>
    /// Pauseの処理を書く
    /// </summary>
    public void Pause();

    /// <summary>
    /// Pauseが解除されたときの処理を書く
    /// </summary>
    public void Resume();
}