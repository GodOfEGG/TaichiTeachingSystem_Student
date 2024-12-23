using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class ResetAvatar : StateMachineBehaviour
        {
            // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
            override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            {
                animator.applyRootMotion = false;
                animator.avatarRoot.localPosition = Vector3.zero;
                animator.avatarRoot.localRotation = Quaternion.identity;
            }
            override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            {
                animator.applyRootMotion = true;
            }

        }
    }
}
