/*
{
  "CATEGORIES" : [
    "Stylize",
    "Glitch"
  ],
  "DESCRIPTION" : "Stretches the edges out a region of the video",
  "INPUTS" : [
    {
      "NAME" : "inputImage",
      "TYPE" : "image"
    },
    {
      "MIN" : 0,
      "IDENTITY" : 0,
      "DEFAULT" : 0.25,
      "LABEL" : "Left Edge",
      "TYPE" : "float",
      "MAX" : 1,
      "NAME" : "leftEdge"
    },
    {
      "MIN" : 0,
      "IDENTITY" : 1,
      "DEFAULT" : 0.75,
      "LABEL" : "Right Edge",
      "TYPE" : "float",
      "MAX" : 1,
      "NAME" : "rightEdge"
    },
    {
      "MIN" : 0,
      "IDENTITY" : 0,
      "DEFAULT" : 0.25,
      "LABEL" : "Bottom Edge",
      "TYPE" : "float",
      "MAX" : 1,
      "NAME" : "bottomEdge"
    },
    {
      "MIN" : 0,
      "IDENTITY" : 1,
      "DEFAULT" : 0.75,
      "LABEL" : "Top Edge",
      "TYPE" : "float",
      "MAX" : 1,
      "NAME" : "topEdge"
    },
    {
      "NAME" : "doHorizontal",
      "TYPE" : "bool",
      "DEFAULT" : true,
      "LABEL" : "Horizontal Bleed"
    },
    {
      "NAME" : "doVertical",
      "TYPE" : "bool",
      "DEFAULT" : true,
      "LABEL" : "Vertical Bleed"
    },
    {
      "NAME" : "insideBleed",
      "TYPE" : "bool",
      "DEFAULT" : true,
      "LABEL" : "Inside Bleed"
    },
    {
      "NAME" : "outsideBleed",
      "TYPE" : "bool",
      "DEFAULT" : true,
      "LABEL" : "Outside Bleed"
    }
  ],
  "ISFVSN" : "2",
  "CREDIT" : "VIDVOX"
}
*/

void main()	{
	vec4		outputPixelColor;
	float		realLeftEdge = (leftEdge < rightEdge) ? leftEdge : rightEdge;
	float		realRightEdge = (leftEdge > rightEdge) ? leftEdge : rightEdge;
	
	float		realBottomEdge = (bottomEdge < topEdge) ? bottomEdge : topEdge;
	float		realTopEdge = (bottomEdge > topEdge) ? bottomEdge : topEdge;
	
	vec2		sampleCoord = isf_FragNormCoord.xy;

	bool		insideBox = false;
	
	vec2		region = vec2(0.0,0.0);
	
	if (sampleCoord.x < realLeftEdge)	{
		region.x = 0.0;
	}
	else if (sampleCoord.x > realRightEdge)	{
		region.x = 2.0;
	}
	else {
		region.x = 1.0;
	}
	
	if (sampleCoord.y < realBottomEdge)	{
		region.y = 0.0;
	}
	else if (sampleCoord.y > realTopEdge)	{
		region.y = 2.0;
	}
	else {
		region.y = 1.0;
	}
	
	if ((region.x == 1.0) && (region.y == 1.0) && (insideBleed == true))	{
		insideBox = true;
	}
	else if (outsideBleed == true)	{
		//	if we are in the bottom left...
		if ((region.x == 0.0) && (region.y == 0.0))	{
			if ((doHorizontal) && (doVertical))	{
				insideBox = true;
				realRightEdge = realLeftEdge;
				realLeftEdge = 0.0;
				realTopEdge = realBottomEdge;
				realBottomEdge = 0.0;
			}
		}
		else if ((region.x == 1.0) && (region.y == 0.0))	{
			if ((doHorizontal) && (doVertical))	{
				insideBox = true;
				realTopEdge = realBottomEdge;
				realBottomEdge = 0.0;
			}
			else if (doVertical)	{
				insideBox = true;
				realTopEdge = realBottomEdge;
				realBottomEdge = 0.0;
			}
		}
		else if ((region.x == 2.0) && (region.y == 0.0))	{
			if ((doHorizontal) && (doVertical))	{
				insideBox = true;
				realLeftEdge = realRightEdge;
				realRightEdge = 1.0;
				realTopEdge = realBottomEdge;
				realBottomEdge = 0.0;
			}
		}
		else if ((region.x == 0.0) && (region.y == 1.0))	{
			if (doHorizontal)	{
				insideBox = true;
				realRightEdge = realLeftEdge;
				realLeftEdge = 0.0;
			}
		}
		else if ((region.x == 2.0) && (region.y == 1.0))	{
			if (doHorizontal)	{
				insideBox = true;
				realLeftEdge = realRightEdge;
				realRightEdge = 1.0;
			}
		}
		else if ((region.x == 0.0) && (region.y == 2.0))	{
			if ((doHorizontal) && (doVertical))	{
				insideBox = true;
				realRightEdge = realLeftEdge;
				realLeftEdge = 0.0;
				realBottomEdge = realTopEdge;
				realTopEdge = 1.0;
			}
		}
		else if ((region.x == 1.0) && (region.y == 2.0))	{
			if (doVertical)	{
				insideBox = true;
				realBottomEdge = realTopEdge;
				realTopEdge = 1.0;
			}
		}
		else if ((region.x == 2.0) && (region.y == 2.0))	{
			if ((doHorizontal) && (doVertical))	{
				insideBox = true;
				realLeftEdge = realRightEdge;
				realRightEdge = 1.0;
				realBottomEdge = realTopEdge;
				realTopEdge = 1.0;
			}
		}
	}
	
	//	if we're doing inside bleed
	if (insideBox)	{
		
		//	how close are we to each edge?			
		if ((doHorizontal) && (doVertical))	{
			float	leftDistance = sampleCoord.x - realLeftEdge;
			float	rightDistance = realRightEdge - sampleCoord.x;
			float	bottomDistance = sampleCoord.y - realBottomEdge;
			float	topDistance = realTopEdge - sampleCoord.y;
			float	totalDistance = (leftDistance + rightDistance + bottomDistance + topDistance);
			
			vec4	leftPixel = IMG_NORM_PIXEL(inputImage, vec2(realLeftEdge, sampleCoord.y));
			vec4	rightPixel = IMG_NORM_PIXEL(inputImage, vec2(realRightEdge, sampleCoord.y));
			vec4	bottomPixel = IMG_NORM_PIXEL(inputImage, vec2(sampleCoord.x, realBottomEdge));
			vec4	topPixel = IMG_NORM_PIXEL(inputImage, vec2(sampleCoord.x, realTopEdge));
				
			outputPixelColor = (rightDistance * leftPixel + leftDistance * rightPixel + topDistance * bottomPixel + bottomDistance * topPixel) / totalDistance;
		}
		else if (doHorizontal)	{
			float	leftDistance = sampleCoord.x - realLeftEdge;
			float	rightDistance = realRightEdge - sampleCoord.x;
			float	totalDistance = leftDistance + rightDistance;
			vec4	leftPixel = IMG_NORM_PIXEL(inputImage, vec2(realLeftEdge, sampleCoord.y));
			vec4	rightPixel = IMG_NORM_PIXEL(inputImage, vec2(realRightEdge, sampleCoord.y));
			outputPixelColor = (rightDistance * leftPixel + leftDistance * rightPixel) / totalDistance;
		}
		else if (doVertical)	{
			float	bottomDistance = sampleCoord.y - realBottomEdge;
			float	topDistance = realTopEdge - sampleCoord.y;
			float	totalDistance = bottomDistance + topDistance;
			vec4	bottomPixel = IMG_NORM_PIXEL(inputImage, vec2(sampleCoord.x, realBottomEdge));
			vec4	topPixel = IMG_NORM_PIXEL(inputImage, vec2(sampleCoord.x, realTopEdge));
			outputPixelColor = (topDistance * bottomPixel + bottomDistance * topPixel) / totalDistance;	
		}
		else	{
			outputPixelColor = IMG_NORM_PIXEL(inputImage, sampleCoord);	
		}
	}
	else	{
		outputPixelColor = IMG_NORM_PIXEL(inputImage, sampleCoord);
	}
	
	gl_FragColor = outputPixelColor;
}
