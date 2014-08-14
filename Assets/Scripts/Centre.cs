﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private GameCellObject _lastHitCellObject;
        private int _initialValueCycles = 4;
        private int _rCounter;
        private int _ishit = -1;
        private bool _isReady;
        public GameObject GameBoard;
        public AnimationCurve ColorCurve;
        public LineDrawer LineDrawer;

        public Centre()
        {
            Instance = this;
        }

        public void AssignRingIds()
        {
            var sourcesArray = (SourceObject[]) FindObjectsOfType(typeof (SourceObject));
            SourceList = sourcesArray.ToList();
            SourceList.Sort((p1, p2) => p1.GetRadius.CompareTo(p2.GetRadius));
            for (int i = 0; i < SourceList.Count; i++)
            {
                SourceList[i].ReceiveId(i + 1);
            }

            StartCoroutine(InitialValues());
        }

        public void Start()
        {
            AssignRingIds();
        }

        private IEnumerator InitialValues()
        {
            while (_initialValueCycles > 0)
            {
                Time.timeScale = 2f;
                yield return new WaitForSeconds(3.2f);
                GenerateNumber();
                _initialValueCycles--;
            }
            Time.timeScale = 1f;
            _isReady = true;
        }

        public void Update()
        {
            if (_isReady)
                HandleInputs();

            GameBoard.renderer.material.SetColor("_SpecColor", new Color(ColorCurve.Evaluate(Time.time*0.1f), 1f, 0));
        }


        private bool _lMouseDown;

        private void HandleInputsPropper()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lMouseDown = true;


                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                Physics.Raycast(ray, out rayHit, 400f);
                if (rayHit.collider)
                {
                    var cellObject = rayHit.collider.GetComponent<GameCellObject>();
                    if (cellObject)
                    {
                        if (!cellObject.IsCentre)
                        {
                            _lastHitCellObject = cellObject;
                            cellObject.GhostTransformVisibility(true);
                            cellObject.HighlightDestinations();

                            var lineStartVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            lineStartVec.y = 0f;

                            LineDrawer.UpdatePosition(0, lineStartVec);
                            LineDrawer.UpdatePosition(1, lineStartVec);

                            Time.timeScale = .45f;
                        }
                    }
                }
            }

            if (_lMouseDown)
            {
                var lineEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lineEnd.y = 0f;

                if (_lastHitCellObject)
                    LineDrawer.UpdatePosition(0, _lastHitCellObject.transform.position);

                LineDrawer.UpdatePosition(1, lineEnd);


                //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //    RaycastHit rayHit;
                //    Physics.Raycast(ray, out rayHit, 400f);
                //    if (rayHit.collider)
                //    {
                //        var cellObject = rayHit.collider.GetComponent<GameCellObject>();
                //        if (cellObject)
                //        {
                //            if (!cellObject.IsCentre)
                //            {

                //                var lineStartVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //                lineStartVec.y = 10f;

                //                LineDrawer.UpdatePosition(0, lineStartVec);
                //                LineDrawer.UpdatePosition(1, lineStartVec);


                //            }
                //        }
                //    }
            }


            if (Input.GetMouseButtonUp(0))
            {
                _lMouseDown = false;
                if (_lastHitCellObject)
                {
                    _lastHitCellObject.GhostTransformVisibility(false);
                    _lastHitCellObject.UnHighlightDestinations();
                    _lastHitCellObject = null;
                }

                LineDrawer.Hide();


                Time.timeScale = 1f;
            }
        }

        public void RayTrace()
        {
        }

        private void HandleInputs()
        {
            HandleInputsPropper();
            return;


            if (Input.GetMouseButtonDown(0))
            {
                _ishit = 0;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _ishit = 1;
            }

            //Oh Shit!
            if (_ishit != -1)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                var lineStartVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lineStartVec.y = 10f;

                LineDrawer.UpdatePosition(0, lineStartVec);
                LineDrawer.UpdatePosition(1, lineStartVec);


                RaycastHit rayHit;
                Physics.Raycast(ray, out rayHit, 400f);
                if (rayHit.collider)
                {
                    var cellObject = rayHit.collider.GetComponent<GameCellObject>();
                    if (cellObject)
                    {
                        if (!cellObject.IsCentre)
                        {
                            _lastHitCellObject = cellObject;
                            cellObject.GhostTransformVisibility(true);
                            Time.timeScale = .5f;
                        }
                        else
                        {
                            _lastHitCellObject = null;
                            _ishit = -1;
                        }
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

                    _lastHitCellObject.GhostTransformVisibility(false);
                }

                Time.timeScale = 1;
                _lastHitCellObject = null;
                _ishit = -1;
                LineDrawer.Hide();
            }
        }

        public void GenerateNumber()
        {
            SourceList[2 - _rCounter%3].RespawnRandomValue(3 - (_rCounter%3));

            _rCounter++;
        }


        public void Colorize(int value)
        {
            if (GameBoard)
            {
                //  GameBoard.renderer.material.SetColor("_SpecColor", new Color(value / 255f, value / 255f, 0.2f));
            }
        }
    }
}