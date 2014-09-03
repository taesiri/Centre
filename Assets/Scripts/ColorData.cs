using UnityEngine;

namespace Assets.Scripts
{
    public class ColorData
    {
        public Color CurrentColor;

        public ColorData()
        {
        }

        public ColorData(Color color)
        {
            CurrentColor = color;
        }

        public void Attenuate(float attenuationRate)
        {
            CurrentColor = new Color(CurrentColor.r - attenuationRate/10f, CurrentColor.g - attenuationRate/10f, CurrentColor.b - attenuationRate/10f);
        }

        public void ChnageColor(Color color)
        {
            CurrentColor = color;
        }
    }
}