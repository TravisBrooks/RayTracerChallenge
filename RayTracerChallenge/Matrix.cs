using System;

namespace RayTracerChallenge
{
    public class Matrix : IEquatable<Matrix>
    {
        private double[,] _matrix;

        public Matrix(int width, int height)
        {
            _matrix = new double[width, height];
        }

        public Matrix(double[,] data)
        {
            _matrix = new double[data.GetLength(0), data.GetLength(1)];
            Array.Copy(data, _matrix, data.Length);
        }

        public int Width => _matrix.GetLength(0);

        public int Height => _matrix.GetLength(1);

        public double this[int x, int y]
        {
            get => _matrix[x, y];
            set => _matrix[x, y] = value;
        }

        public static Matrix Identity(int dimensions = 4)
        {
            var m = new Matrix(dimensions, dimensions);
            for(var d=0; d < dimensions; d++)
            {
                m[d, d] = 1;
            }

            return m;
        }

        public Matrix Transpose()
        {
            var transpose = new Matrix(Height, Width);
            for (var w = 0; w < Width; w++)
            {
                for (var h = 0; h < Height; h++)
                {
                    transpose[h, w] = this[w, h];
                }
            }

            return transpose;
        }

        public double Determinant()
        {
            double determinant;
            if (Width == Height && Height == 2)
            {
                var a = this[0, 0];
                var b = this[0, 1];
                var c = this[1, 0];
                var d = this[1, 1];
                determinant = (a * d) - (b * c);
            }
            else
            {
                determinant = 0.0;
                for (var h = 0; h < Height; h++)
                {
                    var cofactor = Cofactor(0, h);
                    var cofactorProd = this[0, h] * cofactor;
                    determinant += cofactorProd;
                }
            }

            return determinant;
        }

        public Matrix SubMatrix(int rowIdx, int colIdx)
        {
            var subM = new Matrix(Width - 1, Height - 1);
            for (int w = 0, subW = 0; w < Width; w++)
            {
                if (w == rowIdx)
                {
                    continue;
                }
                for (int h = 0, subH = 0; h < Height; h++)
                {
                    if (h == colIdx)
                    {
                        continue;
                    }
                    subM[subW, subH] = this[w, h];
                    subH++;
                }
                subW++;
            }

            return subM;
        }

        public double Minor(int rowIdx, int colIdx)
        {
            var subMatrix = SubMatrix(rowIdx, colIdx);
            var subDeterminant = subMatrix.Determinant();
            return subDeterminant;
        }

        public double Cofactor(int rowIdx, int colIdx)
        {
            var even = (rowIdx + colIdx) % 2 == 0;
            var minor = Minor(rowIdx, colIdx);
            if (even)
            {
                return minor;
            }
            return -minor;
        }

        public bool IsInvertible()
        {
            var d = Determinant();
            var aboutZero = d.AboutEqual(0);
            return !aboutZero;
        }

        public Matrix Inverse()
        {
            if (IsInvertible() == false)
            {
                throw new Exception("This matrix is not invertible");
            }

            var inversion = new Matrix(Width, Height);
            var determinant = Determinant();
            for (var w = 0; w < Width; w++)
            {
                for (var h = 0; h < Height; h++)
                {
                    var cofactor = Cofactor(w, h);
                    inversion[h, w] = cofactor / determinant;
                }
            }

            return inversion;
        }

        public bool Equals(Matrix other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Width != other.Width || Height != other.Height)
            {
                return false;
            }

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    if (_matrix[x, y].AboutEqual(other[x, y]) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        bool IEquatable<Matrix>.Equals(Matrix other)
        {
            return Equals(other);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Matrix)obj);
        }

        public override int GetHashCode()
        {
            return (_matrix != null ? _matrix.GetHashCode() : 0);
        }

        public static bool operator ==(Matrix left, Matrix right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Matrix left, Matrix right)
        {
            return !Equals(left, right);
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            // I didn't follow what book said about matrix mult, code below is taken almost directly from the Definition found at https://en.wikipedia.org/wiki/Matrix_multiplication
            var m = a.Width;
            var n = a.Height;
            var otherN = b.Width;
            var p = b.Height;

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

        public static Tuple operator *(Matrix a, in Tuple t)
        {
            if (a.Height != 4)
            {
                throw new ArgumentException("To multiply Matrix*Tuple the matrix must have a Height==4.");
            }

            var tAsMatrix = t.AsMatrix();
            var m = a * tAsMatrix;
            var mAsTuple = new Tuple(m[0, 0], m[1, 0], m[2, 0], m[3, 0]);
            return mAsTuple;
        }

    }
}
