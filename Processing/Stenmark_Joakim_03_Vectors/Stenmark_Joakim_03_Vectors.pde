PVector circlePos;
PVector circleVelocity;

float speed = 2.0;

float weight = 75;

void setup()
{
	size(512, 512);
	circlePos = new PVector(width/2, height/2);
	circleVelocity = new PVector(0,0);
}

void draw()
{
	background(196);
	stroke(132, 81, 189);
	strokeWeight(2);
	if(mousePressed)
	{
		
		PVector newPosition = new PVector(mouseX, mouseY);
		//set velocity
		circleVelocity = PVector.sub(newPosition, circlePos);
		
		speed = circleVelocity.mag()/weight;
		line(circlePos.x, circlePos.y, mouseX, mouseY);

	}
	//make direction
	circleVelocity.normalize();
	circlePos.add(circleVelocity.mult(speed));
	speed *= 0.95;
	CheckBounce();
	ellipse(circlePos.x, circlePos.y, 50, 50);
	//saveFrame();
}

void CheckBounce()
{
		if((circlePos.x > width) || (circlePos.x < 0))
		{

			circleVelocity.x = circleVelocity.x * -1;

		}
		if((circlePos.y > height) || (circlePos.y < 0))
		{
			circleVelocity.y = circleVelocity.y * -1;
		}
}
