using System;
using OpenTK;

namespace COA
{
    public static class MathHelper
    {
        public static Matrix4 ScreenSpaceMatrix
        {
            get
            {
                return Matrix4.CreateOrthographicOffCenter(0, Convars.ResolutionWidth, Convars.ResolutionHeight, 0, -1, 1);
            }
        }

        public const float PiF = (float)Math.PI;
        public const float Pi2F = PiF*2;
        public const float HalfPiF = PiF/2;
        public const float QuarterPiF = PiF/4;

        private const double R2DFactor = Math.PI/180.0;
        private const float R2DFactorF = (float)(Math.PI/180.0);

        public static double ToDegrees(double radians)
        {
            return radians/R2DFactor;
        }

        public static float ToDegrees(float radians)
        {
            return radians/R2DFactorF;
        }

        public static double ToRadians(double degrees)
        {
            return degrees*R2DFactor;
        }

        public static float ToRadians(float degrees)
        {
            return degrees*R2DFactorF;
        }
    }
}