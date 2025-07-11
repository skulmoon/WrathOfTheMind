shader_type canvas_item;

// Настройки шума
uniform float noise_amount : hint_range(0, 1) = 0.1; // Сила шума
uniform float noise_scale : hint_range(0.1, 10.0) = 2.0; // Масштаб шума
uniform sampler2D noise_texture : hint_default_white; // Текстура шума (можно заменить)

void fragment() {
    // Получаем исходный цвет текстуры
    vec4 original_color = texture(TEXTURE, UV);
    
    // Генерируем шум (используем встроенный шум или текстуру)
    vec2 noise_uv = UV * noise_scale;
    float noise_value = texture(noise_texture, noise_uv).r;
    
    // Добавляем шум к цвету
    vec3 noisy_color = original_color.rgb + (noise_value - 0.5) * noise_amount;
    
    // Ограничиваем значения цвета
    noisy_color = clamp(noisy_color, 0.0, 1.0);
    
    COLOR = vec4(noisy_color, original_color.a);
}