// Task4_.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include "pch.h"
#include <iostream>
#include "TGroup.h"
#include "Circle.h"
#include "Point.h"
#include "Section.h"
#include "TObject.h"
#include "Sphere.h"

int main()
{
	Point* p = new Point(1, 2);
	Section* s = new Section(1, 2, 3, 4);
	Circle* c = new Circle(1, 2, 3);
	Sphere* sph = new Sphere(c, 10);

	TGroup* g = new TGroup();
	g->Add(p);
	g->Add(s);
	g->Add(c);
	g->Add(sph);
	TGroup* b = new TGroup(*g);
	g->Add(b);

	g->Get(3)->Show();

	g->Show();
}