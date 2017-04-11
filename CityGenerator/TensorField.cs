using System;
using MathNet.Numerics.LinearAlgebra;
namespace CityGenerator
{
	public class TensorField
	{
        private Matrix<float>[,] Tensors;
        private Vector[,] MajorEigenVectors;
        private Vector[,] MinorEigenVectors;
        private static int FIELD_SIZE = 512;

        private Vector<float> initialDirection;
        private List<Vector> radialPoints;
        private List<List<Vector>> boundaryLines;

		public TensorField(Vector initialDirection, List<Vector> radialPoints, List<List<Vector>> boundaryLines)
		{
            Tensors = new Matrix<float>[FIELD_SIZE, FIELD_SIZE];
            MajorEigenVectors = new Vector<float>[FIELD_SIZE, FIELD_SIZE];
            MinorEigenVectors = new Vector<float>[FIELD_SIZE, FIELD_SIZE];
            CreateTensorField();
            CalulateEigenVectors();
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
                    Tensors[i][j] = createTensorAt(i, j);
                }
            }
        }

        private Matrix<float> createTensorAt(int row, int col) 
        {
            float R = Math.Sqrt(Math.Pow(initialDirection[0], 2), Math.Pow(initialDirection[1], 2));
            float theta = Math.Atan(initialDirection[1]/ initialDirection[0]);

            return Matrix.DenseOfRowArrays( // gives grid Tensor
                new float[] {Math.Cos(2 * theta), Math.Sin(2 * theta)},
                new float[] {Math.Sin(2* theta), -1 * Math.Cos(2 * theta)}
                );
        }

        private void CalculateEigenVectors() 
        {
            for (int i = 0; i < FIELD_SIZE; i++) 
            {
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                    CalulateEigenVectors(TensorField[i][j], i, j);
                }
            }
        }


        private void CalulateEigenVectors(Matrix<float> tensor, int row, int col) 
        {
            float trace = tensor.Trace();
            float det = tensor.Determinant();
            float eigenValueOne = trace / 2 + Math.Sqrt((Math.Pow(trace) / (4 - det)));
            float eigenValueTwo = trace / 2 - Math.Sqrt((Math.Pow(trace) / (4 - det)));

            if (tensor[1][0] != 0) 
            {
                MajorEigenVectors[row][col] = new DenseVector(new int[] {eigenValueOne - det, tensor[1,0]});
                MinorEigenVectors[row][col] = new DenseVector(new int[] {eigenValueTwo - det, tensor[1,0]});
            } else if (tensor[0][1] != 0) 
            {
                MajorEigenVectors[row][col] = new DenseVector(new int[] {tensor[0,1], eigenValueOne - det});
                MinorEigenVectors = new DenseVector(new int[] {tensor[0,1], eigenValueTwo - det});
            } else 
            {
                MajorEigenVectors[row][col] = new DenseVector(new int[] {1.0f, 0.0f});
                MinorEigenVectors = new DenseVector(new int[] {0.0f, 1.0f});
            }
        }
	}
}
