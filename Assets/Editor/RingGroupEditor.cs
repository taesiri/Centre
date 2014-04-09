using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof (RingGroupObject))]
    public class RingGroupEditor : UnityEditor.Editor
    {
        [SerializeField] private Vector3 _cellColiderSize = Vector3.one;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Label("Custom Editor");

            GUILayout.BeginHorizontal();
            _cellColiderSize = EditorGUILayout.Vector3Field("Cell Coliders", _cellColiderSize);
            if (GUILayout.Button("Apply Changes"))
            {
                ChangeCellsColider();
            }
            GUILayout.EndHorizontal();
        }

        private void ChangeCellsColider()
        {
            var currentObject = (RingGroupObject) target;
            foreach (var cellObject in currentObject.CellChilds)
            {
                var bcolider = cellObject.GetComponent<BoxCollider>();

                bcolider.size = _cellColiderSize;
            }
        }
    }
}