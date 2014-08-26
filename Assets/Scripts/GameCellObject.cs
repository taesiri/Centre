using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameCellObject : MonoBehaviour
    {
        public float Speed = 50.0f;
        public Vector3 Origin = Vector3.zero;
        public TextMesh InnerTextMesh;
        private Transform _transform;
        public SourceObject Parent;
        public Transform GhostTransform;
        public ColorData ColorData;
        public bool IsCentre = false;
        public int CellValue = 0;

        private List<GameCellObject> _possibleDestinations = new List<GameCellObject>();

        public void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            HandleRotation();
            //DrawDebugInformation();
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
            RayCheck(new Ray(_transform.position, Origin - _transform.position), 1 << (Parent.RingId + 7));
        }

        public void CheckBackward()
        {
            RayCheck(new Ray(_transform.position, _transform.position - Origin), 1 << (Parent.RingId + 9));
        }

        public GameCellObject GetVicinity(int index, GameCellObject cell, int mask)
        {
            var vec = cell.transform.position - Origin;
            vec.y = 0;
            vec.Normalize();
            vec = new Vector3(-vec.z, 0, vec.x);

            if (index == 1)
                vec *= -1;

            return GetCellAlongRay(new Ray(cell.transform.position, vec), mask, 100f);
        }

        public GameCellObject GetCellAtFront()
        {
            return GetCellAlongRay(new Ray(_transform.position, Origin - _transform.position), 1 << (Parent.RingId + 7), Parent.GetRadius);
        }

        public GameCellObject GetCellAtBack()
        {
            return GetCellAlongRay(new Ray(_transform.position, _transform.position - Origin), 1 << (Parent.RingId + 9), Parent.GetRadius);
        }


        private void FindDestinations()
        {
            // TODO : Check visinities, forward and backward! :-)

            _possibleDestinations = new List<GameCellObject>();

            var back = GetCellAtBack();
            if (back)
            {
                _possibleDestinations.Add(back);

                var backVRight = GetVicinity(0, back, 1 << (Parent.RingId + 9));
                var backVLeft = GetVicinity(1, back, 1 << (Parent.RingId + 9));

                if (backVRight)
                    _possibleDestinations.Add(backVRight);
                if (backVLeft)
                    _possibleDestinations.Add(backVLeft);
            }


            var front = GetCellAtFront();
            if (front)
            {
                _possibleDestinations.Add(front);

                var frontVRight = GetVicinity(0, front, 1 << (Parent.RingId + 7));
                var frontVLeft = GetVicinity(1, front, 1 << (Parent.RingId + 7));

                if (frontVRight)
                    _possibleDestinations.Add(frontVRight);
                if (frontVLeft)
                    _possibleDestinations.Add(frontVLeft);
            }
        }


        public void HighlightDestinations()
        {
            FindDestinations();

            foreach (var destination in _possibleDestinations)
            {
                destination.GhostTransformVisibility(true);
            }
        }

        public void UnHighlightDestinations()
        {
            foreach (var destination in _possibleDestinations)
            {
                destination.GhostTransformVisibility(false);
            }
        }

        public void RayCheck(Ray ray, int mask)
        {
            if (!IsCentre)
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
        }

        public GameCellObject GetCellAlongRay(Ray ray, int mask, float raduis)
        {
            RaycastHit rayHit;
            Physics.Raycast(ray, out rayHit, raduis, mask);

            if (rayHit.collider)
            {
                var otherCell = rayHit.collider.GetComponent<GameCellObject>();
                if (otherCell)
                {
                    otherCell.renderer.material.color = Color.blue;
                    return otherCell;
                }
            }
            return null;
        }

        public void Promote()
        {
            SetValue(CellValue*2);
            if (IsCentre)
            {
                Centre.Instance.Colorize(CellValue);
            }
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
            InnerTextMesh.text = string.Format(" {0} ", newValue);
        }

        public bool IsFree()
        {
            return CellValue == 0;
        }

        public void GhostTransformVisibility(bool value)
        {
            GhostTransform.renderer.enabled = value;
        }
    }
}