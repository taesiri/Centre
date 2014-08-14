using UnityEngine;

namespace Assets.Scripts
{
    public class LineDrawer : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
        }

        public void Hide()
        {
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
        }

        public void DisableRenderer()
        {
            _lineRenderer.renderer.enabled = false;
        }


        public void UpdatePosition(int index, Vector3 vec)
        {
            _lineRenderer.SetPosition(index, vec);
        }

    }
}