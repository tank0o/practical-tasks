#include <string>
#include<iostream>
#include"ship.h"
#include <cmath>  
using namespace std;
#pragma once
class SailingShip :
	public Ship
{
public:
	Vector windDirection;
	SailingShip()
	{
		sailCount = 0;
	}
	SailingShip(double speed, double disp, Point loc, int sc) :Ship(speed, disp, loc)
	{
		sailCount = sc;
	}
	~SailingShip()
	{
	}
	string Info()
	{
		string info;
		info += "\nType: Sailing ship\n";
		info += "Max Speed: " + to_string(maxSpeed) + " knots \n";
		info += "Displacement: " + to_string(displacement) + " tons \n";
		info += "Current location: (" + to_string(location.x) + "; " + to_string(location.y) + ") \n";
		info += "Sail count: " + to_string(sailCount) + "\n";
		return info;
	}
	void Move(Vector d)
	{
		double angle = (d.x*windDirection.x + d.y*windDirection.y) / (sqrt(d.x*d.x + d.y*d.y)*sqrt(windDirection.x*windDirection.x + windDirection.y*windDirection.y));
		angle = 1 / angle;

		location.x += d.x*(angle*windSpeed);
		location.y += d.y*(angle*windSpeed);
	}
protected:
	int sailCount;
	double windSpeed = 10;
};