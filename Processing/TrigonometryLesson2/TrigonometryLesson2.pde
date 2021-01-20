int frame = 0;
float speed = 0.011f;
float offset = 1.0f;
void setup()
{
	size(640, 480);
}

void draw()
{ 
	int numberOfPoints = 330;

	float angleSpacingY = (PI/numberOfPoints);
	
	PVector pos = new PVector(width * 0.5, height * 0.5f);
	color pointColor1 = color(199, 57, 57);
	color pointColor2 = color(66, 203, 43);

	float rad = 170.2;
	

	background(0);  //<>// //<>//
	strokeWeight(1);  //<>// //<>//
	stroke(255);
	line(0, height * 0.5f, width, height * 0.5f);
	strokeWeight(5); 
	stroke(pointColor1);
	point(pos.x, pos.y);

	//PointCircle(pos, rad, numberOfPoints);
	PointSpiral(pos, frame, numberOfPoints);

	frame++;
}

void PointSpiral(PVector pos, float radius, int numberOfPoints)
{
	if (numberOfPoints == 0) 
	{
		return;
	}

	for (int i = 0; i < numberOfPoints; i++) 
	{
		float angleSpacing = (2*PI/numberOfPoints) + (0.0005f * frame);
		point(pos.x + cos(i * angleSpacing) * i+ offset, pos.y + sin(i * angleSpacing) * i + offset); 

	}
}

void PointCircle(PVector pos, float radius, int numberOfPoints)
{
	if (numberOfPoints == 0) 
	{
		return;
	}

	for (int i = 0; i < numberOfPoints; i++) 
	{
		float angleSpacing = 2*PI/numberOfPoints;
		point(pos.x + cos(i * angleSpacing) * radius, pos.y + sin(i * angleSpacing) * radius); 

	}
}
