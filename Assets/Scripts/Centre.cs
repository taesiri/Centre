using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class Centre : MonoBehaviour
    {
        public GameObject GameCellPrefab;
        public GameObject SourcePrefab;
        public List<SourceObject> Sources;
        public static Centre Instance;
        public List<SourceObject> SourceList;
        
        public Random RndGenerator = new Random(DateTime.Now.Millisecond);

        public Centre()
        {
            Instance = this;
        }

        public void Start()
        {
            StartCoroutine(BroadCastRingsIds());
        }

        private IEnumerator BroadCastRingsIds()
        {
            yield return new WaitForSeconds(0.1f);
            SourceList.Sort((p1, p2) => p1.GetRadius.CompareTo(p2.GetRadius));
            for (int i = 0; i < SourceList.Count; i++)
            {
                SourceList[i].ReceiveId(i + 1);
            }
        }


        public void RegisterRing(SourceObject src)
        {
            SourceList.Add(src);
        }

        public void Update()
        {
            HandleInputs();
        }

        private void HandleInputs()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                Physics.Raycast(ray, out rayHit, 400f);
                if (rayHit.collider)
                {
                    var cellObject = rayHit.collider.GetComponent<GameCellObject>();
                    if (cellObject)
                    {
                        cellObject.CheckForward();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                GenerateNumber();
            }
        }


        public void GenerateNumber()
        {
            Debug.Log(SourceList[2].NumberOfFreeCells());
            if (SourceList[2].NumberOfFreeCells() != 0)
            {
                SourceList[2].RespawnRandomValue();
            }
        }
    }
}