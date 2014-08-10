using UnityEngine;

namespace Assets.Scripts
{
    public class GameScore : MonoBehaviour
    {
        private int _score;
        public GUISkin Skin;
        public GUILocationHelper Location = new GUILocationHelper();


        private Matrix4x4 _guiMatrix = Matrix4x4.identity;

        public void AddScore(int sc)
        {
            _score += sc;
        }

        public void Start()
        {
            Location.PointLocation = GUILocationHelper.Point.TopLeft;
            Location.UpdateLocation();

            Vector2 ratio = Location.GuiOffset;
            _guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
        }

        public void OnGUI()
        {
            GUI.matrix = _guiMatrix;

            GUI.Label(new Rect(Location.Offset.x, Location.Offset.y + 25, 100, 50), _score.ToString(), Skin.label);

            GUI.matrix = Matrix4x4.identity;
        }
    }
}