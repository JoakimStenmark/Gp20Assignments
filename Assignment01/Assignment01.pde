float lineX1 = 200;
float lineX2 = 497;

float jX = 240;
float jY = 190;

float oX = 282;
float oY = 185;

float aX = 326;
float aY = 185;

float kX = 364;
float kY = 144;

float iX = 402;
float iY = 178;

float mX = 416;
float mY = 169;

float speed = 15;

boolean lineComplete = false;

float minRandom = 0;
float maxRandom = 0;

void setup() 
{
  size(768, 432);
}

void draw() 
{
  background(0);

  if (lineX1 < 349 && lineX2 > 349)
  {
    DrawCountdownLine();
    maxRandom = maxRandom + 0.1 * 1.06;
    minRandom = minRandom - 0.1 * 1.06;
  } 
  else
  {
    lineComplete = true;
  }

  if (lineComplete)	
  {
    jX = MoveThing(jX, -1);

    oX = MoveThing(oX, -1);
    oY = MoveThing(oY, -0.5);

    aX = MoveThing(aX, -0.5);
    aY = MoveThing(aY, -1);

    kX = MoveThing(kX, 0.5);
    kY = MoveThing(kY, -1);

    iX = MoveThing(iX, 1);
    iY = MoveThing(iY, -0.5);

    mX = MoveThing(mX, 1);

  }

  noFill();
  
  DrawJ(jX + random(minRandom, maxRandom), jY + random(minRandom, maxRandom));

  DrawO(oX + random(minRandom, maxRandom), oY + random(minRandom, maxRandom));

  DrawA(aX + random(minRandom, maxRandom), aY + random(minRandom, maxRandom));

  DrawK(kX + random(minRandom, maxRandom), kY + random(minRandom, maxRandom));

  DrawI(iX + random(minRandom, maxRandom), iY + random(minRandom, maxRandom));

  DrawM(mX + random(minRandom, maxRandom), mY + random(minRandom, maxRandom));
}

void DrawCountdownLine()
{
  stroke(202, 25, 25);
  strokeWeight(2.5);
  line(lineX1, 215, lineX2, 215);
  lineX1++;
  lineX2--;
}

float MoveThing(float x, float xDirection)
{
  x = x + xDirection * speed;
  return x;
}

void DrawJ(float x, float y)
{

  stroke(202, 25, 25);
  strokeWeight(2.5);

  arc(x, y, 25, 25, 0, PI);
  line(x + 12, y - 46, x + 12, y - 2);
}

void DrawO(float x, float y)
{
  stroke(130, 13, 229);
  strokeWeight(2.5);

  ellipse(x, y, 35, 35);
}
void DrawA(float x, float y)
{
  stroke(25, 125, 202);
  strokeWeight(2.5);

  ellipse(x, y, 35, 35);
  line(x+13, y-12, x+30, y+15);
}
void DrawK(float x, float y)
{	
  stroke(25, 202, 46);
  strokeWeight(2.5);

  line(x, y, x, y+56);
  line(x+20, y+24, x, y+42);
  line(x+1, y+41, x+18, y+56);
}
void DrawI(float x, float y)
{
  stroke(192, 202, 25);
  strokeWeight(2.5);

  line(x, y, x, y+22);
  fill(8, 8, 8);
  ellipse(x, y-11, 6, 8);
}
void DrawM(float x, float y)
{
  stroke(202, 100, 25);
  strokeWeight(2.5);

  line(x, y, x, y+31);

  noFill();

  arc(x+13, y+10, 25, 23, -PI, 0);
  arc(x+38, y+10, 25, 23, -PI, 0);
  line(x+25, y+12, x+25, y+31);
  line(x+50, y+12, x+50, y+31);
}
