using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using POC;
using System.Linq;
using UniRx;

namespace TodoPOC2
{
    public class TodoPOC2 : POCComponent
    {
        /// <summary>
        /// データ定義
        /// </summary>
        public class Data
        {
            // todo 一覧
            public ReactiveCollection<TodoPOC2Item.Data> Todos { get; set; }
            // TODO computedみたいの仕組み
            public TodoPOC2Item.Data FirstTodo { get; set; }
            // 入力中の text
            public ReactiveProperty<string> TypingText { get; set; }
        }

        public Data DTO { get; set; }

        /// <summary>
        /// (親方->子供)データあるけど、子供まだない
        /// </summary>
        public override void Initialize()
        {
            DTO = new Data();
            DTO.Todos = new ReactiveCollection<TodoPOC2Item.Data>(
                Enumerable
                .Range(1, 500)
                .Select(v => new TodoPOC2Item.Data()
                {
                    Description = new ReactiveProperty<string>($"test {v}")
                })
                .ToList()
            );
            DTO.FirstTodo = DTO.Todos.First();
            DTO.TypingText = new ReactiveProperty<string>();

            Debug.Log($"__Parent initialize__ no children {Children.Count}");
        }

        /// <summary>
        /// (子供->親方)データある、子供もある
        /// </summary>
        // public override void Mounted()
        // {
        //     Debug.Log($"__Parent mounted__ has children {Children.Count}");
        // }

        /// <summary>
        /// todo 追加
        /// </summary>
        public void AddItem()
        {
            DTO.Todos.Add(new TodoPOC2Item.Data()
            {
                Description = new ReactiveProperty<string>(DTO.TypingText.Value)
            });
            DTO.TypingText.Value = "";
        }

        public void SetTypingText(string input)
        {
            DTO.TypingText.Value = input;
        }
    }
}

