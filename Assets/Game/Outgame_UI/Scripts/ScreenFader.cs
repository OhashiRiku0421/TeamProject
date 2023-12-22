using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    [SerializeField]
    private float _duration = 1f;
    [SerializeField]
    private Image _panelImage;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        FadeOut(_duration);
    }

    public Tweener FadeIn(float duration)
    {
        var panelColor = _panelImage.color;
        panelColor.a = 0;
        _panelImage.color = panelColor;
        _panelImage.enabled = true;
        return _panelImage.DOFade(1, duration).SetEase(Ease.OutQuad);
    }

    public Tweener FadeOut(float duration)
    {
        if(_panelImage != null)
        {
            var panelColor = _panelImage.color;
            panelColor.a = 1;
            _panelImage.color = panelColor;
            _panelImage.enabled = true;
        }
        return _panelImage.DOFade(0, duration).SetEase(Ease.InQuad).OnComplete(() => _panelImage.enabled = false);
    }
}
