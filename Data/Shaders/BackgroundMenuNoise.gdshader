shader_type canvas_item;

uniform float noise_scale = 1.0;
uniform vec4 color1 : source_color;
uniform vec4 color2 : source_color;
uniform vec4 color3 : source_color;
uniform vec4 color4 : source_color;
uniform vec4 cursor_position;

void fragment() {
	if (COLOR.r < 0.5) {
		COLOR = color1;
	}
	else if (COLOR.r < 0.66) {
		COLOR = color2;
	}
	else if (COLOR.r < 0.82) {
		COLOR = color3;
	}
	else {
		COLOR = color4;
	}
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
