/*
{
  "CATEGORIES" : [
    "Geometry Adjustment"
  ],
  "DESCRIPTION" : "Warps an image to fit in a triangle by fitting the height of the image to the height of a triangle",
  "ISFVSN" : "2",
  "INPUTS" : [
    {
      "NAME" : "inputImage",
      "TYPE" : "image"
    },
    {
      "NAME" : "peakPoint",
      "TYPE" : "point2D",
      "MAX" : [
        1,
        1
      ],
      "DEFAULT" : [
        0.5,
        1
      ],
      "MIN" : [
        0,
        0
      ]
    },
    {
      "NAME" : "distortX",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0,
      "MIN" : 0
    }
  ],
  "CREDIT" : "VIDVOX"
}
*/

void main()	{
	vec4		inputPixelColor = vec4(0.0);
	vec2		pt = isf_FragNormCoord;
	float		val = 0.0;
	
	if (pt.x < peakPoint.x)	{
		pt.x = pt.x * 0.5 / peakPoint.x;
		val = 2.0 * pt.x * peakPoint.y;
	}
	else	{
		pt.x = 0.5 + 0.5 * (pt.x - peakPoint.x) / (1.0 - peakPoint.x);
		val = (2.0 - 2.0 * pt.x) * peakPoint.y;
	}
	if (pt.y <= val)	{
		pt.x = mix(isf_FragNormCoord.x,pt.x,distortX);
		pt.y /= val;
		inputPixelColor = IMG_NORM_PIXEL(inputImage, pt);
	}
	
	gl_FragColor = inputPixelColor;
}
