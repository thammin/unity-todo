using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace TodoMvp
{
    public class TodoMvpPresenter : MonoBehaviour
    {
        [SerializeField]
        private GameObject _itemPrefab;

        private List<TodoMvpItemModel> _models;

        private TodoMvpView _view;

        /// <summary>
        /// 初期化(本来はIEnumeratorのInitializeだけど割愛する)
        /// </summary>
        public void Start()
        {
            _view = GetComponent<TodoMvpView>();

            // 本来はWorkからデータをモデルを作るけど割愛する
            _models = new List<TodoMvpItemModel>();
            for (var i = 0; i < 500; i++)
            {
                AddItem($"item {i}");
            }

            BindEvents();
        }

        private void BindEvents()
        {
            _view.Input.OnValueChangedAsObservable()
                .Subscribe(_view.SetTemp)
                .AddTo(this);

            _view.Button.OnClickAsObservable()
                .Subscribe(u =>
                {
                    AddItem(_view.Input.text);
                    _view.Input.text = "";
                })
                .AddTo(this);
        }

        private void AddItem(string description)
        {
            var model = new TodoMvpItemModel();
            model.SetDescription(description);
            _models.Add(model);

            var instance = Instantiate(_itemPrefab);
            var presenter = instance.GetComponent<TodoMvpItemPresenter>();
            presenter.Initialize(model);
            _view.AddItem(instance);
        }
    }
}

