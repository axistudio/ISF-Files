/*{
	"DESCRIPTION": "Pixel with brightness levels below the threshold do not update.",
	"CREDIT": "by VIDVOX",
	"ISFVSN": "2",
	"CATEGORIES": [
		"Glitch"
	],
	"INPUTS": [
		{
			"NAME": "inputImage",
			"TYPE": "image"
		},
		{
			"NAME": "thresh",
			"LABEL": "Threshold",
			"TYPE": "float",
			"MIN": 0.0,
			"MAX": 1.0,
			"DEFAULT": 0.0
		},
		{
			"NAME": "gain",
			"LABEL": "Gain",
			"TYPE": "float",
			"MIN": 0.0,
			"MAX": 2.0,
			"DEFAULT": 1.0
		},
		{
			"NAME": "hardCutoff",
			"LABEL": "Hard Cutoff",
			"TYPE": "bool",
			"DEFAULT": true
		},
		{
			"NAME": "invert",
			"LABEL": "Invert",
			"TYPE": "bool",
			"DEFAULT": false
		}
	],
	"PASSES": [
		{
			"TARGET":"bufferVariableNameA",
			"FLOAT": true,
			"PERSISTENT": true
		},
		{
		
		}
	]
	
}*/

void main()
{
	vec4		freshPixel = IMG_PIXEL(inputImage,gl_FragCoord.xy);
	vec4		stalePixel = IMG_PIXEL(bufferVariableNameA,gl_FragCoord.xy);
	float		brightLevel = (freshPixel.r + freshPixel.b + freshPixel.g) / 3.0;
	if (invert)
		brightLevel = 1.0 - brightLevel;
	brightLevel = brightLevel * gain;
	if (hardCutoff)	{
		if (brightLevel < thresh)
			brightLevel = 1.0;
		else
			brightLevel = 0.0;
	}
	gl_FragColor = mix(freshPixel,stalePixel, brightLevel);
}
