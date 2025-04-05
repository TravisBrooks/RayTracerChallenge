using RayTracerChallenge;

namespace RayTracerTest
{
	public class MatrixTest
	{
		[Fact]
		public void MatrixConstructor4X4RowsAndColumns()
		{
			var m = new Matrix(4, 4)
			{
				[0, 0] = 1,
				[0, 1] = 2,
				[0, 2] = 3,
				[0, 3] = 4,
				[1, 0] = 5.5f,
				[1, 1] = 6.5f,
				[1, 2] = 7.5f,
				[1, 3] = 8.5f,
				[2, 0] = 9,
				[2, 1] = 10,
				[2, 2] = 11,
				[2, 3] = 12,
				[3, 0] = 13.5f,
				[3, 1] = 14.5f,
				[3, 2] = 15.5f,
				[3, 3] = 16.5f
			};
			Assert.Equal(4, m.Rows);
			Assert.Equal(4, m.Columns);
			Assert.Equal(1, m[0, 0]);
			Assert.Equal(4, m[0, 3]);
			Assert.Equal(5.5f, m[1, 0]);
			Assert.Equal(7.5f, m[1, 2]);
			Assert.Equal(11, m[2, 2]);
			Assert.Equal(13.5f, m[3, 0]);
			Assert.Equal(15.5f, m[3, 2]);
		}

		[Fact]
		public void MatrixConstructor4X4Values()
		{
			var m = new Matrix(new[,]
			{
				{ 1, 2, 3, 4 },
				{ 5.5f, 6.5f, 7.5f, 8.5f },
				{ 9, 10, 11, 12 },
				{ 13.5f, 14.5f, 15.5f, 16.5f }
			});
			Assert.Equal(4, m.Rows);
			Assert.Equal(4, m.Columns);
			Assert.Equal(1, m[0, 0]);
			Assert.Equal(4, m[0, 3]);
			Assert.Equal(5.5f, m[1, 0]);
			Assert.Equal(7.5f, m[1, 2]);
			Assert.Equal(11, m[2, 2]);
			Assert.Equal(13.5f, m[3, 0]);
			Assert.Equal(15.5f, m[3, 2]);
		}

		[Fact]
		public void MatrixConstructor2X2()
		{
			var m = new Matrix(new float[,]
			{
				{ -3, 5 },
				{ 1, -2 },
			});
			Assert.Equal(2, m.Rows);
			Assert.Equal(2, m.Columns);
			Assert.Equal(-3, m[0, 0]);
			Assert.Equal(5, m[0, 1]);
			Assert.Equal(1, m[1, 0]);
			Assert.Equal(-2, m[1, 1]);
		}

		[Fact]
		public void MatrixConstructor3X3()
		{
			var m = new Matrix(new float[,]
			{
				{ -3, 5, 0 },
				{ 1, -2, 7 },
				{ 0, 1, 1 },
			});
			Assert.Equal(3, m.Rows);
			Assert.Equal(3, m.Columns);
			Assert.Equal(-3, m[0, 0]);
			Assert.Equal(-2, m[1, 1]);
			Assert.Equal(1, m[2, 2]);
		}

		[Fact]
		public void MatrixEquality()
		{
			var m1 = new Matrix(new float[,]
			{
				{ 1, 2, 3, 4 },
				{ 5, 6, 7, 8 },
				{ 9, 8, 7, 6 },
				{ 5, 4, 3, 2 }
			});
			var m2 = new Matrix(new float[,]
			{
				{ 1, 2, 3, 4 },
				{ 5, 6, 7, 8 },
				{ 9, 8, 7, 6 },
				{ 5, 4, 3, 2 }
			});
			Assert.Equal(m1, m2);
		}

		[Fact]
		public void MatrixNotEqual()
		{
			var m1 = new Matrix(new float[,]
			{
				{ 1, 2, 3, 4 },
				{ 5, 6, 7, 8 },
				{ 9, 8, 7, 6 },
				{ 5, 4, 3, 2 }
			});
			var m2 = new Matrix(new float[,]
			{
				{ 2, 3, 4, 5 },
				{ 6, 7, 8, 9 },
				{ 8, 7, 6, 5 },
				{ 4, 3, 2, 1 }
			});
			Assert.NotEqual(m1, m2);
		}

		[Fact]
		public void MultiplyMatrices()
		{
			var m1 = new Matrix(new float[,]
			{
				{ 1, 2, 3, 4 },
				{ 5, 6, 7, 8 },
				{ 9, 8, 7, 6 },
				{ 5, 4, 3, 2 }
			});
			var m2 = new Matrix(new float[,]
			{
				{ -2, 1, 2, 3 },
				{ 3, 2, 1, -1 },
				{ 4, 3, 6, 5 },
				{ 1, 2, 7, 8 }
			});
			var expected = new Matrix(new float[,]
			{
				{ 20, 22, 50, 48 },
				{ 44, 54, 114, 108 },
				{ 40, 58, 110, 102 },
				{ 16, 26, 46, 42 }
			});
			Assert.Equal(expected, m1 * m2);
		}

		[Fact]
		public void MultiplyMatrixByPoint()
		{
			var m = new Matrix(new float[,]
			{
				{ 1, 2, 3, 4 },
				{ 2, 4, 4, 2 },
				{ 8, 6, 4, 1 },
				{ 0, 0, 0, 1 }
			});
			var pt = new Point(1, 2, 3);
			var expected = new Point(18, 24, 33);
			var maybeVorP = m * pt;

			maybeVorP.HandleResult(
				vector => { Assert.Fail($"Was not expecting a vector result: {vector}"); },
				point => { Assert.Equal(expected, point); });
		}

		[Fact]
		public void MultiplyMatrixByVector()
		{
			var m = new Matrix(new float[,]
			{
				{ 1, 2, 3, 4 },
				{ 2, 4, 4, 2 },
				{ 8, 6, 4, 1 },
				{ 0, 0, 0, 1 }
			});
			var v = new Vector(1, 2, 3);
			var expected = new Vector(14, 22, 32);
			var maybeVorP = m * v;

			maybeVorP.HandleResult(
				vector => { Assert.Equal(expected, vector); },
				point => { Assert.Fail($"Was not expecting a point result: {point}"); }
			);
		}

		[Fact]
		public void IdentityMultiplyingMatrix()
		{
			var id = Matrix.Identity();
			var m = new Matrix(new float[,]
			{
				{ 0, 1, 2, 4 },
				{ 1, 2, 4, 8 },
				{ 2, 4, 8, 16 },
				{ 4, 8, 16, 32 }
			});
			var mult = m * id;
			Assert.Equal(m, mult);
		}

		[Fact]
		public void IdentityMultiplyingPoint()
		{
			var p = new Point(1, 2, 3);
			var maybeVectorOrPoint = Matrix.Identity() * p;
			maybeVectorOrPoint.HandleResult(
				vector => { Assert.Fail($"Was not expecting a vector result: {vector}"); },
				point => { Assert.Equal(p, point); }
			);
		}

		[Fact]
		public void IdentityMultiplyingVector()
		{
			var v = new Vector(1, 2, 3);
			var maybeVectorOrPoint = Matrix.Identity() * v;
			maybeVectorOrPoint.HandleResult(
				vector => { Assert.Equal(v, vector); },
				point => { Assert.Fail($"Was not expecting a point result: {point}"); }
			);
		}

		[Fact]
		public void TransposeMatrix()
		{
			var m = new Matrix(new float[,]
			{
				{ 0, 9, 3, 0 },
				{ 9, 8, 0, 8 },
				{ 1, 8, 5, 3 },
				{ 0, 0, 5, 8 }
			});
			var expected = new Matrix(new float[,]
			{
				{ 0, 9, 1, 0 },
				{ 9, 8, 8, 0 },
				{ 3, 0, 5, 5 },
				{ 0, 8, 3, 8 }
			});
			Assert.Equal(expected, m.Transpose());
		}

		[Fact]
		public void TransposeIdentityMatrix()
		{
			var id = Matrix.Identity();
			Assert.Equal(id, id.Transpose());
		}

		[Fact]
		public void Determinant2X2()
		{
			var m = new Matrix(new float[,]
			{
				{ 1, 5 },
				{ -3, 2 }
			});
			Assert.Equal(17, m.Determinant());
		}

		[Fact]
		public void Submatrix3X3()
		{
			var m = new Matrix(new float[,]
			{
				{ 1, 5, 0 },
				{ -3, 2, 7 },
				{ 0, 6, -3 }
			});
			var expected = new Matrix(new float[,]
			{
				{ -3, 2 },
				{ 0, 6 }
			});
			Assert.Equal(expected, m.Submatrix(0, 2));
		}

		[Fact]
		public void Submatrix4X4()
		{
			var m = new Matrix(new float[,]
			{
				{ -6, 1, 1, 6 },
				{ -8, 5, 8, 6 },
				{ -1, 0, 8, 2 },
				{ -7, 1, -1, 1 }
			});
			var expected = new Matrix(new float[,]
			{
				{ -6, 1, 6 },
				{ -8, 8, 6 },
				{ -7, -1, 1 }
			});
			Assert.Equal(expected, m.Submatrix(2, 1));
		}

		[Fact]
		public void Minor3X3()
		{
			var m = new Matrix(new float[,]
			{
				{ 3, 5, 0 },
				{ 2, -1, -7 },
				{ 6, -1, 5 }
			});
			var sub = m.Submatrix(1, 0);
			Assert.Equal(25, sub.Determinant());
			Assert.Equal(25, m.Minor(1, 0));
		}

		[Fact]
		public void Cofactor3X3()
		{
			var m = new Matrix(new float[,]
			{
				{ 3, 5, 0 },
				{ 2, -1, -7 },
				{ 6, -1, 5 }
			});
			Assert.Equal(-12, m.Minor(0, 0));
			Assert.Equal(-12, m.Cofactor(0, 0));
			Assert.Equal(25, m.Minor(1, 0));
			Assert.Equal(-25, m.Cofactor(1, 0));
		}

		[Fact]
		public void Determinant3X3()
		{
			var m = new Matrix(new float[,]
			{
				{ 1, 2, 6 },
				{ -5, 8, -4 },
				{ 2, 6, 4 }
			});
			Assert.Equal(56, m.Cofactor(0, 0));
			Assert.Equal(12, m.Cofactor(0, 1));
			Assert.Equal(-46, m.Cofactor(0, 2));
			Assert.Equal(-196, m.Determinant());
		}

		[Fact]
		public void Determinant4X4()
		{
			var m = new Matrix(new float[,]
			{
				{ -2, -8, 3, 5 },
				{ -3, 1, 7, 3 },
				{ 1, 2, -9, 6 },
				{ -6, 7, 7, -9 }
			});
			Assert.Equal(690, m.Cofactor(0, 0));
			Assert.Equal(447, m.Cofactor(0, 1));
			Assert.Equal(210, m.Cofactor(0, 2));
			Assert.Equal(51, m.Cofactor(0, 3));
			Assert.Equal(-4071, m.Determinant());
		}

		[Fact]
		public void InvertibleMatrix()
		{
			var m = new Matrix(new float[,]
			{
				{ 6, 4, 4, 4 },
				{ 5, 5, 7, 6 },
				{ 4, -9, 3, -7 },
				{ 9, 1, 7, -6 }
			});
			Assert.Equal(-2120, m.Determinant());
			Assert.True(m.IsInvertible());
		}

		[Fact]
		public void NonInvertibleMatrix()
		{
			var m = new Matrix(new float[,]
			{
				{ -4, 2, -2, -3 },
				{ 9, 6, 2, 6 },
				{ 0, -5, 1, -5 },
				{ 0, 0, 0, 0 }
			});
			Assert.Equal(0, m.Determinant());
			Assert.False(m.IsInvertible());
		}

		[Fact]
		public void InverseMatrix()
		{
			var m = new Matrix(new float[,]
			{
				{ -5, 2, 6, -8 },
				{ 1, -5, 1, 8 },
				{ 7, 7, -6, -7 },
				{ 1, -3, 7, 4 }
			});
			var inv = m.Inverse();
			Assert.Equal(532, m.Determinant());
			Assert.Equal(-160, m.Cofactor(2, 3));
			Assert.Equal(-160f / 532, inv[3, 2]);
			Assert.Equal(105, m.Cofactor(3, 2));
			Assert.Equal(105f / 532, inv[2, 3]);
			var expected = new Matrix(new float[,]
			{
				{ 0.21805f, 0.45113f, 0.24060f, -0.04511f },
				{ -0.80827f, -1.45677f, -0.44361f, 0.52068f },
				{ -0.07895f, -0.22368f, -0.05263f, 0.19737f },
				{ -0.52256f, -0.81391f, -0.30075f, 0.30639f }
			});
			Assert.Equal(expected, inv);
		}

		[Fact]
		public void InverseMatrix2()
		{
			var m = new Matrix(new float[,]
			{
				{ 8, -5, 9, 2 },
				{ 7, 5, 6, 1 },
				{ -6, 0, 9, 6 },
				{ -3, 0, -9, -4 }
			});
			var inv = m.Inverse();
			var expected = new Matrix(new float[,]
			{
				{ -0.15385f, -0.15385f, -0.28205f, -0.53846f },
				{ -0.07692f, 0.12308f, 0.02564f, 0.03077f },
				{ 0.35897f, 0.35897f, 0.43590f, 0.92308f },
				{ -0.69231f, -0.69231f, -0.76923f, -1.92308f }
			});
			Assert.Equal(expected, inv);
		}

		[Fact]
		public void InverseMatrix3()
		{
			var m = new Matrix(new float[,]
			{
				{ 9, 3, 0, 9 },
				{ -5, -2, -6, -3 },
				{ -4, 9, 6, 4 },
				{ -7, 6, 6, 2 }
			});
			var inv = m.Inverse();
			var expected = new Matrix(new float[,]
			{
				{ -0.04074f, -0.07778f, 0.14444f, -0.22222f },
				{ -0.07778f, 0.03333f, 0.36667f, -0.33333f },
				{ -0.02901f, -0.14630f, -0.10926f, 0.12963f },
				{ 0.17778f, 0.06667f, -0.26667f, 0.33333f }
			});
			Assert.Equal(expected, inv);
		}

		[Fact]
		public void InverseMatrix4()
		{
			var mA = new Matrix(new float[,]
			{
				{ 3, -9, 7, 3 },
				{ 3, -8, 2, -9 },
				{ -4, 4, 4, 1 },
				{ -6, 5, -1, 1 }
			});
			var mB = new Matrix(new float[,]
			{
				{ 8, 2, 2, 2 },
				{ 3, -1, 7, 0 },
				{ 7, 0, 5, 4 },
				{ 6, -2, 0, 5 }
			});
			var mC = mA * mB;
			// if you multiply some matrix A by another matrix B, producing C, you can multiply C by the inverse of B to get A again
			Assert.Equal(mA, mC * mB.Inverse());
		}
	}
}
