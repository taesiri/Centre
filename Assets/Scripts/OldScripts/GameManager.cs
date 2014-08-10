using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private bool _isHit = false;
        private bool _isMoved = false;
        public float Threshold = 0.5f;
        public float RotationUnit = 60;
        private CellObject _hitCell;
        private RingGroupObject _hitGroup;
        public GUISkin DefaultSkin;
        public Texture2D MenuTexture;
        public GUILocationHelper Location = new GUILocationHelper();

        public GameScore ScoreManager;

        public void Start()
        {
            Location.PointLocation = GUILocationHelper.Point.Center;
            Location.UpdateLocation();
        }

        public float CalculateAngle(Vector3 line1, Vector3 line2)
        {
            var theta1 = Mathf.Atan2(line1.z, line1.x)*Mathf.Rad2Deg;
            var theta2 = Mathf.Atan2(line2.z, line2.x)*Mathf.Rad2Deg;
            return theta2 - theta1;
        }


        private void Update()
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    var ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                    RaycastHit hitInfo;
                    Physics.Raycast(ray, out hitInfo);

                    if (hitInfo.collider)
                    {
                        _isHit = true;
                        _hitCell = hitInfo.collider.gameObject.GetComponent<CellObject>();
                        _hitGroup = hitInfo.collider.gameObject.transform.parent.GetComponent<RingGroupObject>();
                    }
                }
                else if (Input.touches[0].phase != TouchPhase.Ended)
                {
                    if (_isHit)
                    {
                        if (Input.touches[0].deltaPosition.magnitude > Threshold)
                        {
                            _isMoved = true;
                        }

                        var worldPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);


                        var startPoint = _hitCell.transform.position;
                        var endPoint = new Vector3(_hitCell.transform.parent.position.x, _hitCell.transform.position.y, _hitCell.transform.parent.position.z);

                        var lastTouchPoint = new Vector3(worldPoint.x, endPoint.y, worldPoint.z);

                        Debug.DrawLine(startPoint, lastTouchPoint, Color.blue);
                        Debug.DrawLine(startPoint, endPoint, Color.red);
                    }
                }

                else if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    if (_isHit)
                    {
                        _isHit = false;

                        if (_isMoved)
                        {
                            _isMoved = false;

                            var worldPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);


                            var startPoint = _hitCell.transform.position;
                            var endPoint = new Vector3(_hitCell.transform.parent.position.x, _hitCell.transform.position.y, _hitCell.transform.parent.position.z);

                            var lastTouchPoint = new Vector3(worldPoint.x, endPoint.y, worldPoint.z);

                            Debug.DrawLine(startPoint, lastTouchPoint, Color.blue);
                            Debug.DrawLine(startPoint, endPoint, Color.red);


                            var line1 = endPoint - startPoint;
                            var line2 = lastTouchPoint - startPoint;
                            var diffAngle = MathHelper.TruncateAngle(CalculateAngle(line1, line2));


                            if (0 < diffAngle && diffAngle < 40.0f)
                            {
                                _hitGroup.MoveDiagonal(_hitCell, RingGroupObject.MovementType.MoveIn);
                            }
                            else if (diffAngle < 359 && diffAngle > 360 - 40.0f)
                            {
                                _hitGroup.MoveDiagonal(_hitCell, RingGroupObject.MovementType.MoveIn);
                            }
                            else if (diffAngle < 180 + 30.0f && diffAngle > 180 - 30.0f)
                            {
                                // Outer
                                _hitGroup.MoveDiagonal(_hitCell, RingGroupObject.MovementType.MoveOut);
                            }
                            else if (diffAngle < 180 - 30.0f && diffAngle > 40)
                            {
                                _hitGroup.Rotate(_hitCell, 1*RotationUnit);
                            }
                            else if (diffAngle < 360 - 40.0f && diffAngle > 180 - 30.0f)
                            {
                                _hitGroup.Rotate(_hitCell, -1*RotationUnit);
                            }
                        }
                    }
                }
            }
        }
    }
}