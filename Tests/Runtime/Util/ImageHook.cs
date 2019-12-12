using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UnityEngine.UI.Tests
{
    // Hook into the graphic callback so we can do our check.
    public class ImageHook : Image
    {
        public bool isGeometryUpdated;
        public bool isLayoutRebuild;
        public bool isMaterialRebuilt;
        public bool isCacheUsed;

        public void ResetTest()
        {
            isGeometryUpdated = false;
            isLayoutRebuild = false;
            isMaterialRebuilt = false;
            isCacheUsed = false;
        }

        public override void SetLayoutDirty()
        {
            base.SetLayoutDirty();
            isLayoutRebuild = true;
        }

        public override void SetMaterialDirty()
        {
            base.SetMaterialDirty();
            isMaterialRebuilt = true;
        }

        protected override void UpdateGeometry()
        {
            base.UpdateGeometry();
            isGeometryUpdated = true;
            FieldInfo fieldInfo = typeof(Image).GetField("m_UseCache", BindingFlags.Instance | BindingFlags.NonPublic);
            isCacheUsed = (bool)fieldInfo.GetValue(gameObject.GetComponent<ImageHook>());
        }
    }
}
