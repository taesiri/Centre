using UnityEngine;

namespace Assets.Scripts
{
    public class GameCellObject : MonoBehaviour
    {
        public float Speed = 50.0f;
        public Vector3 Origin = Vector3.zero;
        public TextMesh InnerTextMesh;
        private Transform _transform;
        public int CellValue = 0;

        public SourceObject Parent;

        public void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            HandleRotation();
            DrawDebugInformation();
        }

        public void HandleRotation()
        {
            _transform.RotateAround(Origin, Vector3.up, Speed*Time.deltaTime);
        }

        public void DrawDebugInformation()
        {
            Debug.DrawLine(_transform.position, Origin, Color.cyan);
        }

        public void CheckForward()
        {
            Debug.DrawLine(_transform.position, Origin, Color.red, 2f, true);

            if (Parent.RingId == 1)
            {
                return;
            }
            RaycastHit rayHit;
            Physics.Raycast(_transform.position, Origin - _transform.position, out rayHit, Parent.GetRadius, 1 << (Parent.RingId + 6));
            if (rayHit.collider)
            {
                Debug.Log(rayHit.collider.gameObject.name);
                rayHit.collider.renderer.material.color = Color.red;
            }
        }

        public void CheckBackward()
        {
        }

        public void Promote()
        {
            CellValue *= 2;
        }

        public void ClearCell()
        {
            CellValue = 0;
            InnerTextMesh.renderer.enabled = false;
        }

        public void SetValue(int newValue)
        {
            if (newValue == 0)
            {
                ClearCell();
                return;
            }

            CellValue = newValue;
            InnerTextMesh.renderer.enabled = true;
            InnerTextMesh.text = newValue.ToString();
        }

        public bool IsFree()
        {
            return CellValue == 0;
        }
    }
}