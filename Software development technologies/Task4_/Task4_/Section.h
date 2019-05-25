#pragma once
#include "pch.h"
#include <iostream>
#include "Point.h"

class Section :public Point
{
protected:
	double x2, y2;
public:
	Section();
	Section(double x1, double y1, double x2, double y2);
	Section( Point* p1,  Point* p2);
	virtual void Show();

	friend istream& operator>> (istream& in, Section& section);
	friend ostream& operator<< (ostream& out, const Section& section);
	friend ofstream& operator<<(ofstream& out, Section& section);
	friend ifstream& operator >>(ifstream& in, Section& section);
};
