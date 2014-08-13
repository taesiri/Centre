using UnityEngine;

namespace Assets.Scripts
{
    public class Trailer : MonoBehaviour
    {
        public TrailRenderer TRenderer;

        public void Start()
        {
            TRenderer = GetComponent<TrailRenderer>();
        }

        private void Update()
        {
        }
    }
}