namespace RayTracerChallenge;

public class Matrix : IEquatable<Matrix>
{
	private readonly double[,] _values;

	public Matrix(int rows, int cols)
	{
		Rows = rows;
		Columns = cols;
		_values = new double[Rows, Columns];
	}

	public Matrix(double[,] values) : this(values.GetLength(0), values.GetLength(1))
	{
		Array.Copy(values, 0, _values, 0, values.Length);
	}

	public int Columns { get; }
	public int Rows { get; }

	public double this[int row, int col]
	{
		get => _values[row, col];
		set => _values[row, col] = value;
	}

	/// <summary>
	/// Multiple matrices where the first matrix is (m, n) and the second matrix is (n, p)
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public static Matrix operator *(Matrix a, Matrix b)
	{
		// I didn't follow what book said about matrix mult, code below is taken almost directly from the Definition found at https://en.wikipedia.org/wiki/Matrix_multiplication
		var m = a.Rows;
		var n = a.Columns;
		var otherN = b.Rows;
		var p = b.Columns;

		if (n != otherN)
		{
			throw new ArgumentException("Sorry, I only know how to multiple matrices in dimension pattern (m, n) * (n, p) ");
		}

		var c = new Matrix(m, p);
		for (var i = 0; i < m; i++)
		{
			for (var j = 0; j < p; j++)
			{
				for (var k = 0; k < n; k++)
				{
					var ab = a[i, k] * b[k, j];
					c[i, j] += ab;
				}
			}
		}

		return c;
	}

	public static MaybeVectorOrPoint operator *(Matrix m, ITuple3D t)
	{
		if (m.Rows != 4)
		{
			throw new ArgumentException("To multiply Matrix*ITuple3D the matrix must have Rows==4.");
		}
		var tAsMatrix = t.AsMatrix();
		var mult = m * tAsMatrix;
		var x = mult[0, 0];
		var y = mult[1, 0];
		var z = mult[2, 0];
		var w = mult[3, 0];
		return w.AboutEqual(0) ? 
			new MaybeVectorOrPoint(new Vector(x, y, z))
			:
			new MaybeVectorOrPoint(new Point(x, y, z));
	}

	public static Matrix Identity()
	{
		var m = new Matrix(new double[,]
		{
			{1, 0, 0, 0},
			{0, 1, 0, 0},
			{0, 0, 1, 0},
			{0, 0, 0, 1},
		});
		return m;
	}

	public Matrix Transpose()
	{
		var transpose = new Matrix(Columns, Rows);
		for (var r = 0; r < Rows; r++)
		{
			for (var c = 0; c < Columns; c++)
			{
				transpose[c, r] = this[r, c];
			}
		}

		return transpose;
	}

	public double Determinant()
	{
		if (Rows == 2 && Columns == 2)
		{
			// |a b|
			// |c d|
			// det = ad - bc
			return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
		}
		var det = 0.0;
		for (var c = 0; c < Columns; c++)
		{
			det += this[0, c] * Cofactor(0, c);
		}
		return det;
	}

	public Matrix Submatrix(int rowToRemove, int columnToRemove)
	{
		var m = new Matrix(Rows - 1, Columns - 1);
		var mr = 0;
		for (var r = 0; r < Rows; r++)
		{
			if (r == rowToRemove)
			{
				continue;
			}
			var mc = 0;
			for (var c = 0; c < Columns; c++)
			{
				if (c == columnToRemove)
				{
					continue;
				}
				m[mr, mc] = this[r, c];
				mc++;
			}
			mr++;
		}

		return m;
	}

	public double Minor(int row, int col)
	{
		var sub = Submatrix(row, col);
		return sub.Determinant();
	}

	public double Cofactor(int row, int col)
	{
		var sign = (row + col) % 2 == 0 ? 1.0 : -1.0;
		return Minor(row, col) * sign;
	}

	public bool IsInvertible()
	{
		var det = Determinant();
		return !det.AboutEqual(0);
	}

	public Matrix Inverse()
	{
		var det = Determinant();
		if (det.AboutEqual(0))
		{
			throw new InvalidOperationException("Matrix is not invertible");
		}
		var m2 = new Matrix(Rows, Columns);
		for (var r = 0; r < Rows; r++)
		{
			for (var c = 0; c < Columns; c++)
			{
				var cof = Cofactor(r, c);
				m2[c, r] = cof / det;
			}
		}
		return m2;
	}

	#region Equality stuff

	public bool Equals(Matrix? other)
	{
		if (other is null)
		{
			return false;
		}

		if (ReferenceEquals(this, other))
		{
			return true;
		}

		if (Columns != other.Columns || Rows != other.Rows)
		{
			return false;
		}
		// it didn't look like SequenceEqual works with multidimensional arrays so have to do looping...
		for (var r = 0; r < Rows; r++)
		{
			for (var c = 0; c < Columns; c++)
			{
				if (!this[r, c].AboutEqual(other[r, c]))
				{
					return false;
				}
			}
		}

		return true;
	}

	public override bool Equals(object? obj)
	{
		if (obj is null)
		{
			return false;
		}

		if (ReferenceEquals(this, obj))
		{
			return true;
		}

		if (obj.GetType() != GetType())
		{
			return false;
		}

		return Equals((Matrix)obj);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(_values, Columns, Rows);
	}

	public static bool operator ==(Matrix? left, Matrix? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(Matrix? left, Matrix? right)
	{
		return !Equals(left, right);
	}

	#endregion
}