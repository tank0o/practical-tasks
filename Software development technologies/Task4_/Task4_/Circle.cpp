#include "pch.h"
#include <iostream>
#include "Point.h"
#include "Circle.h"

Circle::Circle()
{
	x = 0;
	y = 0;
	r = 0;
}

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

istream& operator>>(istream& in, Circle& circle)
{
	in >> circle.x >> circle.y >> circle.r;
	return in;
}

ostream& operator<<(ostream& out, const Circle& circle)
{
	out << Manioulator::wp(100, '*') << "Circle: x(" << circle.x << "), y(" << circle.y << "), r(" << circle.r << ")" << endl;
	return out;
}

ofstream& operator<<(ofstream& out, Circle& circle)
{
	out.write(reinterpret_cast<char*>(&circle), sizeof(Circle));
	return out;
}

ifstream& operator>>(ifstream& in, Circle& circle)
{
	in.read(reinterpret_cast<char*>(&circle), sizeof(Circle));
	return in;
}
