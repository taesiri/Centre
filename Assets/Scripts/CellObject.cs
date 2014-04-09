using UnityEngine;

namespace Assets.Scripts
{
    public class CellObject : MonoBehaviour
    {
        public TextMesh Text;

        public CellObject Right;
        public CellObject Left;
        public GameScore ScoreManager;

        private int _cellValue = 1;

        public int Index = -1;

        public int CellValue
        {
            get { return _cellValue; }
            set
            {
                _cellValue = value;
                Text.text = _cellValue == 0 ? string.Empty : _cellValue.ToString();
            }
        }

        public void DoubleCell()
        {
            CellValue *= 2;
            ScoreUp();
        }

        public void ScoreUp()
        {
            if (ScoreManager)
            {
                ScoreManager.AddScore(CellValue);
            }
        }

        public void CustomMove(CellObject targetCell)
        {
            if (targetCell.CellValue != 0 && targetCell.CellValue == CellValue)
            {
                CellValue = 0;
                targetCell.DoubleCell();
            }
            else
            {
                if (CellValue != 0 && targetCell.CellValue == 0)
                {
                    targetCell.CellValue = CellValue;
                    CellValue = 0;
                }
            }
        }


        public void AddToRight(int remainingMoves)
        {
            remainingMoves--;

            if (Right.CellValue == CellValue && CellValue != 0)
            {
                CellValue = 0;
                Right.DoubleCell();


                if (remainingMoves > 0)
                    Right.Right.AddToRight(remainingMoves - 2);
            }
            else
            {
                //if (Right.CellValue == 0 && CellValue != 0)
                //{
                //    Right.CellValue = CellValue;
                //    CellValue = 0;
                //}


                if (remainingMoves > 0)
                    Right.AddToRight(remainingMoves);
            }
        }


        public void AddToLeft(int remainingMoves)
        {
            remainingMoves--;

            if (Left.CellValue == CellValue && CellValue != 0)
            {
                CellValue = 0;
                Left.DoubleCell();


                if (remainingMoves > 0)
                    Left.Left.AddToLeft(remainingMoves - 2);
            }
            else
            {
                //if (Left.CellValue == 0 && CellValue != 0)
                //{
                //    Left.CellValue = CellValue;
                //    CellValue = 0;
                //}


                if (remainingMoves > 0)
                    Left.AddToLeft(remainingMoves);
            }
        }
    }
}