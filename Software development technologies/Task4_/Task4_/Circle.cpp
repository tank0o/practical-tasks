#include "pch.h"
#include <iostream>
#include "Point.h"
#include "Circle.h"

Circle::Circle(double x, double y, double r):Point(x,y)
{
	this->r = r;
}
Circle::Circle(Point* p, double r) : Point(p)
{
	this->r = r;
}
Circle::Circle(Circle* cir): Point (cir->x,cir->y)
{	
	this->r = cir->r;
}
void Circle::Show()
{
	std::cout << "Circle: center = ( " << x << " , " << y << " ) radius = " << r << "\n";
}