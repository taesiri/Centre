using UnityEngine;

namespace Assets.Scripts
{
    public class GameCellObject : MonoBehaviour
    {
        public float Speed = 50.0f;
        public Vector3 Origin = Vector3.zero;
        public TextMesh InnerTextMesh;

        private void Update()
        {
            transform.RotateAround(Origin, Vector3.up, Speed*Time.deltaTime);
            
            Debug.DrawLine(transform.position, Origin, Color.red);
        }
    }
}