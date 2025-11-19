shader_type canvas_item;
            
uniform float angle = 0.0;

void fragment() {
    vec2 centered_uv = UV - 0.5;
    float cos_angle = cos(angle);
    float sin_angle = sin(angle);              
    vec2 rotated_uv = vec2(
        centered_uv.x * cos_angle - centered_uv.y * sin_angle,
        centered_uv.x * sin_angle + centered_uv.y * cos_angle
    ) + 0.5;    
    COLOR = texture(TEXTURE, rotated_uv);
}
