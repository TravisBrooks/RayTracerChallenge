using System.Drawing;
using System.Text;

namespace RayTracerChallenge
{
	public class Canvas(int width, int height)
	{
		private readonly Color[,] _pixels = new Color[width, height];

		public int Width { get; } = width;
		public int Height { get; } = height;

		public Color this[int x, int y]
		{
			get => _pixels[x, y];
			set => _pixels[x, y] = value;
		}

		public string ToPpm()
		{
			var sb = new StringBuilder();
			WritePpmHeader(sb);
			WritePpmBody(sb);
			return sb.ToString();
		}

		private StringBuilder WritePpmHeader(StringBuilder sb)
		{
			// Magic number, then width and height, then max color value
			return sb.AppendLine("P3")
				.AppendLine($"{Width} {Height}")
				.AppendLine("255");
		}

		private void WritePpmBody(StringBuilder sb)
		{
			for (var y = 0; y < Height; y++)
			{
				var rowOfColors = new Color[Width];
				for (var x = 0; x < Width; x++)
				{
					rowOfColors[x] = _pixels[x, y];
				}
				_PpmColorRow(sb, rowOfColors);
			}
		}

		private void _PpmColorRow(StringBuilder sb, Color[] rowOfColors)
		{
			var rowBuffer = new StringBuilder();
			foreach (var cRow in rowOfColors)
			{
				var rgb = cRow.ToPpmColor();
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
	}
}
