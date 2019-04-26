
#include "pch.h"
#include <iostream>
#include "Point.h"

Point::Point(double x, double y)
{
	this->x = x;
	this->y = y;
}
Point::Point(const Point* p)
{
	x = p->x;
	y = p->y;
}

void Point::Show()
{
	std::cout << "Point x = " << x << " y = " << y << "\n";
}
const double Point::GetX()
{
	return x;
}
const double Point::GetY()
{
	return y;
}