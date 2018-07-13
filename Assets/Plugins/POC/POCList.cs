using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UniRx;
using System.Linq;

namespace POC
{
    public class POCList : POCComponent
    {
        [SerializeField]
        public Component Src;

        [SerializeField]
        public string SrcProperty;

        private IList data;

        private PropertyInfo childInfo;

        private List<Component> children = new List<Component>();

        public override void Initialize()
        {
            AutoBind();
        }

        /// <summary>
        /// 500 => ~800ms
        /// </summary>
        void AutoBind()
        {
            var targetType = typeof(POCComponent);

            var infoTemp = Src.GetType().GetProperty("DTO");
            var infoData = infoTemp.GetValue(Src);
            var info = infoTemp.PropertyType.GetProperty(SrcProperty);

            var test = (Func<IList>)Delegate.CreateDelegate(
                typeof(Func<IList>),
                infoData,
                info.GetMethod
            );

            foreach (var child in transform.GetChild(0).GetComponents<Component>())
            {
                if (child.GetType().IsSubclassOf(targetType))
                {
                    childInfo = child.GetType().GetProperty("DTO");
                    children.Add(child);
                }
            }

            BindChildren(test());

            var isFirstTime = true;
            Observable.EveryUpdate()
                .Select(x =>
                {
                    data = test();
                    return data.Count;
                })
                .DistinctUntilChanged()
                .Subscribe(_ =>
                {
                    if (isFirstTime)
                    {
                        isFirstTime = false;
                        return;
                    }
                    BindChildren(data);
                });
        }

        void BindChildren(IList data)
        {
            var timer = System.Diagnostics.Stopwatch.StartNew();

            for (var i = 0; i < data.Count; i++)
            {
                if (children.ElementAtOrDefault(i) != null)
                {
                    childInfo.SetValue(children[0], data[i]);
                }
                else
                {
                    var copied = Instantiate(children[0], transform);
                    children.Add(copied);
                    childInfo.SetValue(copied, data[i]);
                }
            }

            timer.Stop();
            Debug.Log($"Refresh bind children took {timer.ElapsedMilliseconds}ms");
        }

        /// <summary>
        /// 500 => ~800ms
        /// </summary>
        // void ManualBind()
        // {
        //     var timer = System.Diagnostics.Stopwatch.StartNew();

        //     var child = transform.GetChild(0).GetComponent<TodoPOC.TodoPOCItem>();

        //     var data = Src.Todos;

        //     Debug.Log(data.Count);

        //     for (var i = 0; i < data.Count; i++)
        //     {
        //         if (i == 0)
        //         {
        //             child.Data = (TodoPOC.TODOPOCItemData)data[i];
        //             continue;
        //         }

        //         var copied = Instantiate(child, transform);
        //         copied.Data = (TodoPOC.TODOPOCItemData)data[i];
        //     }

        //     timer.Stop();
        //     Debug.Log($"Menual list bind took {timer.ElapsedMilliseconds}ms");
        // }
    }
}

