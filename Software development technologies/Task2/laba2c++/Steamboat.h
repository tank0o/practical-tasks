#include <string>
#include<iostream>
#include "Ship.h"
using namespace std;
#pragma once
class Steamboat :
	public Ship
{
public:
	Steamboat()
	{
		fuelType = Oil;
	}
	Steamboat(double speed, double disp, Point loc, FuelType ft) :Ship(speed, disp, loc)
	{
		fuelType = ft;
	}
	~Steamboat()
	{

	}
	string Info()
	{
		string info;
		info += "\nType: Steamboat\n";
		info += "Max Speed: " + to_string(maxSpeed) + " knots \n";
		info += "Displacement: " + to_string(displacement) + " tons \n";
		info += "Current location: (" + to_string(location.x) + "; " + to_string(location.y) + ") \n";
		info += "Fuel type: " + to_string(fuelType) + "\n";
		return info;
	}
	void Move(Vector d)
	{
		location.x += d.x;
		location.y += d.y;
	}
protected:
	FuelType fuelType;
};
