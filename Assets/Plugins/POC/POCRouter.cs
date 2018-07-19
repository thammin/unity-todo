using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Linq;

namespace POC
{
    public class POCRouter
    {
        public class Route
        {
            public string Name;
            public Dictionary<string, object> Parameters;
            public Route(string name, Dictionary<string, object> parameters = null)
            {
                Name = name;
                Parameters = parameters;
            }
        }

        public static Dictionary<string, string> Scenes = new Dictionary<string, string>()
        {
            { "Battle", "TodoPOC/TodoPOC" }
        };

        public static Dictionary<string, string> Pages = new Dictionary<string, string>()
        {
            { "Home", "Home" },
            { "Gacha", "Gacha" },
            { "Evolution", "Evolution" }
        };

        public static Route Current;

        public static Task<bool> To(string name)
        {
            return To(new Route(name));
        }

        public static async Task<bool> To(Route route)
        {
            // TODO guard
            var name = route.Name;
            var parameters = route.Parameters;

            TaskCompletionSource<bool> promise = new TaskCompletionSource<bool>();

            if (Scenes.ContainsKey(name))
            {
                Debug.Log($"Move to scene: {name}");
                var loading = SceneManager.LoadSceneAsync(Scenes[name]);
                loading.completed += finish => promise.SetResult(true);
            }
            else if (Pages.ContainsKey(name))
            {
                Debug.Log($"Move to page: {name}");
                var root = POCComponentRoot.Instance;
                // TODO 全部
                var results = await Task.WhenAll(root.Children.Select(child => CheckLeaveGuard(child, route, Current)).ToArray());
                if (results.Contains(false)) return false;

                var loading = Resources.LoadAsync(Pages[name]);
                loading.completed += async (finish) =>
                {
                    var page = UnityEngine.Object.Instantiate(loading.asset) as GameObject;
                    var pageCom = page.GetComponent<POCComponent>();

                    // TODO should check with static function
                    var entering = await CheckEnterGuard(pageCom, route, Current);
                    if (entering)
                    {
                        Debug.Log(root.Children.Count);
                        POCComponentRoot.DestroyChildren(root);
                        root.Children.Clear();
                        Debug.Log(root.Children.Count);

                        page.transform.SetParent(root.transform, false);
                        POCComponentRoot.InitializeChildren(root.transform, root);
                        promise.SetResult(true);
                    }
                    else
                    {
                        GameObject.Destroy(page);
                        promise.SetResult(false);
                    }
                };
            }

            return await promise.Task;
        }

        static Task<bool> CheckEnterGuard(POCComponent child, Route to, Route from)
        {
            var promise = new TaskCompletionSource<bool>();
            child.BeforeRouteEnter(to, from, result => promise.SetResult(result));
            return promise.Task;
        }

        static Task<bool> CheckLeaveGuard(POCComponent child, Route to, Route from)
        {
            var promise = new TaskCompletionSource<bool>();
            child.BeforeRouteLeave(to, from, result => promise.SetResult(result));
            return promise.Task;
        }
    }
}


