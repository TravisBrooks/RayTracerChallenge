using System.Runtime.InteropServices.ComTypes;
using NUnit.Framework;
using RayTracerChallenge;

namespace Tests
{
    [TestFixture]
    public class MatrixTest
    {
        [Test]
        public void ConstructorTest()
        {
            var m = new Matrix(new[,]
            {
                { 1, 2, 3, 4 },
                { 5.5, 6.5, 7.5, 8.5 },
                { 9, 10, 11, 12},
                { 13.5, 14.5, 15.5, 16.5 }
            });

            Assert.That(m[0, 0], Is.EqualTo(1), "[0, 0]");
            Assert.That(m[0, 3], Is.EqualTo(4), "[0, 3]");
            Assert.That(m[1, 0], Is.EqualTo(5.5), "[1, 0]");
            Assert.That(m[1, 2], Is.EqualTo(7.5), "[1, 2]");
            Assert.That(m[2, 2], Is.EqualTo(11), "[2, 2]");
            Assert.That(m[3, 0], Is.EqualTo(13.5), "[3, 0]");
            Assert.That(m[3, 2], Is.EqualTo(15.5), "[3, 2]");
        }

        [Test]
        public void CanDo2x2Matrix()
        {
            var m = new Matrix(new double[,]
            {
                { -3, 5},
                { 1, -2 }
            });

            Assert.That(m[0, 0], Is.EqualTo(-3), "[0, 0]");
            Assert.That(m[0, 1], Is.EqualTo(5), "[0, 1]");
            Assert.That(m[1, 0], Is.EqualTo(1), "[1, 0]");
            Assert.That(m[1, 1], Is.EqualTo(-2), "[1, 1]");
        }

        [Test]
        public void CanDo3x3Matrix()
        {
            var m = new Matrix(new[,]
            {
                { -3.0, 5, 0 },
                { 1, -2, -7 },
                { 0, 1, 1 }
            });

            Assert.That(m[0, 0], Is.EqualTo(-3), "[0, 0]");
            Assert.That(m[1, 1], Is.EqualTo(-2), "[1, 1]");
            Assert.That(m[2, 2], Is.EqualTo(1), "[2, 2]");
        }

        [Test]
        public void MatrixEquality()
        {
            var matrixA = new Matrix(new double[,]
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 8, 7, 6 },
                { 5, 4, 3, 2 }
            });

            var matrixB = new Matrix(new double[,]
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 8, 7, 6 },
                { 5, 4, 3, 2 }
            });

            Assert.That(matrixA, Is.EqualTo(matrixB));
        }

        [Test]
        public void MatrixInEquality()
        {
            var matrixA = new Matrix(new double[,]
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 8, 7, 6 },
                { 5, 4, 3, 2 }
            });

            var matrixB = new Matrix(new double[,]
            {
                { 2, 3, 4, 5 },
                { 6, 7, 8, 9 },
                { 8, 7, 6, 5 },
                { 4, 3, 2, 1 }
            });

            Assert.That(matrixA, Is.Not.EqualTo(matrixB));
        }

        [Test]
        public void MatrixMultIdentityMatrix()
        {
            var idM = Matrix.Identity();
            var matrixA = new Matrix(new double[,]
            {
                { 0, 1,  2,  4 },
                { 1, 2,  4,  8 },
                { 2, 4,  8, 16 },
                { 4, 8, 16, 32 }
            });
            var mult = matrixA * idM;

            Assert.That(mult, Is.EqualTo(matrixA));
        }

        [Test]
        public void MatrixMultIdentityTuple()
        {
            var idM = Matrix.Identity();
            var tplA = new Tuple(1, 2, 3, 4);
            var mult = idM * tplA;

            Assert.That(tplA, Is.EqualTo(mult));
        }

        [Test]
        public void TransposeTest()
        {
            var matrixA = new Matrix(new double[,]
            {
                { 0, 9, 3, 0 },
                { 9, 8, 0, 8 },
                { 1, 8, 5, 3 },
                { 0, 0, 5, 8 }
            });

            var expected = new Matrix(new double[,]
            {
                { 0, 9, 1, 0 },
                { 9, 8, 8, 0 },
                { 3, 0, 5, 5 },
                { 0, 8, 3, 8 }
            });
            var transpose = matrixA.Transpose();

            Assert.That(transpose, Is.EqualTo(expected));
        }

        [Test]
        public void TransposeIdentity()
        {
            var identity = Matrix.Identity();
            var transpose = identity.Transpose();
            Assert.That(transpose, Is.EqualTo(identity));
        }
    }
}
