using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class FaceCamera : MonoBehaviour
        {
            [SerializeField] private Camera _mainCamera;

            // Update is called once per frame
            void Update()
            {
                this.transform.LookAt(_mainCamera.transform);
            }
            

            
        }
    }
}
