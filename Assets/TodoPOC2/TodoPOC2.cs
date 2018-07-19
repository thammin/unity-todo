using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using POC;
using System.Linq;
using UniRx;
using LazyCache;
using System;
using UnityEngine.Profiling;

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
                    Description = new ReactiveProperty<string>($"test {v}"),
                    IsCheck = new ReactiveProperty<bool>(true)
                })
                .ToList()
            );
            DTO.FirstTodo = DTO.Todos.First();
            DTO.TypingText = new ReactiveProperty<string>();

            Debug.Log($"__Parent initialize__ no children {Children.Count}");

            // IAppCache cache = new CachingService();
            // Func<string> complexObjectFactory = () => "result";
            // string cachedResults = cache.GetOrAdd("uniqueKey", complexObjectFactory);
        }

        public override void BeforeRouteEnter(POCRouter.Route To, POCRouter.Route From, Action<bool> next)
        {
            Debug.Log("hairu");
            next(true);
        }

        public override void BeforeRouteLeave(POCRouter.Route To, POCRouter.Route From, Action<bool> next)
        {
            Debug.Log("きた");
            next(true);
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
            //POCRouter.To("Home");
            //return;
            DTO.Todos.Add(new TodoPOC2Item.Data()
            {
                Description = new ReactiveProperty<string>(DTO.TypingText.Value),
                IsCheck = new ReactiveProperty<bool>(true)
            });
            DTO.TypingText.Value = "";

            // var info = GetStudentInfo((id: "100-000-1000", index: 5));
            // Debug.Log($"Name: {info.name}, Age: {info.age}");
            // MoveTo("abc", new Dictionary<string, object>(){
            //     { "a", 1 },
            //     { "b", 2 },
            //     { "c", true}
            // });
        }

        public void SetTypingText(string input)
        {
            DTO.TypingText.Value = input;
        }

        public void MoveTo(string target, Dictionary<string, object> input)
        {
            // foreach (var i in this.GetType().GetMethod("reMoveTo").GetParameters())
            // {

            // }
            // foreach (var item in input)
            // {
            //     item.Key
            // }
        }

        public void reMoveTo()
        {
            //Debug.Log(a);
            //Debug.Log(b);
            //Debug.Log(c);
        }

        public (string name, int age) GetStudentInfo((string id, int index) input)
        {
            // Search by ID and find the student.
            return (input.id, input.index);
        }
    }
}

