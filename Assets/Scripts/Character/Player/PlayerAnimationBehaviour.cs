using UnityEngine;

namespace Characters.Player
{
    public class PlayerAnimationBehaviour : MonoBehaviour
    {
        [SerializeField] private Animator topAnimator;
        [SerializeField] private Animator legsAnimator;

        private readonly int LEGS_IDLE = Animator.StringToHash("legs_idle");
        private readonly int LEGS_WALK = Animator.StringToHash("legs_walk");
        private readonly int LEGS_RUN = Animator.StringToHash("legs_run");
        
        private readonly int TOP_IDLE = Animator.StringToHash("top_idle");
        private readonly int TOP_WALK = Animator.StringToHash("top_walk");
        private readonly int TOP_RUN = Animator.StringToHash("top_run");
        
        private State _state = State.IDLE;

        public void playWalkAnimation()
        {
            if (_state == State.WALK)
            {
                return;
            }
            topAnimator.Play(TOP_IDLE);
            legsAnimator.Play(LEGS_WALK);
            _state = State.WALK;
        }
        
        public void playIdleAnimation()
        {
            if (_state == State.IDLE)
            {
                return;
            }
            topAnimator.Play(TOP_IDLE);
            legsAnimator.Play(LEGS_IDLE);
            _state = State.IDLE;
        } 
        
        public void playRunAnimation()
        {
            if (_state == State.RUN)
            {
                return;
            }
            topAnimator.Play(TOP_RUN);
            legsAnimator.Play(LEGS_RUN);
            _state = State.RUN;
        }

        public void SendLegsDirection(Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            legsAnimator.transform.rotation = Quaternion.Euler(0,0, angle-90);
        }

        private enum State
        {
            IDLE,
            WALK,
            RUN
        }

    }
}