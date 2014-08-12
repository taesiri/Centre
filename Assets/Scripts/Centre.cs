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
        private int _initialValueCycles = 4;

        private int _rCounter = 0;
        private int _ishit = -1;
        private GameCellObject _lastHitCellObject;

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

            StartCoroutine(InitialValues());
        }

        private IEnumerator InitialValues()
        {
            while (_initialValueCycles > 0)
            {
                Time.timeScale = 2f;
                yield return new WaitForSeconds(3f);
                GenerateNumber();
                _initialValueCycles--;
            }
            Time.timeScale = 1f;
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
                _ishit = 0;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _ishit = 1;
            }


            if (_ishit != -1)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                Physics.Raycast(ray, out rayHit, 400f);
                if (rayHit.collider)
                {
                    var cellObject = rayHit.collider.GetComponent<GameCellObject>();
                    if (cellObject)
                    {
                        _lastHitCellObject = cellObject;
                        Time.timeScale = .5f;
                    }
                }
            }


            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                if (_lastHitCellObject != null)
                {
                    if (_ishit == 0)
                    {
                        _lastHitCellObject.CheckForward();
                    }
                    else if (_ishit == 1)
                    {
                        _lastHitCellObject.CheckBackward();
                    }
                }

                Time.timeScale = 1;
                _lastHitCellObject = null;


                _ishit = -1;
            }
        }

        public void GenerateNumber()
        {
            SourceList[2 - _rCounter%3].RespawnRandomValue(3 - (_rCounter%3));

            _rCounter++;
        }
    }
}