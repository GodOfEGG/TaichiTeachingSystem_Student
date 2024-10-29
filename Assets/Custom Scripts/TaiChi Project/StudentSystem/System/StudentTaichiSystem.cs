using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace TaichiTeachingSystem
{
    namespace StudentSystem{
        public class StudentTaichiSystem : MonoBehaviour
        {
            [SerializeField] private PlayMode _playMode;
            [SerializeField] private RecordMode _recordMode;

            [SerializeField] private TMP_Dropdown _modeDropdown;


            void Start()
            {
                SetMode();
            }
            void Update()
            {
                switch (_modeDropdown.value){
                    // Record Mode
                    case 0:
                        _recordMode.Run();
                        break;

                    // Play Mode
                    case 1:
                        _playMode.Run();
                        break;
                }
            }

            public void SetMode(){
                switch (_modeDropdown.value){
                    // Record Mode
                    case 0:
                        _recordMode.Enter();
                        _playMode.Leave();

                        break;
                    // Play Mode
                    case 1:
                        _playMode.Enter();
                        _recordMode.Leave();
                        break;
                }
            }
            public void SetFPS(){
                switch (_modeDropdown.value){
                    // Record Mode
                    case 0:
                        _recordMode.SetFPS();
                        break;

                    // Play Mode
                    case 1:
                        _playMode.SetFPS();
                        break;
                }
            }

        }


    }
}