using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TodoDataBind
{
    public class TodoDataBindItemModel
    {
        // Propertyが必要である
        public string Description { get; set; }
    }

    public class TodoDataBindItem : MonoBehaviour
    {
        [SerializeField]
        private Image _checkMark;

        public void SetCheckMark(bool isOn)
        {
            _checkMark.enabled = isOn;
        }
    }
}

