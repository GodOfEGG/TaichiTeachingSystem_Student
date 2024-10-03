using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class CoachPanelManager : MonoBehaviour
        {
            [SerializeField] private Toggle _coachActiveToggle;
            [SerializeField] private TMP_Dropdown _taichiMoveDropdown;
            // Start is called before the first frame update
            public float GetTaichiMoveID(){
                return _taichiMoveDropdown.value;
            }
            public bool GetCoachActive(){
                return _coachActiveToggle.isOn;
            }
            public void SetTaichiMoveDropdownInteractable(bool p_interactable){
                _taichiMoveDropdown.interactable = p_interactable;
            }
        }
    }
}