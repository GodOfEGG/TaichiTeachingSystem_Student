using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class Indicators : MonoBehaviour
        {
            [SerializeField] private IndicatorPair[] _indicatorPairs;
            
            public void SetIndicatorBodyPart(Transform[] p_bodyPartList){
                for(int i=0 ; i<_indicatorPairs.Length ; i++){
                    _indicatorPairs[i].bodyPart = p_bodyPartList[i];
                }
            }

            public void SetIndicatorTransform(){
                for(int i=0 ; i<_indicatorPairs.Length ; i++){
                    _indicatorPairs[i].SetTransform();
                }
            }

            public void SetIndicatorMaterial(List<AvatarBodyPartList> p_avatarBodyPartList, Material p_originMaterial, Material p_modifyMaterial){
                for(int i=0 ; i<_indicatorPairs.Length ; i++){
                    if(p_avatarBodyPartList.Contains(_indicatorPairs[i].bodyPartIndex)){
                        _indicatorPairs[i].indicator.GetComponent<Renderer>().material = p_modifyMaterial;
                    }
                    else{
                        _indicatorPairs[i].indicator.GetComponent<Renderer>().material = p_originMaterial;
                    }
                }
            }
            public void ResetIndicatorMaterial(Material p_originMaterial){
                for(int i=0 ; i<_indicatorPairs.Length ; i++){
                    _indicatorPairs[i].indicator.GetComponent<Renderer>().material = p_originMaterial;
                }
            }
        }
        [Serializable]
        public class IndicatorPair
        {
            public GameObject indicator;
            public Transform bodyPart;
            public AvatarBodyPartList bodyPartIndex;
            public void SetTransform(){
                indicator.transform.position = bodyPart.position;
                indicator.transform.rotation = bodyPart.rotation;
            }
        }

        
        
    }
}