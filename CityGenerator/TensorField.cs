using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CityGenerator
{
    public class TensorField
    {
        private float[,] TensorFieldX;
        private float[,] TensorFieldY;
        private float[,] MajorEigenX;
        private float[,] MajorEigenY;
        private float[,] MinorEigenX;
        private float[,] MinorEigenY;

        private static int FIELD_SIZE = 512;
        private static float D = 0.2f;

        private List<float> TensorPointX;
        private List<float> TensorPointY;
        private List<float> TensorX;
        private List<float> TensorY;

        public TensorField(
            List<float> radialPointsX, 
            List<float> radialPointsY, 
            List<float> polylinePointsX,
            List<float> polylinePointsY)
        {
            TensorFieldX = new float[FIELD_SIZE, FIELD_SIZE];
            TensorFieldY = new float[FIELD_SIZE, FIELD_SIZE];
            CreateRadialTensors(radialPointsX, radialPointsY);
            CreatePolylineTensors(polylinePointsX, polylinePointsY);
            CreateTensorField();
            CalculateEigenVectors();
        }

        public float[,] GetMajorEigenVectorX()
        {
            return MajorEigenX;
        }

        public float[,] GetMajorEigenVectorY()
        {
            return MajorEigenY;
        }

        public float[,] GetMinorEigenVectorX()
        {
            return MinorEigenX;
        }

        public float[,] GetMinorEigenVectorY()
        {
            return MinorEigenY;
        }

        private void CreateTensorField()
        { 
            for (int row = 0; row < FIELD_SIZE; row++)
            {
                for (int col = 0; col < FIELD_SIZE; col++)
                {
                    float tensorSumX = 0.0f;
                    float tensorSumY = 0.0f;

                    for (int tensor = 0; tensor < TensorPointX.Count; tensor++)
                    {
                        float dist = (float) Math.Sqrt((float) (Math.Pow(TensorPointX[tensor] - col, 2) - 
                            Math.Pow(TensorPointY[tensor] - row, 2)));
                        tensorSumX += (float) Math.Exp(-D * Math.Pow(dist, 2)) * TensorX[tensor];
                        tensorSumY += (float)Math.Exp(-D * Math.Pow(dist, 2)) * TensorY[tensor];
                    }

                    TensorFieldX[row, col] = (float)(tensorSumX);
                    TensorFieldY[row, col] = (float)(tensorSumY);
                }
            }
        }

        private void CreatePolylineTensors(List<float> polylinePointsX, List<float> polylinePointsY)
        {
            for (int i = 0; i < polylinePointsX.Count - 1; i++)
            {
                float deltaX = (polylinePointsX[i + 1] - polylinePointsX[i]) / 2;
                float deltaY = (polylinePointsY[i + 1] - polylinePointsY[i]) / 2;
                float R = (float)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
                float theta = (float)Math.Atan2(deltaY, deltaX);


                TensorPointX.Add(polylinePointsX[i] + deltaX);
                TensorPointY.Add(polylinePointsY[i] + deltaY);
                TensorX.Add((float)(R * Math.Cos(2 * theta)));
                TensorY.Add((float)(R * Math.Sin(2 * theta)));
            }
        }

        private void CreateRadialTensors(List<float> radialPointsX, List<float> radialPointsY)
        {
            for (int i = 0; i < radialPointsY.Count; i++)
            {
                TensorPointX.Add(radialPointsX[i]);
                TensorPointY.Add(radialPointsY[i]);
                TensorX.Add((float)(Math.Pow(radialPointsY[i], 2) - Math.Pow(radialPointsX[i], 2)));
                TensorY.Add((float)(-2.0f * radialPointsY[i] * radialPointsX[i]));
            }
        }

        private void CalculateEigenVectors()
        {
            for (int row = 0; row < FIELD_SIZE; row++)
            {
                for (int col = 0; col < FIELD_SIZE; col++)
                {
                    // float R = (float)Math.Sqrt(Math.Pow(TensorX[row, col], 2) + Math.Pow(TensorY[row, col], 2));
                    float theta = (float)(Math.Atan2((-1 * TensorFieldY[row, col]), TensorFieldY[row, col]) / 2.0f);

                    MajorEigenX[row, col] = (float)Math.Cos(theta);
                    MajorEigenY[row, col] = (float)Math.Sin(theta);
                    MinorEigenX[row, col] = (float)(-1.0f * Math.Sin(theta));
                    MinorEigenY[row, col] = (float)Math.Cos(theta);
                }
            }
        }     
    }
}
