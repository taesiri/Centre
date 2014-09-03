using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private GameCellObject _lastHitCellObject;
        private int _initialValueCycles = 4;
        public GameObject GameBoard;
        public AnimationCurve ColorCurve;
        public LineDrawer LineDrawer;
        public bool IsMouseDown;
        public float SlowmotionTime = .75f;
        private float _mouseDownTime;
        private bool _forceUpdate;
        private bool _isGameReadyToPlay;
        public float Threshold = 15;
        public float GoalRotation = -1000f;
        public float GoalLayer = -1;
        public Random RndGenerator = new Random(DateTime.Now.Millisecond);

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
            _isGameReadyToPlay = true;
        }

        public void Update()
        {
            if (_isGameReadyToPlay)
                HandleInputs();

            GameBoard.renderer.material.SetColor("_SpecColor", new Color(ColorCurve.Evaluate(Time.time*0.1f), 1f, 0));
        }


        private void HandleInputsPropper()
        {
            if (Input.GetMouseButtonDown(0))
            {
                IsMouseDown = true;

                var cellObject = GetCellAtMousePosition();
                if (cellObject)
                {
                    if (!cellObject.IsCentre)
                    {
                        _lastHitCellObject = cellObject;
                        cellObject.GhostTransformVisibility(true);
                        GoalRotation = cellObject.CurrentDegree;
                        GoalLayer = cellObject.gameObject.layer;
                        _mouseDownTime = Time.time;
                        Time.timeScale = .35f;
                    }
                }
            }

            if (IsMouseDown)
            {
                if (_lastHitCellObject)
                {
                    var lineEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    lineEnd.y = 0f;
                    LineDrawer.UpdatePosition(0, _lastHitCellObject.transform.position);
                    LineDrawer.UpdatePosition(1, lineEnd);

                    GoalRotation = _lastHitCellObject.CurrentDegree;

                    if (Time.time - _mouseDownTime > SlowmotionTime)
                    {
                        _forceUpdate = true;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                var destinationCell = GetCellAtMousePosition();
                if (destinationCell)
                {
                    if (destinationCell != _lastHitCellObject)
                        if (destinationCell.AllowToDrop)
                        {
                            destinationCell.OnDraw(_lastHitCellObject);
                            _lastHitCellObject.OnLeave();
                        }
                }

                Reset();
            }

            if (_forceUpdate)
            {
                Reset();
            }
        }

        private GameCellObject GetCellAtMousePosition()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            Physics.Raycast(ray, out rayHit, 400f);
            if (rayHit.collider)
            {
                var cellObject = rayHit.collider.GetComponent<GameCellObject>();
                if (cellObject)
                {
                    return cellObject;
                }
            }

            return null;
        }

        private void Reset()
        {
            IsMouseDown = false;
            _forceUpdate = false;

            if (_lastHitCellObject)
            {
                _lastHitCellObject.GhostTransformVisibility(false);
                _lastHitCellObject = null;
                GoalRotation = -10000;
                GoalLayer = -1;
            }

            LineDrawer.Hide();
            Time.timeScale = 1f;
        }

        private void HandleInputs()
        {
            HandleInputsPropper();
        }

        public void GenerateNumber()
        {
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