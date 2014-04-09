using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof (CentreObject))]
    public class CentreEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Label("Custom Editor");

            if (GUILayout.Button("Generate Game Board"))
            {
                GenerateGrid();
            }
            if (GUILayout.Button("Clear Childs"))
            {
                ClearChilds();
            }
            if (GUI.changed)
            {
                HandleChanges();
            }
        }

        private void HandleChanges()
        {
        }

        private void ClearChilds()
        {
            var currentObject = (CentreObject) target;
            var killList = new Transform[currentObject.transform.childCount];
            for (int i = 0; i < killList.Length; i++)
            {
                killList[i] = currentObject.transform.GetChild(i);
            }

            for (int i = 0; i < killList.Length; i++)
            {
                DestroyImmediate(killList[i].gameObject);
            }
        }

        private void GenerateGrid()
        {
            var currentObject = (CentreObject) target;

            var scoreManger = currentObject.GetComponent<GameScore>();

            RingGroupObject prevGroup = null;

            var angle = 360.0f/currentObject.NumberOfSubnNodesInRingGroup;
            currentObject.GetComponent<GameManager>().RotationUnit = angle;

            for (int j = 1; j < currentObject.Radius; j += currentObject.RadiusIncerement)
            {
                var newGroupObject = new GameObject(string.Format("Gorup {0}", j));
                var groupScript = newGroupObject.AddComponent<RingGroupObject>();
                newGroupObject.transform.parent = currentObject.transform;

                groupScript.CellChilds = new CellObject[currentObject.NumberOfSubnNodesInRingGroup];

                if (prevGroup != null)
                {
                    groupScript.PrevGroup = prevGroup;
                    prevGroup.NextGroup = groupScript;
                }

                CellObject prevCell = null;
                CellObject first = null;

                for (int i = 0; i < currentObject.NumberOfSubnNodesInRingGroup; i++)
                {
                    var objects = new GameObject[currentObject.NumberOfSubnNodesInRingGroup];
                    var pos = currentObject.CentreTransfrom.position + Vector3.right*(j*currentObject.DiagonalOffset) + Vector3.right*currentObject.CentreOffset;

                    objects[i] = (GameObject) Instantiate(currentObject.CellElementPrefab,
                        pos, Quaternion.identity);


                    objects[i].name = string.Format("CellNode {0}:{1}", j, i);
                    objects[i].transform.RotateAround(currentObject.CentreTransfrom.position, Vector3.up, i*angle);
                    objects[i].transform.parent = newGroupObject.transform;


                    var cs = objects[i].GetComponent<CellObject>();
                    if (prevCell != null)
                    {
                        cs.Left = prevCell;
                        prevCell.Right = cs;
                    }
                    else
                    {
                        first = cs;
                    }

                    prevCell = cs;

                    groupScript.CellChilds[i] = cs;
                    cs.Index = i;

                    cs.ScoreManager = scoreManger;
                }

                if (first != null)
                {
                    first.Left = prevCell;
                    prevCell.Right = first;
                }

                newGroupObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                prevGroup = groupScript;
            }
        }
    }
}