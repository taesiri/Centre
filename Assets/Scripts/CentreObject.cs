using UnityEngine;

namespace Assets.Scripts
{
    public class CentreObject : MonoBehaviour
    {
        public GameObject CellElementPrefab;
        public GameObject CEntreObjectPrefab;

        public Transform CentreTransfrom;
        public float Radius = 10;
        public int NumberOfSubnNodesInRingGroup = 6;
        public int RadiusIncerement = 1;
        public float DiagonalOffset = 2.0f;
        public float CentreOffset = 2.0f;

        public void Start()
        {
            if (!CentreTransfrom)
            {
                CentreTransfrom = transform;
            }
        }
    }
}