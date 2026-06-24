shader_type canvas_item;
uniform vec2 sunPosition;
uniform sampler2D upperSkyGradient;
uniform sampler2D lowerSkyGradient;
uniform float DayProgress;
void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
	float timeFactor = 0.5 * sin(DayProgress) + 0.5;
	float interpolant = UV.g;
	vec4 upperSky = texture(upperSkyGradient, vec2(timeFactor, 0));
	vec4 lowerSky = texture(lowerSkyGradient, vec2(timeFactor, 0));
	COLOR = mix(upperSky, lowerSky, interpolant);
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
