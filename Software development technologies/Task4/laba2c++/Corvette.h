#include <string>
#include<iostream>
#include "SailingShip.h"
#include "Steamboat.h"
using namespace std;
#pragma once
class Corvette :
	public Steamboat, SailingShip
{
public:
	Corvette()
	{
		shipType = steamboat_type;
	}
	Corvette(double speed, double disp, Point loc, FuelType ft, int sc, ShipType st) : Steamboat(speed, disp, loc, ft), SailingShip(speed, disp, loc, sc)
	{
		shipType = st;
	}
	~Corvette()
	{

	}
	string Info()
	{
		string info;
		info += "\nType: Corvette\n";
		info += "Max Speed: " + to_string(Ship::maxSpeed) + " knots \n";
		info += "Displacement: " + to_string(Ship::displacement) + " tons \n";
		info += "Current location: (" + to_string(Ship::location.x) + "; " + to_string(Ship::location.y) + ") \n";
		if (shipType == steamboat_type)
		{
			info += "Fuel type: " + to_string(fuelType) + "\n";
		}
		else
		{
			info += "Sail count: " + to_string(shipType) + "\n";
		}
		return info;
	}
	void Move(Vector d)
	{
		if (shipType == steamboat_type)
		{
			Steamboat::location.x += d.x;
			Steamboat::location.y += d.y;
		}
		else
		{
			double angle = (d.x*windDirection.x + d.y*windDirection.y) / (sqrt(d.x*d.x + d.y*d.y)*sqrt(windDirection.x*windDirection.x + windDirection.y*windDirection.y));
			angle = 1 / angle;

			Steamboat::location.x += d.x*(angle*windSpeed);
			Steamboat::location.y += d.y*(angle*windSpeed);
		}

	}
protected:
	ShipType shipType;
};
#pragma endregion Classes
