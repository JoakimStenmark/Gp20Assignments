float time;
float deltaTime;

Player player;
BallManager ballManager;

void setup() 
{
	size(640, 480);

	player = new Player();
	ballManager = new BallManager(10);
	ellipseMode(CENTER);
	
}

void draw() 
{
	ClearBackground();	

	long currentTime = millis();
	deltaTime = (currentTime - time) * 0.001f;

	player.update();

	ballManager.update();

	time = currentTime;
}

void ClearBackground()
{
	fill(123, 167, 198, 100);
	rect(0, 0, width, height);
	fill(123, 167, 198);
}
