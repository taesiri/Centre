using UnityEngine;

namespace Assets.Scripts.Cells
{
    public class NormalCell : GameCellBehaviour
    {
        protected readonly Renderer RefToRenderer;

        public NormalCell(GameCellObject owner) : base(owner)
        {
            CellColorData = new ColorData(owner.renderer.material.color);
            RefToRenderer = owner.GetColorRenderer();
        }

        public override void OnUpdate()
        {
            // Do Nothing!
        }

        public void ForceUpdateMaterial()
        {
            RefToRenderer.material.color = CellColorData.CurrentColor;
        }

        public override void OnDraw(GameCellObject otherCell)
        {
            // Do Nothing!
            Debug.Log(otherCell.name);
            if (otherCell)
            {
                var cellColorData = otherCell.CellBehaviour;
                if (cellColorData != null)
                {
                    CellColorData.ChnageColor(cellColorData.CellColorData.CurrentColor);
                    ForceUpdateMaterial();
                    Debug.Log("DON!E!");
                }
            }
        }

        public override void OnLeave()
        {
            Debug.Log("LEFT");
            CellColorData.ChnageColor(Color.white);
            ForceUpdateMaterial();
        }
    }
}