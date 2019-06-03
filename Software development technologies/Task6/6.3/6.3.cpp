#include <iostream>
#include <map>
#include <algorithm>
#include <iterator>
#include "Product.h"

void initialize_components();
int menu();
void view();
int insert();
int edit();
int deleteItem();
int combine();

int main()
{
	initialize_components();

	int variant = menu();

	while (variant != 0) {
		switch (variant) {
		case 1:
			view();
			break;
		case 2:
			insert();
			break;
		case 3:
			edit();
			break;
		case 4:
			deleteItem();
			break;
		case 5:
			combine();
			break;
		default:
			break;
		}
		variant = menu();
	}
}

std::map<int, Product> fruits;
std::map<int, Product> vegetables;

void initialize_components() {
	fruits = {
		{1, Product("Banana", 20.1, 12, 26, 05, 2019, "AppleCo")},
		{2, Product("Apple", 25, 10, 5, 05, 2019, "AppleCo")},
		{3, Product("Orange", 40.5, 11, 26, 04, 2019, "Farm&Co")},
		{4, Product("Kiwi", 80.54, 5, 4, 05, 2019, "Farm&Co")},
		{5, Product("Pineapple", 83.12, 25, 15, 05, 2019, "Farm&Co")},
		{6, Product("Raspberry", 120, 3, 3, 05, 2019, "AppleCo")}
	};

	vegetables = {
		{1, Product("Potato", 11, 13, 26, 05, 2019, "AppleCo")},
		{2, Product("Peper", 32, 17, 5, 05, 2019, "AppleCo")},
		{3, Product("Carrot", 12, 14, 26, 04, 2019, "Farm&Co")},
		{4, Product("Cucumber", 27, 7, 4, 05, 2019, "Farm&Co")},
	};
}

int menu() {

	int variant;
	std::cout << "\nChoose variant\n" << std::endl;
	std::cout << "1. View content\n"
		<< "2. Insert new object\n"
		<< "3. Edit object\n"
		<< "4. Delete object\n"
		<< "5. Combine maps\n"
		<< "0. Exit\n"
		<< std::endl;
	std::cout << ">>> ";
	std::cin >> variant;
	return variant;
}

std::istream& operator>>(std::istream& in, Product& product) {
	std::cout << "Name: ";
	in >> product.name;
	std::cout << "\nCost: ";
	in >> product.cost;
	std::cout << "\nCount: ";
	in >> product.count;
	std::cout << "\nDate of manufacture\nDay: ";
	in >> product.dateManufacture[0];
	std::cout << "\nMonth: ";
	in >> product.dateManufacture[1];
	std::cout << "\nYear: ";
	in >> product.dateManufacture[2];
	std::cout << "\nManufacture: ";
	in >> product.nameManufacture;
	std::cout << "\nIs done.";
	return in;
}

std::ostream& operator<< (std::ostream& out, Product& product)
{
	out << product.Get();
	return out;
}

int insert() {
	Product product;
	std::cin >> product;
	fruits.insert(fruits.end(), std::pair<int, Product>((--fruits.end())->first + 1, product));
	return 0;
}

void view() {
	system("CLS");
	std::cout << "Fruits:" << std::endl;
	for (auto iter = fruits.begin(); iter != fruits.end(); ++iter)
		std::cout << "\nID : " << iter->first << iter->second << std::endl;

	std::cout << "\nVegetables:" << std::endl;
	for (auto iter = vegetables.begin(); iter != vegetables.end(); ++iter)
		std::cout << "\nID : " << iter->first << iter->second << std::endl;
	std::cout << "-------------------------------------------" << std::endl;
}

int edit() {
	int index;
	std::cout << "ID of the item being edited: ";
	std::cin >> index;
	std::map<int, Product>::iterator iter = fruits.find(index);
	if (iter != fruits.end()) {
		Product product;
		std::cin >> product;
		iter->second = product;
		return 0;
	}
	else
	{
		std::cout << "Item not found" << std::endl;
		return -1;
	}
}

int deleteItem() {
	int index;
	std::cout << "ID of the item being edited: ";
	std::cin >> index;
	std::map<int, Product>::iterator iter = fruits.find(index);
	if (iter != fruits.end()) {
		fruits.erase(iter);
		return 0;
	}
	else
	{
		std::cout << "Item not found" << std::endl;
		return -1;
	}
}

int combine() {
	int index;
	int count;
	int n = 0;
	std::cout << "ID of the item after which you want to insert: ";
	std::cin >> index;
	std::cout << "The number of deleted items: ";
	std::cin >> count;
	std::map<int, Product>::iterator iter = fruits.find(index);
	if (iter++ != fruits.end()) {
		while (iter != fruits.end() && n != count) {
			fruits.erase(iter++);
			n++;
		}
		for (iter = vegetables.begin(); iter != vegetables.end(); ++iter)
			fruits.insert(fruits.end(), std::pair<int, Product>((--fruits.end())->first + 1, iter->second));
		return 0;
	}
	else
	{
		std::cout << "Item not found" << std::endl;
		return -1;
	}
}


