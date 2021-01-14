int frame = 0;
float multiplier = 0.002;
int numberOfPoints = 320;

void setup()
{
  size(640, 480);
  stroke(255);
  strokeWeight(5);
}

void draw()
{
  background(0);
  line(0, height * 0.5f, width, height * 0.5f);

  //Draw animated point
  point(100, 240 + sin(frame * 0.04) * 100);

  frame++;
}
