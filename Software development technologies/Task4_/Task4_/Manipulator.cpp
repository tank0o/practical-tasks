#include "Manipulator.h"

ostream& operator<<(ostream& out, Manioulator manipulator)
{
	return manipulator.f(out, manipulator.w, manipulator.fill);
}
//ostream& fmanip(ostream& s, int w, char fill)
//{
//	s.width(w);
//	s.flags(ios::fixed);
//	s.fill(fill);
//	return s;
//}
//Manioulator wp(int w, char fill)
//{
//	return Manioulator(fmanip, w, fill);
//}