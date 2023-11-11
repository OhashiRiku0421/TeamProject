using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    /// <summary>ダメージを送る関数</summary>
    /// <param name="damage">与えたいダメージ</param>
    public void SendDamage(float damage);
}