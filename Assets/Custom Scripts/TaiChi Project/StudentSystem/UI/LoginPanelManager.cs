using System.Collections;
using System.Collections.Generic;
using Keyboard;
using TMPro;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class LoginPanelManager : MonoBehaviour
        {
            [SerializeField] private GameObject _loginPanel;
            [SerializeField] private TMP_InputField _emailInputField;
            [SerializeField] private TMP_InputField _passwordInputField;
            [SerializeField] private KeyboardManager _keyboardManager;

            public string GetEmail(){
                return _emailInputField.text;
            }
            public string GetPassword(){
                return _passwordInputField.text;
            }
            public void SetLoginPanelActive(bool p_active){
                _loginPanel.SetActive(p_active);
            }
            public void SetKeyboardOutputField(string p_outputFieldName){
                if(p_outputFieldName == "Email"){
                    _keyboardManager.outputField = _emailInputField;
                }
                else if(p_outputFieldName == "Password"){
                    _keyboardManager.outputField = _passwordInputField;
                } 

            }
        }
    }
}