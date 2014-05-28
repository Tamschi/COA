#version 330

in vec4 TexCoords;

out vec4 OutputColor;

uniform vec4 Tint;
uniform sampler2D DiffuseMap;

//uniform bool SpecularEnabled;
//uniform sampler2D SpecularMap;

void main()
{
	OutputColor = texture(DiffuseTexture, TexCoords) * Tint;
}