using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POC
{
    public class POCComponentRoot : POCComponent
    {
        void Awake()
        {
            Debug.Log("__Core Awake__");

            var timer = System.Diagnostics.Stopwatch.StartNew();

            InitializeChildren(transform, this);

            timer.Stop();
            Debug.Log($"Initializaiton took {timer.ElapsedMilliseconds}ms");

            //DebugTree(this);
        }

        IEnumerator Start()
        {
            Debug.Log("__Core Start__");

            yield return null;

            var timer = System.Diagnostics.Stopwatch.StartNew();

            MountedChildren(this);

            timer.Stop();
            Debug.Log($"Mounted took {timer.ElapsedMilliseconds}ms");
        }

        /// <summary>
        /// Call child.Initialize() recursively by depth first
        /// </summary>
        /// <param name="node"></param>
        void InitializeChildren(Transform node, POCComponent parent)
        {
            var transform = node.transform;

            foreach (Transform child in transform)
            {
                var c = child.GetComponent<POCComponent>();
                c?.InitializeBase(parent);
                InitializeChildren(child, c ?? parent);
            }
        }

        void MountedChildren(POCComponent node)
        {
            foreach (var child in node.Children)
            {
                MountedChildren(child);
                child.MountedBase();
            }
        }

        void DebugTree(POCComponent node)
        {
            Debug.Log($"Parent: {node.Parent}, Node: {node}, Child: {node.Children}");

            foreach (var child in node.Children)
            {
                DebugTree(child);
            }
        }
    }
}

