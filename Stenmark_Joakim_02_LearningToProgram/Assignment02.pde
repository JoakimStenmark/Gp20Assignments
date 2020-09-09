
float x = 0;
float y = 0;
float x2;
float y2;
int lineAmount = 20;
int lineCount = 0;

PVector axis1;
PVector axis2;

ParabolicCurves instance;

void setup()
{
	size(640, 480);
	background(255);
	x2 = width;
	y2 = height;
	//axis1 = new PVector(0, 480);
	//axis2 = new PVector(640, 480);

	//instance = new ParabolicCurves();
}

void draw()
{

	strokeWeight(2);


	if (lineCount <= lineAmount)
	{
		if (lineCount % 3 == 2)
		{
			stroke(0);
		} 
		else
		{
			stroke(127);
		}

		//line(0, y2, x2, height);


		x = x + width/lineAmount;
		y = y + height/lineAmount;
		x2 = x2 - width/lineAmount;
		y2 = y2 - height/lineAmount;
		line(0, y2, x2, height);

		lineCount++;
	}






  //line(0, 0, 64, height);
}
