using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class SceneLoaderForDemo : MonoBehaviour
        {
            public void LoadMainScene(){
                SceneManager.LoadScene("MainSceneForDemo");
            }
            public void LoadLoginScene(){
                SceneManager.LoadScene("LoginSceneForDemo");
            }
        }
    }
}