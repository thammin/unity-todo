using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

namespace POC
{
    [CustomEditor(typeof(POCComponentRoot))]
    public class POCComponentRootEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var obj = (POCComponentRoot)serializedObject.targetObject;
            var oldPage = obj.EntryPage;
            var newPage = GetPageName(obj.EntryPage);

            if (oldPage != newPage)
            {
                obj.EntryPage = newPage;
                TrySetPrefab(newPage);
            }

            serializedObject.ApplyModifiedProperties();
        }

        public string GetPageName(string selectedPage)
        {
            string[] pages = POCRouter.Pages.Keys.ToArray();
            var selectedIndex = Array.IndexOf(pages, selectedPage);
            selectedIndex = EditorGUILayout.Popup("EntryPage", selectedIndex, pages);
            return pages[selectedIndex];
        }

        public void TrySetPrefab(string selectedPage)
        {
            string prefab = POCRouter.Pages[selectedPage];
            var root = Selection.activeObject as GameObject;

            var loading = Resources.LoadAsync(prefab);
            loading.completed += finish =>
            {
                var page = UnityEngine.Object.Instantiate(loading.asset) as GameObject;

                foreach (Transform child in root.transform)
                {
                    GameObject.DestroyImmediate(child.gameObject);
                }

                page.transform.SetParent(root.transform, false);
            };
        }
    }
}

