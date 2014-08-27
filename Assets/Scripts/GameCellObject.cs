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
        private float _rotationDegree;
        private bool _affected;
        private int _layer;

        public bool AllowToDrop
        {
            get { return GhostTransform.renderer.enabled; }
        }


        public float CurrentDegree
        {
            get { return _rotationDegree; }
        }

        public void Start()
        {
            _transform = transform;
            _layer = gameObject.layer;
        }

        private void Update()
        {
            HandleRotation();

            if (Centre.Instance.IsMouseDown)
            {
                CheckRotationAngle();
            }
            else if (_affected)
            {
                GhostTransformVisibility(false);
            }

            //DrawDebugInformation();
        }

        private void CheckRotationAngle()
        {
            if (!IsCentre)
            {
                _rotationDegree = Mathf.Rad2Deg*Mathf.Atan2(_transform.position.z, _transform.position.x);
                if (Mathf.Abs(_layer - Centre.Instance.GoalLayer) <= 1)
                {
                    if (Mathf.Abs(_rotationDegree - Centre.Instance.GoalRotation) < Centre.Instance.Threshold)
                    {
                        GhostTransformVisibility(true);
                        _affected = true;
                    }
                    else
                    {
                        GhostTransformVisibility(false);
                    }
                }
            }
            else if (Mathf.Abs(_layer - Centre.Instance.GoalLayer) <= 1)
            {
                GhostTransformVisibility(true);
                _affected = true;
            }
            else
            {
                GhostTransformVisibility(false);
            }
        }

        public void HandleRotation()
        {
            _transform.RotateAround(Origin, Vector3.up, Speed*Time.deltaTime);
        }

        public void DrawDebugInformation()
        {
            Debug.DrawLine(_transform.position, Origin, Color.cyan);
        }


        public void GhostTransformVisibility(bool value)
        {
            if (GhostTransform)
                GhostTransform.renderer.enabled = value;
            else
            {
                renderer.material.color = Color.red;
            }
        }
    }
}