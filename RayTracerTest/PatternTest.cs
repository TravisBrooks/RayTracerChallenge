using RayTracerChallenge;
using RayTracerChallenge.Patterns;

namespace RayTracerTest;

public class PatternTest
{
	[Fact]
	public void CreatingStripePattern()
	{
		var pattern = new StripePattern(Color.White, Color.Black);
		Assert.Equal(Color.White, pattern.A);
		Assert.Equal(Color.Black, pattern.B);
	}

	[Fact]
	public void StripePatternIsConstantInY()
	{
		var pattern = new StripePattern(Color.White, Color.Black);
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 0, 0)));
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 1, 0)));
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 2, 0)));
	}

	[Fact]
	public void StripePatternIsConstantInZ()
	{
		var pattern = new StripePattern(Color.White, Color.Black);
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 0, 0)));
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 0, 1)));
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 0, 2)));
	}

	[Fact]
	public void StripePatternAlternatesInX()
	{
		var pattern = new StripePattern(Color.White, Color.Black);
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 0, 0)));
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0.9, 0, 0)));
		Assert.Equal(Color.Black, pattern.PatternAt(new Point(1, 0, 0)));
		Assert.Equal(Color.Black, pattern.PatternAt(new Point(-.1, 0, 0)));
		Assert.Equal(Color.Black, pattern.PatternAt(new Point(-1, 0, 0)));
		Assert.Equal(Color.White, pattern.PatternAt(new Point(-1.1, 0, 0)));
	}

	[Fact]
	public void StripePatternWithObjectTransformation()
	{
		var stripePattern = new StripePattern(Color.White, Color.Black);
		var obj = new Sphere
		{
			Transform = Transformation.Scaling(2, 2, 2),
			Pattern = stripePattern
		};
		var c = stripePattern.PatternAt(obj, new Point(1.5, 0, 0));
		Assert.Equal(Color.White, c);
	}

	[Fact]
	public void StripePatternWithPatternTransformation()
	{
		var stripePattern = new StripePattern(Color.White, Color.Black)
		{
			Transform = Transformation.Scaling(2, 2, 2)
		};
		var obj = new Sphere
		{
			Transform = Transformation.Scaling(2, 2, 2),
			Pattern = stripePattern
		};
		var c = stripePattern.PatternAt(obj, new Point(1.5, 0, 0));
		Assert.Equal(Color.White, c);
	}

	[Fact]
	public void StripePatternWithBothObjectAndPatternTransformations()
	{
		var stripePattern = new StripePattern(Color.White, Color.Black)
		{
			Transform = Transformation.Translation(.5, 0, 0)
		};
		var obj = new Sphere
		{
			Transform = Transformation.Scaling(2, 2, 2),
			Pattern = stripePattern
		};
		var c = stripePattern.PatternAt(obj, new Point(2.5, 0, 0));
		Assert.Equal(Color.White, c);
	}

	[Fact]
	public void DefaultPatternTransformation()
	{
		var pattern = new TestPattern();
		Assert.Equal(Matrix.Identity(), pattern.Transform);
	}

	[Fact]
	public void AssigningPatternTransformation()
	{
		var pattern = new TestPattern
		{
			Transform = Transformation.Translation(1, 2, 3)
		};
		Assert.Equal(Transformation.Translation(1, 2, 3), pattern.Transform);
	}

	[Fact]
	public void PatternAtObjectWithTransformation()
	{
		var shape = new Sphere
		{
			Transform = Transformation.Scaling(2, 2, 2)
		};
		var pattern = new TestPattern();
		var c = pattern.PatternAt(shape, new Point(2, 3, 4));
		Assert.Equal(new Color(1, 1.5, 2), c);
	}

	[Fact]
	public void PatternWithPatternTransformation()
	{
		var shape = new Sphere();
		var pattern = new TestPattern
		{
			Transform = Transformation.Scaling(2, 2, 2)
		};
		var c = pattern.PatternAt(shape, new Point(2, 3, 4));
		Assert.Equal(new Color(1, 1.5, 2), c);
	}

	[Fact]
	public void PatternWithBothObjectAndPatternTransformations()
	{
		var shape = new Sphere
		{
			Transform = Transformation.Scaling(2, 2, 2)
		};
		var pattern = new TestPattern
		{
			Transform = Transformation.Translation(.5, 1, 1.5)
		};
		var c = pattern.PatternAt(shape, new Point(2.5, 3, 3.5));
		Assert.Equal(new Color(.75, .5, .25), c);
	}

	[Fact]
	public void GradientLinearlyInterpolatesBetweenColors()
	{
		var pattern = new GradientPattern(Color.White, Color.Black);
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 0, 0)));
		Assert.Equal(new Color(.75, .75, .75), pattern.PatternAt(new Point(.25, 0, 0)));
		Assert.Equal(new Color(.5, .5, .5), pattern.PatternAt(new Point(.5, 0, 0)));
		Assert.Equal(new Color(.25, .25, .25), pattern.PatternAt(new Point(.75, 0, 0)));
	}

	[Fact]
	public void RingShouldExtendInXAndZ()
	{
		var pattern = new RingPattern(Color.White, Color.Black);
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 0, 0)));
		Assert.Equal(Color.Black, pattern.PatternAt(new Point(1, 0, 0)));
		Assert.Equal(Color.Black, pattern.PatternAt(new Point(0, 0, 1)));
		Assert.Equal(Color.Black, pattern.PatternAt(new Point(0.708, 0, 0.708)));
	}

	[Fact]
	public void CheckersShouldRepeatInX()
	{
		var pattern = new CheckersPattern(Color.White, Color.Black);
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 0, 0)));
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0.99, 0, 0)));
		Assert.Equal(Color.Black, pattern.PatternAt(new Point(1.01, 0, 0)));
	}

	[Fact]
	public void CheckersShouldRepeatInY()
	{
		var pattern = new CheckersPattern(Color.White, Color.Black);
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 0, 0)));
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 0.99, 0)));
		Assert.Equal(Color.Black, pattern.PatternAt(new Point(0, 1.01, 0)));
	}

	[Fact]
	public void CheckersShouldRepeatInZ()
	{
		var pattern = new CheckersPattern(Color.White, Color.Black);
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 0, 0)));
		Assert.Equal(Color.White, pattern.PatternAt(new Point(0, 0, 0.99)));
		Assert.Equal(Color.Black, pattern.PatternAt(new Point(0, 0, 1.01)));
	}
}