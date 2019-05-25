#pragma once
#include "pch.h"
#include <iostream>
#include <istream>
#include "TObject.h"
#include "Manipulator.h"
#include <fstream>

using namespace std;

class Point : public TObject
{
protected:
	double x, y;
public:
	Point();
	Point(double x, double y);
	Point(const Point* p);
	void SetPoint(int _x, int _y);
	void Show();
	const double GetX();
	const double GetY();
	const bool point_equel(double _x, double _y);


	friend istream& operator>> (istream& in, Point& point);
	friend ostream& operator<< (ostream& out, const Point& point);
	friend ofstream& operator<<(ofstream& out, Point& point);
	friend ifstream& operator >>(ifstream& in, Point& point);
};
