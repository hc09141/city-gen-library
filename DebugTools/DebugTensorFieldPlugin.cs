using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CityGenerator;

namespace DebugTools
{
    public class DebugTensorFieldPlugin
    {
        private TensorField Tensors;
        

        public DebugTensorFieldPlugin(float initX, float initY)
        {
            Tensors = new TensorField(initX, initY, null, null);
        }

        public float[,] GetMajorEigenVectorX()
        {
            return Tensors.GetMajorEigenVectorX();
        }

        public float[,] GetMajorEigenVectorY()
        {
            return Tensors.GetMajorEigenVectorY();
        }

        public float[,] GetMinorEigenVectorX()
        {
            return Tensors.GetMinorEigenVectorX();
        }

        public float[,] GetMinorEigenVectorY()
        {
            return Tensors.GetMinorEigenVectorY();
        }
    }
}

