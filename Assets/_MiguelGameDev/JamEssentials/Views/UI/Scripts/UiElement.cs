using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev
{
    public class UiElement : MonoBehaviour
    {
        private RectTransform _rectTansform;


        public RectTransform RectTransform {
            get {
                if (_rectTansform == null)
                {
                    _rectTansform = GetComponent<RectTransform>();
                }
                return _rectTansform;
            }
        }
    }
}