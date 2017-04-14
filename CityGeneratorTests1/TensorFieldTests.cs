using Microsoft.VisualStudio.TestTools.UnitTesting;
using CityGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityGenerator.Tests
{
    [TestClass()]
    public class TensorFieldTests
    {
        [TestMethod()]
        public void TensorFieldTest()
        {
            List<List<float>> polylinesX = new List<List<float>>();
            List<List<float>> polylinesY = new List<List<float>>();
            List<float> x = new List<float>() { 0.0f, 1.0f };
            List<float> y = new List<float>() { 0.0f, 0.0f };
            polylinesX.Add(x);
            polylinesY.Add(y);
            TensorField tf = new TensorField(null, null, polylinesX, polylinesY);
            Assert.AreEqual(tf.GetMajorEigenVectorX()[0,0], 1.0f);
            Assert.AreEqual(tf.GetMajorEigenVectorY()[0, 0], 0.0f);
        }
    

        [TestMethod()]
        public void GetMajorEigenVectorXTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetMajorEigenVectorYTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetMinorEigenVectorXTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetMinorEigenVectorYTest()
        {
            Assert.Fail();
        }
    }
}