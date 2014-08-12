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
                return; // CENTER
            }

            RayCheck(new Ray(_transform.position, Origin - _transform.position), 1 << (Parent.RingId + 6));
        }

        public void CheckBackward()
        {
            if (Parent.RingId == 1)
            {
                return; // CENTER
            }

            RayCheck(new Ray(_transform.position, _transform.position - Origin), 1 << (Parent.RingId + 8));
        }

        public void RayCheck(Ray ray, int mask)
        {
            RaycastHit rayHit;
            Physics.Raycast(ray, out rayHit, Parent.GetRadius, mask);
            if (rayHit.collider)
            {
                var otherCell = rayHit.collider.GetComponent<GameCellObject>();
                if (otherCell)
                {
                    if (otherCell.CellValue == 0)
                    {
                        otherCell.SetValue(CellValue);
                        ClearCell();
                        Centre.Instance.GenerateNumber();
                    }
                    else if (otherCell.CellValue == CellValue)
                    {
                        otherCell.Promote();
                        ClearCell();
                        Centre.Instance.GenerateNumber();
                    }
                }
            }
        }

        public void Promote()
        {
            SetValue(CellValue*2);
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