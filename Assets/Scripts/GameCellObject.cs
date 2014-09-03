using Assets.Scripts.Cells;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameCellObject : MonoBehaviour
    {
        #region PublicFields

        public bool IsCentre = false;
        public float Speed = 50.0f;
        public Vector3 Origin = Vector3.zero;
        public TextMesh InnerTextMesh;
        public SourceObject Parent;
        public Transform GhostTransform;
        public GameCellBehaviour CellBehaviour;
        public Renderer ColorRenderer;

        #endregion

        #region PrivateFields

        private float _rotationDegree;
        private bool _affected;
        private int _layer;
        private Transform _transform;

        #endregion

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
            CellBehaviour = new NormalCell(this);

            if (ColorRenderer == null)
            {
                Debug.LogWarning("Please Assign ColorRenderer");
            }
        }

        private void Update()
        {
            HandleRotation();

            // Ghost Outline Visibilities
            if (Centre.Instance.IsMouseDown)
            {
                CheckRotationAngle();
            }
            else if (_affected)
            {
                GhostTransformVisibility(false);
            }

            // Cell Behaviour Update
            if (CellBehaviour != null)
            {
                CellBehaviour.OnUpdate();
            }

            //Debug Information
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

        public void OnDraw(GameCellObject otherCell)
        {
            if (CellBehaviour != null)
            {
                CellBehaviour.OnDraw(otherCell);
            }
        }

        public void OnLeave()
        {
            if (CellBehaviour != null)
            {
                CellBehaviour.OnLeave();
            }
        }

        public Renderer GetColorRenderer()
        {
            return ColorRenderer;
        }
    }
}