﻿using System;
using System.Collections.Generic;
using UnityEngine;


namespace VRM
{
    [DisallowMultipleComponent]
    public class VRMBlendShapeProxy : MonoBehaviour, IVRMComponent
    {
        [SerializeField]
        public BlendShapeAvatar BlendShapeAvatar;

        public void OnImported(VRMImporterContext context)
        {
            throw new NotImplementedException();
        }

        BlendShapeMerger m_merger;

        BlendShapeMerger Merger
        {
            get
            {
                if (m_merger == null)
                {
                    m_merger = new BlendShapeMerger(BlendShapeAvatar.Clips, transform);
                }
                return m_merger;
            }
        }

        /// <summary>
        /// BlendShapeAvatar.Clips の変更を反映したいときに BlendShapeMerger を削除して状態をクリアします。
        /// </summary>
        public void Clear()
        {
            m_merger = null;
        }

        private void OnDestroy()
        {
            if (m_merger != null)
            {
                m_merger.RestoreMaterialInitialValues(BlendShapeAvatar.Clips);
            }
        }

        /// <summary>
        /// Immediately SetValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void ImmediatelySetValue(BlendShapeKey key, float value)
        {
            var merger = Merger;
            if (merger != null)
            {
                merger.ImmediatelySetValue(key, value);
            }
        }

        /// <summary>
        /// AccumulateValue. After, Should call Apply
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AccumulateValue(BlendShapeKey key, float value)
        {
            var merger = Merger;
            if (merger != null)
            {
                merger.AccumulateValue(key, value);
            }
        }

        /// <summary>
        /// Get a blendShape value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public float GetValue(BlendShapeKey key)
        {
            var merger = Merger;
            if (merger == null)
            {
                return 0;
            }
            return merger.GetValue(key);
        }

        public IEnumerable<KeyValuePair<BlendShapeKey, float>> GetValues()
        {
            var merger = Merger;
            if (merger != null && BlendShapeAvatar != null)
            {
                foreach (var clip in BlendShapeAvatar.Clips)
                {
                    var key = BlendShapeKey.CreateFromClip(clip);
                    yield return new KeyValuePair<BlendShapeKey, float>(key, merger.GetValue(key));
                }
            }
        }

        /// <summary>
        /// Set blendShape values immediate.
        /// </summary>
        /// <param name="values"></param>
        public void SetValues(IEnumerable<KeyValuePair<BlendShapeKey, float>> values)
        {
            var merger = Merger;
            if (merger != null)
            {
                merger.SetValues(values);
            }
        }

        /// <summary>
        /// Apply blendShape values that use SetValue apply=false
        /// </summary>
        public void Apply()
        {
            var merger = Merger;
            if (merger != null)
            {
                merger.Apply();
            }
        }
    }

    public static class VRMBlendShapeProxyExtensions
    {
        [Obsolete("Use BlendShapeKey.CreateFromPreset")]
        public static float GetValue(this VRMBlendShapeProxy proxy, BlendShapePreset key)
        {
            return proxy.GetValue(BlendShapeKey.CreateFromPreset(key));
        }

        [Obsolete("Use BlendShapeKey.CreateUnknown")]
        public static float GetValue(this VRMBlendShapeProxy proxy, String key)
        {
            return proxy.GetValue(BlendShapeKey.CreateUnknown(key));
        }

        [Obsolete("Use ImmediatelySetValue")]
        public static void SetValue(this VRMBlendShapeProxy proxy, BlendShapePreset key, float value)
        {
            proxy.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(key), value);
        }

        [Obsolete("Use BlendShapeKey.CreateFromPreset")]
        public static void ImmediatelySetValue(this VRMBlendShapeProxy proxy, BlendShapePreset key, float value)
        {
            proxy.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(key), value);
        }

        [Obsolete("Use BlendShapeKey.CreateFromPreset")]
        public static void AccumulateValue(this VRMBlendShapeProxy proxy, BlendShapePreset key, float value)
        {
            proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(key), value);
        }

        [Obsolete("Use ImmediatelySetValue")]
        public static void SetValue(this VRMBlendShapeProxy proxy, String key, float value)
        {
            proxy.ImmediatelySetValue(BlendShapeKey.CreateUnknown(key), value);
        }

        [Obsolete("Use BlendShapeKey.CreateUnknown")]
        public static void ImmediatelySetValue(this VRMBlendShapeProxy proxy, String key, float value)
        {
            proxy.ImmediatelySetValue(BlendShapeKey.CreateUnknown(key), value);
        }

        [Obsolete("Use BlendShapeKey.CreateUnknown")]
        public static void AccumulateValue(this VRMBlendShapeProxy proxy, String key, float value)
        {
            proxy.AccumulateValue(BlendShapeKey.CreateUnknown(key), value);
        }

        [Obsolete("Use ImmediatelySetValue")]
        public static void SetValue(this VRMBlendShapeProxy proxy, BlendShapeKey key, float value)
        {
            proxy.ImmediatelySetValue(key, value);
        }

        [Obsolete("Use ImmediatelySetValue or AccumulateValue")]
        public static void SetValue(this VRMBlendShapeProxy proxy, BlendShapePreset key, float value, bool apply)
        {
            if (apply)
            {
                proxy.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(key), value);
            }
            else
            {
                proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(key), value);
            }
        }

        [Obsolete("Use ImmediatelySetValue or AccumulateValue")]
        public static void SetValue(this VRMBlendShapeProxy proxy, String key, float value, bool apply)
        {
            if (apply)
            {
                proxy.ImmediatelySetValue(BlendShapeKey.CreateUnknown(key), value);
            }
            else
            {
                proxy.AccumulateValue(BlendShapeKey.CreateUnknown(key), value);
            }
        }

        [Obsolete("Use ImmediatelySetValue or AccumulateValue")]
        public static void SetValue(this VRMBlendShapeProxy proxy, BlendShapeKey key, float value, bool apply)
        {
            if (apply)
            {
                proxy.ImmediatelySetValue(key, value);
            }
            else
            {
                proxy.AccumulateValue(key, value);
            }
        }
    }
}
