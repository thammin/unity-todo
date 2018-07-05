using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace TodoMvp
{
    [RequireComponent(typeof(TodoMvpItemView))]
    public class TodoMvpItemPresenter : MonoBehaviour
    {

        private TodoMvpItemModel _model;
        private TodoMvpItemView _view;

        /// <summary>
        /// 初期化(本来はIEnumeratorだけど割愛する)
        /// </summary>
        public void Initialize(TodoMvpItemModel model)
        {
            _model = model;
            _view = GetComponent<TodoMvpItemView>();
            BindData();
            BindEvents();
        }

        private void BindData()
        {
            _model.Description
                .Subscribe(_view.SetDescription)
                .AddTo(this);
        }

        private void BindEvents()
        {
            _view.IsCheck.OnValueChangedAsObservable()
                .Subscribe(_view.SetCheckMark)
                .AddTo(this);
        }
    }

}
