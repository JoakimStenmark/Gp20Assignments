
float time;
float deltaTime;
CharacterManager _characterManager;

int timeSinceFirstInfection = 0;

boolean gameOver = false;

void setup() 
{
	size(1000, 800);
	//c =  new Human(width/2, height/2);
	//z =  new Zombie(width/2, height/2);
	_characterManager = new CharacterManager();
	ellipseMode(CENTER);
}

void draw() 
{

	background(0);
	long currentTime = millis();
	deltaTime = (currentTime - time) * 0.001f;

	_characterManager.update();

	DrawStatusText();
	time = currentTime;
	if (gameOver) 
	{
		DrawGameOverText();
	}
}

void DrawStatusText()
{

	textAlign(LEFT);		
	textSize(32);
	fill(215, 65, 65, 150);
	text("Humans: " + _characterManager.amountOfHumans, 11, 6, width, height);

	textSize(32);
	fill(92, 169, 79, 150);
	text("Zombies: " + _characterManager.amountOfZombies, 11, 39, width, height);

}

void DrawGameOverText()
{

	int seconds = timeSinceFirstInfection/1000;
	int minutes = seconds/60; 

	textAlign(CENTER);
	textSize(64);
	fill(255, 255, 255);
	text("GAME OVER", 14, 200, width, height);
	fill(255, 0, 0);
	text("GAME OVER", 12, 200, width, height);
	fill(255, 255, 255);
	textSize(32);
	text("Time since first infection: " + minutes + " minutes and " + seconds + " seconds", 14, 373, width, height);
	fill(255, 0, 0);
	text("Time since first infection: " + minutes + " minutes and " + seconds + " seconds", 12, 373, width, height);


}
