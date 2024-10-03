using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class IndicatorManager : MonoBehaviour
        {
            [SerializeField] private Indicators[] _play_duo_originIndicatorList;
            [SerializeField] private Indicators[] _play_duo_modifyIndicatorList;
            [SerializeField] private Material _originMaterial;
            [SerializeField] private Material _modifyMaterial;


            public void SetIndicatorTransform(){
                for(int i=0 ; i<_play_duo_originIndicatorList.Length ; i++){
                    _play_duo_originIndicatorList[i].SetIndicatorTransform();
                }
                for(int i=0 ; i<_play_duo_modifyIndicatorList.Length ; i++){
                    _play_duo_modifyIndicatorList[i].SetIndicatorTransform();
                }
            }

            public void SetIndicatorActive(bool p_active, bool p_singleAvatarMode, bool p_singleAvatarOriginPose){
                if(p_singleAvatarMode){
                    for(int i=0 ; i<_play_duo_originIndicatorList.Length ; i++){
                        _play_duo_originIndicatorList[i].gameObject.SetActive(p_active && p_singleAvatarOriginPose);
                    }
                    for(int i=0 ; i<_play_duo_modifyIndicatorList.Length ; i++){
                        _play_duo_modifyIndicatorList[i].gameObject.SetActive(p_active && !p_singleAvatarOriginPose);
                    }
                }
                else{
                    for(int i=0 ; i<_play_duo_originIndicatorList.Length ; i++){
                        _play_duo_originIndicatorList[i].gameObject.SetActive(p_active);
                    }
                    for(int i=0 ; i<_play_duo_modifyIndicatorList.Length ; i++){
                        _play_duo_modifyIndicatorList[i].gameObject.SetActive(p_active);
                    }
                }
            }

            public void SetIndicatorMaterial(List<AvatarBodyPartList> p_avatarBodyPartList){
                for(int i=0 ; i<_play_duo_originIndicatorList.Length ; i++){
                    _play_duo_originIndicatorList[i].SetIndicatorMaterial(p_avatarBodyPartList, _originMaterial, _modifyMaterial);
                }
                for(int i=0 ; i<_play_duo_modifyIndicatorList.Length ; i++){
                    _play_duo_modifyIndicatorList[i].SetIndicatorMaterial(p_avatarBodyPartList, _originMaterial, _modifyMaterial);
                }
            }
            public void ResetIndicatorMaterial(){
                for(int i=0 ; i<_play_duo_originIndicatorList.Length ; i++){
                    _play_duo_originIndicatorList[i].ResetIndicatorMaterial( _originMaterial);
                }
                for(int i=0 ; i<_play_duo_modifyIndicatorList.Length ; i++){
                    _play_duo_modifyIndicatorList[i].ResetIndicatorMaterial(_originMaterial);
                }
            }

        }
    }
}