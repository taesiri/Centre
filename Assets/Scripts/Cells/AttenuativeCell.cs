using UnityEngine;

namespace Assets.Scripts.Cells
{
    public class AttenuativeCell : NormalCell
    {
        public float AttenuationRate = 0.2f;

        public AttenuativeCell(GameCellObject owner) : base(owner)
        {
        }

        public override void OnUpdate()
        {
            if (CellColorData != null)
            {
                CellColorData.Attenuate(AttenuationRate*Time.deltaTime);

                RefToRenderer.material.color = CellColorData.CurrentColor;
            }
        }
    }
}