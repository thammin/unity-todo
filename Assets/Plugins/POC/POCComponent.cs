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
                    //_isShallowNode = this as POCList || this as POCShow || this as POCBind2;
                    _isShallowNode = GetComponent<POCList>() != null && GetComponent<POCShow>() != null && GetComponent<POCBind2>() != null;
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

            Parent.Children.Add(this);

            Initialize();
        }

        public void MountedBase()
        {
            Mounted();
        }

        public void DestroyBase()
        {
            Parent = null;
            Children.Clear();
            GameObject.Destroy(gameObject);
        }

        #region virtual
        /// <summary>
        /// 
        /// </summary>
        public virtual void BeforeRouteEnter(POCRouter.Route To, POCRouter.Route From, System.Action<bool> next)
        {
            next(true);
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void BeforeRouteUpdate(POCRouter.Route To, POCRouter.Route From, System.Action<bool> next)
        {
            next(true);
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void BeforeRouteLeave(POCRouter.Route To, POCRouter.Route From, System.Action<bool> next)
        {
            next(true);
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void Initialize() { }
        /// <summary>
        /// 
        /// </summary>
        public virtual void Mounted() { }
        #endregion
    }


}

