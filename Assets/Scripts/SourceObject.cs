using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class SourceObject : MonoBehaviour
    {
        public GameObject GameCellPrefab;
        private float _radius = -1.0f;
        public int ObjectCount = 10;
        private int _counter;
        public bool Inverted = false;
        public float Speed = 10f;
        public float Angle = 90f;
        private float _waitForDesireFrame = 1f;
        public int RingId = -1;
        public List<GameCellObject> ListofCells;

        public float GetRadius
        {
            get
            {
                var pVec = new Vector2(transform.position.x, transform.position.z);
                _radius = pVec.magnitude;

                return _radius;
            }
        }

        public void Start()
        {
            renderer.enabled = false;
            StartCoroutine(DeployObject());
            _waitForDesireFrame = Angle/(ObjectCount*Speed);

            Centre.Instance.RegisterRing(this);
            ListofCells = new List<GameCellObject>(ObjectCount);
        }

        public int NumberOfFreeCells()
        {
            return ListofCells.Count(o => o.IsFree());
        }

        public void ReceiveId(int id)
        {
            RingId = id;
        }

        private IEnumerator DeployObject()
        {
            while (true)
            {
                yield return new WaitForSeconds(_waitForDesireFrame);
                if (RingId == -1)
                {
                    Debug.LogError("wrong id!");
                }

                var gCell = (GameObject) Instantiate(GameCellPrefab, transform.position, Quaternion.identity);
                gCell.layer = 7 + RingId;
                gCell.transform.parent = transform;

                var gco = gCell.GetComponent<GameCellObject>();
                gco.Speed = Inverted ? -Speed : Speed;
                gco.Parent = this;

                ListofCells.Add(gco);

                _counter++;

                if (_counter >= ObjectCount)
                {
                    break;
                }
            }
        }

        public void RespawnRandomValue()
        {
            var listofFrees = ListofCells.Where(o => o.IsFree()).ToList();
            var index = Centre.Instance.RndGenerator.Next(0, listofFrees.Count);
            var power = Centre.Instance.RndGenerator.Next(0, 2);

            listofFrees[index].SetValue((int) Math.Pow(2.0, power));
        }
    }
}