
#include "pch.h"
#include <iostream>
#include "Point.h"

Point::Point()
{
	x = 0;
	y = 0;
}

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

void Point::SetPoint(int _x, int _y)
{
	x = _x;
	y = _y;
}

void Point::Show()
{
	cout << "Point x = " << x << " y = " << y << "\n";
}
const double Point::GetX()
{
	return x;
}
const double Point::GetY()
{
	return y;
}

const bool Point::point_equel(double _x, double _y)
{
	return (x==_x) && (y == _y);
}

istream& operator>>(istream& in, Point& point)
{
	in >> point.x >> point.y;
	return in;
}

ostream& operator<<(ostream& out, const Point& point)
{
	out << Manioulator::wp(100,'*') << "Point: x(" << point.x << "), y(" << point.y << ")" << endl;
	return out;
}

ofstream& operator<<(ofstream& out, Point& point)
{
	out.write(reinterpret_cast<char*>(&point), sizeof(Point));
	return out;
}

ifstream& operator>>(ifstream& in, Point& point)
{
	in.read(reinterpret_cast<char*>(&point), sizeof(Point));
	return in;
}
