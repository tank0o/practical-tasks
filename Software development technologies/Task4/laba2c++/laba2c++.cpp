#include "stdafx.h"
#include <iostream>
#include <string>
#include "Ship.h"
#include "Steamboat.h"
#include "SailingShip.h"
#include "Corvette.h"
using namespace std;

Ship* Ship::begin = new SailingShip(77, 122, Point(50, 24), 18);

int main()
{
	Steamboat* sb = new Steamboat(50, 100, Point(100, 200), coal);
	SailingShip* sh = new SailingShip(42, 60, Point(700, 600), 12);
	Corvette* c = new Corvette(36, 120, Point(400, 500), Oil, 0, steamboat_type);
	sb->Insert();
	sh->Insert();
	c->Ship::Insert();

	Ship::Print();

	sh->windDirection = Vector(3, 2);
	sh->Move(Vector(5, 5));

	int a; cin >> a;
}