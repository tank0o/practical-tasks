#include "pch.h"
#include <iostream>
#include "Point.h"
#include "Section.h"

Section::Section()
{
	x = 0;
	y = 0;
	x2 = 0;
	y2 = 0;
}

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

istream& operator>>(istream& in, Section& section)
{
	in >> section.x >> section.y >> section.x2 >> section.y2;
	return in;
}

ostream& operator<<(ostream& out, const Section& section)
{
	out << Manioulator::wp(100, '*') << "Section: x(" << section.x << "), y(" << section.y << "), x2(" << section.x2 << "), y2(" << section.y2 << ")" << endl;
	return out;
}

ofstream& operator<<(ofstream& out, Section& section)
{
	out.write(reinterpret_cast<char*>(&section), sizeof(Section));
	return out;
}

ifstream& operator>>(ifstream& in, Section& section)
{
	in.read(reinterpret_cast<char*>(&section), sizeof(Section));
	return in;
}
