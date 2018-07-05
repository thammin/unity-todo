using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TodoMvp
{
    public class TodoMvpView : MonoBehaviour
    {
        [SerializeField]
        private Text _temp;

        [SerializeField]
        private InputField _input;
        public InputField Input { get { return _input; } }

        [SerializeField]
        private Button _button;
        public Button Button { get { return _button; } }

        [SerializeField]
        private Transform _listContainer;

        public void AddItem(GameObject item)
        {
            item.transform.SetParent(_listContainer, false);
        }

        public void SetTemp(string value)
        {
            _temp.text = value;
        }
    }
}

