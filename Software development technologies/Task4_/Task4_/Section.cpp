#include "pch.h"
#include <iostream>
#include "Point.h"
#include "Section.h"

Section::Section(double x1, double y1, double x2, double y2):Point(x1,y1)
{
	this->x2 = x2;
	this->y2 = y2;
}
Section::Section( Point* p1,  Point* p2):Point(p1)
{
	x2 = p2->GetX();
	y2 = p2->GetY();
}

void Section::Show()
{
	std::cout << "Section = (( " << x << " , " << y << " ),( " << x2 << " , " << y2 << " ))" << "\n";
}