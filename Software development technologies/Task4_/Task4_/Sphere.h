#pragma once
#include "pch.h"
#include <iostream>
#include "Point.h"
#include "Circle.h"

class Sphere :public Circle
{
protected:
	double z;
public:
	Sphere();
	Sphere(double x, double y, double z, double r);
	Sphere(Circle* cir, double z);
	virtual void Show();

	friend istream& operator>> (istream& in, Sphere& sphere);
	friend ostream& operator<< (ostream& out, const Sphere& sphere);
	friend ofstream& operator<<(ofstream& out, Sphere& sphere);
	friend ifstream& operator >>(ifstream& in, Sphere& sphere);
};