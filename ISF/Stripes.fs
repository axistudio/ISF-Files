/*
{
  "CATEGORIES" : [
    "Generator"
  ],
  "ISFVSN" : "2",
  "INPUTS" : [
    {
      "NAME" : "width",
      "TYPE" : "float",
      "DEFAULT" : 0.25
    },
    {
      "NAME" : "offset",
      "TYPE" : "float",
      "DEFAULT" : 0
    },
    {
      "NAME" : "vertical",
      "TYPE" : "bool",
      "DEFAULT" : 0
    },
    {
      "NAME" : "color1",
      "TYPE" : "color",
      "DEFAULT" : [
        1,
        1,
        1,
        1
      ]
    },
    {
      "NAME" : "color2",
      "TYPE" : "color",
      "DEFAULT" : [
        0,
        0,
        0,
        1
      ]
    },
    {
      "NAME" : "splitPos",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.5,
      "MIN" : 0
    }
  ],
  "CREDIT" : "VIDVOX"
}
*/



void main() {
	//	determine if we are on an even or odd line
	//	math goes like..
	//	mod(((coord+offset) / width),2)
	
	
	vec4 out_color = color2;
	float coord = isf_FragNormCoord[0];

	if (vertical)	{
		coord = isf_FragNormCoord[1];
	}
	if (width == 0.0)	{
		out_color = color1;
	}
	else if(mod(((coord+offset) / width),2.0) < 2.0 * splitPos)	{
		out_color = color1;
	}
	
	gl_FragColor = out_color;
}