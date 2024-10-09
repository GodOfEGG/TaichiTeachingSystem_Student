using System.Collections;
using System.Collections.Generic;
using Keyboard;
using TMPro;
using UnityEngine;

namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class IPPanelManager : MonoBehaviour
        {
            [SerializeField] private GameObject _ipPanel;
            [SerializeField] private TMP_InputField _ipInputField;
            [SerializeField] private KeyboardManager _keyboardManager;

            public void SetServerIP(){
                HttpService.SetBaseUrl(_ipInputField.text);
            }
            public void SetIPPanelActive(bool p_active){
                _ipPanel.SetActive(p_active);
            }
            public void SetKeyboardOutputField(){
                _keyboardManager.SetOutputField(_ipInputField);
            }
        }
    }
}