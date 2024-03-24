using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MiguelGameDev
{
    public class GraphicFadeAnimation : MonoBehaviour
    {
        [SerializeField] float _fade = 0.5f;
        [SerializeField] float _duration = 1f;


        private Graphic _graphic;
        private float _defaultAlpha;
        private Tween _tween;

        private Graphic Graphic {
            get {
                if (_graphic == null)
                {
                    _graphic = GetComponent<Graphic>();
                }
                return _graphic;
            }
        }

        private void OnDestroy()
        {
            if (_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }
        }

        private void OnDisable()
        {
            Graphic.color.SetAlpha(_defaultAlpha);
            if (_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }
        }

        private void OnEnable()
        {
            _defaultAlpha = Graphic.color.a;
            _tween = Graphic.DOFade(_fade, _duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
    }
}