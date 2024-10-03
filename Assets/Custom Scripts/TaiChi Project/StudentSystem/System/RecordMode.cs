using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace TaichiTeachingSystem{
    namespace StudentSystem{
        public class RecordMode : MonoBehaviour
        {
            private float _FPS = 30;

            [SerializeField] private AvatarManager _avatarManager;
            [SerializeField] private FramePanelManager _framePanelManager;
            [SerializeField] private RecordPanelManager _recordPanelManager;
            [SerializeField] private CoachManager _coachManager;
            [SerializeField] private LoginManager _loginManager;

            private bool _onRecording = false;
            private int _frameID;
            

            private MotionData _motionData;
            
            private float _fpsDeltaTime = 0f;
            private float _updateTimer = 0f;



            //////////////////////////////////////////////////////
            ////////////   For StudentTaichiSystem  //////////////
            /////////////////////////////////////////////////////
            
            public void Enter(){
                _onRecording = false;
                _frameID = 0;
                SetFPS();

                // Avatars
                _avatarManager.EnterRecordMode();
                // UI Panels
                _recordPanelManager.SetPanelActive(true);
                //Coach
                _coachManager.EnterRecordMode();

            }
            public void Run(){
                _avatarManager.SetRecordAvatarsPose();
                if(!_onRecording)
                    return;

                _updateTimer -= Time.deltaTime;
                if (_updateTimer > 0)
                    return;
                _updateTimer += _fpsDeltaTime;

                _framePanelManager.SetFrameNumberText(_frameID);

                _DoRecording();
            }
            public void Leave(){
                _recordPanelManager.SetPanelActive(false);

            }

            private void _DoRecording()
            {
                // muscle value
                MuscleValues tmpValue = new MuscleValues();
                _avatarManager.GetRecordMuscleValue(ref tmpValue);

                // add a frames
                _motionData.motionFrames.Add(tmpValue);

                // update recordFrameIndex
                _frameID += 1;
            }

            //////////////////////////////////////////////////////
            ////////////////   For UI Panels  //////////////////
            /////////////////////////////////////////////////////
            
            public void SetFPS(){
                _FPS = _framePanelManager.GetFPS();
                _fpsDeltaTime = 1/_FPS;
                _coachManager.SetCoachSpeed();
            }


            public void ChangeRecordingState(){

                // Start Recording
                if(!_onRecording){
                    StartCoroutine(_StartRecording());
                }
                //Stop Recording
                else{
                    _recordPanelManager.SetRecordDataBufferActive(true, _motionData.motionFrames.Count);
                    _framePanelManager.SetFpsSliderInteractable(true);
                    _onRecording = !_onRecording;
                    _recordPanelManager.SetRecordingButton(_onRecording);
                }
                
            }
            IEnumerator _StartRecording(){
                _motionData = new MotionData
                {
                    motionFrames = new List<MuscleValues>(),
                    modifiedFrames = new List<ModifyValues>(),
                };
                _updateTimer = 0;
                _frameID = 0;
                _coachManager.RestartCoachMove();
                _coachManager.SetCoachSpeed(0);
                _recordPanelManager.SetRecordDataBufferActive(false, 0);
                _framePanelManager.SetFpsSliderInteractable(false);

                yield return StartCoroutine(_StartRecordingCountdown());
                _onRecording = !_onRecording;
                _coachManager.SetCoachSpeed();
                _recordPanelManager.SetRecordingButton(_onRecording);
            }
            IEnumerator _StartRecordingCountdown(){
                _recordPanelManager.SetRecordCountdownPanelActive(true);

                _recordPanelManager.SetRecordCountdownText(3);
                yield return new WaitForSeconds(1);
                _recordPanelManager.SetRecordCountdownText(2);
                yield return new WaitForSeconds(1);
                _recordPanelManager.SetRecordCountdownText(1);
                yield return new WaitForSeconds(1);
                _recordPanelManager.SetRecordCountdownText(0);
                yield return new WaitForSeconds(1);

                _recordPanelManager.SetRecordCountdownPanelActive(false);

            }

            IEnumerator _SetCoachIdDropdown(){
                string accessToken = _loginManager.GetAccessToken();
                yield return StartCoroutine(HttpService.Get_CoachAll(accessToken));

                List<CoachData> coachDataList = HttpService.GetCoachDataList();
                if(coachDataList != null)
                    _recordPanelManager.SetCoachIdDropdown(coachDataList);
                else
                    Debug.LogError("Get CoachData failed");
            }
            public void OpenUploadRecordDataPanel(){
                _recordPanelManager.SetUploadRecordDataPanelActive(true);
                StartCoroutine(_SetCoachIdDropdown());
            }
            IEnumerator _UploadHandler(MotionData p_motionData, string p_accessToken){
                yield return StartCoroutine(HttpService.Post_MotionDataNew(p_motionData, p_accessToken));
                bool uploadSuccess = HttpService.GetUploadSuccess();
                _recordPanelManager.SetUploadRecordDataPanelActive(false);
                _recordPanelManager.SetUploadResultPanelActive(true);
                _recordPanelManager.SetUploadResultText(uploadSuccess);
            }
            public void UploadRecordData(){
                string accessToken = _loginManager.GetAccessToken();
                _motionData.coachId = _recordPanelManager.GetCoachId();
                _motionData.userId = _loginManager.GetUserId();
                StartCoroutine(_UploadHandler(_motionData, accessToken));
            }
        }
    }
}
