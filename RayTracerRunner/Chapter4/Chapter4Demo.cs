using RayTracerChallenge;

namespace RayTracerRunner.Chapter4;

public class Chapter4Demo : DemoRun
{
	protected override Canvas RunCanvasRender()
	{
		var canvas = new Canvas(1000, 1000);
		var color = Color.FromRgb(100, 149, 237);
		// negative rotation turns us clockwise in inverted y space
		var rotate = Transformation.RotationZ(-30.0.ToRadians());
		// we want to rotate around the origin so to start 12 o'clock is 400 pixels up
		var twelve = new Point(0, 400, 0);
		// instead of the center being in the left corner we want it to be in the center of the canvas
		// this translates twelve to 500 over and 500 up (in inverted y) or (500, 900)
		var translate = Transformation.Translation(500, 500, 0);
		new ClockPoint((translate * twelve).AssumePoint(), color).Draw(canvas);

		var p = twelve;
		for (var i = 0; i<11; i++)
		{
			// breaking up rotate and translate into two steps so we can keep rotating clock points
			// around the origin instead of where they get translated to
			var p1 = (rotate * p).AssumePoint();
			new ClockPoint((translate * p1).AssumePoint(), color).Draw(canvas);
			p = p1;
		}

		return canvas;
	}

}