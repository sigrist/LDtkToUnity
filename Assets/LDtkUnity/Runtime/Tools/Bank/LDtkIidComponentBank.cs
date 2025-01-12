﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace LDtkUnity
{
    /// <summary>
    /// Unlike <see cref="LDtkIidBank"/>, this contains components in the scene, and used for referencing in runtime.<br/>
    /// This is also fully accessible during the import process.
    /// Note: If there are duplicated instances of a GameObject with an iid component, it may result in inconsistent references.
    /// </summary>
    public static class LDtkIidComponentBank
    {
        private static readonly Dictionary<string, LDtkIid> IidObjects = new Dictionary<string, LDtkIid>();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        internal static void Release()
        {
            IidObjects.Clear();
        }
        
        internal static void Add(LDtkIid iid)
        {
            if (iid == null)
            {
                return;
            }
            
            if (!IsIidValid(iid.Iid))
            {
                return;
            }
            
            if (!IidObjects.ContainsKey(iid.Iid))
            {
                IidObjects.Add(iid.Iid, iid);
            }
        }
        
        internal static void Remove(LDtkIid iid)
        {
            if (iid == null)
            {
                return;
            }
            
            if (!IsIidValid(iid.Iid))
            {
                return;
            }
            
            if (IidObjects.ContainsKey(iid.Iid))
            {
                IidObjects.Remove(iid.Iid);
            }
        }
        
        /// <summary>
        /// Gets a uid GameObject. The objects are only available after their OnEnable. Otherwise, try using <see cref="FindObjectOfIid"/>. <br/>
        /// If the component was not found, then make sure the referenced component is active and accessed after it's OnEnable.
        /// </summary>
        /// <param name="iid">
        /// The iid value.
        /// </param>
        /// <returns>
        /// The iid component that matches the iid.
        /// </returns>
        [PublicAPI]
        public static LDtkIid GetByIid(string iid)
        {
            if (!IsIidValid(iid))
            {
                return null;
            }
            

            return IidObjects.ContainsKey(iid) ? IidObjects[iid] : null;
        }

        private static bool IsIidValid(string iid)
        {
            if (string.IsNullOrEmpty(iid))
            {
                return false;
            }
            
            const int iidFormatLength = 36; 
            if (iid.Length != iidFormatLength)
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Finds an iid component.<br/>
        /// </summary>
        /// <param name="iid">
        /// The iid value.
        /// </param>
        /// <returns>
        /// The iid component that matches the iid.
        /// </returns>
        /// <remarks>
        /// This function uses Object.FindObjectsOfType if a cached component is not found, so it is slow and not recommended to use every frame. <br/>
        /// In most cases you can use <see cref="GetByIid"/> instead.
        /// </remarks>
        [PublicAPI]
        public static LDtkIid FindObjectOfIid(string iid)
        {
            if (!IsIidValid(iid))
            {
                return null;
            }
            
            LDtkIid iidComponent = GetByIid(iid);
            if (iidComponent != null)
            {
                return iidComponent;
            }
            
            LDtkIid[] iidComponents = Object.FindObjectsOfType<LDtkIid>
#if UNITY_2020_1_OR_NEWER
                (true);
#else
                ();
#endif
            
            iidComponent = iidComponents.FirstOrDefault(p => p.Iid == iid);
            Add(iidComponent);
            
            return iidComponent;
        }
    }
}