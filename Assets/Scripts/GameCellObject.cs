using UnityEngine;

namespace Assets.Scripts
{
    public class GameCellObject : MonoBehaviour
    {
        public float Speed = 50.0f;
        public Vector3 Origin = Vector3.zero;
        

        private void Start()
        {
        }

        private void Update()
        {
            transform.RotateAround(Origin, Vector3.up, Speed*Time.deltaTime);
        }
    }
}