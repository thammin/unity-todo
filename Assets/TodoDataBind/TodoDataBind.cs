using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TodoDataBind
{
    public class TodoDataBind : MonoBehaviour
    {

        private DataBindContext _context;
        private ObservableList _items;

        [SerializeField]
        private InputField _input;

        private void Awake()
        {
            _context = GetComponent<DataBindContext>();
            _items = new ObservableList("Items");
            _context["Items"] = _items;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void Start()
        {
            // 本来はWorkからデータをモデルを作るけど割愛する
            for (var i = 0; i < 5; i++)
            {
                _items.Add(new TodoDataBindItemModel
                {
                    Description = $"item {i}"
                });
            }
        }

        public void AddItem()
        {
            _items.Add(new TodoDataBindItemModel
            {
                Description = _input.text
            });
            _input.text = "";
        }
    }
}

