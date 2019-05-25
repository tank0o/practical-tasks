#pragma once

#include "pch.h"
#include <iostream>
#include "Point.h"

class Circle :public Point
{
protected:
		double r;
public:
	Circle();
	Circle(double x, double y, double r);
	Circle(Point* p, double r);
	Circle(Circle* cir);
	virtual void Show();

	friend istream& operator>> (istream& in, Circle& circle);
	friend ostream& operator<< (ostream& out, const Circle& circle);
	friend ofstream& operator<<(ofstream& out, Circle& circle);
	friend ifstream& operator >>(ifstream& in, Circle& circle);
};