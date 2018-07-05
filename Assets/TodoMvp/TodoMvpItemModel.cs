using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace TodoMvp
{
    public class TodoMvpItemModel
    {

        private ReactiveProperty<string> _description;
        public IReadOnlyReactiveProperty<string> Description { get { return _description; } }

        /// <summary>
        /// 初期化(本来はIEnumeratorだけど割愛する)
        /// </summary>
        public TodoMvpItemModel()
        {
            _description = new ReactiveProperty<string>();
        }

        public void SetDescription(string value)
        {
            _description.Value = value;
        }
    }
}
