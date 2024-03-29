using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public abstract class Map : MonoBehaviour
    {
        [SerializeField] private RectTransform _map;
        [SerializeField] private Vector2 _worldSize = new Vector2(1000f, 1000f);
        [SerializeField] private Vector2 _visibleWorldRatio = new Vector2(1f, 1f);

        [SerializeField] protected RectTransform _playerMarker;
        [SerializeField] protected RectTransform _targetMarker;

        protected Transform _player;
        protected PlayerGoals _playerGoals;

        protected Vector2 _halfWorldSize;

        protected Vector2 _mapScale;
        protected Vector2 _halfMapSize;

        public void Setup(Transform player, PlayerGoals playerGoals)
        {
            _player = player;
            _playerGoals = playerGoals;

            _halfWorldSize = _worldSize / 2f;

            var mapSize = _map.rect.size;
            _halfMapSize = mapSize / 2f;

            _mapScale = mapSize  / (_worldSize * _visibleWorldRatio);
        }

        public abstract void Tick();
    }
}
