int frame = 0;
void setup()
{
	size(640, 480);
}

void draw()
{
	color pointColor1 = color(199, 57, 57);
	color pointColor2 = color(66, 203, 43);
	float speed = 0.011;
	float depth = 120;
	int numberOfPoints = 128;
	float pointSpacingX = 10.25;
	float pointSpacingY = 0.07;

	background(0);
	strokeWeight(1);
	stroke(255);
	line(0, height * 0.5f, width, height * 0.5f);
	strokeWeight(5);
	stroke(pointColor1);
	for (int i = 0; i < numberOfPoints; i++)
	{ 

		point(i * pointSpacingX, 240 + sin(i * pointSpacingY + frame * speed) * depth);
	}

	stroke(pointColor2);

	for (int i = 0; i < numberOfPoints; i++) 
	{

		point(i * pointSpacingX, 240 + cos(i * pointSpacingY + frame * speed) * depth);
	}

	frame++;
}
