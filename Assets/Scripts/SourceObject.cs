using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SourceObject : MonoBehaviour
    {
        public GameObject GameCellPrefab;
        private float _radius = -1.0f;
        public int ObjectCount = 10;
        private int _counter = 0;
        public bool Inverted = false;
        public float Speed = 10f;
        public float Angle = 90f;
        private float _waitForDesireFrame = 1f;

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
        }

        public void Update()
        {
        }

        private IEnumerator DeployObject()
        {
            while (true)
            {
               
                yield return new WaitForSeconds(_waitForDesireFrame);
                
                var gCell = (GameObject)Instantiate(GameCellPrefab, transform.position, Quaternion.identity);
                gCell.transform.parent = transform;
                var gco = gCell.GetComponent<GameCellObject>();
                gco.Speed = Inverted ? -Speed : Speed;


                _counter++;

                if (_counter >= ObjectCount)
                {
                    break;
                }
            }
        }
    }
}