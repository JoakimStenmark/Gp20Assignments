PVector goalVector;
PVector playerEndVector;
PVector playerStartVector;
PVector startVector;

void setup()
{
	size(512, 512);
	startVector = new PVector(width/2, height/2);
	
	goalVector = new PVector(random(0, width), random(0, height));	
	goalVector.sub(startVector);
	println("goalVector: "+goalVector);
	playerStartVector = new PVector(0,0);
	playerEndVector = new PVector(0,0);

}

void draw()
{
	
	background(50);
	stroke(255);
	fill(0, 0, 0);

	
	if(mousePressed)
	{

		line(playerStartVector.x, playerStartVector.y, constrain(mouseX, 0, width), constrain(mouseY, 0, height));

	}

	line(startVector.x, startVector.y, startVector.x + goalVector.x, startVector.y + goalVector.y);

}

void mousePressed()
{
	playerStartVector.set(mouseX, mouseY);
}

void mouseReleased() 
{
	//PVector temp = new PVector(constrain(mouseX, width * -1, width),constrain(mouseY, height * -1, height));
	PVector temp = new PVector (mouseX,mouseY);
	playerEndVector = PVector.sub(temp, startVector);
	println("playerVector: "+playerEndVector);

	CalculatePoints();

}

void CalculatePoints()
{
	float playerPoints = playerEndVector.mag();
	println("playerPoints: "+playerPoints);
	float goalPoints = goalVector.mag();
	println("goalPoints: "+goalPoints);

	float score = goalPoints - playerPoints;
	if(score < 0)
	{
		score = score * -1;
	}
	println("score: " + score);
	int finalScore = int(constrain(100 - ((score / goalPoints) * 100),0 , 100));
	println("finalScore: " + finalScore);

}
