using System.Text;

namespace RayTracerChallenge;

public class Canvas(uint width, uint height)
{
	private const char Space = ' ';
	private readonly Color[,] _pixels = new Color[width, height];

	public uint Width { get; } = width;
	public uint Height { get; } = height;

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
			var rowBuffer = new StringBuilder();
			for (var x = 0; x < Width; x++)
			{
				var color = _pixels[x, y];
				var rgb = color.ToRgb();
				rowBuffer.Append(rgb.red).Append(Space)
					.Append(rgb.green).Append(Space)
					.Append(rgb.blue).Append(Space);
			}
			var rowStr = rowBuffer.ToString().Trim();
			_SplitLongLine(sb, rowStr);
		}
	}

	private static void _SplitLongLine(StringBuilder sb, string line)
	{
		const int maxLineLen = 70;
		while (line.Length > maxLineLen)
		{
			var cutoff = maxLineLen;
			while (cutoff > 0 && line[cutoff] != Space)
			{
				cutoff--;
			}
			sb.AppendLine(line[..cutoff]);
			line = line[(cutoff + 1)..];
		}
		sb.AppendLine(line);
	}

	public void SetAllPixels(Color color)
	{
		for (var y = 0; y < Height; y++)
		{
			for (var x = 0; x < Width; x++)
			{
				_pixels[x, y] = color;
			}
		}	
	}
}