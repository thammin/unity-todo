using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BindingsRx.Bindings;
using UniRx;

namespace TodoBindingsrx
{
    public class TodoBindingsrxItemModel
    {
        public string Description;
    }

    public class TodoBingindsrxItem : MonoBehaviour
    {
        [SerializeField]
        private Toggle _isCheck;

        [SerializeField]
        private Text _description;

        [SerializeField]
        private Image _checkMark;

        private TodoBindingsrxItemModel _model;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize(TodoBindingsrxItemModel model)
        {
            _model = model;
            BindData();
            BindEvents();
        }

        private void BindData()
        {
            _description.BindTextTo(() => _model.Description);
        }

        private void BindEvents()
        {
            _isCheck.OnValueChangedAsObservable()
                .Subscribe(isOn => _checkMark.enabled = isOn)
                .AddTo(this);
        }
    }
}

