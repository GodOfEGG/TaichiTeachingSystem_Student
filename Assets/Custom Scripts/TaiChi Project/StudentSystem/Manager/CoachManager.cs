using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class CoachManager : MonoBehaviour
        {
            [SerializeField] private CoachPanelManager _coachPanelManager;
            [SerializeField] private FramePanelManager _framePanelManager;
            [SerializeField] private GameObject _coachAvatars;
            private Animator _coachAnimator;
            private bool _coachActive;
            

            void Start(){
                SetCoachActive();
                SetCoachTaichiMove();
            }

            public void SetCoachActive(){
                _coachActive = _coachPanelManager.GetCoachActive();
                _coachPanelManager.SetTaichiMoveDropdownInteractable(_coachActive);
                _coachAvatars.SetActive(_coachActive);
            }
            public void SetCoachTaichiMove(){
                int taichiMoveID = (int)_coachPanelManager.GetTaichiMoveID();
                for(int i=0 ; i<_coachAvatars.transform.childCount ; i++){
                    _coachAnimator = _coachAvatars.transform.GetChild(i).GetComponent<Animator>();
                    _coachAnimator.SetInteger("TaichiMoveID", taichiMoveID);
                    _coachAnimator.SetTrigger("ChangeTaichiMove");
                }
            }

            public void Play(){
                SetCoachSpeed();
            }
            public void Stop(){
                SetCoachSpeed(0);
            }

            
            public void EnterRecordMode(){
                RestartCoachMove();
                SetCoachSpeed(1);
                _SetCoachAvatarsPosition(1f);
            }
            public void EnterPlayMode(bool p_singleAvatarMode){
                RestartCoachMove();
                SetCoachSpeed(0);
                if(p_singleAvatarMode)
                    _SetCoachAvatarsPosition(1f);
                else
                    _SetCoachAvatarsPosition(2f);
            }

            public void SetCoachSpeed(float p_speed = -1){ // p_speed = -1 => Get speed from frame Panel
                if(p_speed == -1)
                    p_speed = _framePanelManager.GetSpeed();
                for(int i=0 ; i< _coachAvatars.transform.childCount ; i++){
                    _coachAnimator = _coachAvatars.transform.GetChild(i).GetComponent<Animator>();
                    _coachAnimator.speed = p_speed;
                }
            }

            public void RestartCoachMove(){
                for(int i=0 ; i< _coachAvatars.transform.childCount ; i++){
                    _coachAnimator = _coachAvatars.transform.GetChild(i).GetComponent<Animator>();
                    _coachAnimator.Play(0, -1, 0);
                }
            }
            private void _SetCoachAvatarsPosition(float p_posX){
                Vector3 pos = _coachAvatars.transform.localPosition;
                _coachAvatars.transform.localPosition = new Vector3(p_posX, pos.y, pos.z);
            }

        }
    }
}