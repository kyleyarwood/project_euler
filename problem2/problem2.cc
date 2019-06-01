#include <iostream>

/*sumEvenFibo: returns and prints the sum of all even fibonacci numbers
that are at most bound*/
int sumEvenFibo(const int bound) {
	int fibo1 = 0;
	int fibo2 = 1;
	int tmp;
	int sum = 0;
	while (fibo2 <= bound) {
		if (!(fibo2%2)) sum += fibo2;
		tmp = fibo2;
		fibo2 = fibo2 + fibo1;
		fibo1 = tmp;
	}
	std::cout << sum << std::endl;
	return sum;
}

int main(int argc, char *argv[]) {
	const int BOUND = 4000000;
	sumEvenFibo(BOUND);
	return 0;
}


