using UnityEngine;

namespace Assets.Scripts
{
    public class Trailer : MonoBehaviour
    {
        public Vector3 Origin = Vector3.zero;
        public float Speed = 50.0f;
        private Transform _transform;

        public void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.RotateAround(Origin, Vector3.up, Speed*Time.deltaTime);
        }
    }
}