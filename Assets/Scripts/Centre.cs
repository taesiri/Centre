using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Centre : MonoBehaviour
    {
        public GameObject GameCellPrefab;
        public GameObject SourcePrefab;
        public List<SourceObject> Sources;

        public static Centre Instance;

        public void Start()
        {
            Instance = this;
        }
    }
}