#pragma once
#include "pch.h"
#include <iostream>
#include "TObject.h"

class Point : public TObject
{
protected:
	double x, y;
public:
	Point(double x, double y);
	Point(const Point* p);
	void Show();
	const double GetX();
	const double GetY();
};
