using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace POC
{
    public class POCShow : POCComponent
    {
        [SerializeField]
        public Component Src;
        [SerializeField]
        public string SrcProperty;

        public override void Initialize()
        {
            var dataProperty = Src.GetType().GetProperty("DTO");
            var data = dataProperty.GetValue(Parent);
            var resultProperty = dataProperty.PropertyType.GetProperty(SrcProperty);
            var result = resultProperty.GetValue(data);
            if (result == null)
            {
                result = new ReactiveProperty<bool>();
                resultProperty.SetValue(data, result);
            }

            var resultType = resultProperty.PropertyType;
            var r = (ReactiveProperty<bool>)result;

            var cr = GetComponent<CanvasRenderer>();

            //cr.SetAlpha(0);
            r.Subscribe(v => cr.SetAlpha(v ? 1 : 0)).AddTo(this);
        }
    }
}

