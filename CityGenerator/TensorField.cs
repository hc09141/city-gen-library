using System;
using MathNet.Numerics.LinearAlgebra;
namespace CityGenerator
{
	public class TensorField
	{
        private Matrix<float>[,] Tensors;
        private static int FIELD_SIZE = 512;

        private Vector<float> initialDirection;
        private List<Vector> radialPoints;
        private List<List<Vector>> boundaryLines;

		public TensorField(Vector initialDirection, List<Vector> radialPoints, List<List<Vector>> boundaryLines)
		{
            Tensors = new Matrix<float>[FIELD_SIZE, FIELD_SIZE];
            CreateTensorField();
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
            
        }
	}
}
