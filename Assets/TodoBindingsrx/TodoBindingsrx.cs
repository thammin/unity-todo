using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using BindingsRx.Bindings;

namespace TodoBindingsrx
{
    public class TodoBindingsrx : MonoBehaviour
    {
        [SerializeField]
        private GameObject _itemPrefab;

        [SerializeField]
        private Text _temp;

        [SerializeField]
        private InputField _input;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private Transform _listContainer;

        private List<TodoBindingsrxItemModel> _models;

        /// <summary>
        /// 初期化(本来はIEnumeratorのInitializeだけど割愛する)
        /// </summary>
        public void Start()
        {
            // 本来はWorkからデータをモデルを作るけど割愛する
            _models = new List<TodoBindingsrxItemModel>();
            for (var i = 0; i < 5; i++)
            {
                AddItem($"item {i}");
            }

            BindData();
            BindEvents();
        }

        private void BindData()
        {
            _temp.BindTextTo(() => _input.text);
        }

        private void BindEvents()
        {
            _button.OnClickAsObservable()
                .Subscribe(u =>
                {
                    AddItem(_input.text);
                    _input.text = "";
                })
                .AddTo(this);
        }

        private void AddItem(string description)
        {
            var model = new TodoBindingsrxItemModel()
            {
                Description = description
            };
            _models.Add(model);

            var instance = Instantiate(_itemPrefab);
            var presenter = instance.GetComponent<TodoBingindsrxItem>();
            presenter.Initialize(model);
            instance.transform.SetParent(_listContainer, false);
        }

    }
}

