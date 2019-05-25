// Task6.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <map>
#include <algorithm>
#include <iterator>

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

std::map<std::string, int> fruits;
std::map<std::string, int> vegetables;

void initialize_components() {
	fruits = {
		{"Banana", 30},
		{"Apple", 20},
		{"Orange", 40},
		{"Kiwi", 45},
		{"Pineapple", 80},
		{"Raspberry", 120}
	};

	vegetables = {
		{"Potato", 15},
		{"Peper", 35},
		{"Carrot", 12},
		{"Cucumber", 23}
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

int insert() {
	std::string name;
	int price;
	std::cout << "Name: ";
	std::cin >> name;
	std::cout << "Price per kg: ";
	std::cin >> price;
	fruits.insert(fruits.end(), std::pair<std::string, int>(name, price));
	return 0;
}

void print(std::pair<std::string, int> pair)
{
	std::cout << pair.first << " - " << pair.second << std::endl;
}

void view() {
	system("CLS");
	std::cout << "Fruits:\n" << std::endl;
	/*for (auto iter = fruits.begin(); iter != fruits.end(); ++iter)
		std::cout << iter->first << " - " << iter->second << std::endl;*/
	std::for_each(fruits.begin(), fruits.end(), print);
	std::cout << "\nVegetables:\n" << std::endl;
	for (auto iter = vegetables.begin(); iter != vegetables.end(); ++iter)
		std::cout << iter->first << " - " << iter->second << std::endl;
	std::cout << "-------------------------------------------" << std::endl;
}

int edit() {
	std::string name;
	int price;
	std::cout << "Name of the item being edited: ";
	std::cin >> name;
	std::map<std::string, int>::iterator iter = fruits.find(name);
	if (iter != fruits.end()) {
		std::cout << "New price per kg: ";
		std::cin >> price;
		iter->second = price;
		return 0;
	}
	else
	{
		std::cout << "Item not found" << std::endl;
		return -1;
	}
}

int deleteItem() {
	std::string name;
	std::cout << "Name of the item being edited: ";
	std::cin >> name;
	std::map<std::string, int>::iterator iter = fruits.find(name);
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
	std::string name;
	int count;
	int n = 0;
	std::cout << "Name of the item after which you want to insert: ";
	std::cin >> name;
	std::cout << "The number of deleted items: ";
	std::cin >> count;
	std::map<std::string, int>::iterator iter = fruits.find(name);
	if (iter++ != fruits.end()) {
		while (iter != fruits.end() && n != count) {
			fruits.erase(iter++);
			n++;
		}
		for (iter = vegetables.begin(); iter != vegetables.end(); ++iter)
			fruits.insert(fruits.end(), (*iter));
		return 0;
	}
	else
	{
		std::cout << "Item not found" << std::endl;
		return -1;
	}
}