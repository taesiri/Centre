using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof (Centre))]
    public class CentreOrbitEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Centre Editor");

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Source"))
            {
                CreateSource();
            }
            if (GUILayout.Button("Create Sink"))
            {
            }
            if (GUILayout.Button("Clear All"))
            {
                ClearAll();
            }

            GUILayout.EndHorizontal();

            base.OnInspectorGUI();
            if (GUI.changed)
            {
                HandleChanges();
            }
        }

        private void HandleChanges()
        {
        }

        private void ClearAll()
        {
            var ctr = (Centre) target;
            var killList = new SourceObject[ctr.Sources.Count];

            for (int i = 0; i < ctr.Sources.Count; i++)
            {
                killList[i] = ctr.Sources[i];
            }

            ctr.Sources.Clear();

            for (int i = 0; i < killList.Length; i++)
            {
                DestroyImmediate(killList[i].gameObject);
            }
        }


        private void CreateSource()
        {
            var ctr = (Centre) target;

            var newSourceObj = (GameObject) Instantiate(ctr.SourcePrefab, new Vector3(2, 0, 2), Quaternion.identity);
            var newSource = newSourceObj.GetComponent<SourceObject>();
            newSource.ParentCentre = ctr;
            newSource.GameCellPrefab = ctr.GameCellPrefab;
            ctr.Sources.Add(newSource);
            newSourceObj.transform.parent = ctr.transform;
        }
    }
}