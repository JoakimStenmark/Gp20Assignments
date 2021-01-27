//This file is only for testing your movement/behavior. //<>//
//This file is only for testing your movement/behavior.
//The Walkers will compete in a different program!

WalkerInterface walker;
PVector walkerPos;

void setup() 
{
	size(500, 500);
	background(0);
	stroke(255);

	walker = new JoaSte();

	walkerPos = walker.getStartPosition(width, height);
}

void draw()
{
	point(walkerPos.x, walkerPos.y);
	walkerPos.add(walker.update());
		
}

void keyPressed()
{

}
