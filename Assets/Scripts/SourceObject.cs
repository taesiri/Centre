using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SourceObject : MonoBehaviour
    {
        public Centre ParentCentre;
        public GameObject GameCellPrefab;
        private float _radius = -1.0f;

        public int ObjectCount = 10;
        private int _counter = 1;
        public bool Inverted = false;

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
        }

        public void Update()
        {
        }

        private IEnumerator DeployObject()
        {
            while (true)
            {
                var gCell = (GameObject) Instantiate(GameCellPrefab, transform.position, Quaternion.identity);
                gCell.transform.parent = transform;
                var gco = gCell.GetComponent<GameCellObject>();
                gco.Speed *= Inverted ? -1 : 1;

                yield return new WaitForSeconds(1f);
                _counter++;

                if (_counter >= ObjectCount)
                {
                    break;
                }
            }
        }
    }
}