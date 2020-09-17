int playerSize = 40;
float playerWidth = playerSize/2; 
PVector playerPos;
PVector direction;
PVector velocity;
float speed = 0;
float acceleration = 200;
float drag = 0.95;

PVector gravity;

float time;
float deltaTime;

void setup() 
{
  size(640, 480);
  
  playerPos = new PVector(width/2, height/2);	
  velocity = new PVector(0, 0);
  direction = new PVector(0, 0);
  gravity = new PVector(0, 25);
  frameRate(60);
  
}

void draw() 
{
  background(123, 167, 198);

  long currentTime = millis();
  deltaTime = (currentTime - time) * 0.001f;

  ReadDirection();

  MoveObject();

  CheckEdges();

  ellipse(playerPos.x, playerPos.y, playerSize, playerSize);
  
  ApplyDrag();

  time = currentTime;
}

void MoveObject()
{
  if (keyPressed) 
  {
    speed += acceleration * deltaTime;
  }
  
  //morecontrol begränsar vilka riktningar som man kan styra bollen i på nåt vis 
  //MoreControl();

  velocity.add(direction.mult(speed));

  if(gravityOn)
  {
    velocity.add(gravity);
  }
  PVector move = velocity.copy();
  playerPos.add(move.mult(deltaTime));
}

void MoreControl()
{

  velocity.normalize();
  velocity.mult(speed);
}

void ApplyDrag()
{
  speed *= drag;
  velocity.mult(drag);
}

void CheckEdges()
{
  if (playerPos.y > height-playerWidth)
  {
    velocity.y *= -1;
    playerPos.y = height - playerWidth;
  }
  else if (playerPos.y < 0 + playerWidth)
  {
    velocity.y *= -1;
    playerPos.y = 0 + playerWidth;
  }

  if (playerPos.x < 0 - playerWidth)
  {
    playerPos.x = width + playerWidth;
  }
  else if (playerPos.x > width + playerWidth)
  {
    playerPos.x = 0 - playerWidth;
  }
}


void ReadDirection()
{
  direction.set (0, 0);

  if (moveLeft) 
  {
    direction.add(-1, 0);
  }
  if (moveRight)
  {
    direction.add(1, 0);
  }

  if (moveUp) 
  {
    direction.add(0, -1);
    //gravity.set(0,1);
  }
  if (moveDown)
  {
    direction.add(0, 1);
  }

  direction.normalize();
}
