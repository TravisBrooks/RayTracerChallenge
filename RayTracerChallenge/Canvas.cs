using System.Text;

namespace RayTracerChallenge
{
    public class Canvas
    {
        private readonly FColor[,] _colorArr;

        public Canvas(int width, int height)
        {
            _colorArr = new FColor[width, height];
        }

        public int Width => _colorArr.GetLength(0);

        public int Height => _colorArr.GetLength(1);

        public void WritePixel(int xIndex, int yIndex, FColor color)
        {
            if (xIndex >= 0 && xIndex < Width && yIndex >= 0 && yIndex < Height)
            {
                _colorArr[xIndex, yIndex] = color;
            }
        }

        public FColor PixelAt(int xIndex, int yIndex)
        {
            var c = _colorArr[xIndex, yIndex];
            return c;
        }

        public void SetEveryPixel(in FColor color)
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    _colorArr[x, y] = color;
                }
            }
        }

        public string ToPpm()
        {
            var sb = new StringBuilder();
            _PpmHeader(sb);

            for (var y = 0; y < Height; y++)
            {
                var rowOfColors = new FColor[Width];
                for (var x = 0; x < Width; x++)
                {
                    rowOfColors[x] = PixelAt(x, y);
                }
                _PpmColorRow(sb, rowOfColors);
            }

            var ppm = sb.ToString();
            return ppm;
        }

        private void _PpmColorRow(StringBuilder sb, FColor[] rowOfColors)
        {
            var rowBuffer = new StringBuilder();
            foreach (var fColor in rowOfColors)
            {
                var rgb = fColor.ToPpmColor();
                rowBuffer.Append(rgb.red);
                rowBuffer.Append(" ");
                rowBuffer.Append(rgb.green);
                rowBuffer.Append(" ");
                rowBuffer.Append(rgb.blue);
                rowBuffer.Append(" ");
            }

            var rowStr = rowBuffer.ToString();
            _SplitLongLine(sb, rowStr);
        }


        private void _SplitLongLine(StringBuilder sb, string line)
        {
            line = line.Trim();
            const int maxLineLen = 70;
            if (line.Length <= maxLineLen)
            {
                sb.AppendLine(line);
            }
            else
            {
                var sp = ' ';
                for (var cutoff = maxLineLen; cutoff >= 0; cutoff--) 
                {
                    if (line[cutoff] == sp)
                    {
                        var subLhs = line.Substring(0, cutoff);
                        var subRhs = line.Substring(cutoff);
                        sb.AppendLine(subLhs);
                        _SplitLongLine(sb, subRhs);
                        break;
                    }
                }
            }
        }

        private void _PpmHeader(StringBuilder sb)
        {
            sb.AppendLine("P3");
            sb.AppendLine($"{Width} {Height}");
            sb.AppendLine("255");
        }
    }
}
