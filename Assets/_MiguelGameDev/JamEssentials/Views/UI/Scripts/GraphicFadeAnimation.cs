using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MiguelGameDev
{
    public class GraphicFadeAnimation : MonoBehaviour
    {
        [SerializeField] float _fade = 0.5f;
        [SerializeField] float _duration = 1f;
        [SerializeField] bool _unescaleTime = true;

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
            if (_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }
            Graphic.SetAlpha(_defaultAlpha);
        }

        private void OnEnable()
        {
            _defaultAlpha = Graphic.color.a;
            _tween = Graphic.DOFade(_fade, _duration).SetUpdate(_unescaleTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
    }
}