shader_type canvas_item;
uniform sampler2D mask_texture;
uniform vec2 mask_offset = vec2(0.0);

void fragment() {
	vec4 color = texture(TEXTURE, UV); // Основной цвет (изображение)
    float mask_value = texture(mask_texture, UV + mask_offset).r; // Берём красный канал (если маска ч/б)
    COLOR = vec4(color.r, color.g, color.b, color.a * (mask_value)); // Умножаем альфа на яркость маски
}
