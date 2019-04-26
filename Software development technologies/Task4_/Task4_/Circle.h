#pragma once

#include "pch.h"
#include <iostream>
#include "Point.h"

class Circle :public Point
{
protected:
		double r;
public:
	Circle(double x, double y, double r);
	Circle(Point* p, double r);
	Circle(Circle* cir);
	virtual void Show();
};