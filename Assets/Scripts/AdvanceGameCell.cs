using UnityEngine;

namespace Assets.Scripts
{
    public class AdvanceGameCell : MonoBehaviour
    {
        public float Speed = 50.0f;
        public Vector3 Origin = Vector3.zero;
        private Transform _transform;
        public int CellValue = 0;
        public AdvanceSourceObject Parent;
        public bool IsCentre = false;

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
            _transform.RotateAround(Origin, Vector3.up, Speed * Time.deltaTime);
        }

        public void DrawDebugInformation()
        {
            Debug.DrawLine(_transform.position, Origin, Color.cyan);
        }

        public void CheckForward()
        {
            Debug.DrawLine(_transform.position, Origin, Color.red, 2f, true);
            RayCheck(new Ray(_transform.position, Origin - _transform.position), 1 << (Parent.RingId + 7));
        }

        public void CheckBackward()
        {
            RayCheck(new Ray(_transform.position, _transform.position - Origin), 1 << (Parent.RingId + 9));
        }

        public void RayCheck(Ray ray, int mask)
        {
            if (!IsCentre)
            {
                RaycastHit rayHit;
                Physics.Raycast(ray, out rayHit, Parent.GetRadius, mask);


                if (rayHit.collider)
                {
                    var otherCell = rayHit.collider.GetComponent<AdvanceGameCell>();
                    if (otherCell)
                    {
                        if (otherCell.CellValue == 0)
                        {
                            otherCell.SetValue(CellValue);
                            ClearCell();
                            AdvanceCentre.Instance.GenerateNumber();
                        }
                        else if (otherCell.CellValue == CellValue)
                        {
                            otherCell.Promote();
                            ClearCell();
                            AdvanceCentre.Instance.GenerateNumber();
                        }
                    }
                }
            }
        }

        public void Promote()
        {
            SetValue(CellValue * 2);
            if (IsCentre)
            {
                AdvanceCentre.Instance.Colorize(CellValue);
            }
        }

        public void ClearCell()
        {
            CellValue = 0;
        }

        public void SetValue(int newValue)
        {
            if (newValue == 0)
            {
                ClearCell();
                return;
            }

            CellValue = newValue;
        }

        public bool IsFree()
        {
            return CellValue == 0;
        }
    }
}