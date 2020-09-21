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
	background(123, 167, 198);	

	long currentTime = millis();
	deltaTime = (currentTime - time) * 0.001f;

	player.update();

	ballManager.update();

	time = currentTime;
}
