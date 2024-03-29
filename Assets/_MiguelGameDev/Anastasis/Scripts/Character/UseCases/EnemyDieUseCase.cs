using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class EnemyDieUseCase : CharacterDieUseCase
    {
        private readonly EnemyFacade _enemyFacade;
        private readonly ParticleSystem _dieEffect;
        private readonly PlayerKillEnemyUseCase _playerKillEnemyUseCase;
        private readonly int _experience;

        public EnemyDieUseCase(EnemyFacade enemyFacade, CharacterUi characterUi, 
                ParticleSystem dieEffect, PlayerKillEnemyUseCase playerKillEnemyUseCase, int experience) : base(enemyFacade.Motor, enemyFacade.Animation, enemyFacade.Audio, characterUi)
        {
            _enemyFacade = enemyFacade;
            _dieEffect = dieEffect;
            _playerKillEnemyUseCase = playerKillEnemyUseCase;
            _experience = experience;
        }

        public override async UniTask<bool> CharacterDie()
        {
            if (!await base.CharacterDie())
            {
                return false;
            }

            _animation.gameObject.SetActive(false);
            _dieEffect.Play();

            await _playerKillEnemyUseCase.PlayerKillEnemy(_enemyFacade, _experience);

            return true;
        }
    }
}