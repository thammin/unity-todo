using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace POC
{
    public abstract class POCComponent : MonoBehaviour
    {
        [HideInInspector]
        public POCComponent Parent;

        [HideInInspector]
        public List<POCComponent> Children;

        private bool _isChecked = false;
        private bool _isShallowNode = false;

        public bool IsShallowNode
        {
            get
            {
                if (_isChecked)
                {
                    return _isShallowNode;
                }
                else
                {
                    _isShallowNode = GetComponent<POCList>() != null && GetComponent<POCShow>() != null;
                    _isChecked = true;
                    return _isShallowNode;
                }
            }
        }

        public void InitializeBase(POCComponent parent)
        {
            if (!parent.IsShallowNode)
            {
                Parent = parent;
            }
            else
            {
                Parent = parent.Parent;
            }

            if (!IsShallowNode)
            {
                Parent.Children.Add(this);
            }

            Initialize();
        }

        public void MountedBase()
        {
            Mounted();
        }

        public virtual void Initialize()
        {

        }

        public virtual void Mounted()
        {

        }
    }
}

