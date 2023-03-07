using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationHPBar : MonoBehaviour
{
    [SerializeField] Image GreenGauge;
    [SerializeField] Image RedGauge;

    PlayerController _player;
    Tween redGaugeTween;

    public void GaugeReduction(float reducionValue, float time = 1f)
    {
        var valueFrom = _player._Life / _player._initialLife;
        var valueTo = (_player._Life - reducionValue) / _player._initialLife;

        //�΃Q�[�W����
        GreenGauge.fillAmount = valueTo;

        if (redGaugeTween != null)
        {
            redGaugeTween.Kill();
        }

        //�ԃQ�[�W����
        redGaugeTween = DOTween.To(
            () => valueFrom,
            x =>
            {
                RedGauge.fillAmount = x;
            },
            valueTo,
            time
            );
        Debug.Log("�_���[�W");
    }

    public void SetPlayer(PlayerController player)
    {
        this._player = player;
    }
}
