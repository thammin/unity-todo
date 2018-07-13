using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using POC;
using System.Linq;

namespace TodoPOC
{
    public class TodoPOC : POCComponent
    {
        /// <summary>
        /// データ定義
        /// </summary>
        public class Data
        {
            // todo 一覧
            public List<TodoPOCItem.Data> Todos { get; set; }
            // TODO computedみたいの仕組み
            public TodoPOCItem.Data FirstTodo { get; set; }
            // 入力中の text
            public string TypingText { get; set; }
        }

        public Data DTO { get; set; }

        /// <summary>
        /// (親方->子供)データあるけど、子供まだない
        /// </summary>
        public override void Initialize()
        {
            DTO = new Data();
            DTO.Todos = Enumerable
                .Range(1, 500)
                .Select(v => new TodoPOCItem.Data()
                {
                    Description = $"test {v}"
                })
                .ToList();
            DTO.FirstTodo = DTO.Todos.First();

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
            DTO.Todos.Add(new TodoPOCItem.Data()
            {
                Description = DTO.TypingText
            });
            DTO.TypingText = "";
        }

        public void SetTypingText(string input)
        {
            DTO.TypingText = input;
        }
    }
}

