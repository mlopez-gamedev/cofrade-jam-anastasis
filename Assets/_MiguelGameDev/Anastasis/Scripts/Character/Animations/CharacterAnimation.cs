using UnityEngine;

namespace MiguelGameDev.Anastasis
{

    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        
        public virtual void WakeUp()
        {
            // Nothing to do here
        }

        public void SetSpeedSqr(float speedSqr)
        {
            _animator.SetFloat("speedSqr", speedSqr);
        }

        public void TriggerHurt()
        {
            _animator.SetTrigger("hurt");
        }

        public void SetIsDead(bool isDead)
        {
            _animator.SetBool("isDead", isDead);
        }
    }
}