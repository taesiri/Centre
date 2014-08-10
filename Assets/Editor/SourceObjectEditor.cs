using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof (SourceObject))]
    public class SourceObjectEditor : UnityEditor.Editor
    {
        public void OnSceneGUI()
        {
            var sourceObject = (SourceObject) target;
            Handles.color = Color.red;
            Handles.CircleCap(0, Vector3.zero, Quaternion.Euler(90, 0, 0), sourceObject.GetRadius);
        }
    }
}