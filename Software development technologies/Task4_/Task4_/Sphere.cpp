#include "pch.h"
#include <iostream>
#include "Point.h"
#include "Circle.h"
#include "Sphere.h"


Sphere::Sphere()
{
	x = 0;
	y = 0;
	r = 0;
	z = 0;
}

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

istream& operator>>(istream& in, Sphere& sphere)
{
	in >> sphere.x >> sphere.y >> sphere.r >> sphere.z;
	return in;
}

ostream& operator<<(ostream& out, const Sphere& sphere)
{
	out << Manioulator::wp(100, '*') << "Sphere: x(" << sphere.x << "), y(" << sphere.y << "), r(" << sphere.r << "), z(" << sphere.z << ")" << endl;
	return out;
}

ofstream& operator<<(ofstream& out, Sphere& sphere)
{
	out.write(reinterpret_cast<char*>(&sphere), sizeof(Sphere));
	return out;
}

ifstream& operator>>(ifstream& in, Sphere& sphere)
{
	in.read(reinterpret_cast<char*>(&sphere), sizeof(Sphere));
	return in;
}
