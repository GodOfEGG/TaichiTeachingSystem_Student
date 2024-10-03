using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TaichiTeachingSystem{
    public class ShowPanelsButton : MonoBehaviour
    {
        [SerializeField] private GameObject _panels;
        [SerializeField] private Image _buttonImage;
        private bool _showPaanels;
        void Start(){
            _showPaanels = true;
        }
        
        public void SwitchShowPanels(){
            _showPaanels = !_showPaanels;
            _panels.SetActive(_showPaanels);
            _buttonImage.transform.Rotate(0, 0, 180);
        }
    }
}
