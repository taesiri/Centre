namespace Assets.Scripts
{
    public class MathHelper
    {
        public static float TruncateAngle(float angle)
        {
            if (angle < 0)
            {
                angle += 360;
            }
            else if (angle > 360)
            {
                angle -= 360;
            }

            return angle;
        }
    }
}