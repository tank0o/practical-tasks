#pragma once
#include "pch.h"
#include <iostream>
#include "Point.h"

class Section :public Point
{
protected:
	double x2, y2;
public:
	Section(double x1, double y1, double x2, double y2);
	Section( Point* p1,  Point* p2);
	virtual void Show();
};
