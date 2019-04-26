#include "pch.h"
#include <iostream>
#include "Point.h"
#include "Circle.h"
#include "Sphere.h"


Sphere::Sphere(double x, double y, double z, double r):Circle(x,y,r)
{
	this->z = z;
}
Sphere::Sphere(Circle* cir, double z): Circle(cir)
{
	this->z = z;
}
void Sphere::Show()
{
	std::cout << "Sphere: center = (" << x << " , " << y << " , " << z << " ) radius = " << r<<"\n";
}