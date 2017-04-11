using System;
using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;

namespace CityGenerator
{
	public class TensorField
	{
        private Matrix<float>[,] Tensors;
        private Vector<float>[,] MajorEigenVectors;
        private Vector<float>[,] MinorEigenVectors;
        private static int FIELD_SIZE = 512;

        private Vector<float> initialDirection;
        private List<Vector<float>> radialPoints;
        private List<List<Vector<float>>> boundaryLines;

		public TensorField(Vector<float> initialDirection, List<Vector<float>> radialPoints, List<List<Vector<float>>> boundaryLines)
		{
            Tensors = new Matrix<float>[FIELD_SIZE, FIELD_SIZE];
            MajorEigenVectors = new Vector<float>[FIELD_SIZE, FIELD_SIZE];
            MinorEigenVectors = new Vector<float>[FIELD_SIZE, FIELD_SIZE];
            this.initialDirection = initialDirection;
            this.radialPoints = radialPoints;
            this.boundaryLines = boundaryLines;
            CreateTensorField();
            CalculateEigenVectors();
		}

        public Vector<float>[,] GetMajorEigenVectors()
        {
            return MajorEigenVectors;
        }

        public Vector<float>[,] GetMinorEigenVectors()
        {
            return MinorEigenVectors;
        }

        public Matrix<float> GetTensor(int row, int col) 
        {
            return Tensors[row, col];
        }

        private void CreateTensorField()
        {
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                for(int j = 0; j < FIELD_SIZE; j++) 
                {
                    Tensors[i,j] = CreateTensorAt(i, j);
                }
            }
        }

        private Matrix<float> CreateTensorAt(int row, int col) 
        {
            float R = (float) Math.Sqrt(Math.Pow(initialDirection[0], 2) + Math.Pow(initialDirection[1], 2));
            float theta = (float) Math.Atan(initialDirection[1]/ initialDirection[0]);
            var M = Matrix<float>.Build;

            return M.DenseOfRowArrays( // gives grid Tensor
                new float[] {(float) Math.Cos(2 * theta), (float) Math.Sin(2 * theta)},
                new float[] {(float) Math.Sin(2* theta), (float) (-1 * Math.Cos(2 * theta))}
                ).Multiply(R);
        }

        private void CalculateEigenVectors() 
        {
            for (int i = 0; i < FIELD_SIZE; i++) 
            {
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                    CalulateEigenVectors(Tensors[i,j], i, j);
                }
            }
        }

        private void CalulateEigenVectors(Matrix<float> tensor, int row, int col) 
        {
            float trace = tensor.Trace();
            float det = tensor.Determinant();
            float eigenValueOne = (float) (trace / 2 + Math.Sqrt((Math.Pow(trace, 2) / (4 - det))));
            float eigenValueTwo = (float) (trace / 2 - Math.Sqrt((Math.Pow(trace, 2) / (4 - det))));
            var V = Vector<float>.Build;

            if (tensor[1,0] != 0) 
            {
                MajorEigenVectors[row,col] = V.Dense(new float[] {eigenValueOne - det, tensor[1,0]});
                MinorEigenVectors[row,col] = V.Dense(new float[] {eigenValueTwo - det, tensor[1,0]});
            } else if (tensor[0,1] != 0) 
            {
                MajorEigenVectors[row,col] = V.Dense(new float[] {tensor[0,1], eigenValueOne - det});
                MinorEigenVectors[row,col] = V.Dense(new float[] {tensor[0,1], eigenValueTwo - det});
            } else 
            {
                MajorEigenVectors[row,col] = V.Dense(new float[] {1.0f, 0.0f});
                MinorEigenVectors[row, col] = V.Dense(new float[] {0.0f, 1.0f});
            }
        }
	}
}
