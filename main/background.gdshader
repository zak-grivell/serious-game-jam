shader_type canvas_item;
uniform vec2 sunPosition;
uniform sampler2D colourGradient;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
	vec2 flippedUV = UV.gr;
	COLOR = texture(colourGradient, flippedUV);
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
