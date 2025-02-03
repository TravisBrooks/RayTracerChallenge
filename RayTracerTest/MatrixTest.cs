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
				{1, 2, 3, 4},
				{5.5f, 6.5f, 7.5f, 8.5f},
				{9, 10, 11, 12},
				{13.5f, 14.5f, 15.5f, 16.5f}
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
				{-3, 5},
				{1, -2},
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
				{-3, 5, 0},
				{1, -2, 7},
				{0, 1, 1},	
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
				{1, 2, 3, 4},
				{5, 6, 7, 8},
				{9, 8, 7, 6},
				{5, 4, 3, 2}
			});
			var m2 = new Matrix(new float[,]
			{
				{1, 2, 3, 4},
				{5, 6, 7, 8},
				{9, 8, 7, 6},
				{5, 4, 3, 2}
			});
			Assert.Equal(m1, m2);
		}

		[Fact]
		public void MatrixNotEqual()
		{
			var m1 = new Matrix(new float[,]
			{
				{1, 2, 3, 4},
				{5, 6, 7, 8},
				{9, 8, 7, 6},
				{5, 4, 3, 2}
			});
			var m2 = new Matrix(new float[,]
			{
				{2, 3, 4, 5},
				{6, 7, 8, 9},
				{8, 7, 6, 5},
				{4, 3, 2, 1}
			});
			Assert.NotEqual(m1, m2);
		}

		[Fact]
		public void MultiplyMatrices()
		{
			var m1 = new Matrix(new float[,]
			{
				{1, 2, 3, 4},
				{5, 6, 7, 8},
				{9, 8, 7, 6},
				{5, 4, 3, 2}
			});
			var m2 = new Matrix(new float[,]
			{
				{-2, 1, 2, 3},
				{3, 2, 1, -1},
				{4, 3, 6, 5},
				{1, 2, 7, 8}
			});
			var expected = new Matrix(new float[,]
			{
				{20, 22, 50, 48},
				{44, 54, 114, 108},
				{40, 58, 110, 102},
				{16, 26, 46, 42}
			});
			Assert.Equal(expected, m1 * m2);
		}

		[Fact]
		public void MultiplyMatrixByPoint()
		{
			var m = new Matrix(new float[,]
			{
				{1, 2, 3, 4},
				{2, 4, 4, 2},
				{8, 6, 4, 1},
				{0, 0, 0, 1}
			});
			var pt = new Point(1, 2, 3);
			var expected = new Point(18, 24, 33);
			var maybeVorP = m * pt;

			maybeVorP.HandleResult(
				vector => 
				{ 
					Assert.Fail($"Was not expecting a vector result: {vector}");
				},
				point =>
				{
					Assert.Equal(expected, point);
				});
		}

		[Fact]
		public void MultiplyMatrixByVector()
		{
			var m = new Matrix(new float[,]
			{
				{1, 2, 3, 4},
				{2, 4, 4, 2},
				{8, 6, 4, 1},
				{0, 0, 0, 1}
			});
			var v = new Vector(1, 2, 3);
			var expected = new Vector(14, 22, 32);
			var maybeVorP = m * v;

			maybeVorP.HandleResult(vector =>
			{
				Assert.Equal(expected, vector);
			},
			point =>
			{
				Assert.Fail($"Was not expecting a point result: {point}");
			});
		}

		[Fact]
		public void IdentityMultiplyingMatrix()
		{
			var id = Matrix.Identity();
			var m = new Matrix(new float[,]
			{
				{0, 1, 2, 4},
				{1, 2, 4, 8},
				{2, 4, 8, 16},
				{4, 8, 16, 32}
			});
			var mult = m * id;
			Assert.Equal(m, mult);
		}

		[Fact]
		public void IdentityMultiplyingPoint()
		{
			var p = new Point(1, 2, 3);
			var maybeVectorOrPoint = Matrix.Identity() * p;
			maybeVectorOrPoint.HandleResult(vector =>
			{
				Assert.Fail($"Was not expecting a vector result: {vector}");
			},
			point =>
			{
				Assert.Equal(p, point);
			});
		}

		[Fact]
		public void IdentityMultiplyingVector()
		{
			var v = new Vector(1, 2, 3);
			var maybeVectorOrPoint = Matrix.Identity() * v;
			maybeVectorOrPoint.HandleResult(vector =>
			{
				Assert.Equal(v, vector);
			},
			point =>
			{
				Assert.Fail($"Was not expecting a point result: {point}");
			});
		}
	}
}
