using System.Text;

namespace RayTracerChallenge
{
	public class Canvas(int width, int height)
	{
		private const char Space = ' ';
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

		private void WritePpmHeader(StringBuilder sb)
		{
			// Magic number, then width and height, then max color value
			sb.AppendLine("P3")
				.Append(Width).Append(Space).Append(Height)
				.AppendLine()
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
				var rgb = cRow.ToRgb();
				rowBuffer.Append(rgb.red);
				rowBuffer.Append(Space);
				rowBuffer.Append(rgb.green);
				rowBuffer.Append(Space);
				rowBuffer.Append(rgb.blue);
				rowBuffer.Append(Space);
			}

			var rowStr = rowBuffer.ToString();
			_SplitLongLine(sb, rowStr);
		}

		private static void _SplitLongLine(StringBuilder sb, string line)
		{
			line = line.Trim();
			const int maxLineLen = 70;
			if (line.Length <= maxLineLen)
			{
				sb.AppendLine(line);
			}
			else
			{
				for (var cutoff = maxLineLen; cutoff >= 0; cutoff--)
				{
					if (line[cutoff] == Space)
					{
						var subLhs = line[..cutoff];
						var subRhs = line[cutoff..];
						sb.AppendLine(subLhs);
						_SplitLongLine(sb, subRhs);
						break;
					}
				}
			}
		}
	}
}
