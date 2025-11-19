shader_type canvas_item;

uniform float dark_power = 0.6;
uniform vec2 cursor_position = vec2(0, 0);

void fragment() {
	vec2 uv = (UV + cursor_position - 0.5) * 2.0;
	float dist = length(uv);
	COLOR.q = (dist - 1.0 + dark_power * 2.0);
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
