﻿using MEC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MiguelGameDev.Anastasis
{
    public class HolyFireAvatar : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] float _avatarSizeMultiplier = 0.5f;

        private HolyFireAbility _ability;
        private ObjectPool<HolyFireAvatar> _objectPool;
        private bool _enable = false;

        private List<Collider> _targetsDamaged;

        internal void Setup(HolyFireAbility ability, ObjectPool<HolyFireAvatar> objectPool)
        {
            _ability = ability;
            _objectPool = objectPool;
            _targetsDamaged = new List<Collider>();
        }

        internal void Init(Vector3 position)
        {
            transform.position = position;
            ChangeAvatarSize();
            Timing.CallDelayed(0.5f, Cast, gameObject);
            gameObject.SetActive(true);
        }

        private void Cast()
        {
            _enable = true;
            _audioSource.pitch = Random.Range(0.85f, 1.1f);
            _audioSource.Play();

            Timing.CallDelayed(_ability.Duration.Value, Kill, gameObject);
        }

        private void Kill()
        {
            _objectPool.Release(this);
        }

        private void ChangeAvatarSize()
        {
            var avatarSize = _ability.Size.Value * _avatarSizeMultiplier;
            transform.localScale = new Vector3(avatarSize, 1f, avatarSize);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_enable)
            {
                return;
            }

            if (_targetsDamaged.Contains(other))
            {
                return;
            }

            if (!_ability.TryMakeDamage(other))
            {
                return;
            }

            _targetsDamaged.Add(other);
        }

        internal void Finish()
        {
            _enable = false;
            gameObject.SetActive(false);
            _targetsDamaged.Clear();
        }
    }
}
