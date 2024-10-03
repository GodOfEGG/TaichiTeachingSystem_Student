using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;

namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class LoginManager : MonoBehaviour
        {
            [SerializeField] private GameObject _mainSystem;
            [SerializeField] private LoginPanelManager _loginPanelManager;
            [SerializeField] private CreateUserPanelManager _createUserPanelManager;
            [SerializeField] private GameObject _loginCanvas;
            private int _userId;
            private string _username;
            private string _password;
            private string _email;
            private string _accessToken;
            void Start(){
                _mainSystem.SetActive(false);
            }

            public int GetUserId(){
                return _userId;
            }
            public string GetUsername(){
                return _username;
            }
            public string GetAccessToken(){
                return _accessToken;
            }

            public void Login(){
                StartCoroutine(_HandleLogin());
            }
            IEnumerator _HandleLogin(){
                _email = _loginPanelManager.GetEmail();
                _password = _loginPanelManager.GetPassword();
                yield return StartCoroutine(HttpService.GetUserAccessToken(_email, _password));

                _accessToken = HttpService.GetAccessToken();
                if(_accessToken == null){
                    Debug.LogError("Login Failed");
                }
                else{
                    yield return StartCoroutine(HttpService.Get_MeUser(_accessToken));
                    _userId = HttpService.GetUserId();
                    _username = HttpService.GetUsername();
                    _loginPanelManager.SetLoginPanelActive(false);
                    _loginCanvas.SetActive(false);
                    _mainSystem.SetActive(true);

                }
            }
            public void CreateUser(){
                StartCoroutine(_HandleCreateUser());
            }
            IEnumerator _HandleCreateUser(){
                _username = _createUserPanelManager.GetUsername();
                _email = _createUserPanelManager.GetEmail();
                _password = _createUserPanelManager.GetPassword();
                yield return StartCoroutine(HttpService.Post_CreateUser(_username, _email, _password));
                if(HttpService.IsCreateUserSuccess()){
                    Debug.Log("Create User Success");
                    _loginPanelManager.SetLoginPanelActive(true);
                    _createUserPanelManager.SetCreateUserPanelActive(false);
                }
                else{
                    Debug.LogError("Create User Failed");
                }

            }
            
        }
    }
}
