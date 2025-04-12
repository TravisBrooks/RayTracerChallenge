using RayTracerChallenge;

namespace RayTracerTest;

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
			[1, 0] = 5.5,
			[1, 1] = 6.5,
			[1, 2] = 7.5,
			[1, 3] = 8.5,
			[2, 0] = 9,
			[2, 1] = 10,
			[2, 2] = 11,
			[2, 3] = 12,
			[3, 0] = 13.5,
			[3, 1] = 14.5,
			[3, 2] = 15.5,
			[3, 3] = 16.5
		};
		Assert.Equal(4, m.Rows);
		Assert.Equal(4, m.Columns);
		Assert.Equal(1, m[0, 0]);
		Assert.Equal(4, m[0, 3]);
		Assert.Equal(5.5, m[1, 0]);
		Assert.Equal(7.5, m[1, 2]);
		Assert.Equal(11, m[2, 2]);
		Assert.Equal(13.5, m[3, 0]);
		Assert.Equal(15.5, m[3, 2]);
	}

	[Fact]
	public void MatrixConstructor4X4Values()
	{
		var m = new Matrix(new[,]
		{
			{ 1, 2, 3, 4 },
			{ 5.5, 6.5, 7.5, 8.5 },
			{ 9, 10, 11, 12 },
			{ 13.5, 14.5, 15.5, 16.5 }
		});
		Assert.Equal(4, m.Rows);
		Assert.Equal(4, m.Columns);
		Assert.Equal(1, m[0, 0]);
		Assert.Equal(4, m[0, 3]);
		Assert.Equal(5.5, m[1, 0]);
		Assert.Equal(7.5, m[1, 2]);
		Assert.Equal(11, m[2, 2]);
		Assert.Equal(13.5, m[3, 0]);
		Assert.Equal(15.5, m[3, 2]);
	}

	[Fact]
	public void MatrixConstructor2X2()
	{
		var m = new Matrix(new double[,]
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
		var m = new Matrix(new double[,]
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
		var m1 = new Matrix(new double[,]
		{
			{ 1, 2, 3, 4 },
			{ 5, 6, 7, 8 },
			{ 9, 8, 7, 6 },
			{ 5, 4, 3, 2 }
		});
		var m2 = new Matrix(new double[,]
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
		var m1 = new Matrix(new double[,]
		{
			{ 1, 2, 3, 4 },
			{ 5, 6, 7, 8 },
			{ 9, 8, 7, 6 },
			{ 5, 4, 3, 2 }
		});
		var m2 = new Matrix(new double[,]
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
		var m1 = new Matrix(new double[,]
		{
			{ 1, 2, 3, 4 },
			{ 5, 6, 7, 8 },
			{ 9, 8, 7, 6 },
			{ 5, 4, 3, 2 }
		});
		var m2 = new Matrix(new double[,]
		{
			{ -2, 1, 2, 3 },
			{ 3, 2, 1, -1 },
			{ 4, 3, 6, 5 },
			{ 1, 2, 7, 8 }
		});
		var expected = new Matrix(new double[,]
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
		var m = new Matrix(new double[,]
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
			vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
			point => Assert.Equal(expected, point)
		);
	}

	[Fact]
	public void MultiplyMatrixByVector()
	{
		var m = new Matrix(new double[,]
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
			vector => Assert.Equal(expected, vector),
			point => Assert.Fail($"Was not expecting a point result: {point}")
		);
	}

	[Fact]
	public void IdentityMultiplyingMatrix()
	{
		var id = Matrix.Identity();
		var m = new Matrix(new double[,]
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
			point => Assert.Equal(p, point)
		);
	}

	[Fact]
	public void IdentityMultiplyingVector()
	{
		var v = new Vector(1, 2, 3);
		var maybeVectorOrPoint = Matrix.Identity() * v;
		maybeVectorOrPoint.HandleResult(
			vector => Assert.Equal(v, vector),
			point => Assert.Fail($"Was not expecting a point result: {point}")
		);
	}

	[Fact]
	public void TransposeMatrix()
	{
		var m = new Matrix(new double[,]
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
		var m = new Matrix(new double[,]
		{
			{ 1, 5 },
			{ -3, 2 }
		});
		Assert.Equal(17, m.Determinant());
	}

	[Fact]
	public void Submatrix3X3()
	{
		var m = new Matrix(new double[,]
		{
			{ 1, 5, 0 },
			{ -3, 2, 7 },
			{ 0, 6, -3 }
		});
		var expected = new Matrix(new double[,]
		{
			{ -3, 2 },
			{ 0, 6 }
		});
		Assert.Equal(expected, m.Submatrix(0, 2));
	}

	[Fact]
	public void Submatrix4X4()
	{
		var m = new Matrix(new double[,]
		{
			{ -6, 1, 1, 6 },
			{ -8, 5, 8, 6 },
			{ -1, 0, 8, 2 },
			{ -7, 1, -1, 1 }
		});
		var expected = new Matrix(new double[,]
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
		var m = new Matrix(new double[,]
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
		var m = new Matrix(new double[,]
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
		var m = new Matrix(new double[,]
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
		var m = new Matrix(new double[,]
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
		var m = new Matrix(new double[,]
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
		var m = new Matrix(new double[,]
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
		var m = new Matrix(new double[,]
		{
			{ -5, 2, 6, -8 },
			{ 1, -5, 1, 8 },
			{ 7, 7, -6, -7 },
			{ 1, -3, 7, 4 }
		});
		var inv = m.Inverse();
		Assert.Equal(532, m.Determinant());
		Assert.Equal(-160, m.Cofactor(2, 3));
		Assert.Equal(-160.0 / 532.0, inv[3, 2]);
		Assert.Equal(105, m.Cofactor(3, 2));
		Assert.Equal(105.0 / 532.0, inv[2, 3]);
		var expected = new Matrix(new[,]
		{
			{  0.21805, 0.45113,  0.24060, -0.04511 },
			{ -0.80827, -1.45677, -0.44361, 0.52068 },
			{ -0.07895, -0.22368, -0.05263, 0.19737 },
			{ -0.52256, -0.81391, -0.30075, 0.30639 }
		});
		Assert.Equal(expected, inv);
	}

	[Fact]
	public void InverseMatrix2()
	{
		var m = new Matrix(new double[,]
		{
			{ 8, -5, 9, 2 },
			{ 7, 5, 6, 1 },
			{ -6, 0, 9, 6 },
			{ -3, 0, -9, -4 }
		});
		var inv = m.Inverse();
		var expected = new Matrix(new[,]
		{
			{ -0.15385, -0.15385, -0.28205, -0.53846 },
			{ -0.07692,  0.12308,  0.02564,  0.03077 },
			{  0.35897,  0.35897,  0.43590,  0.92308 },
			{ -0.69231, -0.69231, -0.76923, -1.92308 }
		});
		Assert.Equal(expected, inv);
	}

	[Fact]
	public void InverseMatrix3()
	{
		var m = new Matrix(new double[,]
		{
			{ 9, 3, 0, 9 },
			{ -5, -2, -6, -3 },
			{ -4, 9, 6, 4 },
			{ -7, 6, 6, 2 }
		});
		var inv = m.Inverse();
		var expected = new Matrix(new[,]
		{
			{ -0.04074, -0.07778,  0.14444, -0.22222 },
			{ -0.07778,  0.03333,  0.36667, -0.33333 },
			{ -0.02901, -0.14630, -0.10926,  0.12963 },
			{  0.17778,  0.06667, -0.26667,  0.33333 }
		});
		Assert.Equal(expected, inv);
	}

	[Fact]
	public void InverseMatrix4()
	{
		var mA = new Matrix(new double[,]
		{
			{ 3, -9, 7, 3 },
			{ 3, -8, 2, -9 },
			{ -4, 4, 4, 1 },
			{ -6, 5, -1, 1 }
		});
		var mB = new Matrix(new double[,]
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