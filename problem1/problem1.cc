#include <iostream>

/*sumMultiples: sum all of the multiples of firstMultiple or secondMultiple
that are less than bound*/
int sumMultiples(const int firstMultiple, 
		const int secondMultiple, const int bound) {
	int sum = 0;
	for (int i = 1; i < bound; ++i) {
		if (!(i % firstMultiple && i % secondMultiple)) sum += i;
	}
	std::cout << sum << std::endl;
	return sum;
}







int main(int argc, char *argv[]) {
	const int FIRST_MULTIPLE = 3;
	const int SECOND_MULTIPLE = 5;
	const int BOUND = 1000;
	sumMultiples(FIRST_MULTIPLE, SECOND_MULTIPLE, BOUND);
	return 0;
}
