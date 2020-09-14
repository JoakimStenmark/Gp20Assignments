class ParabolicCurves
{
	
	PVector axis1;
	PVector axis2;

	int numberOfLines;
	int lineCount = 0;
	//float maxSize = 0;
	boolean mode = true;
	ParabolicCurves(PVector newAx1,PVector newAx2, int lines, boolean b)
	{
		axis1 = new PVector(0,0);
		axis2 = new PVector(0,0);
		axis1.set(newAx1);
		axis2.set(newAx2);
		numberOfLines = lines;
		mode = b;

		//println("highestValue: "+highestValue);
	}

	void makeCurve()
	{

		float[] values = {axis1.x, axis1.y, axis2.x, axis2.y};
		float highestValue = max(values);
		if (mode) 
		{

			for (int i = 0; i < numberOfLines; i++) 
			{
				stroke(127);
				
				if (i % 3 == 2)
				{
					stroke(0);
				} 


				//vänstra nedre hörnet och motsatt sida
				axis1.y += highestValue/numberOfLines;
				axis2.x += highestValue/numberOfLines;
				line(axis1.x, axis1.y, axis2.x, axis2.y);
				
			}
			
		}
		else 
		{
			for (int i = 0; i < numberOfLines; i++) 
			{
				stroke(127);
				
				if (i % 3 == 2)
				{
					stroke(0);
				} 

				//högra nedre hörnet och motsatt sida
				axis1.y += highestValue/numberOfLines;
				axis2.x -= highestValue/numberOfLines;
				line(axis1.x, axis1.y, axis2.x, axis2.y);


			}
		}


	}

}
