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
	Sphere(double x, double y, double z, double r);
	Sphere(Circle* cir, double z);
	virtual void Show();
};