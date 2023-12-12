using UnityEngine;
using UnityEngine.UI;

public class NeedleController : MonoBehaviour
{
    [SerializeField, Tooltip("秤の針の位置")]
    private RectTransform _needleTransform;

    [SerializeField, Tooltip("ライフベットスライダー")]
    private Slider _slider;

    public void NeedleRotate()
    {
        var anglePerStep = 180 / (_slider.minValue + _slider.maxValue); // 段階数を180で割って一目盛りあたりの角度を求める
        var currentAngle = anglePerStep * _slider.value; // 現在のスライダーの値に対して一目盛りの値を掛ける
        _needleTransform.localEulerAngles = new Vector3(0, 0, currentAngle - 90); // 針の角度をを更新する
    }
}
