using UnityEngine;

namespace Assets.Scripts.Cells
{
    public class AttenuativeCell : NormalCell
    {
        public float AttenuationRate = 0.2f;
        private readonly Renderer _refToRenderer;

        public AttenuativeCell(GameCellObject owner) : base(owner)
        {
            CellColorData = new ColorData(owner.renderer.material.color);
            _refToRenderer = owner.renderer;
        }

        public override void OnUpdate()
        {
            if (CellColorData != null)
            {
                CellColorData.Attenuate(AttenuationRate*Time.deltaTime);

                _refToRenderer.material.color = CellColorData.CurrentColor;
            }
        }
    }
}