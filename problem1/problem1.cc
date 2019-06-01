#include <iostream>

/*sumMultiples: sum all of the multiples of firstMultiple or secondMultiple
that are less than bound*/
int sumMultiples(int firstMultiple, int secondMultiple, int bound) {
	int sum = 0;
	for (int i = 1; i < bound; ++i) {
		if (!(i % firstMultiple && i % secondMultiple)) sum += i;
	}
	std::cout << sum << std::endl;
	return sum;
}







int main(int argc, char *argv[]) {
	int firstMultiple = 3;
	int secondMultiple = 5;
	int bound = 1000;
	sumMultiples(firstMultiple, secondMultiple, bound);
	return 0;
}
