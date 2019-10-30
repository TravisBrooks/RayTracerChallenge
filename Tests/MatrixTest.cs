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

        [Test]
        public void Determinant2x2()
        {
            var matrix = new Matrix(new double[,]
            {
                { 1, 5 },
                { -3, 2 }
            });
            var dt = matrix.Determinant();
            Assert.That(dt, Is.EqualTo(17));
        }

        [Test]
        public void Submatrix3x3()
        {
            var matrix = new Matrix(new double[,]
            {
                { 1, 5, 0 },
                { -3, 2, 7 },
                { 0, 6, -3 }
            });
            var subMatrix = matrix.SubMatrix(0, 2);
            var expected = new Matrix(new double[,]
            {
                { -3, 2 },
                { 0, 6 }
            });

            Assert.That(subMatrix, Is.EqualTo(expected));
        }

        [Test]
        public void Submatrix4x4()
        {
            var matrix = new Matrix(new double[,]
            {
                { -6, 1, 1, 6 },
                { -8, 5, 8, 6 },
                { -1, 0, 8, 2 },
                { -7, 1, -1, 1 }
            });
            var subMatrix = matrix.SubMatrix(2, 1);
            var expected = new Matrix(new double[,]
            {
                { -6, 1, 6 },
                { -8, 8, 6 },
                { -7, -1, 1 }
            });

            Assert.That(subMatrix, Is.EqualTo(expected));
        }

        [Test]
        public void Minor3x3()
        {
            var matrix = new Matrix(new double[,]
            {
                { 3, 5, 0 },
                { 2, -1, -7 },
                { 6, -1, 5 }
            });
            var submatrix = matrix.SubMatrix(1, 0);
            var determinant = submatrix.Determinant();
            var minor = matrix.Minor(1, 0);
            Assert.That(determinant, Is.EqualTo(25), "determinant");
            Assert.That(minor, Is.EqualTo(25), "minor");
        }

        [Test]
        public void Cofactor3x3()
        {
            var matrix = new Matrix(new double[,]
            {
                { 3, 5, 0 },
                { 2, -1, -7 },
                { 6, -1, 5 }
            });
            var minorOne = matrix.Minor(0, 0);
            var cofactorOne = matrix.Cofactor(0, 0);
            var minorTwo = matrix.Minor(1, 0);
            var cofactorTwo = matrix.Cofactor(1, 0);

            Assert.That(minorOne, Is.EqualTo(-12), "minorOne");
            Assert.That(cofactorOne, Is.EqualTo(-12), "cofactorOne");
            Assert.That(minorTwo, Is.EqualTo(25), "minorTwo");
            Assert.That(cofactorTwo, Is.EqualTo(-25), "cofactorTwo");
        }

        [Test]
        public void Determinant3x3()
        {
            var matrix = new Matrix(new double[,]
            {
                { 1, 2, 6 },
                { -5, 8, -4 },
                { 2, 6, 4 }
            });
            var cofactor_0_0 = matrix.Cofactor(0, 0);
            var cofactor_0_1 = matrix.Cofactor(0, 1);
            var cofactor_0_2 = matrix.Cofactor(0, 2);
            var determinant = matrix.Determinant();

            Assert.That(cofactor_0_0, Is.EqualTo(56), "cofactor_0_0");
            Assert.That(cofactor_0_1, Is.EqualTo(12), "cofactor_0_1");
            Assert.That(cofactor_0_2, Is.EqualTo(-46), "cofactor_0_2");
            Assert.That(determinant, Is.EqualTo(-196), "determinant");
        }

        [Test]
        public void Determinant4x4()
        {
            var matrix = new Matrix(new double[,]
            {
                { -2, -8, 3, 5 },
                { -3, 1, 7, 3 },
                { 1, 2, -9, 6 },
                { -6, 7, 7, -9 }
            });
            var cofactor_0_0 = matrix.Cofactor(0, 0);
            var cofactor_0_1 = matrix.Cofactor(0, 1);
            var cofactor_0_2 = matrix.Cofactor(0, 2);
            var cofactor_0_3 = matrix.Cofactor(0, 3);
            var determinant = matrix.Determinant();

            Assert.That(cofactor_0_0, Is.EqualTo(690), "cofactor_0_0");
            Assert.That(cofactor_0_1, Is.EqualTo(447), "cofactor_0_1");
            Assert.That(cofactor_0_2, Is.EqualTo(210), "cofactor_0_2");
            Assert.That(cofactor_0_3, Is.EqualTo(51), "cofactor_0_3");
            Assert.That(determinant, Is.EqualTo(-4071), "determinant");
        }

        [Test]
        public void IsInvertible()
        {
            var matrix = new Matrix(new double[,]
            {
                { 6, 4, 4, 4 },
                { 5, 5, 7, 6 },
                { 4, -9, 3, -7 },
                { 9, 1, 7, -6 }
            });
            var determinant = matrix.Determinant();
            Assert.That(determinant, Is.EqualTo(-2120), "determinant");
            Assert.That(matrix.IsInvertible(), Is.True);
        }

        [Test]
        public void IsNotInvertible()
        {
            var matrix = new Matrix(new double[,]
            {
                { -4, 2, -2, -3 },
                { 9, 6, 2, 6 },
                { 0, -5, 1, -5 },
                { 0, 0, 0, 0 }
            });
            var determinant = matrix.Determinant();
            Assert.That(determinant, Is.EqualTo(0), "determinant");
            Assert.That(matrix.IsInvertible(), Is.False);
        }

        [Test]
        public void InversionTestOne()
        {
            var matrix = new Matrix(new double[,]
            {
                { -5, 2, 6, -8 },
                { 1, -5, 1, 8 },
                { 7, 7, -6, -7 },
                { 1, -3, 7, 4 }
            });
            var expected = new Matrix(new double[,]
            {
                { 0.21805, 0.45113, 0.24060, -0.04511 },
                { -0.80827, -1.45677, -0.44361, 0.52068 },
                { -0.07895, -0.22368, -0.05263, 0.19737 },
                { -0.52256, -0.81391, -0.30075, 0.30639 }
            });

            var actual = matrix.Inverse();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void InversionTestTwo()
        {
            var matrix = new Matrix(new double[,]
            {
                { 8, -5, 9, 2 },
                { 7, 5, 6, 1 },
                { -6, 0, 9, 6 },
                { -3, 0, -9, -4 }
            });
            var expected = new Matrix(new double[,]
            {
                { -0.15385, -0.15385, -0.28205, -0.53846 },
                { -0.07692, 0.12308, 0.02564, 0.03077 },
                { 0.35897, 0.35897, 0.43590, 0.92308 },
                { -0.69231, -0.69231, -0.76923, -1.92308 }
            });

            var actual = matrix.Inverse();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void InversionTestThree()
        {
            var matrix = new Matrix(new double[,]
            {
                { 9, 3, 0, 9 },
                { -5, -2, -6, -3 },
                { -4, 9, 6, 4 },
                { -7, 6, 6, 2 }
            });
            var expected = new Matrix(new double[,]
            {
                { -0.04074, -0.07778, 0.14444, -0.22222 },
                { -0.07778, 0.03333, 0.36667, -0.33333 },
                { -0.02901, -0.14630, -0.10926, 0.12963 },
                { 0.17778, 0.06667, -0.26667, 0.33333 }
            });

            var actual = matrix.Inverse();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MultiplyProductByItsInverse()
        {
            var matrixA = new Matrix(new double[,]
            {
                { 3, -9, 7, 3 },
                { 3, -8, 2, -9 },
                { -4, 4, 4, 1 },
                { -6, 5, -1, 1 }
            });
            var matrixB = new Matrix(new double[,]
            {
                { 8, 2, 2, 2 },
                { 3, -1, 7, 0 },
                { 7, 0, 5, 4 },
                { 5, -2, 0, 5 }
            });
            var matrixC = matrixA * matrixB;

            var inverseOfB = matrixB.Inverse();
            var productOfCandInverse = matrixC * inverseOfB;

            Assert.That(productOfCandInverse, Is.EqualTo(matrixA));
        }

    }
}
