class CharacterManager
{
	//Human _human;
	//Zombie _zombie;
	//Human[] humans;
	//Zombie[] zombies;
	Character[] characters;

	int startingZombieAmount = 1;
	
	int amountOfZombies;
	int amountOfHumans;

	CharacterManager()
	{
		amountOfZombies = 0;
		characters = new Character[100];
		for (int i = 0; i < characters.length; ++i) 
		{

			if (amountOfZombies < startingZombieAmount) 
			{
				characters[i] = new Zombie(width/2, height/2);
				amountOfZombies++;
				
			}
			else
			{
				characters[i] = new Human(random(width),random(height));
				amountOfHumans++;
			}
		}
	}

	void update()
	{
		CheckHumanZombieCollision();



		for (int i = 0; i < characters.length; ++i) 
		{
			characters[i].CheckEdges();
			characters[i].update();
			characters[i].draw();
		}



	}

	void CheckHumanZombieCollision()
	{
		for (int i = 0; i < characters.length; ++i) 
		{
			if (characters[i] instanceof Zombie) 
			{
				for (int j = 0; j < characters.length; ++j) 
				{
					if (roundCollision(characters[i], characters[j])) 
					{
						if (characters[j] instanceof Human) 
						{

							characters[j] = new Zombie(characters[j].position.x,characters[j].position.y, characters[j].size);
							amountOfHumans--;
							amountOfZombies++;

							if (amountOfHumans == 0) 
							{
								timeSinceFirstInfection = millis();	
								gameOver = true;
							}
						}
					}
				}
			}
			else 
			{
				// println("is Human");
				
			}
		}
	}


	boolean roundCollision(Character one, Character two)
	{
		float maxDistance = one.radius + two.radius;
		
		if(abs(one.position.x - two.position.x) > maxDistance || abs(one.position.y - two.position.y) > maxDistance)
		{
			return false;
		}

		else if(dist(one.position.x, one.position.y, two.position.x, two.position.y) > maxDistance)
		{
			return false;
		}

		else
		{
			return true;
		}

	}

}
