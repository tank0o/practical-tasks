// Task4_.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include "pch.h"
#include <iostream>
#include "Circle.h"
#include "Point.h"
#include "Section.h"
#include "TObject.h"
#include "Sphere.h"
#include <fstream>
#include <iomanip>
#include <clocale>



enum bass_type
{
	EPoint,
	ESection,
	ECircle,
	ESphere
};

template <class T>
void write_to_file(T& obj, bass_type etype, string path)
{
	ofstream ofs(path, ios::binary | ofstream::app);
	ofs.write(reinterpret_cast<char*>(&etype), sizeof(bass_type));
	ofs << obj;
	ofs.close();
}

void read_from_file(string file_name)
{
	ifstream ifs(file_name, ios::binary);
	if (ifs)
	{
		bass_type a;
		ifs.read(reinterpret_cast<char*>(&a), sizeof(bass_type));
		Point point;
		Section section;
		Circle circle;
		Sphere sphere;
		while (!ifs.eof())
		{
			switch (a)
			{
			case ESection:
			{
				ifs >> section;
				cout << section;
			}
			break;
			case ECircle:
			{
				ifs >> circle;
				cout << circle;
			}
			break;
			case ESphere:
			{
				ifs >> sphere;
				cout << sphere;
			}
			break;
			case EPoint:
			{
				ifs >> point;
				cout << point;
			}
			break;
			default:
				break;
			}
			ifs.read(reinterpret_cast<char*>(&a), sizeof(bass_type));
		}
	}
	ifs.close();
	cout << endl;
}


int findObject(double x, double y, string file_name)
{
	bool f = false;
	int last_s = 0;
	int i = 0;
	ifstream ifs(file_name, ios::binary | ios::in);
	if (ifs)
	{
		bass_type a;
		ifs.read(reinterpret_cast<char*>(&a), sizeof(bass_type));
		int etype_size = sizeof(a);
		int seek = etype_size;
		Point point;
		Section section;
		Circle circle;
		Sphere sphere;
		while (!ifs.eof())
		{
			switch (a)
			{
			case EPoint:
			{
				ifs >> point;
				seek += sizeof(point);
				last_s = sizeof(point);
			}
			break;
			case ESection:
			{
				ifs >> section;
				seek += sizeof(section);
				last_s = sizeof(section);
			}
			break;
			case ECircle:
			{
				ifs >> circle;
				seek += sizeof(circle);
				last_s = sizeof(circle);
			}
			break;
			case ESphere:
			{
				ifs >> sphere;
				seek += sizeof(sphere);
				last_s = sizeof(sphere);
			}
			break;
			default:
				break;
			}
			if (point.point_equel(x, y) || section.point_equel(x, y) || circle.point_equel(x, y) || sphere.point_equel(x, y))
			{
				seek -= last_s;
				seek -= sizeof(bass_type);
				last_s = seek;
				f = true;
				break;
			}
			ifs.seekg(seek);
			ifs.read(reinterpret_cast<char*>(&a), sizeof(bass_type));
			seek += etype_size;
		}
	}
	ifs.close();
	if (f)
	{
		cout << "Номер объекта: " << i - 1 << endl;
		cout << "Байт: " << last_s << endl;
		return last_s;
	}
	else return -1;
}

void del(string file_name, int ss)
{
	if (ss > -1)
	{
		ifstream r(file_name, ios::binary | ios::in);
		ofstream w("swap.txt", ios::binary);
		bass_type a;
		Point point;
		Section section;
		Circle circle;
		Sphere sphere;
		int seek = 0;
		int last_r = 0;
		r.seekg(seek);
		int size = 0;
		int i = 0;
		while (!r.eof())
		{
			if (i == 0) r.read(reinterpret_cast<char*>(&a), sizeof(bass_type));
			last_r = seek;
			seek += sizeof(bass_type);
			switch (a)
			{
			case EPoint:
				r >> point;
				seek += sizeof(Point);
				if (ss != last_r)
				{
					w.write(reinterpret_cast<char*>(&a), sizeof(bass_type));
					w << point;
				}
				break;
			case ESection:
				r >> section;
				seek += sizeof(Section);
				if (ss != last_r)
				{
					w.write(reinterpret_cast<char*>(&a), sizeof(bass_type));
					w << section;
				}
				break;
			case ECircle:
				r >> circle;
				seek += sizeof(Circle);
				if (ss != last_r)
				{
					w.write(reinterpret_cast<char*>(&a), sizeof(bass_type));
					w << circle;
				}
				break;
			case ESphere:
				r >> sphere;
				seek += sizeof(Sphere);
				if (ss != last_r)
				{
					w.write(reinterpret_cast<char*>(&a), sizeof(bass_type));
					w << sphere;
				}
				break;
			}
			r.read(reinterpret_cast<char*>(&a), sizeof(bass_type));
			i++;
		}
		r.close();
		w.close();
		remove(file_name.c_str());

		rename("swap.txt", file_name.c_str());
	}
}

void repl(string file_name, int ss, int x, int y)
{
	int seek = ss;
	if (seek != -1)
	{
		fstream fstr;
		fstr.open(file_name, ios::in | ios::out | ios::binary);
		fstr.seekp(seek);
		bass_type a;
		fstr.read(reinterpret_cast<char*>(&a), sizeof(bass_type));
		seek += sizeof(a);
		switch (a)
		{
		case EPoint:
		{
			Point point;
			fstr.read(reinterpret_cast<char*>(&point), sizeof(Point));
			point.SetPoint(x, y);
			fstr.seekp(seek);
			fstr.write(reinterpret_cast<char*>(&point), sizeof(Point));
		}
		break;
		case ESection:
		{
			Section section;
			fstr.read(reinterpret_cast<char*>(&section), sizeof(Section));
			section.SetPoint(x, y);
			fstr.seekp(seek);
			fstr.write(reinterpret_cast<char*>(&section), sizeof(Section));
		}
		break;
		case ECircle:
		{
			Circle circle;
			fstr.read(reinterpret_cast<char*>(&circle), sizeof(Circle));
			circle.SetPoint(x, y);
			fstr.seekp(seek);
			fstr.write(reinterpret_cast<char*>(&circle), sizeof(Circle));
		}
		break;
		case ESphere:
		{
			Sphere sphere;
			fstr.read(reinterpret_cast<char*>(&sphere), sizeof(Sphere));
			sphere.SetPoint(x, y);
			fstr.seekp(seek);
			fstr.write(reinterpret_cast<char*>(&sphere), sizeof(Sphere));
		}
		break;
		}
		fstr.close();
	}
}

int main()
{
	setlocale(LC_CTYPE, "rus");
	Point point;
	Section section;
	Circle circle;
	Sphere sphere;

	string path = "C:\\Users\\Andrew\\Documents\\PracticaTasks\\Software development technologies\\Task4_\\Debug\\dada2";

	cout  << "Point (x,y)" << endl;
	cin >> point;
	cout  << "Section (x,y,x2,y2)" << endl;
	cin >> section;
	cout  << "Circle (x,y,r)" << endl;
	cin >> circle;
	cout  << "Sphere (x,y,r,z)" << endl;
	cin >> sphere;
	write_to_file(point, EPoint, path);
	write_to_file(section, ESection, path);
	write_to_file(circle, ECircle, path);
	write_to_file(sphere, ESphere, path);
	read_from_file(path);
	repl(path, findObject(1, 1, path), 2, 2);
	read_from_file(path);
	del(path, findObject(2, 2, path));
	read_from_file(path);
	return 0;
}