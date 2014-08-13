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

        public GameObject TrailerPrefab;

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


            var trailerObject = (GameObject) Instantiate(TrailerPrefab, transform.position, Quaternion.identity);
            var trailer = trailerObject.GetComponent<GameCellObject>();
            trailer.Speed = Inverted ? -8*Speed : 8*Speed;


            _waitForDesireFrame = Angle/(ObjectCount*Speed);
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
                gCell.layer = 8 + RingId;
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


        private int _rCounter = -1;

        public int GetRandomValue
        {
            get { return _rCounter%2 + 1; }
        }

        public void RespawnRandomValue(int amount)
        {
            if (amount <= 0)
                Debug.LogError("Wrong Value! Vairable \'amount\' should be 1 at least!");

            var listofFrees = ListofCells.Where(o => o.IsFree()).ToList();
            var n = listofFrees.Count;

            _rCounter++;

            if (n == 0)
            {
                return;
            }
            if (n < amount)
            {
                for (int i = 0; i < n; i++)
                {
                    listofFrees[i].SetValue(GetRandomValue);
                }
                return;
            }


            for (int i = 0; i < amount; i++)
            {
                // Possible duplication!
                var index = Centre.Instance.RndGenerator.Next(0, n);
                listofFrees[index].SetValue(GetRandomValue);
            }
        }
    }
}