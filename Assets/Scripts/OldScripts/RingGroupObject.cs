using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class RingGroupObject : MonoBehaviour
    {
        public RingGroupObject NextGroup;
        public RingGroupObject PrevGroup;

        public CellObject CentreCell;

        public CellObject[] CellChilds;

        public float RotationSpeed = 200f;
        private bool _isRotating;
        private float _remainingRotation;
        private float _goalRotation;
        private int _signOfRotation = 1;
        private CellObject _rotatingCell;
        private Action<CellObject> _finishAction;

        public int GroupIndex;

        public enum MovementType
        {
            MoveOut,
            MoveIn
        }

        public void Rotate(CellObject cell, float degree)
        {
            if (!_isRotating)
            {
                if (degree < 0)
                {
                    _signOfRotation = -1;
                    degree *= -1;
                    _finishAction = MoveLeft;
                }
                else
                {
                    _signOfRotation = 1;
                    _finishAction = MoveRight;
                }

                _remainingRotation = degree;
                _goalRotation = _signOfRotation*degree + transform.eulerAngles.y;
                _isRotating = true;
                _rotatingCell = cell;
            }
        }


        public void MoveDiagonal(CellObject cell, MovementType typeOfMovement)
        {
            if (typeOfMovement == MovementType.MoveIn)
            {
                if (PrevGroup)
                {
                    var targetObject = PrevGroup.FindObjectFromChild(cell.transform.rotation.eulerAngles.y);
                    var targetCell = targetObject.GetComponent<CellObject>();

                    if (targetCell)
                    {
                        cell.CustomMove(targetCell);
                    }
                }
                else
                {
                    if (GroupIndex == 1)
                    {
                        cell.CustomMove(CentreCell);
                    }
                }
            }
            if (typeOfMovement == MovementType.MoveOut)
            {
                if (NextGroup)
                {
                    var targetObject = NextGroup.FindObjectFromChild(cell.transform.rotation.eulerAngles.y);
                    var targetCell = targetObject.GetComponent<CellObject>();

                    if (targetCell)
                    {
                        cell.CustomMove(targetCell);
                    }
                }
            }
        }


        public void Update()
        {
            if (_isRotating)
            {
                if (_remainingRotation <= -1)
                {
                    _isRotating = false;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, _goalRotation, transform.eulerAngles.z);

                    _finishAction(_rotatingCell);
                }
                else
                {
                    var deltaR = RotationSpeed*Time.deltaTime;
                    transform.Rotate(Vector3.up, deltaR*_signOfRotation);

                    _remainingRotation -= deltaR;
                }
            }
        }

        public void MoveRight(CellObject cell)
        {
            cell.AddToRight(1);
        }

        public void MoveLeft(CellObject cell)
        {
            cell.AddToLeft(1);
        }

        public GameObject FindObjectFromChild(float targetRotation)
        {
            foreach (var cellObject in CellChilds)
            {
                Debug.Log(cellObject.transform.rotation.y);

                if (Mathf.Abs(cellObject.transform.rotation.eulerAngles.y - targetRotation) < 2f)
                {
                    cellObject.gameObject.renderer.material.color = cellObject.gameObject.renderer.material.color == Color.black ? Color.blue : Color.black;

                    Debug.Log(cellObject.gameObject.name);
                    return cellObject.gameObject;
                }
            }

            return null;
        }
    }
}