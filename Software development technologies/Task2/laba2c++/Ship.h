#pragma once
#include <string>
#include<iostream>
using namespace std;
#pragma region accessoryStuff
class Vector
{
public:
	double x, y;
	Vector()
	{
		x = 0;
		y = 0;
	}
	Vector(double x, double y)
	{
		this->x = x;
		this->y = y;
	}
};
class Point
{
public:
	double x, y;
	Point(int x, int y)
	{
		this->x = x;
		this->y = y;
	}
	Point()
	{
	}
	~Point()
	{

	}
};
enum FuelType { Oil, coal, Atomic, Electrical };
enum ShipType { steamboat_type, sailingShip_type };
#pragma endregion accessoryStuff
#pragma once
class Ship
{
public:
	static Ship* begin;
	Ship()
	{
		maxSpeed = 0;
		displacement = 0;
		location = Point(0, 0);
	}
	Ship(double speed, double disp, Point loc)
	{
		maxSpeed = speed;
		displacement = disp;
		location = loc;
	}
	virtual ~Ship()
	{

	}
	virtual string Info() = 0;
	virtual void Move(Vector direction) = 0;
	static void Print()
	{
		Ship* current = begin;
		while (current != NULL)
		{
			cout << current->Info();
			current = current->next;
		};
	}
	void Insert()
	{
		Ship* pointer = begin;
		while (pointer->next != NULL)
		{
			pointer = pointer->next;
		};
		pointer->next = this;
	}
protected:
	double maxSpeed;//knots
	double displacement;//tons
	Point location;//coordinates(x,y)
	Ship* next;
};