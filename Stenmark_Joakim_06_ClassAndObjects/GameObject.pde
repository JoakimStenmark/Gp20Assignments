class GameObject
{
	PVector position;
    PVector velocity;

    GameObject()
    {
		position = new PVector(width/2, height/2);	
		velocity = new PVector(0, 0);
    }
}
