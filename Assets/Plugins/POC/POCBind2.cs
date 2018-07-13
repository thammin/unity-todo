using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UniRx;
using UnityEngine.UI;

namespace POC
{
    public class POCBind2 : MonoBehaviour
    {
        [SerializeField]
        private Component _src;
        [SerializeField]
        private string _srcProperty;

        //

        public Component Src
        {
            get { return _src; }
            set { _src = value; }
        }
        public string SrcProperty
        {
            get { return _srcProperty; }
            set { _srcProperty = value; }
        }

        private void Start()
        {
            var dataProperty = Src.GetType().GetProperty("DTO");
            var data = dataProperty.GetValue(Src);
            var resultProperty = dataProperty.PropertyType.GetProperty(SrcProperty);
            var result = resultProperty.GetValue(data);

            var resultType = resultProperty.PropertyType;
            if (resultType == typeof(ReactiveProperty<string>))
            {
                var r = (ReactiveProperty<string>)result;
                Text text;
                InputField inputField;

                if (text = GetComponent<Text>())
                {
                    r.Subscribe(value => text.text = value).AddTo(this);
                }
                else if (inputField = GetComponent<InputField>())
                {
                    r.Subscribe(value => inputField.text = value).AddTo(this);
                }
            }
        }
    }
}


