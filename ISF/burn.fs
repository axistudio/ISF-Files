/*{
    "CATEGORIES": [
        "Transition"
    ],
    "CREDIT": null,
    "DESCRIPTION": "Automatically converted from https://gl-transitions.com/",
    "INPUTS": [
        {
            "NAME": "startImage",
            "TYPE": "image"
        },
        {
            "NAME": "endImage",
            "TYPE": "image"
        },
        {
            "DEFAULT": 0,
            "MAX": 1,
            "MIN": 0,
            "NAME": "progress",
            "TYPE": "float"
        },
        {
            "DEFAULT": [
                1,
                1,
                1,
                1
            ],
            "NAME": "color",
            "TYPE": "color"
        }
    ],
    "ISFVSN": "2",
    "VSN": null
}
*/



vec4 getFromColor(vec2 inUV)	{
	return IMG_NORM_PIXEL(startImage, inUV);
}
vec4 getToColor(vec2 inUV)	{
	return IMG_NORM_PIXEL(endImage, inUV);
}



// author: gre
// License: MIT
vec4 transition (vec2 uv) {
  return mix(
    getFromColor(uv) + progress*color,
    getToColor(uv) + (1.0-progress)*color,
    progress
  );
}



void main()	{
	gl_FragColor = transition(isf_FragNormCoord.xy);
}