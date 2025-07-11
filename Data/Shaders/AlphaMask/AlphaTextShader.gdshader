shader_type canvas_item;
uniform sampler2D mask_texture;
uniform vec2 mask_offset = vec2(0.0);
uniform vec2 node_rect_size = vec2(100.0, 100.0); // Значение по умолчанию, будет перезаписано из скрипта
uniform vec4 text_color : source_color = vec4(1.0);

varying vec2 v_node_uv;

void vertex() {
    v_node_uv = VERTEX.xy / node_rect_size;
}

void fragment() {
    vec4 base_color = texture(TEXTURE, UV) * text_color;
    float mask_value = texture(mask_texture, v_node_uv + mask_offset).r;
    COLOR = vec4(base_color.rgb, base_color.a * mask_value);
}