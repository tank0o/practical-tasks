#pragma once
#include "TObject.h"
#include "pch.h"
#include <iostream>

class TGroup :public TObject
{
	class Node
	{
	public:
		TObject* data;
		Node* next;
		Node(TObject* obj)
		{
			this->data = obj;
			this->next = NULL;
		}
		Node(TObject* obj, Node* next)
		{
			this->data = obj;
			this->next = next;
		}
		~Node()
		{};


	private:
	};

	Node* first;
	int count = 0;

public:
	void Show()
	{
		std::cout << "Group Start: Length = " << count << "\n";
		for (Node* node = first; node != NULL; node = node->next)
		{
			std::cout << "\t";
			node->data->Show();
		}
		std::cout << "Group End" << "\n";
	}

	TObject* Get(int i)  const
	{
		if (i >= 0 && i < count)
		{
			Node* node;
			for (node = first; i--; node = node->next);
			return node->data;
		}
		throw _exception();
	}

	TGroup()
	{
		first = NULL;
		count = 0;
	}
	TGroup(const TGroup & old)
	{
		first = NULL;
		count = 0;
		for (int i = 0; i < old.Count(); i++)
		{
			this->Add(old.Get(i));
		}
	}
	~TGroup() {};

	void Add(TObject * obj)
	{
		if (first == NULL)
			first = new Node(obj);

		else
		{
			Node* i;
			for (i = first; i->next != NULL; i = i->next);
			i->next = new Node(obj);
		}
		count++;
	}
	int Count() const
	{
		return count;
	}

};

