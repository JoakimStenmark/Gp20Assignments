PVector goalVector;
PVector playerEndVector;
PVector playerStartVector;
PVector startVector;

String infoGoal;

boolean haveGuessed = false;
int finalScore = 0;

void setup()
{
	size(512, 512);
	startVector = new PVector(width/2, height/2);

	SetNewGoal();
	

	playerStartVector = new PVector(0,0);
	playerEndVector = new PVector(0,0);

}

void draw()
{
	
	background(50);
	stroke(255);
	fill(219, 200, 255);
	text("Vector length guessing game!", 206, 43);
	text(infoGoal, 120, 57);
	
	if (haveGuessed == false) 
	{
		if(mousePressed)
		{

			line(playerStartVector.x, playerStartVector.y, constrain(mouseX, 0, width), constrain(mouseY, 0, height));

		}
	}
	if (haveGuessed)
	{
		text("Your guess was " + finalScore + " % right", 120, 77);
		text("Press spacebar to try again", 120, 97);
	}

//line for debug
//line(startVector.x, startVector.y, startVector.x + goalVector.x, startVector.y + goalVector.y);

}

void mousePressed()
{

	if (haveGuessed == false) 
	{
		playerStartVector.set(mouseX, mouseY);

	}

}

void mouseReleased() 
{
	

	if (haveGuessed == false) 
	{
		PVector temp = new PVector (mouseX,mouseY);
		playerEndVector = PVector.sub(temp, startVector);
		println("playerVector: "+playerEndVector);

		CalculatePoints();
		haveGuessed = true;
	}
	//PVector temp = new PVector(constrain(mouseX, width * -1, width),constrain(mouseY, height * -1, height));


}

void keyPressed()
{
		if (haveGuessed) 
	{

		SetNewGoal();
		haveGuessed = false;

	}
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
	finalScore = int(constrain(100 - ((score / goalPoints) * 100),0 , 100));
	println("finalScore: " + finalScore);


}

void SetNewGoal()
{
	goalVector = new PVector(int(random(0, width)), int(random(0, height)));	
	goalVector.sub(startVector);
	println("goalVector: "+goalVector);
	infoGoal = "Draw the length of this vector: " + goalVector;
}
