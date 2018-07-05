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
            for (var i = 0; i < 5; i++)
            {
                var model = new TodoMvpItemModel();
                model.SetDescription($"item {i}");
                _models.Add(model);
            }

            InitializePrefabs();
            BindEvents();
        }

        private void InitializePrefabs()
        {
            _models.ForEach(model =>
            {
                var instance = Instantiate(_itemPrefab);
                var presenter = instance.GetComponent<TodoMvpItemPresenter>();
                presenter.Initialize(model);
                _view.AddItem(instance);
            });
        }

        private void BindEvents()
        {
            _view.Input.OnValueChangedAsObservable()
                .Subscribe(_view.SetTemp)
                .AddTo(this);

            _view.Button.OnClickAsObservable()
                .Subscribe(AddItem)
                .AddTo(this);
        }

        private void AddItem(Unit u)
        {
            var model = new TodoMvpItemModel();
            model.SetDescription(_view.Input.text);
            _view.Input.text = "";
            _models.Add(model);

            var instance = Instantiate(_itemPrefab);
            var presenter = instance.GetComponent<TodoMvpItemPresenter>();
            presenter.Initialize(model);
            _view.AddItem(instance);
        }
    }
}

