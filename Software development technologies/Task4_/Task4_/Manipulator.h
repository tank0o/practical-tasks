#pragma once
#include <ios>
#include <string>
#include <iostream>

using namespace std;

typedef  ostream& (*PTF)(ostream&, int, char);
class Manioulator
{
	int w; char fill; PTF f;
public:
	Manioulator(PTF F, int W, char FILL) :f(F), w(W), fill(FILL) {};
	~Manioulator()
	{

	};
	friend ostream& operator<<(ostream&, Manioulator);
	static ostream& fmanip(ostream& s, int w, char fill)
	{
		s.width(w);
		s.flags(ios::fixed);
		s.fill(fill);
		return s;
	}
	static Manioulator wp(int w, char fill)
	{
		return Manioulator(fmanip, w, fill);
	}
};
