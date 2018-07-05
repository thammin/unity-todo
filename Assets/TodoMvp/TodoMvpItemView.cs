using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TodoMvp
{
    public class TodoMvpItemView : MonoBehaviour
    {
        [SerializeField]
        private Toggle _isCheck;
        public Toggle IsCheck { get { return _isCheck; } }

        [SerializeField]
        private Text _description;

        [SerializeField]
        private Image _checkMark;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {

        }

        public void SetDescription(string value)
        {
            _description.text = value;
        }

        public void SetCheck(bool value)
        {
            _isCheck.isOn = value;
        }

        public void SetCheckMark(bool value)
        {
            _checkMark.enabled = value;
        }
    }
}
