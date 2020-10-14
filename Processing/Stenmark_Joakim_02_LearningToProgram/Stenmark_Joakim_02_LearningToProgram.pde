

PVector inputVector1;
PVector inputVector2;
int animFrame = 0;
ParabolicCurves instance;

void setup()
{
	size(640, 480);

	surface.setLocation(1000,515);
	inputVector1 = new PVector(640,0);
	inputVector2 = new PVector(0,0);
	
}

void draw()
{
	background(255);
	strokeWeight(2);

	inputVector1.set(318,240);
	inputVector2.set(0,240);
	instance = new ParabolicCurves(inputVector1, inputVector2, 30, true);
	instance.makeCurve();

	inputVector1.set(321,-80);
	inputVector2.set(320,240);
	instance = new ParabolicCurves(inputVector1, inputVector2, 30, true);
	instance.makeCurve();	

	inputVector1.set(320,-80);
	inputVector2.set(320,240);
	instance = new ParabolicCurves(inputVector1, inputVector2, 30, false);
	instance.makeCurve();

	// inputVector1.set(215,224);
	// inputVector2.set(334,237);
	// instance = new ParabolicCurves(inputVector1, inputVector2, 30, false);
	// instance.makeCurve();	

	// if(animFrame == 300)
	// {
	// 	animFrame=0;
	// }
	// animFrame++;
}
