using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class AvatarManager : MonoBehaviour
        {
            [Header("===Avatar Options")]
            [SerializeField] private GameObject[] _avatarOptions;

            [Header("===Record Mode Avatar===")]
            [SerializeField] private GameObject _recordAvatars;
            [SerializeField] private Transform[] _recordPos;
            [SerializeField] private GameObject _recordNameTag;
            private List<Animator> _recordAnimators;
            private List<HumanPoseHandler> _recordPoseHandlers;
            private HumanPose _recordPose;


            [Header("===Play Mode: Origin Avatar===")]
            [SerializeField] private GameObject _play_originAvatars;
            [SerializeField] private Transform[] _play_originPos;
            [SerializeField] private GameObject _play_originNameTag;
            public List<Animator> _play_originAnimators;
            private List<HumanPoseHandler> _play_originPoseHandlers;
            private HumanPose _play_originPose;

            [Header("===Play Mode: Modify Avatar===")]
            [SerializeField] private GameObject _play_modifyAvatars;
            [SerializeField] private Transform[] _play_modifyPos;
            [SerializeField] private GameObject _play_modifyNameTag;
            public List<Animator> _play_modifyAnimators;
            private List<HumanPoseHandler> _play_modifyPoseHandlers;
            private HumanPose _play_modifyPose;

            
            /////////////////////////////////////////////////////////////////
            ////////////////////   Common  //////////////////////////////////
            /////////////////////////////////////////////////////////////////
            private void _SetAvatarsPosition(GameObject p_avatars, float p_posX){
                Vector3 pos = p_avatars.transform.localPosition;
                p_avatars.transform.localPosition = new Vector3(p_posX, pos.y, pos.z);
            }

            /////////////////////////////////////////////////////////////////
            ////////////   Record Mode Avatars   ////////////////////////////////
            /////////////////////////////////////////////////////////////////
            
            public void InstantiateRecordModeAvatars(){
                int avatarId = PlayerPrefs.GetInt("AvatarId");
                // Record Avatars
                for(int i=0 ; i<8 ; i++){
                    GameObject newAvatar = Instantiate(_avatarOptions[avatarId]);
                    newAvatar.SetActive(true);
                    newAvatar.transform.SetParent(_recordAvatars.transform);
                    newAvatar.transform.position = _recordPos[i].position;
                    newAvatar.transform.localRotation = Quaternion.Euler(0, 0, 0);

                    // Add Name Tag
                    GameObject nameTag = Instantiate(_recordNameTag);
                    nameTag.transform.SetParent(newAvatar.transform.GetChild(0).GetChild(0).GetChild(0));
                    nameTag.transform.localPosition = new Vector3(0, 1.0f, 0);
                }
            }
            
            public GameObject GetFirstRecordAvatar(){
                return _recordAvatars.transform.GetChild(0).GetChild(0).gameObject;
            }
            public void _PrepareRecordAvatars(){
                // For record Avatars
                _recordAnimators = new List<Animator>();
                _recordPoseHandlers = new List<HumanPoseHandler>();
                for(int ID = 0 ; ID< _recordAvatars.transform.childCount ; ID++){
                    _recordAnimators.Add(_recordAvatars.transform.GetChild(ID).GetChild(0).GetComponent<Animator>());
                    _recordPoseHandlers.Add(new HumanPoseHandler(_recordAnimators[ID].avatar, _recordAnimators[ID].transform));
                }
                _recordPose = new HumanPose();
            }
                
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
                _PrepareRecordAvatars();
                _recordAvatars.SetActive(true);
                _play_originAvatars.SetActive(false);
                _play_modifyAvatars.SetActive(false);
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
            
            public void InstantiatePlayModeAvatars(){
                int avatarId = PlayerPrefs.GetInt("AvatarId");

                // Play Origin Avatars
                for(int i=0 ; i<8 ; i++){
                    GameObject newAvatar = Instantiate(_avatarOptions[avatarId]);
                    newAvatar.SetActive(true);
                    newAvatar.transform.SetParent(_play_originAvatars.transform);
                    newAvatar.transform.position = _play_originPos[i].position;
                    newAvatar.transform.localRotation = Quaternion.Euler(0, 0, 0);

                    // Add Name Tag
                    GameObject nameTag = Instantiate(_play_originNameTag);
                    nameTag.transform.SetParent(newAvatar.transform.GetChild(0).GetChild(0).GetChild(0));
                    nameTag.transform.localPosition = new Vector3(0, 1.0f, 0);
                }

                // Play Modify Avatars
                for(int i=0 ; i<8 ; i++){
                    GameObject newAvatar = Instantiate(_avatarOptions[avatarId]);
                    newAvatar.SetActive(true);
                    newAvatar.transform.SetParent(_play_modifyAvatars.transform);
                    newAvatar.transform.position = _play_modifyPos[i].position;
                    newAvatar.transform.localRotation = Quaternion.Euler(0, 0, 0);

                    // Add Name Tag
                    GameObject nameTag = Instantiate(_play_modifyNameTag);
                    nameTag.transform.SetParent(newAvatar.transform.GetChild(0).GetChild(0).GetChild(0));
                    nameTag.transform.localPosition = new Vector3(0, 1.0f, 0);
                }
            }

            public void _PreparePlayAvatars(){
                // For origin Avatars
                _play_originAnimators = new List<Animator>();
                _play_originPoseHandlers = new List<HumanPoseHandler>();
                for(int ID = 0 ; ID< _play_originAvatars.transform.childCount ; ID++){
                    _play_originAnimators.Add(_play_originAvatars.transform.GetChild(ID).GetChild(0).GetComponent<Animator>());
                    _play_originPoseHandlers.Add(new HumanPoseHandler(_play_originAnimators[ID].avatar, _play_originAnimators[ID].transform));
                }
                _play_originPose = new HumanPose();
                _play_originPose.muscles = new float[95];

                // For modify Avatars
                _play_modifyAnimators = new List<Animator>();
                _play_modifyPoseHandlers = new List<HumanPoseHandler>();
                for(int ID = 0 ; ID< _play_modifyAvatars.transform.childCount ; ID++){
                    _play_modifyAnimators.Add(_play_modifyAvatars.transform.GetChild(ID).GetChild(0).GetComponent<Animator>());
                    _play_modifyPoseHandlers.Add(new HumanPoseHandler(_play_modifyAnimators[ID].avatar, _play_modifyAnimators[ID].transform));
                }
                _play_modifyPose = new HumanPose();
                _play_modifyPose.muscles = new float[95];
            }
                

            public void SetPlayModeAvatarPose(MuscleValues p_muscleValue, ModifyValues p_modifyValue){
                //For origin avatars
                for (int i = 0; i < _play_originPose.muscles.Length; ++i)
                    _play_originPose.muscles[i] = p_muscleValue.muscleValues[i];

                _play_originPose.bodyPosition = p_muscleValue.position;
                _play_originPose.bodyRotation = p_muscleValue.rotation;

                for(int ID = 0 ; ID<_play_originAvatars.transform.childCount ; ID++){
                    _play_originPoseHandlers[ID].SetHumanPose(ref _play_originPose);
                }

                //For modify avatars
                if(p_modifyValue == null){
                    for (int i = 0; i < _play_modifyPose.muscles.Length; ++i)
                        _play_modifyPose.muscles[i] = p_muscleValue.muscleValues[i];

                    _play_modifyPose.bodyPosition = p_muscleValue.position;
                    _play_modifyPose.bodyRotation = p_muscleValue.rotation;

                    for(int ID = 0 ; ID<_play_modifyAvatars.transform.childCount ; ID++)
                        _play_modifyPoseHandlers[ID].SetHumanPose(ref _play_modifyPose);
                }
                else{
                    for (int i = 0; i < _play_modifyPose.muscles.Length; ++i)
                        _play_modifyPose.muscles[i] = p_modifyValue.muscleValues[i];

                    _play_modifyPose.bodyPosition = p_modifyValue.position;
                    _play_modifyPose.bodyRotation = p_modifyValue.rotation;

                    for(int ID = 0 ; ID<_play_modifyAvatars.transform.childCount ; ID++)
                        _play_modifyPoseHandlers[ID].SetHumanPose(ref _play_modifyPose);
                }
            }

            
            public void SetSingleAvatarPose(bool p_singleAvatarOriginPose){
                _play_originAvatars.SetActive(p_singleAvatarOriginPose);
                _play_modifyAvatars.SetActive(!p_singleAvatarOriginPose);
            }

            public void EnterPlayMode(bool p_singleAvatarMode, bool p_singleAvatarOriginPose){
                _recordAvatars.SetActive(false);
                if(p_singleAvatarMode){
                    SetSingleAvatarPose(p_singleAvatarOriginPose);
                    _SetAvatarsPosition(_play_originAvatars, -1f);
                    _SetAvatarsPosition(_play_modifyAvatars, -1f);
                    
                }
                else{
                    _play_originAvatars.SetActive(true);
                    _play_modifyAvatars.SetActive(true);
                    _SetAvatarsPosition(_play_originAvatars, 0.2f);
                    _SetAvatarsPosition(_play_modifyAvatars, -1.6f);
                    
                }
                _PreparePlayAvatars();
            }


        }
    }    
}