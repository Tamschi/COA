#version 330

layout(location = 0) in vec3 vPosition;
layout(location = 1) in vec3 vTexCoords;

out vec2 TexCoords;

uniform mat4 mModel;
uniform mat4 mViewProjection;

void main()
{
	TexCoords = vTexCoords;
	gl_Position = mModel * mViewProjection * vec4(vPosition, 1.0);
}