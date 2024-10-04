using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class AvatarManager : MonoBehaviour
        {
            [Header("===Record Mode Avatar===")]
            [SerializeField] private GameObject _recordAvatars;
            private List<Animator> _recordAnimators;
            private List<HumanPoseHandler> _recordPoseHandlers;
            private HumanPose _recordPose;


            [Header("===Play Mode: Duo Origin Avatar===")]
            [SerializeField] private GameObject _play_duo_originAvatars; 
            private Animator _play_duo_originAnimator;
            private HumanPoseHandler _play_duo_originPoseHandler;
            private HumanPose _play_duo_originPose;

            [Header("===Play Mode: Duo Modify Avatar===")]
            [SerializeField] private GameObject _play_duo_modifyAvatars;
            private Animator _play_duo_modifyAnimator;
            private HumanPoseHandler _play_duo_modifyPoseHandler;
            private HumanPose _play_duo_modifyPose;


            void Start(){
                // For record Avatars
                _recordAnimators = new List<Animator>();
                _recordPoseHandlers = new List<HumanPoseHandler>();
                for(int ID = 0 ; ID< _recordAvatars.transform.childCount ; ID++){
                    _recordAnimators.Add(_recordAvatars.transform.GetChild(ID).GetChild(0).GetComponent<Animator>());
                    _recordPoseHandlers.Add(new HumanPoseHandler(_recordAnimators[ID].avatar, _recordAnimators[ID].transform));
                }
                _recordPose = new HumanPose();

            }
            
            /////////////////////////////////////////////////////////////////
            ////////////////////   Tools   //////////////////////////////////
            /////////////////////////////////////////////////////////////////
            private void _RefAvatarPose(GameObject p_avatars, int p_avatarID, ref Animator p_animator, ref HumanPose p_humanPose, ref HumanPoseHandler p_humanPoseHandler){
                p_animator = p_avatars.transform.GetChild(p_avatarID).GetChild(0).GetComponent<Animator>();
                p_humanPose = new HumanPose();
                p_humanPoseHandler = new HumanPoseHandler(p_animator.avatar, p_animator.transform);
                p_humanPoseHandler.GetHumanPose(ref p_humanPose);
            }

            private void _SetAvatarsPose(GameObject p_avatars, ref Animator p_animator, ref HumanPose p_pose, ref HumanPoseHandler p_poseHandler, MuscleValues p_muscleValue){
                for(int ID = 0 ; ID<p_avatars.transform.childCount ; ID++){
                    _RefAvatarPose(p_avatars, ID, ref p_animator, ref p_pose, ref p_poseHandler);
                    for (int i = 0; i < p_pose.muscles.Length; ++i)
                        p_pose.muscles[i] = p_muscleValue.muscleValues[i];

                    // set position & rotation
                    p_pose.bodyPosition = p_muscleValue.position;
                    p_pose.bodyRotation = p_muscleValue.rotation;

                    // set to yAvatar
                    p_poseHandler.SetHumanPose(ref p_pose);
                }

            }
            private void _SetAvatarsPosition(GameObject p_avatars, float p_posX){
                Vector3 pos = p_avatars.transform.localPosition;
                p_avatars.transform.localPosition = new Vector3(p_posX, pos.y, pos.z);
            }

            /////////////////////////////////////////////////////////////////
            ////////////   Record Mode Avatars   ////////////////////////////////
            /////////////////////////////////////////////////////////////////
                
            public void GetRecordMuscleValue(ref MuscleValues tmpValue){
                tmpValue.muscleValues = new float[_recordPose.muscles.Length];
                _recordPoseHandlers[0].GetHumanPose(ref _recordPose);
                for (int i = 0; i < _recordPose.muscles.Length; ++i)
                    tmpValue.muscleValues[i] = _recordPose.muscles[i];

                // position & rotation
                tmpValue.position = _recordPose.bodyPosition;
                tmpValue.rotation = _recordPose.bodyRotation;
            }
            
            // Show or Hide Record Avatars
            public void EnterRecordMode(){
                _recordAvatars.SetActive(true);
                _play_duo_originAvatars.SetActive(false);
                _play_duo_modifyAvatars.SetActive(false);
            }

            public void SetRecordAvatarsPose(){
                // Mocopi motion data are sent to RecordAvatar_Front
                _recordPoseHandlers[0].GetHumanPose(ref _recordPose);

                // Copy the pose of RecordAvatar_Front to other RecordAvatars
                for(int ID = 1 ; ID< _recordAvatars.transform.childCount ; ID++){
                    _recordPoseHandlers[ID].SetHumanPose(ref _recordPose);
                }
            }


            /////////////////////////////////////////////////////////////////
            ////////////   Play Mode Avatars   ////////////////////////////////
            /////////////////////////////////////////////////////////////////

            public void SetPlayModeAvatarPose(MuscleValues p_muscleValue, ModifyValues p_modifyValue){
                MuscleValues tmpValue = new MuscleValues();
                if (p_modifyValue != null){
                    tmpValue.muscleValues = new float[p_modifyValue.muscleValues.Length];

                    for(int i=0 ; i<tmpValue.muscleValues.Length ; i++)
                        tmpValue.muscleValues[i] = p_modifyValue.muscleValues[i];
                    tmpValue.position = p_modifyValue.position;
                    tmpValue.rotation = p_modifyValue.rotation;
                }

                _SetAvatarsPose(_play_duo_originAvatars, ref _play_duo_originAnimator, ref _play_duo_originPose, ref _play_duo_originPoseHandler, p_muscleValue);
                if(p_modifyValue == null){
                    _SetAvatarsPose(_play_duo_modifyAvatars, ref _play_duo_modifyAnimator, ref _play_duo_modifyPose, ref _play_duo_modifyPoseHandler, p_muscleValue);
                }
                else{
                    _SetAvatarsPose(_play_duo_modifyAvatars, ref _play_duo_modifyAnimator, ref _play_duo_modifyPose, ref _play_duo_modifyPoseHandler, tmpValue);
                }
            }

            
            public void SetSingleAvatarPose(bool p_singleAvatarOriginPose){
                _play_duo_originAvatars.SetActive(p_singleAvatarOriginPose);
                _play_duo_modifyAvatars.SetActive(!p_singleAvatarOriginPose);
            }

            public void EnterPlayMode(bool p_singleAvatarMode, bool p_singleAvatarOriginPose){
                _recordAvatars.SetActive(false);
                if(p_singleAvatarMode){
                    SetSingleAvatarPose(p_singleAvatarOriginPose);
                    _SetAvatarsPosition(_play_duo_originAvatars, -1f);
                    _SetAvatarsPosition(_play_duo_modifyAvatars, -1f);
                    
                }
                else{
                    _play_duo_originAvatars.SetActive(true);
                    _play_duo_modifyAvatars.SetActive(true);
                    _SetAvatarsPosition(_play_duo_originAvatars, 0.2f);
                    _SetAvatarsPosition(_play_duo_modifyAvatars, -1.6f);
                    
                }
            }


        }
    }    
}