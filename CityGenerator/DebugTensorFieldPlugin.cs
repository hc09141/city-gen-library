using System;
using MathNet.Numerics.LinearAlgebra;
using CityGenerator;

public class DebugTensorFieldPlugin
{
    private TensorField Tensors;
    private float[,] MajorEigenX;
    private float[,] MajorEigenY;
    private float[,] MinorEigenX;
    private float[,] MinorEigenY;

    public DebugTensorFieldPlugin(float initX, float initY)
	{
        Tensors = new TensorField(Vector<float>.Build.Dense(new float[] { initX, initY }), null, null);        
        GetEigenVectors();
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

    private void GetEigenVectors()
    {
        Vector<float>[,] majorEigenVectors = Tensors.GetMajorEigenVectors();
        Vector<float>[,] minorEigenVectors = Tensors.GetMinorEigenVectors();
        int size = majorEigenVectors.Length;

        MajorEigenX = new float[size, size];
        MajorEigenY = new float[size, size];
        MinorEigenX = new float[size, size];
        MinorEigenY = new float[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                MajorEigenX[i,j] = majorEigenVectors[i,j][0];
                MajorEigenY[i,j] = majorEigenVectors[i,j][1];
                MinorEigenX[i,j] = minorEigenVectors[i,j][0];
                MinorEigenY[i,j] = minorEigenVectors[i,j][1];
            }
        }
    }
}
